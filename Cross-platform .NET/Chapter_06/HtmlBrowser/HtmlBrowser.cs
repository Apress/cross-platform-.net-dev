//Filename: HtmlBrowser.cs
using System;
using System.Net;
using System.IO;

namespace Crossplatform.NET.Chapter6
{
    class HtmlBrowser
    {
        static void Main(string[] args)
        {   
            //ensure we have a URI
            if (args.Length != 1)
            {
                Console.Error.WriteLine("Please supply a URL for HtmlBrowser to browse.");
                return;
            }

            try
            {
                using(WebResponse response = WebRequest.Create(args[0]).GetResponse())
                {
                    //Pass the response stream to a stream reader and pump out the contents
                    using(StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        Console.WriteLine(reader.ReadToEnd());
                    }
                }                
            }
            catch(Exception e)
            {
                Console.Error.WriteLine(e.Message);
            }
        }        
    }
}