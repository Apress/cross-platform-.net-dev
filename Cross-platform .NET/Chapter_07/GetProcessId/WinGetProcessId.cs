//WinGetProcessId.cs
using System;
using System.Runtime.InteropServices;

namespace Crossplatform.NET.Chapter07
{
    public class WinGetProcessId
    {
	[DllImport("kernel32", EntryPoint="GetCurrentProcessId")]
	private static extern int getProcessId();

        //Kick things off...
	public static void Main()
	{
	    Console.WriteLine("Process ID: {0}", getProcessId());
	}
    }
}