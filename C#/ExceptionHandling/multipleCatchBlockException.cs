using System;

class Program
{
    static void Main()
    {
        int[] numbers = { 10, 20, 30 };

        try
        {
            Console.Write("Enter an index: ");

            string? input = Console.ReadLine();

            int index = int.Parse(input!);

            Console.WriteLine(numbers[index]);
        }
        //Ab catch block exception ke according chlega jiss type ka error hoga usski type catch block chlega

        catch (FormatException)
        {
            Console.WriteLine("Please enter an integer.");
        }
        catch (IndexOutOfRangeException)
        {
            Console.WriteLine("Index is outside the valid range.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Unexpected error: " + ex.Message);
        }

        Console.WriteLine("Program ends.");
    }
}

//Case 1: Input abc
// Enter an index: abc
// Please enter an integer.
// Program ends.


// Case 2: Input 5
// Enter an index: 5
// Index is outside the valid range.
// Program ends.