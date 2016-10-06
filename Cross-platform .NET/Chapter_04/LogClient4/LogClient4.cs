//LogClient4.cs
using System;

namespace Crossplatform.NET.Chapter04
{
    public class LogClient4
    {
        static void Main(string[] args)
        {
            SingletonLogAbstraction.Instance.Record("Please buy:");
            SingletonLogAbstraction.Instance.Record("\'The Joy of Patterns\'");
            SingletonLogAbstraction.Instance.Record("Now featuring thread-safe singleton, self"
                                                    + "configured log recorder!");
        
        }
    }
}
