//Filename: SimpleServer.cs
using System;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;

namespace Crossplatform.NET.Chapter08
{   
    public class SimpleServer
    {
        public static void Main()
        {        	
            //Register a server channel
            ChannelServices.RegisterChannel(new TcpChannel(30303));
            
            //Register the well known object
            Type remoteType = typeof(RemoteLog);           
            RemotingConfiguration.RegisterWellKnownServiceType (remoteType, "chapter9", WellKnownObjectMode.SingleCall);
            
            //Keep serving until told otherwise
            Console.WriteLine("Press return to exit");
            Console.ReadLine();
        }
    }
}



