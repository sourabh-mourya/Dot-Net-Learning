using System;

class Program
{
    static void Main()
    {
        try
        {
            int[] arr = { 10, 20, 30 };

            Console.WriteLine(arr[3]); // Invalid index
        }
        catch (IndexOutOfRangeException ex)
        {
            Console.WriteLine("Error: Index is out of range.");
            Console.WriteLine(ex.Message);
        }

        Console.WriteLine("Program continues...");
    }
}