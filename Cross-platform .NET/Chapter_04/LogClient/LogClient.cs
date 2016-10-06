//LogClient.cs
using System;

namespace Crossplatform.NET.Chapter04
{
    public class LogClient
    {
        static void Main(string[] args)
        {
            LogAbstraction recorder = new LogAbstraction();
            recorder.Record(string.Join(" ", args));
        }
    }
}
