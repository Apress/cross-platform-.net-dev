//LogAbstraction.cs
using System;
using System.IO;

namespace Crossplatform.NET.Chapter04
{
    public class LogAbstraction
    {
        private BaseLogImplementation recorder;

        
        public LogAbstraction()
        {
            if (IsWindows())
                this.recorder= new WindowsLogImplementation();
            else
                this.recorder = new FileLogImplementation();
        }

        //Are we running on Windows?
        private bool IsWindows()
        {
            return (Path.DirectorySeparatorChar == '\\');
	}

        //Forward calls to the implementation
        public void Record(string message)
        {
            this.recorder.Record(message);
        }
    }
}
