//LogClient3.cs
using System;

namespace Crossplatform.NET.Chapter04
{
    public class LogClient3
    {
        static void Main(string[] args)
        {
            LogAbstraction3 recorder = new LogAbstraction3();
            recorder.Record(string.Join(" ", args));
        }
    }
}
