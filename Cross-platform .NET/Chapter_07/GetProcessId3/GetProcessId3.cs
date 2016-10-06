//GetProcessId3.cs
using System;

namespace Crossplatform.NET.Chapter07
{       
    public class GetProcessId3
    {
        //Kick things off...
	public static void Main()
	{
	    Console.WriteLine("Process ID: {0}", Platform.CreatePlatform().GetProcessId());
	}
    }
}