//Filename: ModalWindow.cs
using System;
using Gtk;

namespace Crossplaform.NET.Chapter05
{
    class ModalWindow : MainWindow
    {
        private Gtk.Window parentWindow;

        public ModalWindow(string caption, Gtk.Window parentWindow) : base(caption)
        {
            this.parentWindow = parentWindow;
            Initialize();
        }

        private void Initialize()
        {
            //Ensure the window is modal
            this.Modal = true;
            this.TransientFor = this.parentWindow;
        }
    }
}
