//Filename: LongShortDates.cs
using System;

namespace Crossplatform.NET.Chapter02.Library
{
    public class DateStringLibrary
    {
        public static void GetTestString()
        {
            Console.WriteLine("---------------------------------------");
            Console.WriteLine("Library code in DLL:");
            System.DateTime dt = new DateTime(2003,12,25);
            Console.WriteLine(dt.ToLongDateString());
            Console.WriteLine(dt.ToShortDateString());
        }
    }
}
