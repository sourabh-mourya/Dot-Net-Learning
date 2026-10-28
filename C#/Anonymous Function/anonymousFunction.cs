
using System;

namespace Anonymous_Function
{
    // Delegate declaration
    public delegate void MyDelegate(int num);

    public class program
    {
        // Non-static method
        public void square1(int num)
        {
            Console.WriteLine(
                "The square of a number {0} is {1}",
                num, num * num
            );
        }

        // Method accepting a delegate as a parameter
        public void fun(MyDelegate del, int a)
        {
            del.Invoke(10);

            Console.WriteLine("The value of a is {0}", a);
        }

        public static void Main()
        {
            // Anonymous function assigned to a delegate
            MyDelegate myObj = delegate (int num)
            {
                Console.WriteLine(
                    "The square of a number {0} is {1}",
                    num, num * num
                );
            };

            // Calling the delegate
            myObj(10);

            // Creating an object to call the non-static fun() method
            program p = new program();

            // Passing an anonymous function to fun()
            p.fun(delegate (int num)
            {
                Console.WriteLine(
                    "The square of a number {0} is {1}",
                    num, num * num
                );
            }, 10);
        }
    }
}
