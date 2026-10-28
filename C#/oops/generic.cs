using System;

namespace GenericClass
{
    public class Program
    {
        // Generic method to print an array
        public static void ShowArray<T>(T[] arr)//like ye apne different type ke array ko print krwa dega means interger array floating array jo apne iss function me passed krenge wo sb.

        {
            Console.WriteLine("\nShowing the array......");

            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write(arr[i] + "  ");
            }
  
            Console.WriteLine();
        }

        public static void Main(string[] args)
        {
            // 1. Integer array
            int[] numbers = { 10, 20, 30, 40, 50 };
            ShowArray(numbers);

            // 2. String array
            string[] names = { "Sourabh", "Rahul", "Aman" };
            ShowArray(names);

            // 3. Double array
            double[] prices = { 10.5, 20.5, 30.5 };
            ShowArray(prices);

            // 4. Different data types in one array
            object[] mixedArray = { 10, "Sourabh", 20.5, true };
            ShowArray(mixedArray);
        }
    }
}