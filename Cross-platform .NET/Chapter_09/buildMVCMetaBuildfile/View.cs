//Filename: View.cs
using System;

namespace Crossplatform.NET.Chapter09
{
    public abstract class View
    {
        protected Person person;
        protected Controller controller;
        
        private View(){}
        
        public View(Person person, Controller controller)
        {
            this.person = person;
            this.controller = controller;
            
            this.person.PersonChangedEvent += new EventHandler(Person_Changed);
        }

        protected virtual void Person_Changed(object sender, EventArgs e)
        {
             UpdateDisplay(); 
        }

        protected abstract void UpdateDisplay();
    }
}

