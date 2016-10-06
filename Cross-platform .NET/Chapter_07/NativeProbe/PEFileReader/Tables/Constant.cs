//************************************************************************************************************************************
// Filename:    Constant.cs
// Authors:     jason.king@profox.co.uk
//			    mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © Profox Ltd 2004
// Date:        24/10/2003
// Note:		Stores Constant data
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
	public class Constant:MetaDataTable
	{
		public Constant(Tables.TableName tableName, Tables tables):base(tableName, tables){}

		public byte[] Type;			//a 1 byte constant, followed by a 1-byte padding zero) : see Clause 22.1.15 .  The encoding of Type for the nullref value for <fieldInit> in ilasm (see Section 15.2) is ELEMENT_TYPE_CLASS with a Value of a 4-byte zero.  Unlike uses of ELEMENT_TYPE_CLASS in signatures, this one is not followed by a type token.
		public uint[] Parent;		//index into the Param or Field or Property table; more precisely, a HasConstant coded index
		public uint[] Value;		//index into Blob heap

		protected override void ReadRows()
		{
			this.Type = new byte[this.numberOfRows];
			this.Parent = new uint[this.numberOfRows];
			this.Value = new uint[this.numberOfRows];
			
			bool longParentIndex = this.parent.use4ByteIndex(Tables.CodedIndexType.HasConstant);
			for(int i = 0; i < this.NumberOfRows; i++)
			{
				this.Type[i] = this.reader.ReadByte();
				// now move over the following 1 byte padding zero
				this.reader.ReadByte();
				this.Parent[i] = longParentIndex ? this.reader.ReadUInt32() : reader.ReadUInt16();
				this.Value[i] = this.parent.use4ByteIndex("#Blob") ? this.reader.ReadUInt32() : this.reader.ReadUInt16();
			}
		}
	}
}
