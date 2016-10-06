//UptimeServer.cs
using System;
using System.IO;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using Remoting.Corba;
using Remoting.Corba.Channels.Iiop;

namespace Crossplatform.NET.Chapter08
{
    [CorbaTypeId("IDL:Uptime:1.0")]
    public abstract class Uptime: CorbaObject
    {
        public abstract int GetSeconds();
        
        //
        public override bool is_a(string repositoryId)
        {
            return (repositoryId == CorbaServices.GetRepositoryId(typeof(Uptime)));
        }
    }

    // Implementation
    public class UptimeImpl: Uptime
    {
	private readonly DateTime startTime = DateTime.Now;

        public override int GetSeconds()
        {
            TimeSpan time = DateTime.Now.Subtract(startTime);
            return (int)time.TotalSeconds;
        }
    }

   // Application main
    class UptimeServer
    {
        static void Main(string[] args)
        {
            IiopServerChannel channel = null;
            try
            {
                //Register the channel
                channel = new IiopServerChannel(0);
                ChannelServices.RegisterChannel(channel);

                //Ensure object is instantiated immediately
                UptimeImpl uptime = new UptimeImpl();
                RemotingServices.Marshal(uptime, "Uptime");

                //Determine the IOR
                string ior = channel.GetUrlsForUri("Uptime")[0];
                Console.WriteLine(ior);

                //Write the IOR to a file for use by clients
                if ((args.Length == 1))
                {
                    using (StreamWriter file = new StreamWriter(args[0]))
                    {
                        file.Write(ior);
                    }
                }
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("Error creating IOR: {0} at {1}", e.Message, e.Source);
            }                  
            finally
            {
                //Keep serving until told otherwise
                Console.WriteLine("Press return to exit");
                Console.ReadLine();
                ChannelServices.UnregisterChannel(channel);
            }
        }
    }
}
