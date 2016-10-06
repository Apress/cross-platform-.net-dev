// Input: ComputerName.exe
// Image type: EXE
// Native code present: No
// 32-bit only: No
// Length of IL data: 1536

.assembly extern mscorlib
{
	.ver 1:0:5000:0
	.publickeytoken = (B7 7A 5C 56 19 34 E0 89)   // .z\V.4..
}
.assembly extern System
{
	.ver 1:0:5000:0
	.publickeytoken = (B7 7A 5C 56 19 34 E0 89)   // .z\V.4..
}
.assembly ComputerName
{
	.hash algorithm 0x00008004
	.ver 0:0:0:0
}
.module ComputerName.exe
// MVID: {84E97AC1-A01E-D4E8-9268-7772BBC6C45C}
.namespace Crossplatform.NET.Chapter03
{
.class private auto ansi ComputerName extends [mscorlib]System.Object
{
	.method public static hidebysig void Main() cil managed 
	{
		// Start of method header: 2050
		.entrypoint
		.maxstack  1
		.locals    init (class System.String)
	?L205c:
		call       class System.String [System]System.Net.Dns::GetHostName()
		stloc.0
		ldloc.0
		call       void [mscorlib]System.Console::WriteLine(class System.String)
		ret
	}
	.method public hidebysig specialname rtspecialname instance void .ctor() cil managed 
	{
		// Start of method header: 206c
		.maxstack  8
	?L206d:
		ldarg.0
		call       instance void [mscorlib]System.Object::.ctor()
		ret
	}
}
}
