;*******************************************************************
;*
;*	 PEDasm generated source file : CompileMe.asm 
;*
;*


.586P
.model flat , stdcall



.radix 10 

include dllImport.inc




;*******************************************************************
;* section    .text
;*	V.A = 0x00402000 , init part size = 756 , total size = 4096
;*	flags :  code section ,  section executable ,  section readable 

.code

assume fs:nothing ; this requiered for SEH


extern	_imp___CorExeMain : dword
_CorExeMain equ _imp___CorExeMain

	dword 022D0h
	byte 4 dup(00h)  , 048h  , 00h  , 00h  , 00h  , 02h  , 00h  , 00h 
	byte 00h  , 07Ch  , 020h  , 00h  , 00h  , 024h  , 02h  , 00h 
	byte 00h  , 01h  , 00h  , 00h  , 00h  , 01h  , 00h  , 00h 
	byte 06h  , 48 dup(00h)  , 013h  , 030h  , 01h  , 00h  , 0Bh  , 7 dup(00h) 
	byte 072h  , 01h  , 00h  , 00h  , 070h  , 028h  , 02h  , 00h 
	byte 00h  , 0Ah  , 02Ah  , 00h  , 013h  , 030h  , 01h  , 00h 
	byte 07h  , 7 dup(00h)  , 02h  , 028h  , 03h  , 00h  , 00h  , 0Ah 
	byte 02Ah  , 00h 
	byte 'BSJB'
	byte 01h  , 00h  , 01h  , 5 dup(00h)  , 0Ch  , 00h  , 00h  , 00h 
	byte 'v1.1.4322'
	byte 5 dup(00h)  , 05h  , 00h  , 06Ch  , 00h  , 00h  , 00h  , 0D0h 
	byte 00h  , 00h  , 00h  , 023h  , 07Eh  , 00h  , 00h  , 03Ch 
	byte 01h  , 00h  , 00h  , 098h  , 00h  , 00h  , 00h 
	byte '#Strings'
	byte 4 dup(00h)  , 0D4h  , 01h  , 00h  , 00h  , 01Ch  , 00h  , 00h 
	byte 00h  , 023h  , 055h  , 053h  , 00h  , 0F0h  , 01h  , 00h 
	byte 00h  , 010h  , 00h  , 00h  , 00h 
	byte '#GUID'
	byte 4 dup(00h)  , 02h  , 00h  , 00h  , 024h  , 00h  , 00h  , 00h 
	byte '#Blob'
	byte 7 dup(00h)  , 01h  , 00h  , 00h  , 01h  , 047h  , 014h  , 00h 
	byte 00h  , 09h  , 4 dup(00h)  , 0FAh  , 01h  , 033h  , 00h  , 02h 
	byte 00h  , 00h  , 01h  , 00h  , 00h  , 00h  , 03h  , 00h 
	byte 00h  , 00h  , 02h  , 00h  , 00h  , 00h  , 02h  , 00h 
	byte 00h  , 00h  , 03h  , 00h  , 00h  , 00h  , 01h  , 00h 
	byte 00h  , 00h  , 01h  , 00h  , 00h  , 00h  , 01h  , 5 dup(00h) 
	byte 0Ah  , 00h  , 01h  , 5 dup(00h)  , 06h  , 00h  , 028h  , 00h 
	byte 021h  , 00h  , 06h  , 00h  , 072h  , 00h  , 05Fh  , 00h 
	byte 06h  , 00h  , 086h  , 00h  , 021h  , 5 dup(00h)  , 01h  , 5 dup(00h) 
	byte 01h  , 00h  , 01h  , 00h  , 01h  , 00h  , 010h  , 00h 
	byte 02Fh  , 00h  , 039h  , 00h  , 05h  , 00h  , 01h  , 00h 
	byte 01h  , 00h  , 050h  , 020h  , 4 dup(00h)  , 096h  , 00h  , 054h 
	byte 00h  , 0Ah  , 00h  , 01h  , 00h  , 068h  , 020h  , 4 dup(00h) 
	byte 086h  , 018h  , 059h  , 00h  , 0Eh  , 00h  , 01h  , 00h 
	byte 011h  , 00h  , 059h  , 00h  , 012h  , 00h  , 019h  , 00h 
	byte 08Eh  , 00h  , 018h  , 00h  , 09h  , 00h  , 059h  , 00h 
	byte 0Eh  , 00h  , 02Eh  , 00h  , 0Bh  , 00h  , 01Dh  , 00h 
	byte 04h  , 080h  , 16 dup(00h)  , 02Fh  , 00h  , 00h  , 00h  , 01h 
	byte 00h  , 00h  , 00h  , 088h  , 013h  , 6 dup(00h)  , 01h  , 00h 
	byte 018h  , 8 dup(00h) 
	byte '<Module>'
	byte 00h 
	byte 'CompileMe.exe'
	byte 00h 
	byte 'mscorlib'
	byte 00h 
	byte 'System'
	byte 00h 
	byte 'Object'
	byte 00h 
	byte 'CompileMe'
	byte 00h 
	byte 'CrossPlatform.Net.Chapter1'
	byte 00h 
	byte 'Main'
	byte 00h 
	byte '.ctor'
	byte 00h 
	byte 'System.Diagnostics'
	byte 00h 
	byte 'DebuggableAttribute'
	byte 00h 
	byte 'Console'
	byte 00h 
	byte 'WriteLine'
	byte 00h  , 00h  , 017h  , 043h  , 00h  , 06Fh  , 00h  , 06Dh 
	byte 00h  , 070h  , 00h  , 069h  , 00h  , 06Ch  , 00h  , 065h 
	byte 00h  , 020h  , 00h  , 04Dh  , 00h  , 065h  , 00h  , 021h 
	byte 5 dup(00h)  , 053h  , 019h  , 0Bh  , 058h  , 0FEh  , 036h  , 04Ch 
	byte 04Eh  , 0B2h  , 072h  , 08Eh  , 0F3h  , 0Ah  , 059h  , 08Ah 
	byte 026h  , 00h  , 08h  , 0B7h  , 07Ah  , 05Ch  , 056h  , 019h 
	byte 034h  , 0E0h  , 089h  , 03h  , 00h  , 00h  , 01h  , 03h 
	byte 020h  , 00h  , 01h  , 05h  , 020h  , 02h  , 01h  , 02h 
	byte 02h  , 04h  , 00h  , 01h  , 01h  , 0Eh  , 06h  , 01h 
	byte 00h  , 00h  , 01h  , 00h  , 00h  , 0C8h  , 022h  , 10 dup(00h) 
	byte 0DEh  , 022h  , 00h  , 00h  , 00h  , 020h  , 22 dup(00h)  , 0D0h 
	byte 022h  , 8 dup(00h) 
	byte '_CorExeMain'
	byte 00h 
	byte 'mscoree.dll'
	byte 5 dup(00h) 




;------------------------------------------------------------
;+

public main
main :: ; proc near
	jmp  dword ptr [ _CorExeMain ]

end
