//************************************************************************************************************************************
// Filename:    MethodImpl.cs
// Authors:     jason.king@profox.co.uk
//			    mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © Profox Ltd 2004
// Date:        24/10/2003
// Note:		Stores MethodImpl data
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

namespace PEFileReader
{
	public class MethodImpl:MetaDataTable
	{
		public MethodImpl(Tables.TableName tableName, Tables tables):base(tableName,tables){}

		public uint[] Class;			//index into TypeDef table
		public uint[] MethodBody;		//index into MethodDef or MemberRef table; more precisely, a MethodDefOrRef coded index
		public uint[] MethodDeclaration;//index into MethodDef or MemberRef table; more precisely, a MethodDefOrRef coded index

		protected override void ReadRows()
		{
			this.Class = new UInt32[this.numberOfRows];
			this.MethodBody = new UInt32[this.numberOfRows];
			this.MethodDeclaration = new UInt32[this.numberOfRows];

			//they are both methoddeforref types, so only need one variable
			bool longMethodIndex = this.parent.use4ByteIndex(Tables.CodedIndexType.MethodDefOrRef);

			for(int i = 0; i < this.NumberOfRows; i++)
			{
				this.Class[i] = this.parent.use4ByteIndex(Tables.TableName.TypeDef) ? this.reader.ReadUInt32() : this.reader.ReadUInt16();
				this.MethodBody[i] = longMethodIndex ? this.reader.ReadUInt32() :this.reader.ReadUInt16();
				this.MethodDeclaration[i] = longMethodIndex ? this.reader.ReadUInt32() :this.reader.ReadUInt16();
			}
		}
	}


}
