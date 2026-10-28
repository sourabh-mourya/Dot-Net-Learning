using System;

class Program
{
    static void Main()
    {
        try
        {
            string? name = null;

            Console.WriteLine(name.Length); // Null reference error
        }
        catch (NullReferenceException ex)
        {
            Console.WriteLine("Error: Object is null.");
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Program continues...");
    }
}