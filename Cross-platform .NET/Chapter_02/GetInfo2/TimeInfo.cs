//Filename: TimeInfo.cs
using System;

namespace Crossplatform.NET.Chapter02
{
    class TimeInfo // code that resides in a different file
    {
        public TimeInfo()
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Code residing in a separate .cs file:");
            Console.WriteLine("The time now is:");
            Console.WriteLine(DateTime.Now.ToString());
        }
    }
}
