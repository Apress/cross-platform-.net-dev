// Filename: Controller.cs
using System;

namespace Crossplatform.NET.Chapter09
{
    public class Controller
    {
        private View view;
        private Person person;

        public Controller(Person person)
        {
            this.person = person;
        }

        public View View
        {
            get{ return this.view; }
            set{ this.view = value; }
        }

        public void ChangeFirstname(string newFirstname)
        {
            this.person.Firstname = newFirstname;
        }

        public void ChangeSurname(string newSurname)
        {
            this.person.Surname = newSurname;
        }

        public void ChangeEmail(string newEmail)
        {
            this.person.Email = newEmail;
        }

        public void ChangeSSN(string newSSN)
        {
            this.person.SocialSecurityNumber = newSSN;
        }
    }
}



                