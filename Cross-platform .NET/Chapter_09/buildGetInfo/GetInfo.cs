//Filename GetInfo.cs
using System;

namespace Crossplatform.NET.Chapter09
{
    public class GetInfo
    {
        static void Main()
        {
            GetInfo info = new GetInfo();
        }

        public GetInfo()
        {    
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Code in Main file.");
            Console.WriteLine("Operating system: " +                   
                              Environment.OSVersion.Platform.ToString());
            Console.WriteLine("OS Version: " +
                              Environment.OSVersion.Version.ToString());
            Console.WriteLine("Todays date is: " + 
                                DateTime.Today.ToString());
        }
    }
}
