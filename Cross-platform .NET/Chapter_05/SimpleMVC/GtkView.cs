//Filename: GtkView.cs
using System;
using Gtk;

namespace Crossplatform.NET.Chapter05
{
    public class GtkView : View
    {
        private Window dummyParent;
        
        public GtkView(Person person, Controller controller) 
                       : base(person, controller)
        {
            Application.Init();
            this.dummyParent = new Gtk.Window("Dummy");
        }

        protected override void UpdateDisplay()
        {
            GtkViewWindow form = new GtkViewWindow ("Person changed:",
                                                    this.dummyParent,
                                                    DialogFlags.DestroyWithParent,
                                                    this.person);
            form.Run();
            form.Destroy();
        }

        private class GtkViewWindow : Dialog
        {
            private Person person;

            public GtkViewWindow (string title, Window parentWindow, 
                                  DialogFlags flags, Person person)
            : base(title, parentWindow, flags)
            {
                this.person = person;
                
                Label nameLabel = new Label(this.person.Firstname + " " +
                                            this.person.Surname);
                Label ssnLabel = new Label(this.person.SocialSecurityNumber);
                Label emailLabel = new Label(this.person.Email);
                this.VBox.PackStart(nameLabel, true, true, 0);
                this.VBox.PackStart(ssnLabel, true, true, 0);
                this.VBox.PackStart(emailLabel, true, true, 0);
                this.VBox.ShowAll();
                AddButton("OK", 0);
            }
        }
    }
}



