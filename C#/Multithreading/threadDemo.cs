
using System;
using System.Threading;

namespace ThreadDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Program started");

            // Get the currently executing thread
            Thread t = Thread.CurrentThread;

            // Set the thread name
            t.Name = "My Main Thread";

            Console.WriteLine(
                "Current thread name: " + t.Name);

            Console.WriteLine(
                "Thread ID: " + t.ManagedThreadId);

            for (int i = 0; i <= 100; i++)
            {
                Console.WriteLine("Test 2: " + i);

                if (i == 50)
                {
                    Console.WriteLine(
                        "Main thread going to sleep...");

                    // Pause the current thread for 5 seconds
                    Thread.Sleep(5000);

                    Console.WriteLine(
                        "Main thread resumed");
                }
            }

            Console.WriteLine("Program completed");
        }
    }
}
