//WindowsLogImplementation.cs
using System;
using System.Diagnostics;

namespace Crossplatform.NET.Chapter04
{
    public class WindowsLogImplementation: BaseLogImplementation
    {
        //Could create "sources" here
        public WindowsLogImplementation() {}
            
        public override void Record(string message)
        {
            EventLog.WriteEntry("WindowsLogImplementation", message);
        }
    }
}
