//Filename: MainWindow.cs
using System;
using Gtk;

namespace Crossplaform.NET.Chapter05
{
    public class MainWindow : Gtk.Window
    {
        private Gtk.VBox buttonBar;
        private Gtk.Button closeButton;

        public MainWindow(string caption) :base(caption)
        {
            Initialize();
        }

        private void Initialize()
        {
            SetDefaultSize(350, 200);

            //Create the button
            this.closeButton = new Gtk.Button("Close");
            this.closeButton.Clicked += new EventHandler(closeButton_Click);

            //Create a container and add the button to it
            this.buttonBar = new Gtk.VBox(false, 5);
            this.buttonBar.PackEnd(this.closeButton, false, false, 5);

            //Add our single top level container to the form
            Add(this.buttonBar);
        }

        private void closeButton_Click(object sender, EventArgs args)
        {
            Destroy();
        }
    }
}
