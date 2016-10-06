//************************************************************************************************************************************
// Filename:    TablesHeader.cs
// Authors:     jason.king@profox.co.uk
// Copyright:	Copyright © Profox Ltd 2004
// Date:        24/10/2003
// Note:		Stores Tables header details
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
	public struct TablesHeader
	{
		public readonly byte HeapSizes;				// describes whether blob indexes are  2 or 4 bytes wide
		public readonly ulong Valid;				// bit vector of present tables.
		public readonly ulong Sorted;				//bit vector of sorted tables
		//private ArrayList strings;
		//private MetaDataStream stringHeap;
		//private BinaryReader reader;
				
		public readonly uint[] NumberOfRows;		// array of 4 byte unsigned ints indicating num of rows for each present table

		public TablesHeader(BinaryReader reader,  MetaDataStream stringheap)
		{
			//this.strings = new ArrayList();
			//this.stringHeap = stringheap;
			//this.reader = reader;
			
			this.NumberOfRows = new UInt32[64];
			// skip the first 6 bytes as we are not interested in them.  They represent the fixed
			// values of 0,1,0 for reserved, major version and minor version.
			reader.BaseStream.Position += 6;
			this.HeapSizes = reader.ReadByte();
			// another fixed value we are not interested in
			reader.BaseStream.Position += 1;
			this.Valid = reader.ReadUInt64();
			this.Sorted = reader.ReadUInt64();
			
			uint length = 0;
			// we are now ready to start reading the row lengths
			for(int i = 0; i < 64; i++)
			{
				length = 0;
				if(((this.Valid >> i) & 1) == 1)
					length = reader.ReadUInt32();

				this.NumberOfRows[i] = length;
			}
		}

	}
}
