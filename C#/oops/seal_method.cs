
using System;

namespace SEAL_METHOD
{
    public class Program1
    {
        public virtual void show1()
        {
            Console.WriteLine("Program1 Show1");
        }

        public virtual void show2()
        {
            Console.WriteLine("Program1 Show2");
        }
    }

    public class Program2 : Program1
    {
        // Normal override: further override allowed
        public override void show1()
        {
            Console.WriteLine("Program2 Show1");
        }

        // Sealed override: further override not allowed
        public sealed override void show2()
        {
            Console.WriteLine("Program2 Show2");
        }
    }

    public class Program3 : Program2
    {
        // Allowed: show1() sealed nahi hai
        public override void show1()
        {
            Console.WriteLine("Program3 Show1");
        }

        // Error: show2() sealed override hai
        // public override void show2()
        // {
        //     Console.WriteLine("Program3 Show2");
        // }
    }

    public class Test
    {
        public static void Main()
        {
            Program3 p = new Program3();

            p.show1();
            p.show2();
        }
    }
}
