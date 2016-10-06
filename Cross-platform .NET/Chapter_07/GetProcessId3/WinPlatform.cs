//WinPlatform.cs
using System;
using System.Runtime.InteropServices;

namespace Crossplatform.NET.Chapter7
{
    public class WinPlatform : Platform
    {
        [DllImport("kernel32", EntryPoint="GetCurrentProcessId")]
	private extern int GetCurrentProcessId();
               
        public override int GetProcessId()
        {
               return GetCurrentProcessId(); 
        }
    }
}