using System;
using System.IO;


namespace ReadFile
{

    public class program
    {
        public static void Main(string[] args)
        {

            string path = "data.txt";

            string data = File.ReadAllText(path);
            Console.WriteLine("File data :- " + data);

            Console.WriteLine("Ending the file...");
        }
    }
}