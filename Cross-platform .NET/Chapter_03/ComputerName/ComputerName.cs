using System;
using System.IO;
using System.Net;

namespace Crossplatform.NET.Chapter03
{
    class GetComputerName
    {
        public static void Main()
        {
            try
            {
            	//Could have used System.Environment.MachineName
                String hostName = Dns.GetHostName();
                Console.WriteLine(hostName);
            }
            catch(Exception e)
            {
                //Let's not clutter the output stream with errors
                using (TextWriter errorWriter = Console.Error) 
                {
                    errorWriter.WriteLine("The following error '{0}' was raised from '{1}'", e.Message, e.Source);
                }
	   }
        }
        
   }
}