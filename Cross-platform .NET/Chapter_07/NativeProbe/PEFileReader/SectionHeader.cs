//************************************************************************************************************************************
// Filename:    SectionHeader.cs
// Authors:     jason.king@profox.co.uk
//				mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © Profox Ltd 2004
//				Copyright © BLiNK Software Ltd 2004 
// Date:        24/10/2003
// Note:		Stores Section header details
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

namespace PEFileReader
{
	public struct SectionHeader
	{		
		public readonly string Name;
		public readonly uint VirtualSize;
		public readonly uint VirtualAddress;
		public readonly uint SizeOfRawData;
		public readonly uint PointerToRawData;
		public readonly uint PointerToRelocations;
		public readonly uint PointerToLineNumbers;
		public readonly ushort NumberOfRelocations;
		public readonly ushort NumberOfLineNumbers;
		public readonly uint Characteristics;

		public SectionHeader(BinaryReader reader)
		{
			//Parse the section header's name
			this.Name = String.Empty;
			for(int headerByteIndex = 0; headerByteIndex < 8; ++headerByteIndex)
			{
				byte currentByte = reader.ReadByte();
				if (currentByte != 0)
					this.Name += (char)currentByte;
			}

			//Parse the remaining fields
			this.VirtualSize = reader.ReadUInt32();
			this.VirtualAddress = reader.ReadUInt32();
			this.SizeOfRawData = reader.ReadUInt32();
			this.PointerToRawData = reader.ReadUInt32();
			this.PointerToRelocations = reader.ReadUInt32();
			this.PointerToLineNumbers = reader.ReadUInt32();
			this.NumberOfRelocations = reader.ReadUInt16();
			this.NumberOfLineNumbers = reader.ReadUInt16();
			this.Characteristics = reader.ReadUInt32();
		}
	}
}
