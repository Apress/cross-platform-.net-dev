//Filename: ViewFactory.cs
using System;

namespace Crossplatform.NET.Chapter09
{
    public class ViewFactory
    {
        public static View CreateView(Person model, Controller controller)
        {             
#if GTK
             return new GtkView(model, controller);
#elif SWF
             return new WindowsView(model, controller);
#else
             return new ConsoleView(model, controller);
#endif
        }
    }
}
