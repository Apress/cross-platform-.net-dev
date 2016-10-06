//SingletonLogAbstraction.cs
using System;
using System.Configuration;
using System.Runtime.Remoting;

namespace Crossplatform.NET.Chapter04
{
    public class SingletonLogAbstraction
    {
        private static readonly SingletonLogAbstraction instance;
  	
	private static string typeName;

        private BaseLogImplementation recorder;

	static SingletonLogAbstraction()
	{
	    AppSettingsReader appSettings = new AppSettingsReader();
            string key = "LogImplementation";
            typeName = (string)appSettings.GetValue(key, typeof(string));
            
            instance = new LogAbstraction3();
	}

        //Ensure no external classes can create instances
        private SingletonLogAbstraction()
        {
            object obj = Activator.CreateInstance(Type.GetType(typeName));
            this.recorder = (BaseLogImplementation)obj;	 
        }

        //Provide access to the single instance
        public static LogAbstraction3 Instance
        {
            get
            {
                return instance;
            }
        }

        //Forward calls to the implementation
        public void Record(string message)
        {
            this.recorder.Record(message);
        }
    }
}
