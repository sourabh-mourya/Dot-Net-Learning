
using System;

namespace LambdaExpressions
{
    public delegate void MyDelegate(int num);

    public class Program
    {
        public static void Main(string[] args)
        {
            // 1. Anonymous Method
            MyDelegate obj = delegate (int num)
            {
                Console.WriteLine(
                    "The square of a number {0} is {1}",
                    num, num * num
                );
            };

            obj.Invoke(10);


            // 2. Statement Lambda
            MyDelegate obj1 = (num) =>
            {
                Console.WriteLine(
                    "The square of a number {0} is {1}",
                    num, num * num
                );
            };

            obj1.Invoke(10);


            // 3. Expression Lambda
            // Func<int, int>: int input, int return value
            Func<int, int> obj2 = (num) => num * num;

            Console.WriteLine(
                "The square of a number 10 is {0}",
                obj2.Invoke(10)
            );
        }
    }
}
