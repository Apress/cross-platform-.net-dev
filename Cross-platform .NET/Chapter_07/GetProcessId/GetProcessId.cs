//GetProcessId.cs
using System;
using System.Runtime.InteropServices;

namespace Crossplatform.NET.Chapter7
{
    public class GetProcessId
    {
	[DllImport("libc", EntryPoint="getpid")]
	private static extern int getProcessId();

        //Kick things off...
	public static void Main()
	{
	    Console.WriteLine("Process ID: {0}", getProcessId());
	}
    }
}