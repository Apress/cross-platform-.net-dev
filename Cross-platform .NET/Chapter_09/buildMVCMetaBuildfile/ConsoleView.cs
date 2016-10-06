//Filename: ConsoleView.cs
using System;

namespace Crossplatform.NET.Chapter09
{
    public class ConsoleView : View
    {   	
        public ConsoleView(Person person, Controller controller) :base(person, controller){}

        protected override void UpdateDisplay()
        {
            Console.WriteLine("*** Person has been updated ***");
            Console.WriteLine("Name is {0} {1}", person.Firstname, person.Surname);
            Console.WriteLine("SSN is {0}", person.SocialSecurityNumber);
            Console.WriteLine("Email is {0}\n", person.Email);
        }
    }
}

