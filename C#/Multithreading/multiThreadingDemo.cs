
using System;
using System.Threading;

namespace MultiThreading
{
    public class Program
    {
        // Method executed by Thread 1
        public static void Test1()
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine(
                    $"Test1: {i}, Thread ID: " +
                    $"{Thread.CurrentThread.ManagedThreadId}");

                Thread.Sleep(500);
            }

            Console.WriteLine("Test1 completed");
        }

        // Method executed by Thread 2
        public static void Test2()
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine(
                    $"Test2: {i}, Thread ID: " +
                    $"{Thread.CurrentThread.ManagedThreadId}");

                Thread.Sleep(500);
            }

            Console.WriteLine("Test2 completed");
        }

        public static void Main(string[] args)
        {
            Console.WriteLine(
                $"Main thread ID: " +
                $"{Thread.CurrentThread.ManagedThreadId}");

            // Create two thread objects
            Thread t1 = new Thread(Test1);
            Thread t2 = new Thread(Test2);

            // Give names to threads
            t1.Name = "Worker Thread 1";
            t2.Name = "Worker Thread 2";

            // Start both threads
            t1.Start();
            t2.Start();

            Console.WriteLine("Both threads started");

            // Wait for both threads to finish
            t1.Join();
            t2.Join();

            Console.WriteLine("Main thread completed");
        }
    }
}
