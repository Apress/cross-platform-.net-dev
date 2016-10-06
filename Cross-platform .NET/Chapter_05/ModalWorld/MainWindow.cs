//Filename: MainWindow.cs
using System;
using Gtk;

namespace Crossplaform.NET.Chapter05
{
    public class MainWindow : Gtk.Window
    {
        private Gtk.VBox buttonBar;
        private Gtk.Button closeButton;
        private Gtk.Button modalButton;

        public MainWindow2(string caption) :base(caption)
        {
            Initialize();
        }

        private void Initialize()
        {
            SetDefaultSize(350, 200);

            //Create the buttons
            this.closeButton = new Gtk.Button("Close this Window");
            this.closeButton.Clicked += new EventHandler(closeButton_Click);

            this.modalButton = new Gtk.Button("Open a Modal Window");
            this.modalButton.Clicked += new EventHandler(modalButton_Click);

            //Create a container and add the button to it
            this.buttonBar = new Gtk.VBox(false, 5);
            this.buttonBar.PackEnd(this.closeButton, false, false, 5);
            this.buttonBar.PackEnd(this.modalButton, false, false, 5);

            //Add our top level container to the window
            Add(this.buttonBar);
        }

        private void closeButton_Click(object sender, EventArgs args)
        {
            Destroy();
        }
        
        private void modalButton_Click(object sender, EventArgs args)
        {
            Console.WriteLine("Launching a modal form...");
            ModalWindow modalWindow = new ModalWindow("A Modal Window", this);
            modalWindow.ShowAll();
            Console.WriteLine("Modal form launched.");
        }
    }
}
