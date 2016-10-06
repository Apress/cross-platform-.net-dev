.assembly extern mscorlib
{
  .ver 1:0:3300:0
}
.assembly extern System
{
  .ver 1:0:3300:0
}
.assembly 'ComputerName'
{
  .hash algorithm 0x00008004
  .ver  0:0:0:0
}
.namespace Crossplatform.NET.Chapter03
{
  .class private auto ansi beforefieldinit ComputerName
  	extends [mscorlib]System.Object
  {

    // method line 1
    .method public hidebysig  specialname  rtspecialname 
           instance default void .ctor()  cil managed 
    {
        // Method begins at RVA 0x20ec
	// Code size 7 (0x7)
	.maxstack 8
	IL_0000: ldarg.0 
	IL_0001: call instance void valuetype [corlib]System.Object::.ctor()
	IL_0006: ret 
    } // end of method ComputerName::instance default void .ctor() 

    // method line 2
    .method public static 
           default void Main()  cil managed 
    {
        // Method begins at RVA 0x20f4
	.entrypoint
	// Code size 13 (0xd)
	.maxstack 3
	.locals init (
		string	V_0)
	IL_0000: call string class [System]System.Net.Dns::GetHostName()
	IL_0005: stloc.0 
	IL_0006: ldloc.0 
	IL_0007: call void class [corlib]System.Console::WriteLine(string)
	IL_000c: ret 
    } // end of method ComputerName::default void Main() 

  } // end of type Crossplatform.NET.Chapter03.ComputerName
}

