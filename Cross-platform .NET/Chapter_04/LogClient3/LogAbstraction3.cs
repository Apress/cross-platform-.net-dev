//LogAbstraction3.cs
using System;
using System.Configuration;
using System.Runtime.Remoting;

namespace Crossplatform.NET.Chapter04
{
    public class LogAbstraction3
    {
	private static string typeName;

        private BaseLogImplementation recorder;

	static LogAbstraction3()
	{
	    AppSettingsReader appSettings = new AppSettingsReader();
            string key = "LogImplementation";
            typeName = (string)appSettings.GetValue(key, typeof(string));
	}

        public LogAbstraction3()
        {
            object obj = Activator.CreateInstance(Type.GetType(typeName));
            this.recorder = (BaseLogImplementation)obj;	 
        }


        //Forward calls to the implementation
        public void Record(string message)
        {
            this.recorder.Record(message);
        }
    }
}
