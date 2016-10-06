//Filename: ModalWorld.cs
using System;
using Gtk;

namespace Crossplaform.NET.Chapter05
{
    class ModalWorld
    {
        private static Gtk.Window mainWindow;
        
        public static void Main()
        {
            Console.WriteLine("Starting Gtk engine...");
            
            Gtk.Application.Init();
            mainWindow = new MainWindow("It's a Modal World");
                       
            //Setup event handling
            mainWindow.Destroyed += new EventHandler(MainForm_Destroyed);
            
            mainWindow.ShowAll();
            Gtk.Application.Run();
            
            Console.WriteLine("Codeflow back in Main()");
        }
        
        public static void MainForm_Destroyed(object sender, EventArgs e)
        {
            Console.WriteLine("Quitting Gtk engine");
            Gtk.Application.Quit();
        }
     }
}
