//Filename: TickleHello.cs
using System;
using TickleSharp;

namespace Crossplatform.NET.Chapter05
{  
    public class TickleThis
    {   
        public static void Main()
        {
            Tk tkInterpreter = new Tk();

            //Pass the script to the interpreter...
            tkInterpreter.Eval("wm title . {Tcl/Tk says Tickle This!}");
            tkInterpreter.Eval("button .b -text {Tickle this!} -command {exit} -padx 20");           	 
            tkInterpreter.Eval("pack .b");
         
            //...and run the script
            tkInterpreter.Run();
        }
    }
}
