//FileLogImplementation.cs
using System;
using System.IO;

namespace Crossplatform.NET.Chapter04
{ 
    public class FileLogImplementation: BaseLogImplementation
    {
        //Should set up paths etc here
        public FileLogImplementation(){}
        
        public override void Record(string message)
        {
            using(TextWriter writer = File.AppendText("SimpleTextFile.txt"))
            {
            	writer.WriteLine(message);
            }
        }
    }
}
