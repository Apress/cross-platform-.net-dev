//************************************************************************************************************************************
// Filename:    Module.cs
// Authors:     jason.king@profox.co.uk
//			    mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © Profox Ltd 2004
// Date:        24/10/2003
// Note:		Stores Module data
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
	public class Module: MetaDataTable
	{	
		public ushort[] Generation;	// Generation 2 bytes
		public uint[] Name;			// Name, index into string heap
		public uint[] MVID;			// mvid, index into guid heap
		public uint[] ENCID;		// encid, index into guid heap
		public uint[] ENCBaseID;	// encbaseid, index into guid heap

		public Module(Tables.TableName tableName, Tables tables):base(tableName, tables){}

		protected override void ReadRows()
		{
			// four fields
			this.rowLength = 18;
			
			this.Generation = new ushort[this.numberOfRows];
			this.Name = new uint[this.numberOfRows];
			this.MVID = new uint[this.numberOfRows];
			this.ENCID = new uint[this.numberOfRows];
			this.ENCBaseID = new uint[this.numberOfRows];

			for(int i = 0; i < this.NumberOfRows; i++)
			{
				
				this.Generation[i] = this.reader.ReadUInt16();
				
				if (this.parent.use4ByteIndex("#String"))
				{
					this.Name[i] = this.reader.ReadUInt32();
				}
				else
				{
					this.Name[i] = this.reader.ReadUInt16();
				}
				// all 3 will be the size
				if (this.parent.use4ByteIndex("#GUID"))
				{
					this.MVID[i] = this.reader.ReadUInt32();
					this.ENCID[i] = this.reader.ReadUInt32();
					this.ENCBaseID[i] = this.reader.ReadUInt32();
				}
				else
				{
					this.MVID[i] = this.reader.ReadUInt16();
					this.ENCID[i] = this.reader.ReadUInt16();
					this.ENCBaseID[i] = this.reader.ReadUInt16();
				}
			}
		}
	}
}
