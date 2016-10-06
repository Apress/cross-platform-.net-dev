//LogClient2.cs
using System;

namespace Crossplatform.NET.Chapter04
{
    public class LogClient2
    {
        static void Main(string[] args)
        {
            LogAbstraction2 recorder;
        		
            Console.WriteLine("Enter '1' to use the WindowsLogImplementation");
            Console.WriteLine("Enter '2' to use the default implementation.");
            
            if ((char)Console.Read() == '1')
            	recorder = new LogAbstraction2(LogType.Windows);
            else
            	recorder = new LogAbstraction2();
            	
	    recorder.Record(string.Join(" ", args));
        }
    }
}
