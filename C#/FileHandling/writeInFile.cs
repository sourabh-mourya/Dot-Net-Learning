using System;
using System.IO;
using System.Xml.Serialization;


namespace WriteFile
{

    public class program
    {

        public static void Main(String[] args)
        {
            string path = "data.txt";

            File.WriteAllText(path, "Hello world , These is is first file creation in c#");
            Console.WriteLine("File written successfully..........");
        }
    }
}