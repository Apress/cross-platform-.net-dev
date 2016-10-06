//WinEntryPoints.cs
using System;
using System.Runtime.InteropServices;

namespace Crossplatform.NET.Chapter7
{
    class WinEntryPoints
    {
        //Implicit ANSI call
        [DllImport("user32.dll")]
        public static extern int MessageBox(IntPtr handle, string message, string caption, int type);

        //Explicit Unicode call
        //[DllImport("user32.dll", CharSet = CharSet.Unicode)]
        //public static extern int MessageBox(IntPtr handle, string message, string caption, int type);
        
        //The string mangling Unicode version
        //[DllImport("user32.dll", EntryPoint = "MessageBoxW")]
        //public static extern int MessageBox(IntPtr handle, string message, string caption, int type);

        public static int Main() 
        {
            return MessageBox(IntPtr.Zero, "Hello World!", "A Win32 Message Box", 0);
        }
    }
}