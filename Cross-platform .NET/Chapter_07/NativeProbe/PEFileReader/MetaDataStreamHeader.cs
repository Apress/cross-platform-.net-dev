//************************************************************************************************************************************
// Filename:    MetaDataHeader.cs
// Authors:     jason.king@profox.co.uk
// Copyright:	Copyright © Profox Ltd 2004
// Date:        24/10/2003
// Note:		Stores meta data header details
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
	public struct MetaDataStreamHeader
	{
		public readonly uint Offset;
		public readonly uint Size;
		public readonly string Name;

		public MetaDataStreamHeader(BinaryReader reader)
		{
			this.Offset = reader.ReadUInt32();
			this.Size = reader.ReadUInt32();
			this.Name = String.Empty;

			// the name is a null terminated string followed by null padding to the next 4 byte boundary
			byte nextByte = reader.ReadByte();
			while (nextByte != 0)
			{
				this.Name += (char)nextByte;
				nextByte = reader.ReadByte();
			}

			// When we exit the above loop, we have the name
			// and the reader has moved on 1 position to a null value
			// The null value denotes that the previous  value
			// was the last in the string.
			// The string is padded to 4 bytes length,
			// so this null is either at a 4 byte boundary, or not.
			// If it not a boundary, then move the pointer until it is.
			while ((reader.BaseStream.Position % 4) != 0)
				reader.BaseStream.Position++;
		}
	}
}
