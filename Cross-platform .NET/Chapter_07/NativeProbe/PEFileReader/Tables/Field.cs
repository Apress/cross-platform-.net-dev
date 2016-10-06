//************************************************************************************************************************************
// Filename:    Field.cs
// Authors:     jason.king@profox.co.uk
//			    mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © Profox Ltd 2004
// Date:        24/10/2003
// Note:		Stores Field data
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
	public class Field:MetaDataTable
	{
		public Field(Tables.TableName tableName, Tables tables):base(tableName, tables){}

		public ushort[] Flags;		//a 2 byte bitmask of type FieldAttributes
		public uint[] Name ;		//index into String heap
		public uint[] Signature;	//index into Blob heap
		
		protected override void ReadRows()
		{
			this.Flags = new ushort[this.numberOfRows];
			this.Name = new uint[this.numberOfRows];
			this.Signature = new uint[this.numberOfRows];
			
			for(int i = 0; i < this.NumberOfRows; i++)
			{
				this.Flags[i] = this.reader.ReadUInt16();
				
				if (this.parent.use4ByteIndex("#String"))
				{
					this.Name[i] = this.reader.ReadUInt32();
				}
				else
				{
					this.Name[i] = this.reader.ReadUInt16();
				}

				if (this.parent.use4ByteIndex("#Blob"))
				{
					this.Signature[i] = this.reader.ReadUInt32();
				}
				else
				{
					this.Signature[i] = this.reader.ReadUInt16();
				}
			}
		}
	}
}
