
//  Microsoft (R) .NET Framework IL Disassembler.  Version 1.1.4322.573
//  Copyright (C) Microsoft Corporation 1998-2002. All rights reserved.

.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 1:0:5000:0
}
.assembly extern System
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 1:0:5000:0
}
.assembly ComputerName
{
  // --- The following custom attribute is added automatically, do not uncomment -------
  //  .custom instance void [mscorlib]System.Diagnostics.DebuggableAttribute::.ctor(bool,
  //                                                                                bool) = ( 01 00 00 01 00 00 ) 
  .hash algorithm 0x00008004
  .ver 0:0:0:0
}
.module ComputerName.exe
// MVID: {22692470-BA12-4142-835A-79D628A32A1A}
.imagebase 0x00400000
.subsystem 0x00000003
.file alignment 512
.corflags 0x00000001
// Image base: 0x06b10000
//
// ============== CLASS STRUCTURE DECLARATION ==================
//
.namespace Crossplatform.NET.Chapter03
{
  .class private auto ansi beforefieldinit ComputerName
         extends [mscorlib]System.Object
  {
  } // end of class ComputerName

} // end of namespace Crossplatform.NET.Chapter03


// =============================================================


// =============== GLOBAL FIELDS AND METHODS ===================


// =============================================================


// =============== CLASS MEMBERS DECLARATION ===================
//   note that class flags, 'extends' and 'implements' clauses
//          are provided here for information only

.namespace Crossplatform.NET.Chapter03
{
  .class private auto ansi beforefieldinit ComputerName
         extends [mscorlib]System.Object
  {
    .method public hidebysig static void 
            Main() cil managed
    {
      .entrypoint
      // Code size       13 (0xd)
      .maxstack  1
      .locals init (string V_0)
      IL_0000:  call       string [System]System.Net.Dns::GetHostName()
      IL_0005:  stloc.0
      IL_0006:  ldloc.0
      IL_0007:  call       void [mscorlib]System.Console::WriteLine(string)
      IL_000c:  ret
    } // end of method ComputerName::Main

    .method public hidebysig specialname rtspecialname 
            instance void  .ctor() cil managed
    {
      // Code size       7 (0x7)
      .maxstack  1
      IL_0000:  ldarg.0
      IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
      IL_0006:  ret
    } // end of method ComputerName::.ctor

  } // end of class ComputerName


// =============================================================

} // end of namespace Crossplatform.NET.Chapter03

//*********** DISASSEMBLY COMPLETE ***********************
// WARNING: Created Win32 resource file MS.NET.res
