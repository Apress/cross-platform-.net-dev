//************************************************************************************************************************************
// Filename:    AssemblyInfo.cs
// Author:      mark.easton@blinksoftware.co.uk
//				jason.king@profox.co.uk
// Copyright:	Copyright © Profox Ltd 2004
//				Copyright © BLiNK Software Ltd 2004 
// Date:        24/02/2003
// Note:		Provides AssemblyInfo declarations and utility routines for use within the assembly
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
using System.Reflection;
using System.Resources;
using System.Security.Permissions;

// Mark the framework assembly as CLS compliant
//[assembly:CLSCompliant(true)]

// Assembly attributes
[assembly: AssemblyTitle("Native Probe")]
[assembly: AssemblyDescription("This program is free software and is protected under the MIT X11 License. For support please goto http://www.blinksoftware.co.uk.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Profox with BLiNK Software")]
[assembly: AssemblyProduct("Native Probe")]
[assembly: AssemblyCopyright("Copyright © Profox/BLiNK Software 2004")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]		

// Assembly version attribute (Major Version.Minor Version.Build Number.Revision)
[assembly: AssemblyVersion("0.3.0.*")]

// Assembly signing attributes
[assembly: AssemblyDelaySign(false)]
[assembly: AssemblyKeyFile("")]
[assembly: AssemblyKeyName("")]

// Set neutral resources language for assembly.
[assembly: NeutralResourcesLanguageAttribute("en")]

// Security permissions for the assembly.
//[assembly:FileIOPermission(SecurityAction.RequestOptional, Unrestricted = true)]