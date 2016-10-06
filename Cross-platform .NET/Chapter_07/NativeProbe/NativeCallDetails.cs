//************************************************************************************************************************************
// Filename:    NativeCallDetails.cs
// Authors:     mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © BLiNK Software Ltd 2004 
// Date:        26/03/2004
// Note:		A structure for storing the details of a native call
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

namespace Crossplatform.NET
{
	//Stores the details of an imported function...
	internal class NativeCallDetails
	{		
		public readonly string TypeName;				//The Type name
		public readonly string MethodName;				//The method name

		//Create an instance
		public NativeCallDetails(string typeName, string methodName)
		{
			this.TypeName = typeName;
			this.MethodName = methodName;
		}

		//Retrieve a unique name for the given typen and method
		public string Key
		{
			get { return String.Format("{0}.{1}", this.TypeName, this.MethodName); }
		}

		//Retrieve a uniqu name for the given typen and method
		public static string GetKey(string typeName, string methodName)
		{
			return String.Format("{0}.{1}", typeName, methodName);
		}
	}
}
