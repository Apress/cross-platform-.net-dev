//Filename: RemoteObjects.cs
using System;
using System.IO;

namespace Crossplatform.NET.Chapter08
{   
    [Serializable]
    public class HostDetails
    {
        private string userName;
        private string operatingSystem;
        private DateTime timeStamp;
        
        //Provide a default constructor
        public HostDetails()
        {
            this.userName = Environment.UserName + "@" + Environment.MachineName;
            this.operatingSystem = Environment.OSVersion.Platform.ToString();
            this.timeStamp = DateTime.Now;
        }

        public string UserName
        {
            get { return this.userName; }
        }
        
        public string OperatingSystem
        {
            get { return this.operatingSystem; }
        }
        
        public DateTime TimeStamp
        {
	    get { return this.timeStamp; }
	}
	
	public static void LogAccess(HostDetails clientDetails, TextWriter logWriter)
        {
            //Bow to simplicity and log to the console...
            logWriter.WriteLine("Connection at {0} to user {1} (running {2})",
                                clientDetails.TimeStamp.ToString(),
                                clientDetails.UserName,
                                clientDetails.OperatingSystem);
        }
    }
    
    public class RemoteLog : MarshalByRefObject
    {   
        //Provide a default constructor
        public RemoteLog(){}

        public HostDetails SwapDetails(HostDetails clientDetails)
        {
            HostDetails.LogAccess(clientDetails, System.Console.Out);
            return new HostDetails();
        }
        
    }
}



