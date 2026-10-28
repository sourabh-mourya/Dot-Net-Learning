using System;

namespace PassByValueVsReference
{
    public class Program
    {
        public void PassByValue(int a)
        {
            Console.WriteLine("Pass by value function is calling...");
            a = a + 10;
            Console.WriteLine($"Value of a inside PassByValue: {a}");
        }

        public void PassByRef(ref int a)
        {
            Console.WriteLine("Pass by reference function is calling...");
            a = a + 10;
            Console.WriteLine($"Value of a inside PassByRef: {a}");
        }

        public void PassByOut(out int b)
        {
            Console.WriteLine("Pass by out function is calling...");
            b = 20; // An out parameter must be assigned before the method returns.
            Console.WriteLine($"Value of b inside PassByOut: {b}");
        }

        public static void Main(string[] args)
        {
            Program p = new Program();

            Console.WriteLine("----- PASS BY VALUE -----");
            int value1 = 10;
            p.PassByValue(value1);
            Console.WriteLine($"Value outside PassByValue: {value1}");

            Console.WriteLine();

            Console.WriteLine("----- PASS BY REF -----");
            int value2 = 10;
            p.PassByRef(ref value2);
            Console.WriteLine($"Value outside PassByRef: {value2}");

            Console.WriteLine();

            Console.WriteLine("----- PASS BY OUT -----");
            int value3; // No initialization needed before calling an out parameter.
            p.PassByOut(out value3);
            Console.WriteLine($"Value outside PassByOut: {value3}");
        }
    }
}
