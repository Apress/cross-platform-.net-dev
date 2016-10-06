//UptimeClient.cs

using System;
using System.IO;
using System.Runtime.Remoting.Channels;
using Remoting.Corba.Channels.Iiop;

namespace Crossplatform.NET.Chapter08
{   
    // interface for remote CORBA object
    interface Uptime
    {
        int GetSeconds();
    };
    
    class UptimeClient
    {
        static void Main(string[] args)
        {
            try
            {
                //Read the IOR from the file
                if (args.Length < 1)
                    throw new Exception("Please specify an IOR filename.");
                    
                string ior;
                using (StreamReader iorFile = new StreamReader(args[0]))
                {
                    ior = iorFile.ReadToEnd();
                }

                //Register IIOP channel with Remoting
                ChannelServices.RegisterChannel(new IiopClientChannel());

                //Create the remote proxy
                Uptime server = (Uptime)Activator.GetObject(typeof(Uptime), ior);
                
                //Invoke the method
		Console.WriteLine("The server has been running for {0} seconds.", server.GetSeconds().ToString());
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("The following error occurred: {0}", ex.Message);
            }
        }
    }
}
