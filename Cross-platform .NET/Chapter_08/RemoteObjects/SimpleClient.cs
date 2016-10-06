//Filename: SimpleClient.cs
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Crossplatform.NET.Chapter08
{   
    public class SimpleClient
    {
        public static void Main(string[] args)
        {
            //Use the given host address or fdefault to the loopback address
            string hostAddress = (args.Length == 1) ? args[0] : "127.0.0.1";
            string serverAddress = String.Format("tcp://{0}:30303/chapter9", hostAddress);
               
            //Register a client channel
            ChannelServices.RegisterChannel(new TcpChannel());
            
            try
            {
            	//Get a refernce to the object
            	RemoteLog log = (RemoteLog)RemotingServices.Connect(typeof(RemoteLog), serverAddress);
                
                //Make the call to the object and show the results
                HostDetails serverDetails = log.SwapDetails(new HostDetails());
                HostDetails.LogAccess(serverDetails, System.Console.Out);
            }
            catch(Exception ex)
            {
            	//Always be prepared...
                Console.Error.WriteLine("An error occurred: {0}", ex.Message);	
            }
        }
    }
}



