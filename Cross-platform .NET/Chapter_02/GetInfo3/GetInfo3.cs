//Filename: GetInfo3.cs
using System;
using Crossplatform.NET.Chapter02.Library;

namespace Crossplatform.NET.Chapter02
{
    class GetInfo3
    {
        static void Main()
        {
            GetInfo3 info = new GetInfo3();
        }

        public GetInfo3 ()
        {    
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Code in Main file.");
            Console.WriteLine("Operating system: " +
                              Environment.OSVersion.Platform.ToString());
            Console.WriteLine("OS Version: " +
                              Environment.OSVersion.Version.ToString());
            Console.WriteLine("Today's date is:" +
                              DateTime.Today.ToString());
            this.callOthers();
        }

        private void callOthers() // new method
        {
            TimeInfo time = new TimeInfo();
            DateStringLibrary.GetTestString();
        }
    }
}
