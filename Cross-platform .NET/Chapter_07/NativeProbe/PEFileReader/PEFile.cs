//************************************************************************************************************************************
// Filename:    PEFile.cs
// Authors:     jason.king@profox.co.uk
//				mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © Profox Ltd 2004
//				Copyright © BLiNK Software Ltd 2004 
// Date:        24/10/2003
// Note:		Stores the details of a PE file
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
using System.Reflection;
using System.Collections;
using System.Runtime.InteropServices;


namespace PEFileReader
{
	public class PEFile :IDisposable
	{	
		public readonly string FileName;					//The assembly's filename

		//The file's contents
		private PEFileHeader peFileHeader;
		private Directory cliDirectory;
		private SectionHeader[] sections;
		private Directory metaDataDirectory;
		private MetaDataRoot metaDataRoot;
		private MetaDataStream[] metaDataStreams;
		private MetaDataStream tablesStream;
		internal MetaDataStream stringHeap;
		private TablesHeader tablesHeader;
		public Tables tables;

		private BinaryReader streamReader = null;

		#region Constructors & Destructors

		//Construct the binary assembly instance
		public PEFile(string fileName)
		{
			this.FileName = fileName;

			//Open the assembly's binary file in shared-read mode and copy it into a buffer so we can close the file post haste.
			FileStream fileStream =  new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
			byte[] streamBuffer = new byte[fileStream.Length]; 
			fileStream.Read(streamBuffer, 0, (int)fileStream.Length);
			fileStream.Close();
		
			//Push the buffer into a memory stream and then access it through a binary reader
			this.streamReader = new BinaryReader(new MemoryStream(streamBuffer));
		
			// The rest of the code moves through the loaded assemebly as follows:
			// 1) DOS header points to the PE header.
			// 2) PE header contains a the CLI directory that points to the The CLI header. 
			// 3) In order to work out the RVAs, we need the "Sections" of the file.
			// 4) CLI header in turn contains the RVA to MetaData directory.
			// 5) Metadata header points to the metadata root.
			// 6) metadata root lists the metadata stream headers.
			// 7) metadata stream headers contain the offsets to the metadata stream.  Hooray!

			// We only need certain elements of the file. Those elements we
			// need are stored in structs/classes, the elements we don't need
			// are parsed by methods whose purpose is:
			// a) to move the stream position to where we need it, 
			// b) to be substituted with classes as the scope of requirements increases.
		
		
			ParseMsDosHeader(this.streamReader);
			this.peFileHeader = new PEFileHeader(this.streamReader);
			ParsePeOptionalHeader(this.streamReader);		
			BuildSections(this.streamReader);
		
			// Now we have the sections we can work out where RVAs lie,
			// so we can get the CLI Header, which in turn allows us to get the 
			// metadata directory.
			ParseCliHeader(this.streamReader);
			this.metaDataDirectory = new Directory(this.streamReader);

			this.streamReader.BaseStream.Position = GetOffsetFromRVA(this.metaDataDirectory.Rva);
			this.metaDataRoot = new MetaDataRoot(this.streamReader);

			// now we have the metadataroot complete with the metadata stream headers,
			// we can build the metadata streams.
			BuildMetaDataStreams(this.streamReader);
	
			// the following builds the descriptive structure found at the start of the #~ stream
			// first off, move to the correct point in the file as the previous step will have left
			// us in the wrong place
			this.streamReader.BaseStream.Position = this.tablesStream.StartPositionInFile;
			this.tablesHeader = new TablesHeader(this.streamReader, this.stringHeap);

			// now we have the tables header, we can build the tables
			this.tables = new Tables(this.tablesHeader,  this.stringHeap, this.streamReader);						
		}

		//Ensure finalization explictly drops resources...
		~PEFile()
		{
			DropResources();
		}

		//Implement the IDispose interface method...
		public void Dispose()
		{
			GC.SuppressFinalize(this); 
			DropResources();
		}

		//Do the actual cleanup work
		private void DropResources() 
		{
			if (this.streamReader != null)
			{
				this.streamReader.Close();
				this.streamReader = null;
			}
		}

		#endregion

		#region Parse Methods

		//Move through the MsDos header
		private void ParseMsDosHeader(BinaryReader fileReader)
		{
			fileReader.BaseStream.Position = 0x3c;

			uint peFileHeaderOffset = fileReader.ReadUInt32();
			fileReader.BaseStream.Position = peFileHeaderOffset;

			//Check we have a PE file
			if (fileReader.ReadUInt32() != 0x00004550) 
				throw new Exception("File is not a portable executable.");
		}


		//Move through the PR optional header which is broken down into 3 sections (a total of 224 bytes)
		private void ParsePeOptionalHeader(BinaryReader fileReader)
		{			
			//Move past the standard fields(28 bytes)
			fileReader.ReadBytes(28);

			//Move past the NT specific fields(68 bytes)
			fileReader.ReadBytes(68);

			//Move through the data directories. There should be 16 of these, each 8 bytes long (16 * 8 = 128).			
			fileReader.ReadBytes(14 * 8);
			//We are only interested in directory number 15 - the CLI directory.
			this.cliDirectory = new Directory(fileReader);
			fileReader.ReadBytes(1 * 8);
		}
		

		//Parse the CLI header and move to position CLIDirectory.RVA and add 8 bytes
		private void ParseCliHeader(BinaryReader fileReader)
		{
			fileReader.BaseStream.Position = GetOffsetFromRVA(this.cliDirectory.Rva) + 8;
		}


		//Retrieve the offset from the RVA
		public uint GetOffsetFromRVA(uint rva)
		{
			foreach (SectionHeader section in this.sections)
			{
				if ((section.VirtualAddress <= rva) && (section.VirtualAddress + section.SizeOfRawData > rva))
					return (section.PointerToRawData + (rva - section.VirtualAddress));
			}
			throw new Exception("Could not find RVA address in any section of the file.");
		}

		#endregion

		#region Build Methods

		//Build the sections
		private void BuildSections(BinaryReader fileReader)
		{
			this.sections = new SectionHeader[this.peFileHeader.NumberOfSections];
			for(int sectionIndex = 0; sectionIndex < this.peFileHeader.NumberOfSections; sectionIndex++)
				this.sections[sectionIndex] = new SectionHeader(fileReader);
		}


		private void BuildMetaDataStreams(BinaryReader fileReader)
		{
			// this code is factored out of script for ease of script reading
			this.metaDataStreams = new MetaDataStream[this.metaDataRoot.NumberOfStreams];

			// lets initialise some handy variables to save typing
			long rootPos = this.metaDataRoot.StartOfRoot;

			for(int i = 0; i < this.metaDataRoot.NumberOfStreams; i++)
			{
				// handy variable for shorter code
				MetaDataStreamHeader streamHead = this.metaDataRoot.MetaDataStreamHeaders[i];
				
				// move to correct position for start of stream.  This is listed
				// as an offset from the start of the metadataroot.
				
				fileReader.BaseStream.Position = rootPos + streamHead.Offset;
				this.metaDataStreams[i] = new MetaDataStream(fileReader, streamHead.Name, streamHead.Size, this);
				if (this.metaDataStreams[i].Name == "#~")
					this.tablesStream = this.metaDataStreams[i];

				if (this.metaDataStreams[i].Name == "#Strings")
					this.stringHeap = this.metaDataStreams[i];
			}
		}

		//MJE - Building a structure to store the strings is no good without faciliteis to map from the string's 'stream index' to an actual index
//		private void BuildStringHeapStrings(BinaryReader fileReader)
//		{
//			fileReader.BaseStream.Position = this.stringHeap.StartPositionInFile;
//			//first entry is always \0
//			this.stringHeapStrings = new ArrayList();
//			this.stringHeapStrings.Add(fileReader.ReadChar());
//
//			//char[] thechars = this.Reader.ReadChars((int)this.StringHeap.LengthOfStream);
//			string theString = String.Empty;
//			for(int i = 1 ; i <= this.stringHeap.LengthOfStream; i++)
//			{
//				byte nextChar = fileReader.ReadByte();
//				if (nextChar != 0)
//					theString += (char)nextChar;
//				else
//				{
//					this.stringHeapStrings.Add(theString);
//					theString = String.Empty;
//				}
//			}
//		}

		#endregion

	}
}
