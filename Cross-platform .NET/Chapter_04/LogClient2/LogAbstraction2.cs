//LogAbstraction2.cs
using System;

namespace Crossplatform.NET.Chapter04
{
    public class LogAbstraction2
    {
        private BaseLogImplementation recorder;

       //Use the file implementation by default
        public LogAbstraction2(): this(LogType.File){}

        public LogAbstraction2(LogType logType)
        {
            switch (logType)
            {
                case (LogType.Windows):
                    this.recorder = new WindowsLogImplementation();
                    break;
               default:
                    this.recorder = new FileLogImplementation();
                    break;
            }
        }


        //Forward calls to the implementation
        public void Record(string message)
        {
            this.recorder.Record(message);
        }
    }
}
