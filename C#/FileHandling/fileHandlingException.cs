using System;
using System.IO;

class Program
{
    static void Main()
    {
        string path = "data.txt";

        try
        {
            string data = File.ReadAllText(path);
            Console.WriteLine(data);
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found.");
        }
        catch (IOException ex)
        {
            Console.WriteLine("File error: " + ex.Message);
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Permission denied.");
        }
    }
}