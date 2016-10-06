using System;
using System.IO;
using System.Xml.Serialization;

namespace Crossplatform.NET.Chapter3 {

    //Provide a structure for storing data
    public struct LastRunData{
        public string userName;
        public DateTime lastRun;
    }
    
    class LastRun3 {

    //Let's not embed strings in the program's guts.
    private const string fileName = "lastRun3.xml";
    private const string errorMessage = "error '{0}' was raised from '{1}'";

    static void Main(string[] args) {
        try {
                //Leave the real work to be done somewhere else
                DisplayUsageDetails();
            } 
            catch (Exception e) {
                Console.Error.WriteLine(errorMessage, e.Message, e.Source);
            }        
            
        }

        private static void DisplayUsageDetails()
        {    
            
            LastRunData data;
            XmlSerializer serializer = new XmlSerializer(typeof(LastRunData));

            //Deserialize the data            
            if (File.Exists(fileName))
            {
                using(FileStream fs = new FileStream(fileName, FileMode.Open))
                {
                    data = (LastRunData)serializer.Deserialize(fs);
                }
            }
            else
            {
                data = new LastRunData();
            }

            //Display the output 
            Console.WriteLine("Last run by user '{0}' at '{1}'", data.userName, data.lastRun);
            
            //Update the data
            data.userName = Environment.UserName;
            data.lastRun = DateTime.Now;

            //Serialize the data
            using(StreamWriter writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, data);
            }
        }        
    }
}