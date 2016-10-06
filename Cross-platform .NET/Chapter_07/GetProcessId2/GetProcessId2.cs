//GetProcessId2.cs
using System;
using System.Runtime.InteropServices;

namespace Crossplatform.NET.Chapter7
{
    public class GetProcessId
    {

#if WINDOWS
	[DllImport("msvcrt.dll", EntryPoint="_getpid")]
	private static extern int getProcessId();
#else
        [DllImport("libc", EntryPoint="_getpid")]
	private static extern int getProcessId();
#endif

        //Kick things off...
	public static void Main()
	{
	    Console.WriteLine("Process ID: {0}", getProcessId());
	}
    }
}