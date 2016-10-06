//************************************************************************************************************************************
// Filename:    XmlResultsbuilder.cs
// Authors:     mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © BLiNK Software Ltd 2004 
// Date:        18/12/2003
// Note:		Builds some XML results for display
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
using System.Xml;

namespace Crossplatform.NET
{
	internal class XmlResultsbuilder : ResultsBuilder
	{		
		//Create an instance
		public XmlResultsbuilder(ProbeScope probeScope, ResultType resultType, XmlDocument document): base(probeScope, resultType, document)
		{}

		//Convert the results to a string
		public override string ToString()
		{
			//Format the results pleasantly...
			StringWriter stringWriter = new StringWriter();
			XmlTextWriter xmlWriter = new XmlTextWriter(stringWriter);
			xmlWriter.Formatting = Formatting.Indented;
			base.Document.WriteTo(xmlWriter);
			xmlWriter.Flush();
					
			return stringWriter.ToString();
		}
	}
}
