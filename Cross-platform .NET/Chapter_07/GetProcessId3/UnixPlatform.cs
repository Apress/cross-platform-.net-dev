//UnixPlatform.cs
using System;
using System.Runtime.InteropServices;

namespace Crossplatform.NET.Chapter7
{
    public class UnixPlatform : Platform
    {
        [DllImport("libc", EntryPoint="getpid")]
	private static extern int getpid();
        
        public UnixPlatform(){}
        
        public override int GetProcessId()
        {
               return getpid(); 
        }
    }
}