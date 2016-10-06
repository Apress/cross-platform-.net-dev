//************************************************************************************************************************************
// Filename:    Probe.cs
// Authors:     mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © BLiNK Software Ltd 2004 
// Date:        24/10/2003
// Note:		Probes a given list of assemblies and generates an analysis of the results...
// 
// TODO:		(1)Improve PEFile performance.  If external information is required it should be retrieved only
//				if the assembly has PInvoke information.
//				(2)Make the output asynchronous!
//************************************************************************************************************************************

#region MIT X11 License Information
/* This file is part of the PInvoke Probe tool.

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation 
files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, 
modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the 
Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS 
BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF 
OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
#endregion
//*****************************************************************
// Assembly dependencies
//*****************************************************************
using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Xml;

using PEFileReader;

namespace Crossplatform.NET
{

	//The actual probe
	internal class Probe
	{		
		private Hashtable pinvokeDetails;				//PInvoke details
		private Hashtable internalCallDetails;			//Internal Call details

		private HybridDictionary loadedAssemblies;		//loaded assembly storage
		private Assembly currentAssembly;				//The assembly currently under analysis
		protected ProbeScope ProbeScope;				//The scope of the probe
		protected ResultType ResultType;				//The types of results to show
		protected ProbeAnalysis ProbeAnalysis;			//The most recent analysis
		private bool xmlResults;						//Should XML results be generated
		
		private bool showPInvokeDetails;				//Should we retrieve PInvoke call details...

		//Create an instance
		public Probe(ProbeScope probeScope, ResultType resultType, bool showPInvokeDetails)
		{
			this.ProbeScope = probeScope;
			this.xmlResults = false;
			this.showPInvokeDetails = showPInvokeDetails;
			this.ResultType = resultType;

			this.internalCallDetails = new Hashtable();
			this.pinvokeDetails = new Hashtable();
		}


		#region Properties

		//Should XML results be generated
		public bool XmlResults
		{
			get { return this.xmlResults; }
			set { this.xmlResults = value; }
		}

		//Should we probe type details?
		private bool IsTypeProbe
		{
			get { return ((this.ProbeScope & ProbeScope.Type) == ProbeScope.Type); }
		}

		//Should we probe method details?
		private bool IsMethodProbe
		{
			get { return ((this.ProbeScope & ProbeScope.Method) == ProbeScope.Method); }
		}


		//Should we only return details for native calling types?
		private bool IsNativeProbe
		{
			get { return ((this.ResultType & ResultType.Native) == ResultType.Native); }
		}

		//Should we return details for all types?
		private bool IsFullProbe
		{
			get { return ((this.ResultType & ResultType.All) == ResultType.All); }
		}

		#endregion

		//Analyse the assemblies using this rather short "template" method
		public string Analyse(string[] assemblyNames)
		{	
			//Create a analysis
			this.ProbeAnalysis = new ProbeAnalysis();

			//Retrieve the external details from underlying PEFiles if required, as reflection
			//doesn't allow us to retrieve external call information
			//if(this.showPInvokeDetails || this.IsNativeProbe)
				ReadAssemblyBinaryFile(assemblyNames);

			//Load the assemblies...
			LoadAssemblies(assemblyNames);

			//Do the probe...
			ProbeAssemblies();

			//...and return some reasonable results...
			return GetResults();
		}
		

		//The results of the last probe...
		private string GetResults()
		{
			//No document implies no results...
			if (this.ProbeAnalysis == null) 
				return String.Empty; 

			ResultsBuilder builder;
			if (this.XmlResults)
				builder = new XmlResultsbuilder(this.ProbeScope, this.ResultType, this.ProbeAnalysis.Document);
			else
				builder = new TextResultsbuilder(this.ProbeScope, this.ResultType, this.ProbeAnalysis.Document);

			return builder.ToString();
		}
		

		//Read the assembly's binary file and retireve interesting details...
		private void ReadAssemblyBinaryFile(string[] assemblyNames)
		{
			//Retrieve the required native details from all of the PEFIles
			for(int assemblyIndex = 0;  assemblyIndex < assemblyNames.Length ; assemblyIndex++)
			{
				string assemblyName = assemblyNames[assemblyIndex];
				try
				{
					PEFileFacade peFileFacade = new PEFileFacade(assemblyName);

					//Retrieve PInvoke details if required
					if (this.showPInvokeDetails)
					{
						ArrayList pinvokeDetails = peFileFacade.GetPInvokeDetails();

						foreach(PInvokeCallDetails importDetails in pinvokeDetails)
						{
							if (!this.pinvokeDetails.Contains(importDetails.Key))
								this.pinvokeDetails.Add(importDetails.Key, importDetails);
						}
					}

					//Retrieve Internal call details...
					ArrayList internalCallDetails = peFileFacade.GetInternalCallDetails();
					foreach(NativeCallDetails importDetails in internalCallDetails)
					{
						if (!this.internalCallDetails.Contains(importDetails.Key))
							this.internalCallDetails.Add(importDetails.Key, importDetails);
					}
				}
				catch
				{
					Console.Error.WriteLine("Could not open file for PE binary probe: {0}", assemblyName);
				}
			}

		}


		#region Probe Methods	

		//Analyse the assemblies
		private void ProbeAssemblies()
		{	
			foreach(Assembly assembly in loadedAssemblies.Values)
			{
				string assemblyFileName = new FileInfo(assembly.Location).Name;
				try
				{
					this.currentAssembly = assembly;
					XmlElement assemblyElement = this.ProbeAnalysis.CreateAssemblyElement(assemblyFileName);

					bool assemblyUsesPI, assemblyUsesIC;
					ProbeTypes(assembly, assemblyElement, out assemblyUsesPI, out assemblyUsesIC);

					//Add the details if we require them for the results
					if (IsResultRequired(assemblyUsesPI | assemblyUsesIC)) 
					{
						this.ProbeAnalysis.AddUsesPiAttribute(assemblyElement, assemblyUsesPI);
						this.ProbeAnalysis.AddUsesIcAttribute(assemblyElement, assemblyUsesIC);
						this.ProbeAnalysis.AddAssemblyElement(assemblyElement);
					}
				}
				catch
				{
					Console.Error.WriteLine("Error reflecting through assembly: {0}", assemblyFileName);
				}
			}
		}


		//Probes the types in an assembly, and optionally the referenced assemblies
		private void ProbeTypes(Assembly assembly, XmlElement assemblyElement, out bool assemblyUsesPI, out bool assemblyUsesIC)
		{
			assemblyUsesPI = false;
			assemblyUsesIC = false;

			Type[] types = assembly.GetTypes();
			foreach(Type type in types)
			{
				try
				{
					XmlElement typeElement = this.ProbeAnalysis.CreateTypeElement(assemblyElement, type.Name);
					

					bool typeUsesPI, typeUsesIC;
					ProbeMethods(type, typeElement, out typeUsesPI, out typeUsesIC);

					assemblyUsesPI = (assemblyUsesPI | typeUsesPI);
					assemblyUsesIC = (assemblyUsesIC | typeUsesIC);

					//Add the details if we require them for the results
					if (IsResultRequired(typeUsesPI | typeUsesIC) && this.IsTypeProbe) 
					{
						//Include PInvoke details
						this.ProbeAnalysis.AddUsesPiAttribute(typeElement, typeUsesPI);
						assemblyElement.AppendChild(typeElement);

						//Include internal call details
						this.ProbeAnalysis.AddUsesIcAttribute(typeElement, typeUsesIC);
						assemblyElement.AppendChild(typeElement); 
					}
				}
				catch
				{
					Console.Error.WriteLine("Error reflecting through type: {0}", type.Name);
				}
			}
		}


		//Probes the methods in a type
		private void ProbeMethods(Type type, XmlElement typeElement, out bool methodUsesPI, out bool methodUsesIC)
		{
			methodUsesPI = false;
			methodUsesIC = false;

			MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
			
			ArrayList retrievedPInvokeCallDetails = new ArrayList(methods.Length);
			foreach(MethodInfo method in methods)
			{
				try
				{
					XmlElement methodElement = this.ProbeAnalysis.CreateMethodElement(typeElement, method.Name);
					
					bool usesPI, usesIC;
					AnalyseMethodNativeDependencies(method, out usesPI, out usesIC);

					methodUsesPI = (methodUsesPI | usesPI);
					methodUsesIC = (methodUsesIC | usesIC);


					//Add details if we require them for the results
					if (IsResultRequired(usesPI | usesIC) && IsMethodProbe) 
					{
						//Include Pinvoke details
						this.ProbeAnalysis.AddUsesPiAttribute(methodElement, usesPI);
						typeElement.AppendChild(methodElement); 

						//Include internal call details
						this.ProbeAnalysis.AddUsesIcAttribute(methodElement, usesIC);
						typeElement.AppendChild(methodElement); 

						//Add details from the PE probe if required...	
						if(usesPI && this.showPInvokeDetails)
						{
							PInvokeCallDetails methodDetails = (PInvokeCallDetails)this.pinvokeDetails[PInvokeCallDetails.GetKey(type.Name, method.Name)];
							retrievedPInvokeCallDetails.Add(methodDetails);
							this.ProbeAnalysis.AddReferencesAttribute(methodElement, methodDetails);
						}
					}
				}
				catch
				{
					Console.Error.WriteLine("Error reflecting through method: {0}", method.Name);
				}
			}
		}


		//Is the PInvoke or InternalCall attribute defined for the method
		private void AnalyseMethodNativeDependencies(MethodInfo method, out bool usesPI, out bool usesIC)
		{
			//Determine if method uses PInvoke by checking mathod attributes
			usesPI = ((method.Attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PinvokeImpl);

			//Determine if method uses PInvoke by checking mathod attributes
			usesIC = internalCallDetails.ContainsKey(NativeCallDetails.GetKey(method.ReflectedType.Name, method.Name));
		}

		#endregion

		#region Helper Methods

		//Determins if the given results should be included in the results
		private bool IsResultRequired(bool isNative)
		{
			return (this.IsFullProbe || (this.IsNativeProbe == isNative));
		}
		

		//Loads the assemblies passed in as arguments...
		private void LoadAssemblies(string[] assemblyNames)
		{
			this.loadedAssemblies = new HybridDictionary();

			foreach(string assemblyName in assemblyNames)
			{
				try
				{
					Assembly assembly = Assembly.LoadFrom(assemblyName);
					if (!this.IsLoaded(assembly))
						this.loadedAssemblies.Add(assemblyName, assembly);
				}
				catch
				{
					//Only show the error message if we won't have already complained
					//about the file not being PE readable
					if(!this.showPInvokeDetails)
						Console.Error.WriteLine("Could not reflect through file: {0}", assemblyName);
				}
			}
		}

		//Checks whether an assembly is loaded
		private bool IsLoaded(Assembly assembly)
		{
			return this.loadedAssemblies.Contains(assembly.FullName);
		}


		//Add an assembly to the loaded collection
		private void AddLoadedAssembly(Assembly assembly)
		{
			this.loadedAssemblies.Add(assembly.FullName, assembly);
		}

		// Loads any referenced assemblies and recursively calls itself for each
		private void LoadReferencedAssemblies(Assembly assembly)
		{
			foreach(AssemblyName referencedAssemblyName in assembly.GetReferencedAssemblies())
			{
				Assembly referencedAssembly = Assembly.Load(referencedAssemblyName);
				if (this.IsLoadable(referencedAssemblyName.Name) && !this.IsLoaded(referencedAssembly))
				{
					this.AddLoadedAssembly(referencedAssembly);
					this.LoadReferencedAssemblies(referencedAssembly);
				}
			}
		}



		// Loads any referenced assemblies and recursively calls itself for each
		private bool ProbeReferencedAssemblies(Assembly assembly)
		{
			foreach(AssemblyName referencedAssemblyName in assembly.GetReferencedAssemblies())
			{
				Assembly referencedAssembly = Assembly.Load(referencedAssemblyName);
				if (this.IsLoadable(referencedAssemblyName.Name) && !this.IsLoaded(referencedAssembly))
				{
					this.AddLoadedAssembly(referencedAssembly);
					this.LoadReferencedAssemblies(referencedAssembly);
				}
			}

			return false;
		}



		// always ignore mscorlib as full of pinvokes???
		private bool IsLoadable(string assemblyName)
		{
			return (Path.GetFileNameWithoutExtension(assemblyName).ToUpper() != "MSCORLIB");
		}

		#endregion
	}
}
