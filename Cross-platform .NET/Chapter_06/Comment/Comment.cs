//Filename: Comment.cs
using System;
using System.Data;
using Crossplatform.NET.Chapter6.Data;

namespace Crossplatform.NET.Chapter6
{  
    class Comment
    {
        private const string errorMsg = "error '{0}' was raised from '{1}'";

        static void Main(string[] args)
        {
            try
            {
                if(args.Length == 1)
                {
                        MessageStore store = new MessageStore();
                        store.SaveMessage(Environment.UserName + "@" + Environment.MachineName, args[0]);
                }
                else if(args.Length == 0)
                {
                    MessageStore store = new MessageStore();
                    DataSet messages = store.GetMessages();

                    foreach(DataRow record in messages.Tables[0].Rows){
                        string line = String.Format("On {0} {1} said '{2}'",
                                                    ((DateTime)record["LoggedDate"]).ToString(),
                                                    record["Name"],
                                                    record["Comments"]);
                        Console.WriteLine(line);
                    }
                }
                else
                {
                    Console.Error.WriteLine("Usage: Comment.exe comments");
                }
            } 
            catch (Exception e) {
                Console.Error.WriteLine(errorMsg, e.Message, e.Source);
            }                  
        }
    }
}
