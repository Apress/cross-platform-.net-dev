//************************************************************************************************************************************
// Filename:    TypeDef.cs
// Authors:     jason.king@profox.co.uk
//			    mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © Profox Ltd 2004
// Date:        24/10/2003
// Note:		Stores TypeDef data
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
	public class TypeDef:MetaDataTable
	{
		public TypeDef(Tables.TableName tableName, Tables tables):base(tableName, tables){}

		public uint[] Flags;			//a 4 byte bitmask of type TypeAttributes
		public uint[] Name;				//index into String heap
		public uint[] Namespace;		//index into String heap
		public uint[] Extends;			//index into TypeDef, TypeRef or TypeSpec table; more precisely, a TypeDefOrRef coded index
		public uint[] FieldList;		//index into field table
		public uint[] MethodList;		//index into method table

		protected override void ReadRows()
		{
			this.Flags = new uint[this.numberOfRows];
			this.Name = new uint[this.numberOfRows];
			this.Namespace = new uint[this.numberOfRows];
			this.Extends = new uint[this.numberOfRows];
			this.FieldList = new uint[this.numberOfRows];
			this.MethodList = new uint[this.numberOfRows];

			bool longExtendsIndex = this.parent.use4ByteIndex(Tables.CodedIndexType.TypeDefOrRef);

			for(int i = 0; i < this.NumberOfRows; i++)
			{
				this.Flags[i] = this.reader.ReadUInt32();
				
				if (this.parent.use4ByteIndex("#String"))
				{
					this.Name[i] = this.reader.ReadUInt32();
					this.Namespace[i] = this.reader.ReadUInt32();
				}
				else
				{
					this.Name[i] = this.reader.ReadUInt16();
					this.Namespace[i] = this.reader.ReadUInt16();
				}

				this.Extends[i] = longExtendsIndex ? this.reader.ReadUInt32() : this.reader.ReadUInt16();
				

				if (this.parent.use4ByteIndex(Tables.TableName.Field))
				{
					this.FieldList[i] = this.reader.ReadUInt32();
				}
				else
				{
					this.FieldList[i] = this.reader.ReadUInt16();
				}

				if (this.parent.use4ByteIndex(Tables.TableName.MethodDef))
				{
					this.MethodList[i] = this.reader.ReadUInt32();
				}
				else
				{
					this.MethodList[i] = this.reader.ReadUInt16();
				}
			}
		}
	}
}
