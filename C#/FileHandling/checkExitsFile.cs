using System;
using System.IO;

class Program
{
    static void Main()
    {
        string path = "data.txt";

        if (File.Exists(path))
        {
            Console.WriteLine("File exists.");

        }
        else
        {
            Console.WriteLine("File does not exist.");

        }
    }
}

