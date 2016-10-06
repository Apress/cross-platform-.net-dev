//************************************************************************************************************************************
// Filename:    PropertyMap.cs
// Authors:     jason.king@profox.co.uk
//			    mark.easton@blinksoftware.co.uk
// Copyright:	Copyright © Profox Ltd 2004
// Date:        24/10/2003
// Note:		Stores PropertyMap data
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
	public class PropertyMap:MetaDataTable
	{
		public PropertyMap(Tables.TableName tableName, Tables tables):base(tableName, tables){}

		public uint[] Parent;			//index into the TypeDef table
		public uint[] PropertyList;		//index into Property table

		protected override void ReadRows()
		{
			this.Parent = new UInt32[this.numberOfRows];
			this.PropertyList = new UInt32[this.numberOfRows];

			for(int i = 0; i < this.NumberOfRows; i++)
			{
				this.Parent[i] = this.parent.use4ByteIndex(Tables.TableName.TypeDef) ? this.reader.ReadUInt32(): this.reader.ReadUInt16();
				this.PropertyList[i] = this.parent.use4ByteIndex(Tables.TableName.Property) ? this.reader.ReadUInt32(): this.reader.ReadUInt16();
			}
		}
	}
}
