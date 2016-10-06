using System;
using Microsoft.Win32;

namespace Crossplatform.NET.Chapter3
{
    class LastRun 
    {
        //Let's not embed strings in the program's guts.
    	private const string Key = "Software\\Crossplatform.NET";
    	private const string userNameValue = "userName";
        private const string lastRunValue = "lastRun";

        //We should ideally place this in a resource file
        private const string errorMessage = "The following error '{0}' was raised from '{1}'";

        static void Main(string[] args)
        {
            try 
            {
                //Leave the real work to be done somewhere else
                DisplayUsageDetails();
            } 
            catch (Exception e)
            {
		    Console.Error.WriteLine(errorMessage, e.Message, e.StackTrace);
            }		
			
        }

        private static void DisplayUsageDetails()
        {	
            RegistryKey rk = Registry.LocalMachine.OpenSubKey(Key, true);
        
            //Try and create the key if it doesn't exist
            if (rk == null)
            {
            	rk = Registry.LocalMachine.CreateSubKey(Key);
            }
            
            
            //Retrieve data from registry
            string userName = (string)rk.GetValue(userNameValue);
            string lastRun = (string)rk.GetValue(lastRunValue);

            Console.WriteLine("Last run by user '{0}' at '{1}'", userName, lastRun);
            
            //Update data in Registry
            rk.SetValue(userNameValue, Environment.UserName);
            rk.SetValue(lastRunValue, DateTime.Now);
        }        
        
    }

}