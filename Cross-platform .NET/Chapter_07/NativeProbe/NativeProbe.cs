//************************************************************************************************************************************
// Filename:    NativeProbe.cs
// Authors:     jason.king@profox.co.uk
//				mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © Profox Ltd 2004
//				Copyright © BLiNK Software Ltd 2004 
// Date:        24/10/2003
// Note:		Probes assemblies to determine if and how they use Platform Invoke

// TODO:		(1) Refactor
//				(2) Investigate PEAPI and use if appropriate.
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
using System.IO;
using System.Reflection;

namespace Crossplatform.NET
{
	//Hosts the tool's main method
	class NativeProbe
	{
		//Store an assembly reference to help creating the usage instructions.
		private static Assembly currentAssembly = Assembly.GetEntryAssembly();

		static void Main(string[] args)
		{
			bool generateXmlOutput = false;					//Should we generate XML?
			bool showPInvokeDetails = false;				//Should we display the PInvoke details?	
			bool showInternalCallDetails = false;			//Should we display the InternalCall details?	
			ProbeScope probeScope = ProbeScope.Assembly;	//What level of detail should be shown
			ResultType resultType = ResultType.None;		//Which types of results should be shown
			ArrayList targetAssemblies = new ArrayList();	//The assemblies to analyse

			//If we haven't got any arguments then tell the user how to use the program...
			if(args.Length == 0)
			{
				DisplayUsageInstructions();
				return;
			}

			foreach(string arg in args)
			{
				switch(arg.ToUpper())
				{
					//Generate XML output
					case "/X":
						generateXmlOutput = true;
						break;

					//Show type details
					case "/T":
						probeScope = ProbeScope.Type;
						break;

					//Show member details
					case "/M":
						probeScope = ProbeScope.All;
						break;

					//Only show managed results
					case "/G":
						resultType = resultType | ResultType.Managed;
						break;

					//Only show native results
					case "/N":
						resultType = resultType | ResultType.Native;
						break;

					//Show PInvoke details
					case "/P":
						showPInvokeDetails = true;
						break;

					//Display usage intructions
					case "/H":
					case "/?":
						DisplayUsageInstructions();
						return;

					//Make sure we can handle directories...
					default:
						AddAssemblyPath(targetAssemblies, arg);
						break;
				}
			}

			//If no result type has been selected then show all results
			if (resultType == ResultType.None)
				resultType = ResultType.All;
			
			DisplayAnalysisResults(generateXmlOutput, showPInvokeDetails, showInternalCallDetails, 
								   probeScope, resultType, (string[])targetAssemblies.ToArray(typeof(string)));
		}
	

		//Show the program's usage instructions
		static private void DisplayUsageInstructions()
		{
			string exeName = currentAssembly.GetName().Name;

			Console.WriteLine(@"{0} (v {1})", exeName, currentAssembly.GetName().Version.ToString());
			Console.WriteLine(@"Probes assemblies to determine native code dependencies");
			Console.WriteLine();
			Console.WriteLine(String.Format("Usage: {0} [/X] [/T] [/M] [/P] [/G] [/N] [/H] [/?] [filenames]", exeName));
			Console.WriteLine(@"/X        Produces XML output");
			Console.WriteLine(@"/T        Probes type details");
			Console.WriteLine(@"/M        Probes method details");
			Console.WriteLine(@"/P        Show details of P/Invoke calls");
			Console.WriteLine(@"/G        Only show purely managed results");
			Console.WriteLine(@"/N        Only show native dependent results");
			Console.WriteLine(@"/H | /?   Show these usage instructions");
			Console.WriteLine(@"filenames A list of filenames or directories to probe");
		}

		//Show the analysis
		static private void DisplayAnalysisResults(bool generateXmlOutput, bool showPInvokeDetails, bool showInternalCallDetails,
												   ProbeScope probeScope, ResultType resultType, string[] assemblyNames)
		{
			Probe probe = new Probe(probeScope, resultType, showPInvokeDetails);
			probe.XmlResults = generateXmlOutput;
			Console.WriteLine(probe.Analyse(assemblyNames));
		}


		//Add an assembly path to the list of assemblies to probe
		static private void AddAssemblyPath(ArrayList assemblies, string path)
		{
			//If path is a real file and its not in the list then add it
			if (!assemblies.Contains(path) && File.Exists(path))
				assemblies.Add(path);

			//If its a directory then add any potential assemblies it contains
			else if (System.IO.Directory.Exists(path))
			{
				string[]directoryContents = System.IO.Directory.GetFiles(path);
				foreach(string filePath in directoryContents)
				{
					string fileExtension = new FileInfo(filePath).Extension;
					if(IsAssemblyExtension(fileExtension))
						assemblies.Add(filePath);
				}
			}
		}


		//Determine if a file extension is a valid extension for an assembly
		static private bool IsAssemblyExtension(string extension)
		{
			string normalisedExtension = extension.ToUpper();
			return (normalisedExtension == ".EXE" || normalisedExtension == ".DLL");
		}
	}
}
