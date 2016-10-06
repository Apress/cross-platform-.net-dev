//Filename: SimpleMVC.cs
using System;

namespace Crossplatform.NET.Chapter09
{
    public class SimpleMVC
    {
        public static void Main()
        {
            //Set up all objects, and their relationships
            Person timmy = new Person("Timothy", 
                                      "Ring",
                                      "000-111",                                     
                                      "Timothy.Ring@Crossplatform.net");

            Controller controlTim = new Controller(timmy);
      
            //Choosing the correct view can be done in a number of ways
            //as discussed throughout the book, notably chapters 4 and 7
            View watchTim = new ConsoleView(timmy, controlTim);
            //View watchTim = new WindowsView(timmy, controlTim);
            //View watchTim = new GtkView(timmy, controlTim);
            
            controlTim.View = watchTim;
            ChangeDetails(controlTim);
        }


        private static void ChangeDetails(Controller controller)
        {
            controller.ChangeFirstname("Tim");
            controller.ChangeSurname("King");
            controller.ChangeEmail("Tim.King@crossplatform.net");
        }
    }
}



                
