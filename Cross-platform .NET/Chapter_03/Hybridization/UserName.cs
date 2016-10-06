using System;
using System.IO;
using System.Reflection;
using Mono.GetOptions;

//Ensure the help switch works bvy default
[assembly: AssemblyTitle("username")]
[assembly: AssemblyDescription("Determines the logged in user.")]
[assembly: AssemblyCopyright("(C) 2003 M.J. Easton & Jason King")]
[assembly: Mono.UsageComplement("")]

namespace Crossplatform.NET.Chapter3
{

    class UsernameOptions: Options
    {	
        [Option("Display the username in uppercase", 'u')]
        public bool DisplayUppercase = false;
    }

    class Username
    {
        static void Main(string[] args)
        {
            //Process the application's commandline options
            UsernameOptions options = new UsernameOptions();
            options.ProcessArgs(args); 

            try
            {
                string name;
                if(options.DisplayUppercase)
                {
                    name = Environment.UserName.ToUpper();
                }
                else
                {
                    name = Environment.UserName.ToLower();
                }
                
                Console.WriteLine("{0}", name);			
            } 
            catch (Exception e)
            {
                using (TextWriter errorWriter = Console.Error) 
                {
                    errorWriter.WriteLine("The following error '{0}' was raised from '{1}'", e.Message, e.Source);
                }
            }		
        }
        
    }
    
}