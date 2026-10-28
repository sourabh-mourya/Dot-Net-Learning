using System;

namespace BoxingNamespace
{
    public class BoxingClass
    {
        public static void Main(string[] args)
        {
            // Boxing (implicit)
            int a = 10;
            object o = a;
            Console.WriteLine($"Boxed value: {o}");

            // Unboxing (explicit)
            int b = (int)o;
            Console.WriteLine($"Unboxed value: {b}");

            // Boxing se alag copy bani: a badalne se o par asar nahi
            a = 99;
            Console.WriteLine($"a = {a}, o = {o}");   // a = 99, o = 10

            // Multiple types ke liye object array
            object[] data = { 10, 'A', 5.5, 2.5f, "Hello", true };

            foreach (object item in data)
            {
                Console.WriteLine($"{item}  ->  {item.GetType().Name}");
            }

            // Galat unboxing (error dikhane ke liye)
            try
            {
                object x = 10;
                double d = (double)x;      // int box tha, double nahi
            }
            catch (InvalidCastException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}