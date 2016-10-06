//Filename: HelloGtk.cs
using System;
using Gtk;

namespace Crossplaform.NET.Chapter05
{
    class HelloGtk
    {
        private static Gtk.Window mainWindow;
        
        public static void Main()
        {
            Console.WriteLine("Starting Gtk engine...");
            
            Gtk.Application.Init();
            mainWindow = new MainWindow("Hello from Gtk");
                       
            //Setup event handling
            mainWindow.Destroyed += new EventHandler(MainWindow_Destroyed);
            
            mainWindow.ShowAll();
            Gtk.Application.Run();
            
            Console.WriteLine("Codeflow back in HelloGtk.Main()");
        }
        
        public static void MainWindow_Destroyed(object sender, EventArgs e)
        {
            Console.WriteLine("Quitting Gtk engine...");
            Gtk.Application.Quit();
        }
     }
}
