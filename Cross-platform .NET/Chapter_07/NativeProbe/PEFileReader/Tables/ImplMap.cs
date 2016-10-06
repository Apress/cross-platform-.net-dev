//************************************************************************************************************************************
// Filename:    ImplMap.cs
// Authors:     jason.king@profox.co.uk
// Copyright:	Copyright © Profox Ltd 2004
// Date:        24/10/2003
// Note:		Stores ImplMap data
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
	public class ImplMap : MetaDataTable
	{
		public ImplMap(Tables.TableName tableName, Tables tables):base(tableName, tables)
		{}
		public ushort[] MappingFlags;	//a 2 byte bitmask of type PInvokeAttributes, clause 22.1.7
		public uint[] MemberForwarded;	//index into the Field or MethodDef table; more precisely, a MemberForwarded coded index.  However, it only ever indexes the MethodDef table, since Field export is not supported.
		public uint[] ImportName;		//index into the String heap
		public uint[] ImportScope;		//index into the ModuleRef table

		protected override void ReadRows()
		{
			this.MappingFlags = new ushort[this.numberOfRows];
			this.MemberForwarded = new UInt32[this.numberOfRows];
			this.ImportName = new UInt32[this.numberOfRows];
			this.ImportScope = new UInt32[this.numberOfRows];

			// heh heh, irresistable!
			bool longMember = this.parent.use4ByteIndex(Tables.CodedIndexType.MemberForwarded);

			for(int i = 0; i < this.NumberOfRows; i++)
			{
				this.MappingFlags[i] = this.reader.ReadUInt16();
				this.MemberForwarded[i]  = longMember ? this.reader.ReadUInt32() : this.reader.ReadUInt16();
				this.ImportName[i] = this.parent.use4ByteIndex("#String") ? this.reader.ReadUInt32() : this.reader.ReadUInt16();
				this.ImportScope[i] = this.parent.use4ByteIndex(Tables.TableName.ModuleRef) ? this.reader.ReadUInt32() : this.reader.ReadUInt16();
			}
		}
	}
}
