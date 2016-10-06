//Filename: GetInfo.cs
using System;

namespace Crossplatform.NET.Chapter02
{
    class GetInfo
    {
        static void Main()
        {
            GetInfo info = new GetInfo();
        }

        public GetInfo ()
        {    
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Code in Main file.");
            Console.WriteLine("Operating system: " +                   
                              Environment.OSVersion.Platform.ToString());
            Console.WriteLine("OS Version: " +
                              Environment.OSVersion.Version.ToString());
            Console.WriteLine("Today's date is: " + 
                                DateTime.Today.ToString());
        }
    }
}

