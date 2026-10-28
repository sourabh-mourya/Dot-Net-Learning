using System;

namespace StaticNamespace
{
    public class StaticClass
    {
        public string name;            // instance: har object ka apna naam
        public static int count = 0;   // static: sab objects ke liye ek hi counter

        public StaticClass(string n)   // constructor: naam = class ka naam
        {
            name = n;
            count++;
        }

        public static void Hello()
        {
            Console.WriteLine("Hello From StaticClass...");
        }

        public void ShowName()
        {
            Console.WriteLine("The student name is {0}", name);
        }

        static int d=10;
        static int e=20;
        public void swapTwoNumber()
        {
                Console.WriteLine($"Before Swap the value {d} and {e}");

                d=d+e;
                e=d-e;
                d=d-e;

                Console.WriteLine($"After Swap the value  {d} and {e}");
        }
        public static void Main(string[] args)
        {
            StaticClass.Hello();                     // static: ClassName se

            StaticClass s1 = new StaticClass("Amit");
            StaticClass s2 = new StaticClass("Riya");

            s1.ShowName();                           // instance: object se
            s2.ShowName();
            Console.WriteLine(StaticClass.count);    // 2
            s1.swapTwoNumber();
        }
    }
}