//************************************************************************************************************************************
// Filename:    MetaDatRoot.cs
// Authors:     jason.king@profox.co.uk
// Copyright:	Copyright © Profox Ltd 2004
// Date:        24/10/2003
// Note:		Stores meta data root details
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
	public struct MetaDataRoot
	{
		public readonly uint Signature;
		public readonly ushort MajorVersion;
		public readonly ushort MinorVersion;
		public readonly uint Reserved;
		public readonly uint VersionStringLength;
		public readonly string VersionString;
		public readonly ushort Flags;
		public readonly ushort NumberOfStreams;
		public readonly long StartOfRoot;

		public MetaDataStreamHeader[] MetaDataStreamHeaders;

		public MetaDataRoot(BinaryReader reader)
		{
			// we must retain the position of the start of the metadataroot
			// as the MetadataStreamHeaders measure the start of the metedata
			// streams as offsets from this point in the file.
			this.StartOfRoot = reader.BaseStream.Position;
			this.Signature = reader.ReadUInt32();
		
			if (this.Signature != 0x424A5342) 
				throw new Exception("MetaData:  Incorrect signature.");

			this.MajorVersion = reader.ReadUInt16();
			this.MinorVersion = reader.ReadUInt16();
			this.Reserved = reader.ReadUInt32();

			//the versionStringLength is actually the number of bytes required
			//rounded up to the nearest 4 byte boundary (as opposed to the length
			//of the string).  Therefore, we don't need any maths to cope
			//with the null padding.
			this.VersionString = String.Empty;
			this.VersionStringLength = reader.ReadUInt32();
			for(int versionStringByteIndex = 0; versionStringByteIndex < this.VersionStringLength; ++versionStringByteIndex)
			{
				byte currentByte = reader.ReadByte();
				if (currentByte != 0)
					this.VersionString += (char)currentByte;
			}
			
			this.Flags = reader.ReadUInt16();;
			this.NumberOfStreams = reader.ReadUInt16();
			
			this.MetaDataStreamHeaders = new MetaDataStreamHeader[this.NumberOfStreams];
			for(int streamIndex = 0; streamIndex < this.NumberOfStreams; streamIndex++)
				this.MetaDataStreamHeaders[streamIndex] = new MetaDataStreamHeader(reader);

		}

	}
}
