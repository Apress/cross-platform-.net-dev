//Soundex.java

//Import the .NET Phoneticator namespace
import cli.Phoneticator.*;

public class Soundex
{     	
    //Display the program's output
    private static void printResults(String name, String encoding)
    {
      System.out.println("The soundex encoding of " + name + " is " + encoding);
    }

    public static void main(String args[])       
    {  	
         //The name to encode should be the only argument
         if (args.length != 1)
         {
             System.out.println("Please pass in a surname to encode.");
         }
         else
         {
             String surname = args[0]; 
             printResults(surname, Phoneticator.Soundex(surname));
         }
    }     
}