//************************************************************************************************************************************
// Filename:    ProbeAnalysis.cs
// Authors:     jason.king@profox.co.uk
//				mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © Profox Ltd 2004
//				Copyright © BLiNK Software Ltd 2004 
// Date:        24/10/2003
// Note:		The analysis of a probe (essentially a wrapped XML document)
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
using System.Text;
using System.Collections.Specialized;
using System.Xml;
using System.Runtime.InteropServices;


namespace Crossplatform.NET
{		
	//Wraps the XML document used for storing results
	internal class ProbeAnalysis
	{
		//Element names to use for document creation
		public const string ProbeElementName = "NativeProbe";
		public const string AssemblyElementName = "Assembly";
		public const string TypeElementName = "Type";
		public const string MethodElementName = "Method";

		//Attribute names to use for document creation
		public const string NameAttributeName = "name";
		public const string LocationAttributeName = "location";
		public const string UsesPinvokeAttributeName = "pinvoke";
		public const string UsesInternalCallAttributeName = "internalcall";
		public const string DllName = "dll";
		public const string FunctionName = "function";


		private XmlDocument dataStore;		//Store data in an XML document
		private XmlElement rootElement;		//The root element


		//Create the base document
		public ProbeAnalysis()
		{
			this.dataStore = new XmlDocument();
			this.dataStore.LoadXml(String.Format("<{0} timestamp=\"{1}\"/>" , ProbeElementName, DateTime.Now));
			this.rootElement = (XmlElement)this.dataStore.GetElementsByTagName(ProbeElementName)[0];
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
			node.Attributes.Append(this.CreateAttribute(UsesPinvokeAttributeName, usesPinvoke.ToString()));
		}

		//Add an attribute to the given node, stating whether it uses InternalCalls
		public void AddUsesIcAttribute(XmlNode node, bool usesInternalCall)
		{
			node.Attributes.Append(this.CreateAttribute(UsesInternalCallAttributeName, usesInternalCall.ToString()));
		}



		//Add an attribute to the given node, stating whether it uses PI
		public void AddReferencesAttribute(XmlNode node, PInvokeCallDetails details)
		{
			node.Attributes.Append(this.CreateAttribute(DllName, details.DllName));
			node.Attributes.Append(this.CreateAttribute(FunctionName, details.ExternalFunctionName));
		}

		//Create an assembly element to the datastore
		public XmlElement CreateAssemblyElement(string name)
		{
			XmlElement assemblyElement = this.dataStore.CreateElement(AssemblyElementName);
			assemblyElement.Attributes.Append(this.CreateAttribute(NameAttributeName, name));
			return assemblyElement;
		}

		//Add an assembly element to the datastore
		public void AddAssemblyElement(XmlElement assemblyElement)
		{
			this.rootElement.AppendChild(assemblyElement);
		}


		//Add a type element to the datastore
		public XmlElement CreateTypeElement(XmlElement assemblyElement, string name)
		{
			XmlElement typeElement = this.dataStore.CreateElement(TypeElementName);
			typeElement.Attributes.Append(this.CreateAttribute(NameAttributeName, name));
			return typeElement;
		}


		//Add a method element to the datastore
		public XmlElement CreateMethodElement(XmlElement typeElement, string name)
		{
			XmlElement methodElement = this.dataStore.CreateElement(MethodElementName);
			methodElement.Attributes.Append(this.CreateAttribute(NameAttributeName, name));
			return methodElement;
		}		
	}
}

