using System;

class Program
{
    static void Main()
    {
        try
        {
            int number = int.Parse("abc");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            Console.WriteLine("Stack :- {0}",ex.StackTrace);
        }

        Console.WriteLine("After the exception of statements");
    }
}