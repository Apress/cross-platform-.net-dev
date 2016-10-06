//Filename: WebComment.cs
using System;
using System.Data;

namespace Crossplatform.NET.Chapter6
{  
    class WebComment
    {
        private const string errorMsg = "error '{0}' was raised from '{1}'";

        static void Main(string[] args)
        {
            try
            {
                WebMessageStoreProxy store = new WebMessageStoreProxy();
                DataSet messages = store.GetMessages();

                foreach(DataRow record in messages.Tables[0].Rows){
                    string line = String.Format("On {0} {1} said '{2}'",
                                                ((DateTime)record["LoggedDate"]).ToString(),
                                                record["Name"],
                                                record["Comments"]);
                    Console.WriteLine(line);
                }
            } 
            catch (Exception e) {
                Console.Error.WriteLine(errorMsg, e.Message, e.Source);
            }                  
        }
    }
}
