//************************************************************************************************************************************
// Filename:    Reflector.cs
// Authors:     jason.king@profox.co.uk
//				mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © Profox Ltd 2003 
//				Copyright © BLiNK Software Ltd 2003 
// Date:        24/10/2003
// Note:		Reflects through assemblies to determine if and how they use Platform Invoke
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
using System.IO;
using System.Reflection;
using System.Collections.Specialized;
using System.Xml;
using System.Runtime.InteropServices;


namespace Crossplatform.NET
{
	//The scope of the probe
	[Flags]
	internal enum ProbeScope
	{
		Assembly = 0 ,
		Type = 1,
		Method = 2,
		All = 3
	}

	//The actual probe
	internal class AssemblyProbe
	{		
		private bool probeReferencedAssemblies;		//Should referenced assemblies be probed
		private HybridDictionary loadedAssemblies;	//loaded assembly storage
		private ProbeResults resultsDoc;			//The probe results
		private Assembly currentAssembly;			//The assembly currently under analysis
		private ProbeScope probeScope;				//The scope of the probe

		#region Constructor and Properties

		//Create an instance
		public AssemblyProbe(ProbeScope probeScope, bool probeReferencedAssemblies)
		{
			this.probeScope = probeScope;
			this.probeReferencedAssemblies = probeReferencedAssemblies;
		}


		//The results of the last probe...
		public string Results
		{
			get 
			{ 
				if (resultsDoc == null) 
					return String.Empty; 
				else
				{
					//Format the results pleasantly
					StringWriter stringWriter = new StringWriter();
					XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);
					xmlWriter.Formatting = Formatting.Indented;
					resultsDoc.Document.WriteTo(xmlWriter);
					xmlWriter.Flush();
					
					return stringWriter.ToString();
				}
			}
		}

		#endregion


		#region Probe Methods

		//Analyse the assemblies
		public bool Probe(string[] assemblyNames)
		{	
			bool usesPI = false;

			this.LoadAssemblies(assemblyNames);
			this.resultsDoc = new ProbeResults();

			XmlElement assembliesElement = this.resultsDoc.AddAssembliesElement();
			usesPI = ProbeAssemblies(assembliesElement);
			this.resultsDoc.AddUsesPiAttribute(assembliesElement, usesPI);

			return usesPI;
		}


		//Analyse the assemblies
		public bool ProbeAssemblies(XmlElement assembliesElement)
		{	
			bool usesPI = false;

			foreach(Assembly assembly in loadedAssemblies.Values)
			{
				this.currentAssembly = assembly;
				XmlElement assemblyElement = this.resultsDoc.AddAssemblyElement(assembliesElement, assembly.FullName, assembly.Location);
				bool assemblyUsesPI = this.ProbeTypes(assembly, assemblyElement);

				if(this.probeReferencedAssemblies)
				{
					//LoadReferencedAssemblies(assembly);
					//usesPinvoke = usesPInvoke | ProbeReferencedAssemblies();
				}

				this.resultsDoc.AddUsesPiAttribute(assemblyElement, assemblyUsesPI);
				usesPI = (usesPI | assemblyUsesPI);
			}

			return usesPI;
		}


		//Probes the types in an assembly, and optionally the referenced assemblies
		private bool ProbeTypes(Assembly assembly, XmlElement assemblyElement)
		{
			bool usesPI = false;

			Type[] types = assembly.GetTypes();
			foreach(Type type in types)
			{
				XmlElement typeElement = this.resultsDoc.CreateTypeElement(assemblyElement, type.Name);
				bool typeUsesPI =  ProbeMethods(type, typeElement);
				usesPI = (usesPI | typeUsesPI);

				//Add type information if required...
				if ((this.probeScope & ProbeScope.Type) == ProbeScope.Type)
				{
					assemblyElement.AppendChild(typeElement);
					this.resultsDoc.AddUsesPiAttribute(typeElement, typeUsesPI);
				}
				else if (usesPI)
				{
					//Or exit if we can...
					return true;
				}
			}

			return usesPI;
		}


		//Probes the methods in a type
		private bool ProbeMethods(Type type, XmlElement typeElement)
		{
			bool usesPI = false;

			MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);
			foreach(MethodInfo method in methods)
			{
				XmlElement methodElement = this.resultsDoc.CreateMethodElement(typeElement, method.Name);
				bool methodUsesPI = ProbeAttributes(method, methodElement);
				usesPI = (usesPI | methodUsesPI);

				//Add member information if required...
				if ((this.probeScope & ProbeScope.Method) == ProbeScope.Method)
				{
					typeElement.AppendChild(methodElement); 
					this.resultsDoc.AddUsesPiAttribute(methodElement, methodUsesPI);
				}
				else if (usesPI)
				{
					//Or exit if we can...
					return true;
				}
				
			}

			return usesPI;
		}

		//Probes the attributes in a method
		private bool ProbeAttributes(MethodInfo method, XmlElement methodElement)
		{
			return ((method.Attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PinvokeImpl);
		}

		#endregion

		#region Helper Methods


		//Loads the assemblies passed in as arguments...
		private void LoadAssemblies(string[] assemblyNames)
		{
			this.loadedAssemblies = new HybridDictionary();

			foreach(string assemblyName in assemblyNames)
			{
				Assembly assembly = Assembly.LoadFrom(assemblyName);
				if (!this.IsLoaded(assembly))
					this.loadedAssemblies.Add(assemblyName, assembly);
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

		#region Private XML Document Wrapper
		
		//Wraps the XML document used for storing results
		private class ProbeResults
		{
			private XmlDocument dataStore;		//Store data in an XML document


			//Create the base document
			public ProbeResults()
			{
				this.dataStore = new XmlDocument();
				this.dataStore.LoadXml("<PIProbe timestamp=\"" + DateTime.Now + "\"/>");
			}

			
			//Provide access to the data
			public XmlDocument Document
			{
				get { return dataStore; }
			}


			//Create an attribute
			private XmlAttribute CreateAttribute(string name, string value)
			{
				XmlAttribute attribute = this.dataStore.CreateAttribute(name);
				attribute.Value = value;			
				return attribute;
			}


			//Add an attribute to the given node, stating whether it uses PI
			public void AddUsesPiAttribute(XmlNode node, bool usesPinvoke)
			{
				node.Attributes.Append(this.CreateAttribute("usesPlatformInvoke", usesPinvoke.ToString()));
			}


			//Add an assembly element to the datastore
			public XmlElement AddAssembliesElement()
			{
				XmlElement assemblies = this.dataStore.CreateElement("Assemblies");
				this.dataStore.DocumentElement.AppendChild(assemblies);
				return assemblies;
			}


			//Add an assembly element to the datastore
			public XmlElement AddAssemblyElement(XmlElement assembliesElement, string name, string location)
			{
				XmlElement assemblyElement = this.dataStore.CreateElement("Assembly");
				assembliesElement.AppendChild(assemblyElement);
				assemblyElement.Attributes.Append(this.CreateAttribute("name", name));
				assemblyElement.Attributes.Append(this.CreateAttribute("location", location));
				return assemblyElement;
			}

			//Add a type element to the datastore
			public XmlElement CreateTypeElement(XmlElement assemblyElement, string name)
			{
				XmlElement typeElement = this.dataStore.CreateElement("Type");
				typeElement.Attributes.Append(this.CreateAttribute("name", name));
				return typeElement;
			}


			//Add a method element to the datastore
			public XmlElement CreateMethodElement(XmlElement typeElement, string name)
			{
				XmlElement methodElement = this.dataStore.CreateElement("Method");
				methodElement.Attributes.Append(this.CreateAttribute("name", name));
				return methodElement;
			}		
		}

		#endregion

	}
}
