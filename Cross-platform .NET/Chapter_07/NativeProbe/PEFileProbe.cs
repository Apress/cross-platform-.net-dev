//************************************************************************************************************************************
// Filename:    PEFileProbe.cs
// Authors:     mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © BLiNK Software Ltd 2003 
// Date:        24/10/2003
// Note:		Loads a PEFile and analyses the structure to determine the PInvoke usage
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
using System.Xml;

using PEF = Crossplatform.NET.PEFile;

namespace Crossplatform.NET
{

	//The actual probe
	internal class PEFileProbe : Probe
	{		

		ArrayList peFiles;			//The PEFiles

		//Create an instance
		public PEFileProbe(ProbeScope probeScope, ResultType resultType): base(probeScope, resultType)
		{}

		//Analyse the assemblies
		protected override void RunAnalysis(string[] assemblyNames)
		{	
			//LOad the actual files...
			LoadPEFiles(assemblyNames);

			//Do the probe...
			ProbePEFiles();
		}

		//Load the PE files into memory
		private void LoadPEFiles(string[] assemblyNames)
		{
			this.peFiles = new ArrayList(assemblyNames.Length);
			for(int assemblyIndex = 0;  assemblyIndex < assemblyNames.Length ; assemblyIndex++)
			{
				PEF.PEFile peFile;
				try
				{
					peFile = new PEF.PEFile(assemblyNames[assemblyIndex]);
					this.peFiles.Add(peFile);
				}
				catch
				{}
			}
		}

		#region Probe Implementations

		//Analyse the assemblies
		private void ProbePEFiles()
		{	
			foreach(PEF.PEFile peFile in this.peFiles)
			{
				//this.currentAssembly = assembly;
				XmlElement assemblyElement = this.ProbeAnalysis.AddAssemblyElement(peFile.FileName, String.Empty);
				
				//bool assemblyUsesPI = this.ProbeTypes(assembly, assemblyElement);
				this.ProbeAnalysis.AddUsesPiAttribute(assemblyElement, false);
			}
		}

		#endregion

	}
}
