//Filename: GetInfo2.cs
using System;
using Crossplatform.NET.Chapter02.Library;

namespace Crossplatform.NET.Chapter02
{
    class GetInfo2
    {
        static void Main()
        {
            GetInfo info = new GetInfo2();
        }

        public GetInfo2()
        {    
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Code in Main file.");
            Console.WriteLine("Operating system: " +
                              Environment.OSVersion.Platform.ToString());
            Console.WriteLine("OS Version: " +
                              Environment.OSVersion.Version.ToString());
            Console.WriteLine("Today's date is: " +
                              DateTime.Today.ToString());
            Console.WriteLine("About to call other methods...");
            callOthers(); 
        }

        private void callOthers() // new method
        {
            TimeInfo time = new TimeInfo();
        }

    }
}