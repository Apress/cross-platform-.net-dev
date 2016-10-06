//************************************************************************************************************************************
// Filename:    Tables.cs
// Authors:     jason.king@profox.co.uk
// Copyright:	Copyright © Profox Ltd 2004
// Date:        24/10/2003
// Note:		Stores Table details
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
	public class Tables
	{
		//private MetaDataStreamHeader streamHeader;
		public TablesHeader tablesHeader;
		public MetaDataStream stringHeap;
		public BinaryReader reader;
		private int indexSizeIndicator;
		public uint[] RowsPerTable;
		public object[] CodedIndexInformation;

		//The tables:
		public Module Module; //0x00
		public TypeRef TypeRef; //0x01
		public TypeDef TypeDef; //0x02
		// FieldPtr 0x03 is not defined in partition 2
		public Field Field; // 0x04
		// MethodPtr 0x05 is not defined in partition 2
		public MethodDef MethodDef; // 0x06
		// ParamPts 0x07 is not defined
		public Param Param; // 0x08
		public InterfaceImpl InterfaceImpl; // 0x09
		public MemberRef MemberRef; //0x0A
		public Constant Constant; // 0X0B
		public CustomAttribute CustomAttribute;	// 0x0C
		public FieldMarshal FieldMarshal;// 0x0D
		public DeclSecurity DeclSecurity; // 0x0E
		public ClassLayout ClassLayout; //0x0F 
		public FieldLayout FieldLayout; //0x10
		public StandAloneSig StandAloneSig; //0x11
		public EventMap EventMap;// 0x12
		// 0x13 eventPtr is not defined in ecma document
		public EventX EventX;// 0x14
		public PropertyMap PropertyMap; // 0x15
		// 0x16 PropertyPtr is not defined in ECMA document
		public Property Property; // 0x17
		public MethodSemantics MethodSemantics; // 0x18
		public MethodImpl MethodImpl; // 0x19
		public ModuleRef ModuleRef; // 0x1A
		public TypeSpec TypeSpec; // 0x1B
		public ImplMap ImplMap; // 0x1C
		

		public enum CodedIndexType
		{
			TypeDefOrRef = 0,
			HasConstant = 1,
			HasCustomAttribute = 2,
			HasFieldMarshal = 3,
			HasDeclSecurity = 4,
			MemberRefParent = 5,
			HasSemantics = 6,
			MethodDefOrRef = 7,
			MemberForwarded = 8,
			Implementation = 9,
			CustomAttributeType = 10,
			ResolutionScope =11
		}
		public enum TableName
		{
			//Tables
			Module = 0,
			TypeRef = 1,
			TypeDef = 2,
			FieldPtr = 3, 
			Field = 4,
			MethodPtr = 5,
			MethodDef = 6,
			ParamPtr = 7, 
			Param = 8,
			InterfaceImpl = 9,
			MemberRef = 10,
			Constant = 11, 
			CustomAttribute = 12,
			FieldMarshal = 13,
			DeclSecurity = 14,
			ClassLayout = 15, 
			FieldLayout = 16,
			StandAloneSig = 17,
			EventMap = 18,
			EventPtr = 19, 
			EventX = 20,
			PropertyMap = 21,
			PropertyPtr = 22,
			Property = 23, 
			MethodSemantics = 24,
			MethodImpl = 25,
			ModuleRef = 26,
			TypeSpec = 27, 
			ImplMap = 28, 
			FieldRVA = 29,
			ENCLog = 30,
			ENCMap = 31, 
			Assembly = 32,
			AssemblyProcessor= 33,
			AssemblyOS = 34,
			AssemblyRef = 35, 
			AssemblyRefProcessor = 36,
			AssemblyRefOS = 37,
			File = 38,
			ExportedType = 39, 
			ManifestResource = 40,
			NestedClass = 41,
			TypeTyPar = 42,
			MethodTyPar = 43
		}


		public Tables(TablesHeader tablesHeader, MetaDataStream stringHeap, BinaryReader reader)
		{
			this.tablesHeader = tablesHeader;
			this.stringHeap = stringHeap;
			this.reader = reader;
			this.indexSizeIndicator = tablesHeader.HeapSizes;
			this.RowsPerTable = tablesHeader.NumberOfRows;

			// set up a table of coded index information
			// the first figure in the array is the number of bits needed to encode
			// which table the index is pointing to.  Note, some indexes use more bits
			// than are necessary, for example CustomAttributeType uses 3 bits even though
			// it only points to 2 tables, so in theory could be encoded in 1 bit.
			
			this.CodedIndexInformation = new object[12];
			CodedIndexInformation[(int)Tables.CodedIndexType.TypeDefOrRef] = new int[] {2,(int)Tables.TableName.TypeDef, (int)Tables.TableName.TypeRef, (int)Tables.TableName.TypeSpec};
			CodedIndexInformation[(int)Tables.CodedIndexType.HasConstant] = new int[] {2, (int)Tables.TableName.Field, (int)Tables.TableName.Param, (int)Tables.TableName.Property};
			CodedIndexInformation[(int)Tables.CodedIndexType.HasCustomAttribute] = new int[] {5,(int)Tables.TableName.MethodDef,
																								 (int)Tables.TableName.Field,
																								 (int)Tables.TableName.TypeRef,
																								 (int)Tables.TableName.TypeDef ,
																								 (int)Tables.TableName.Param ,
																								 (int)Tables.TableName.InterfaceImpl ,
																								 (int)Tables.TableName.MemberRef ,
																								 (int)Tables.TableName.Module ,
																								 (int)Tables.TableName.DeclSecurity ,
																								 (int)Tables.TableName.Property ,
																								 (int)Tables.TableName.EventX ,
																								 (int)Tables.TableName.StandAloneSig ,
																								 (int)Tables.TableName.ModuleRef ,
																								 (int)Tables.TableName.TypeSpec ,
																								 (int)Tables.TableName.Assembly ,
																								 (int)Tables.TableName.AssemblyRef ,
																								 (int)Tables.TableName.File ,
																								 (int)Tables.TableName.ExportedType ,
																								 (int)Tables.TableName.ManifestResource};
			CodedIndexInformation[(int)Tables.CodedIndexType.HasFieldMarshal] = new int[] {1, (int)Tables.TableName.Field, (int)Tables.TableName.Param};
			CodedIndexInformation[(int)Tables.CodedIndexType.HasDeclSecurity] = new int[] {2, (int)Tables.TableName.TypeDef ,(int)Tables.TableName.MethodDef,(int)Tables.TableName.Assembly};
			CodedIndexInformation[(int)Tables.CodedIndexType.MemberRefParent] = new int[] {3, (int)Tables.TableName.TypeRef, (int)Tables.TableName.ModuleRef, (int)Tables.TableName.MethodDef, (int)Tables.TableName.TypeSpec};
			CodedIndexInformation[(int)Tables.CodedIndexType.HasSemantics] = new int[] {1, (int)Tables.TableName.EventX, (int)Tables.TableName.Property};
			CodedIndexInformation[(int)Tables.CodedIndexType.MethodDefOrRef] = new int[] {1, (int)Tables.TableName.MethodDef, (int)Tables.TableName.MemberRef};
			CodedIndexInformation[(int)Tables.CodedIndexType.MemberForwarded] = new int[] {1, (int)Tables.TableName.Field, (int)Tables.TableName.MethodDef};
			CodedIndexInformation[(int)Tables.CodedIndexType.Implementation] = new int[] {2, (int)Tables.TableName.File,(int)Tables.TableName.AssemblyRef, (int)Tables.TableName.ExportedType};
			CodedIndexInformation[(int)Tables.CodedIndexType.CustomAttributeType] = new int[] {3, (int)Tables.TableName.MethodDef,(int)Tables.TableName.MemberRef};
			CodedIndexInformation[(int)Tables.CodedIndexType.ResolutionScope] = new int[] {2, (int)Tables.TableName.Module ,(int)Tables.TableName.ModuleRef,(int)Tables.TableName.AssemblyRef ,(int)Tables.TableName.TypeRef};

			this.Module = new Module(Tables.TableName.Module, this);
			this.TypeRef = new TypeRef(Tables.TableName.TypeRef,this);
			this.TypeDef = new TypeDef(Tables.TableName.TypeDef,this);
			this.Field = new Field(Tables.TableName.Field,this);
			this.MethodDef = new MethodDef(Tables.TableName.MethodDef,this);
			this.Param = new Param(Tables.TableName.Param,this);
			this.InterfaceImpl = new InterfaceImpl(Tables.TableName.InterfaceImpl,this);
			this.MemberRef = new MemberRef(Tables.TableName.MemberRef,this);
			this.Constant = new Constant(Tables.TableName.Constant,this);
			this.CustomAttribute = new CustomAttribute(Tables.TableName.CustomAttribute,this);
			this.FieldMarshal = new FieldMarshal(Tables.TableName.FieldMarshal,this);
			this.DeclSecurity = new DeclSecurity(Tables.TableName.DeclSecurity,this);
			this.ClassLayout = new ClassLayout(Tables.TableName.ClassLayout,this);
			this.FieldLayout = new FieldLayout(Tables.TableName.FieldLayout,this);
			this.StandAloneSig = new StandAloneSig(Tables.TableName.StandAloneSig,this); 
			this.EventMap = new EventMap(Tables.TableName.EventMap,this);
			this.EventX = new EventX(Tables.TableName.EventX,this);
			this.PropertyMap = new PropertyMap(Tables.TableName.PropertyMap,this);
			this.Property = new Property(Tables.TableName.Property,this);
			this.MethodSemantics = new MethodSemantics(Tables.TableName.MethodSemantics,this);
			this.MethodImpl = new MethodImpl(Tables.TableName.MethodImpl,this);
			this.ModuleRef = new ModuleRef(Tables.TableName.ModuleRef,this);
			this.TypeSpec = new TypeSpec(Tables.TableName.TypeSpec,this);
			this.ImplMap = new ImplMap(Tables.TableName.ImplMap,this);

		}

				
		public bool use4ByteIndex(string heapName)
		{
			bool returnValue = false;
			switch (heapName)
			{
				case "#String":
					returnValue = ((this.indexSizeIndicator & 0x01) == 0x01);
					break;
				case "#GUID":
					returnValue = ((this.indexSizeIndicator & 0x02) == 0x02);
					break;
				case "#Blob":
					returnValue = ((this.indexSizeIndicator & 0x04) == 0x04);
					break;
			}
			
			return returnValue;
		}

		public bool use4ByteIndex(TableName tableNumber)
		{
			return (this.RowsPerTable[(int)tableNumber] > 65536);
		}

		public bool use4ByteIndex(CodedIndexType iType)
		{
			int[] tables = (int[])this.CodedIndexInformation[(int)iType];
			//the first element is the number of bits required to encode this index type
			int numOfBits = tables[0];
			int maxValueForRemainingBits = 65535;
			maxValueForRemainingBits = 65535 >> numOfBits;

			// Now loop through each relevant table and see if any of them have more rows
			// than the figure we just calculated.
			uint maxRows = 0;
			for(int i = 1; i < tables.Length; i++)
			{
				if (this.RowsPerTable[tables[i]] > maxRows)
					maxRows = this.RowsPerTable[tables[i]];
			}

			return (maxRows > maxValueForRemainingBits);
		}
	
	}

}
