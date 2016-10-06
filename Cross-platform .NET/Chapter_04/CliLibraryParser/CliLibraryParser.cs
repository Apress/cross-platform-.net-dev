//CliLibraryParser.cs
using System;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Reflection;

namespace Crossplatform.NET.Chapter04
{
    class CliLibraryParser
    {
        static void Main(string[] args)
        {            
            //Check for a filepath
            if (args.Length != 1)
            {
                Console.WriteLine("Please specify the XML file to parse.");
                return;
            } 

            //Rationalise the filepath
            string filePath;
            if(Path.IsPathRooted (args[0]))
                filePath = args[0];
            else
                filePath = Path.Combine(Directory.GetCurrentDirectory(), args[0]);

            //Lets cut to the chase!
            try
            {
                GenerateFiles(filePath);
            } 
            catch (Exception ex)
            {
                Console.Error.WriteLine("An error occurred: {0}.", ex.Message);
            }
        }
    
        //Generates files for the specifed CLI XML document
        static private void GenerateFiles(string filePath)
        {
            //Load the ECMA CLI document
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(filePath);

            //Sort the document so we can split the sections into a file per library
            XPathNavigator navigator = xmlDoc.CreateNavigator();
            XPathExpression expression = navigator.Compile("//Types");
            expression.AddSort("attribute::Library ", XmlSortOrder.Ascending, XmlCaseOrder.None, String.Empty, XmlDataType.Text);
                
            XPathNodeIterator nodeIterator = navigator.Select(expression);
            while (nodeIterator.MoveNext())
            {
                string target;
                target = Path.Combine(Directory.GetCurrentDirectory(),
                         nodeIterator.Current.GetAttribute("Library", String.Empty) + ".txt");

                using (StreamWriter stream = File.CreateText(target))
                {
                    GenerateFileContents(nodeIterator.Current, stream);
                }
            }
        }

        //Generate a file
        static private void GenerateFileContents(XPathNavigator navigator, StreamWriter stream)
        {
            //Sort the nodes to create an ordered file
            XPathExpression expression = navigator.Compile("child::Type");
            expression.AddSort("attribute::FullName ", XmlSortOrder.Ascending, XmlCaseOrder.None, String.Empty, XmlDataType.Text);
            XPathNodeIterator nodeIterator = navigator.Select(expression);
        
            while (nodeIterator.MoveNext())
                stream.WriteLine(nodeIterator.Current.GetAttribute("FullName", String.Empty));
        }
    }
}
