// Filename: Person.cs
using System;

namespace Crossplatform.NET.Chapter09
{
    public class Person
    {
        public event EventHandler PersonChangedEvent;
    	
        private string socialSecurityNumber;
        private string firstname;
        private string surname;
        private string email;

        //Ensure default constructor is not called
        private Person(){}

        public Person(string first, string surname, string ssn, string email)
        {
            this.firstname = first;
            this.surname = surname;
            this.socialSecurityNumber = ssn;
            this.email = email;
        }
       
        protected void RaisePersonChangedEvent(EventArgs e)
        {
            if (this.PersonChangedEvent != null)
                this.PersonChangedEvent(this, e);
        } 
       
        public string Email
        {
            get{ return email; }
            set{ SetValue(ref this.email, value); }
        }
        
        public string Firstname
        {
            get{ return firstname; }
            set{ SetValue(ref this.firstname, value); }
        }

        public string SocialSecurityNumber
        {
            get{return socialSecurityNumber;}
            set{ SetValue(ref this.socialSecurityNumber, value); }
        }

        public string Surname
        {
            get{ return surname; }
            set{ SetValue(ref this.surname, value); }
        }
        
        //Simply properties' set implementation
        private void SetValue(ref string field, string value)
        {
            if(field != value)
            {
                field = value;
                RaisePersonChangedEvent(EventArgs.Empty);
            }
        }

	public override bool Equals(object o)
	{
	    bool returnValue = false;
	    // cast our parameter to the correct type for comparison
	    Person p = (Person)o;
	    if(p.Firstname.ToUpper() == this.Firstname.ToUpper() &&
	        p.Surname.ToUpper() == this.Surname.ToUpper() &&
	        p.SocialSecurityNumber.ToUpper() == this.SocialSecurityNumber.ToUpper())
	    {
	            returnValue = true;
	    }
	    return returnValue;
	}

	public override int GetHashCode()
	{
	    return socialSecurityNumber.GetHashCode();
	}

    }
}
