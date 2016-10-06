//************************************************************************************************************************************
// Filename:    TextResultsbuilder.cs
// Authors:     mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © BLiNK Software Ltd 2004 
// Date:        18/12/2003
// Note:		Builds some Text results for display
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
using System.Text;
using System.Xml;

namespace Crossplatform.NET
{
	internal class TextResultsbuilder : ResultsBuilder
	{		
		//Create an instance
		public TextResultsbuilder(ProbeScope probeScope, ResultType resultType, XmlDocument document) : base(probeScope, resultType, document)
		{}

		//Convert the results to a string
		public override string ToString()
		{
			StringBuilder builder = new StringBuilder();
			XmlNodeList nodeList = base.Document.GetElementsByTagName(ProbeAnalysis.AssemblyElementName);
			foreach(XmlElement element in nodeList)
			{
				builder.AppendFormat(element.GetAttribute(ProbeAnalysis.NameAttributeName));
				builder.Append(GetNativeDependencyStatement(bool.Parse(element.GetAttribute(ProbeAnalysis.UsesPinvokeAttributeName)),
															bool.Parse(element.GetAttribute(ProbeAnalysis.UsesInternalCallAttributeName))));
				builder.Append(GetTypeResults(element));
			}

			return builder.ToString();
		}

		
		//Generate formatted text results...
		private string GetTypeResults(XmlElement parentElement)
		{
			StringBuilder builder = new StringBuilder();
			XmlNodeList nodeList = parentElement.GetElementsByTagName(ProbeAnalysis.TypeElementName);
			string methodResults = String.Empty;
			foreach(XmlElement element in nodeList)
			{
				builder.AppendFormat(" - {0}", element.GetAttribute(ProbeAnalysis.NameAttributeName));
				builder.Append(GetNativeDependencyStatement(bool.Parse(element.GetAttribute(ProbeAnalysis.UsesPinvokeAttributeName)),
															bool.Parse(element.GetAttribute(ProbeAnalysis.UsesInternalCallAttributeName))));
				methodResults = GetMethodResults(element);
				builder.Append(methodResults);
			}

			if(nodeList.Count > 0 && methodResults == String.Empty)
				builder.AppendFormat(Environment.NewLine);

			return builder.ToString();
		}

		//Generate formatted text results...
		private string GetMethodResults(XmlElement parentElement)
		{
			StringBuilder builder = new StringBuilder();
			XmlNodeList nodeList = parentElement.GetElementsByTagName(ProbeAnalysis.MethodElementName);
			foreach(XmlElement element in nodeList)
			{
				builder.AppendFormat("  - {0}()", element.GetAttribute(ProbeAnalysis.NameAttributeName));
				
				//if we have external call details then display them!
				string dllName = element.GetAttribute(ProbeAnalysis.DllName);
				string functionName = element.GetAttribute(ProbeAnalysis.FunctionName);

				if (dllName == String.Empty)
					builder.Append(GetNativeDependencyStatement(bool.Parse(element.GetAttribute(ProbeAnalysis.UsesPinvokeAttributeName)),
																bool.Parse(element.GetAttribute(ProbeAnalysis.UsesInternalCallAttributeName))));
				else
					builder.AppendFormat(" uses PInvoke (DLL: {0} - Function: {1}){2}", dllName, functionName, Environment.NewLine);
			}
			
			if(nodeList.Count > 0)
				builder.AppendFormat(Environment.NewLine);

			return builder.ToString();
		}


		//Retrieve the statement to use to show the PI status...
		private string GetNativeDependencyStatement(bool usesPinvoke, bool usesInternalCall)
		{
			if (usesPinvoke)
			{
				if (usesInternalCall)
					return String.Format(" uses PInvoke and Internal Call.{0}", Environment.NewLine);
				else
					return String.Format(" uses PInvoke.{0}", Environment.NewLine);
			}
			else if (usesInternalCall)
				return String.Format(" uses Internal Call.{0}", Environment.NewLine);
			else
				return String.Format(" does not depend on native call.{0}", Environment.NewLine);
		}
	}
}
