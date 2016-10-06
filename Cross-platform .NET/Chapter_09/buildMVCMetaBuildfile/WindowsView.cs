//Filename: WindowsView.cs
using System;
using System.Windows.Forms;

namespace Crossplatform.NET.Chapter09
{
    public class WindowsView : View
    {
        private WindowsViewForm form;

        public WindowsView(Person person, Controller controller) : base(person, controller)
        {
            this.form = new WindowsViewForm();
        } 

        protected override void UpdateDisplay()
        {
            this.form.firstnameLabel.Text = "First Name: " + this.person.Firstname;
            this.form.surnameLabel.Text = "Surname: " + this.person.Surname;
            this.form.socialSecurityLabel.Text = "SSN: " + this.person.SocialSecurityNumber;
            this.form.emailLabel.Text = "Email: " + this.person.Email;
            this.form.ShowDialog();
        }

        //A basic nested class to act as a modal form
        private class WindowsViewForm : Form
        {
            public Label firstnameLabel;
            public Label surnameLabel;
            public Label socialSecurityLabel;
            public Label emailLabel;
        
            public WindowsViewForm()
            {
            	//Create the labale controls
                this.firstnameLabel = new Label();
                this.surnameLabel = new Label();
                this.socialSecurityLabel = new Label();
                this.emailLabel = new Label();
                
                SuspendLayout();
 
                //Setup the label's properties
                this.firstnameLabel.AutoSize = true;
                this.firstnameLabel.Location = new System.Drawing.Point(40, 40);

                this.surnameLabel.AutoSize = true;
                this.surnameLabel.Location = new System.Drawing.Point(40, 80);

                this.socialSecurityLabel.AutoSize = true;
                this.socialSecurityLabel.Location = new System.Drawing.Point(40, 120);

                this.emailLabel.AutoSize = true;
                this.emailLabel.Location = new System.Drawing.Point(40, 160);

                //Set Form properties
                this.AutoScaleBaseSize = new System.Drawing.Size(6, 20);
                this.ClientSize = new System.Drawing.Size(292, 268);
                this.Controls.AddRange(
                    new System.Windows.Forms.Control[] {this.emailLabel,
                                                        this.socialSecurityLabel,
                                                        this.surnameLabel,
                                                        this.firstnameLabel});
                this.Text = "The MVC Form";
                
                ResumeLayout(false);
            }
        }
    }
}

