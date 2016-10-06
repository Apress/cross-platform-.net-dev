//************************************************************************************************************************************
// Filename:    CustomAttribute.cs
// Authors:     jason.king@profox.co.uk
//			    mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © Profox Ltd 2004
// Date:        24/10/2003
// Note:		Stores CustomAttribute data
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
	public class CustomAttribute:MetaDataTable
	{
		public CustomAttribute(Tables.TableName tableName, Tables tables):base(tableName, tables){}

		public uint[] Parent;		//index into any metadata table, except the CustomAttribute table itself; more precisely, a HasCustomAttribute coded index
		public uint[] Type;			//index into the MethodDef or MemberRef (note ecma document says methodref by mistake) table; more precisely, a CustomAttributeType coded index
		public uint[] Value;		//index into Blob heap

		protected override void ReadRows()
		{
			this.Parent = new uint[this.numberOfRows];
			this.Type = new uint[this.numberOfRows];
			this.Value = new uint[this.numberOfRows];
			
			//rather than call use4Bytes 19 times for every row, lets just do it once
			bool longParent = this.parent.use4ByteIndex(Tables.CodedIndexType.HasCustomAttribute);
			bool longType = this.parent.use4ByteIndex(Tables.CodedIndexType.CustomAttributeType);

			// Parent is a HasCustomAttribute token. The ecma document says it takes 5 bits
			// to encode all the tables it may reference.  So, if the field were to be just
			// 2 bytes long, that would leave 1 byte and 3 bits left over to store the index
			// into that table.  That many bits leaves us a max value of 2047 rows, so if any
			// of the 19 tables have more than 2047 rows, we need a 4 byte index.
			
			for(int i = 0; i < this.NumberOfRows; i++)
			{
				this.Parent[i] = longParent ? this.reader.ReadUInt32() : this.reader.ReadUInt16();
				this.Type[i] = longType ? this.reader.ReadUInt32() : this.reader.ReadUInt16();
				this.Value[i] = this.parent.use4ByteIndex("#Blob") ? this.reader.ReadUInt32() : this.reader.ReadUInt16(); 
			}
		}
	}
}
