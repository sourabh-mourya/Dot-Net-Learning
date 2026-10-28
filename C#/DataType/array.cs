using System;

namespace arrayNamespace
{
    public class arrayClass
    {
        public static void Main(String[] args)
        {
            Console.WriteLine("First Decalaration then intialization ");

            int[] arr;
            arr = new int[5];

            arr[0] = 10;
            arr[1] = 20;
            arr[2] = 30;
            arr[3] = 40;
            arr[4] = 50;

            for (int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine(arr[i]);
            }

            Console.WriteLine("Decalartion and same time declare memory type");
            int[] arr2 = new int[2];

            arr2[0] = 90;
            arr2[1] = 99;

            for (int i = 0; i < arr2.Length; i++)
            {
                Console.WriteLine(arr2[i]);
            }


            Console.WriteLine("Declartion with intialization ");

            int[] arr3 = { 10, 20, 30, 40, 90 };

            for (int i = 0; i < arr3.Length; i++)
            {
                Console.WriteLine(arr3[i]);
            }


            Console.WriteLine("Array Take Input");
            int[] arr5 = new int[5];

            for (int i = 0; i < arr5.Length; i++)
            {
                Console.WriteLine("Enter the value ");

                arr5[i] = Convert.ToInt32(Console.ReadLine());

            }

            Console.WriteLine("Array5 Elements are printing.....");

            for (int i = 0; i < arr5.Length; i++)
            {
                Console.WriteLine(arr5[i]);
            }
        }
    }

}