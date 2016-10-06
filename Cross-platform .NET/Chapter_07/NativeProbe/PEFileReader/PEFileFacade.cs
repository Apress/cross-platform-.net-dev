//************************************************************************************************************************************
// Filename:    PEFileFacade.cs
// Authors:     mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © BLiNK Software Ltd 2004 
// Date:        26/03/2004
// Note:		Provides a high-level abstract wrapper for a PE File
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
using System.Collections;
using System.Text;

namespace PEFileReader
{
	public class PEFileFacade
	{	
		private PEFile peFile;					//Store a reference to the PEFile
		
		//Construct the facade
		public PEFileFacade(string assemblyFileName)
		{
			this.peFile = new PEFile(assemblyFileName);	
		}

		#region Methods to retrieve specific data from the tables

		//Retrieves the details of DLLs and entry points used by P/Invoke
		public ArrayList GetPInvokeDetails()
		{
			ArrayList externalDetails = new ArrayList();

			ImplMap implMap = this.peFile.tables.ImplMap;
			ModuleRef moduleRef = this.peFile.tables.ModuleRef;
			MethodDef moduleDef = this.peFile.tables.MethodDef;

			// get the name from the target type and for each type in TypeDef table check the Name for a match.
			// When the match is found, get the namespace and methodlist start and methodlist finish (by looking at the next row method list start)
			// Look at all the MethodDefs for the type, for each row look in ImplMap.memberForwarded. 
			// If found, return pinvoke details.
			for(int i = 0; i < implMap.NumberOfRows; i++)
			{		
				// ImplMap.MemberForwarded is a coded token with the first bit defining
				// whether it indexes the MethodDef or Field table.  As imported fields are not 
				// supported it always points to the MethodDef table so we can r-shift the bits 
				// to get the correct index into the methoddef table
				uint methodIndex = implMap.MemberForwarded[i] >> 1;
				
				string typeName = GetTypeName(methodIndex);
				string methodName = (methodIndex > 0) ? GetStringFromStringHeap(moduleDef.Name[methodIndex - 1]) : String.Empty;

				// Get the dll name and method name
				string importedMethodName = GetStringFromStringHeap(implMap.ImportName[i]);
				
				// import scope points to the moduleref table
				uint moduleRefEntry = implMap.ImportScope[i];
				string dllName = (moduleRefEntry > 0) ? GetStringFromStringHeap(moduleRef.Name[moduleRefEntry - 1]) : String.Empty ;

				externalDetails.Add(new Crossplatform.NET.PInvokeCallDetails(typeName, methodName, dllName, importedMethodName)); 
			}

			return externalDetails;
		}

		//Retrieves details of the internal calls
		public ArrayList GetInternalCallDetails()
		{
			const uint InternalCallImplFlag = 0x1000;			//The value of the InternalCall flag as defined in CLI Partition II

			MethodDef moduleDef = this.peFile.tables.MethodDef;

			ArrayList internalCallDetails = new ArrayList();

			//Iterate through entries in the MethodDef table and if it has the interncall flag set then add it to the results array
			for(uint methodDefIndex = 0; methodDefIndex < moduleDef.NumberOfRows; methodDefIndex++)
			{
				if ((moduleDef.ImplFlags[methodDefIndex] & InternalCallImplFlag) == InternalCallImplFlag)
				{
					//Retrieve the method and type name for the Method Definition...
					string methodName = GetStringFromStringHeap(moduleDef.Name[methodDefIndex]);
					string typeName = GetTypeName(methodDefIndex);

					internalCallDetails.Add(new Crossplatform.NET.NativeCallDetails(typeName, methodName));
				}
			}

			return internalCallDetails;
		}


		//Determines the name of the type that a method definition is associated with.
		private string GetTypeName(uint methodDefIndex)
		{
			string typeName = String.Empty;

			TypeDef typeDef = this.peFile.tables.TypeDef;

			//Since each TypeDef only stores an initial index into the MethodDef table we work backwards
			//to see if the MethodDefIndex is greater than the initial index
			for(uint typeDefIndex = typeDef.NumberOfRows - 1; typeDefIndex >= 0; typeDefIndex--)
			{
				if (methodDefIndex >= typeDef.MethodList[typeDefIndex])
				{ 
					typeName = GetStringFromStringHeap(typeDef.Name[typeDefIndex]);
					break;
				}
			}

			return typeName;
		}

		#endregion


		//Retrieves a string from the StringHeap
		public string GetStringFromStringHeap(uint stringIndex)
		{
			MetaDataStream stringHeapStream = this.peFile.stringHeap;

			StringBuilder retrievedStringBuilder = new StringBuilder();
			stringHeapStream.Reader.BaseStream.Position = stringHeapStream.StartPositionInFile + stringIndex;

			byte nextChar = stringHeapStream.Reader.ReadByte();
			while (nextChar != 0)
			{
				retrievedStringBuilder.Append((char)nextChar);
				nextChar = stringHeapStream.Reader.ReadByte();
			}
				
			return retrievedStringBuilder.ToString();
		}
	}
}
