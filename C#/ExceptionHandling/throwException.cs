using System;

class Program
{
    static void CheckAge(int age)
    {
        if (age < 18)
        {
            throw new ArgumentOutOfRangeException(
                nameof(age),
                "Age must be 18 or above."
            );
        }

        Console.WriteLine("Access granted.");
    }

    static void Main()
    {
        try
        {
            CheckAge(16);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine("Validation error: " + ex.Message);
        }

        Console.WriteLine("Program continues...");
    }
}

