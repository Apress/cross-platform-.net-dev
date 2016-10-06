//************************************************************************************************************************************
// Filename:    MetaDataTable.cs
// Authors:     jason.king@profox.co.uk
//				mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © Profox Ltd 2004
//				Copyright © BLiNK Software Ltd 2004 
// Date:        24/10/2003
// Note:		The abstract base class for a MetaDataTable
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
	public  abstract class MetaDataTable
	{
		
		// NOTE: all fields that are indexes are defined as 4 bytes long
		// even if they are only 2 bytes in the file. This is to cope
		// with the fact that indexes may be 2 or 4 bytes long.
		// A two byte number can be stored as a 4 byte number
		// even though it is inefficient use of memory, but it makes
		// the coding easier - better than having 2 fields to cope with the differing sizes
		
		protected BinaryReader reader;
		protected uint numberOfRows;
		private long startPosition;
		protected int rowLength;
		private int tableNumber;
		private byte indexSizeIndicator;
		private MetaDataStream stringHeap;
		protected Tables parent;
		

		public uint NumberOfRows
		{
			get{return numberOfRows;}
		}

		public long StartPosition
		{
			get{return startPosition;}
		}
		public int RowLength
		{
			get{return rowLength;}
		}
	
		public int TableNumber
		{
			get{return tableNumber;}
		}

		public MetaDataTable(Tables.TableName table, Tables parent) //long startPosition, TablesHeader tablesHeader, MetaDataStream stringHeap, BinaryReader reader, Hashtable indexes)
		{
			this.tableNumber = (int)table;
			this.parent = parent;
			this.startPosition = parent.reader.BaseStream.Position;
			this.reader = this.parent.reader;
			this.indexSizeIndicator = this.parent.tablesHeader.HeapSizes;
			this.stringHeap = this.parent.stringHeap;
			this.numberOfRows = this.parent.RowsPerTable[this.tableNumber];
			this.ReadRows();
		}
		
		protected virtual void ReadRows(){}
	}
}