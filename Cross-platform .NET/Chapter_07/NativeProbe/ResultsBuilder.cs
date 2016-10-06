//************************************************************************************************************************************
// Filename:    Resultsbuilder.cs
// Authors:     mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © BLiNK Software Ltd 2004 
// Date:        18/12/2003
// Note:		The abstract base class for results builing implementation...
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
using System.Xml;
using System.Xml.XPath;

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


	//The type of results to show
	[Flags]
	internal enum ResultType
	{
		None = 0,
		Native = 1 ,
		Managed = 2,
		All = 3
	}

	//The abstract base class for result builders...
	internal abstract class ResultsBuilder
	{		
		protected readonly ProbeScope ProbeScope;				//The scope of the probe
		protected readonly ResultType ResultType;				//The types of results to show
		protected readonly XmlDocument Document;				//The document to build the results from...

		//Create an instance
		protected ResultsBuilder(ProbeScope probeScope, ResultType resultType, XmlDocument document)
		{
			this.ProbeScope = probeScope;
			this.ResultType = resultType;	
			this.Document = document;
		}

		//Convert the results to a string
		public abstract override string ToString();
	}
}
