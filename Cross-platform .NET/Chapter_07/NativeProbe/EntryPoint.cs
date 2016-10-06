//************************************************************************************************************************************
// Filename:    EntryPoint.cs
// Authors:     jason.king@profox.co.uk
//				mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © Profox Ltd 2003 
//				Copyright © BLiNK Software Ltd 2003 
// Date:        24/10/2003
// Note:		Probes assemblies to determine if and how they use Platform Invoke

// TODO:		(1) Complete Refactoring...
//				(2) Add professional command-line handling
//				(3) Allow user to specify recursive search...
//				(4) Must allow directories to be passed in ...
//				(5) Allow reasonable command-line output...
//				(6) Ensure only true can be received
//				(7) Allow method names to eb retrieved
//				(8) Allow native library names to be retrieved...
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


namespace Crossplatform.NET
{
	//Hosts the tool's main method
	class EntryPoint
	{

		static void Main(string[] args)
		{
			bool generateXmlOutput = false;					//Should we generate XML?
			bool probeReferencedAssemblies = false;			//Should we probe referenced assemblies?	
			ProbeScope probeScope = ProbeScope.Assembly;	//What level of detail should be shown
			ArrayList targetAssemblies = new ArrayList();	//The assemblies to analyse

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

					//Show referenced details
					case "/R":
						probeReferencedAssemblies = true;
						break;
					
					//Display usage intructions
					case "/H":
						DisplayUsageInstructions();
						return;

					default:
						targetAssemblies.Add(arg);
						break;
				}
			}
			
			DisplayAnalysisResults(generateXmlOutput, probeReferencedAssemblies, probeScope, (string[])targetAssemblies.ToArray(typeof(string)));
		}
	

		//Show the program's usage instructions
		static private void DisplayUsageInstructions(){
			Console.WriteLine("PIProbe.exe");
		}


		//Show the analysis
		static private void DisplayAnalysisResults(bool generateXmlOutput, bool probeReferencedAssemblies, 
												   ProbeScope probeScope, string[] assemblyNames)
		{
			try
			{
				AssemblyProbe probe = new AssemblyProbe(probeScope, probeReferencedAssemblies);
				probe.Probe(assemblyNames);
				DisplayAssemblyResults(generateXmlOutput, probe.Results);
			}	
			catch (Exception e)
			{
				Console.Error.WriteLine("The following error occurred: {0}", e.Message);
			}
		}


		//Show the results of a single assembly analysis
		static private void DisplayAssemblyResults(bool generateXmlOutput, string results){
			Console.WriteLine(results);
		}


	}
}
