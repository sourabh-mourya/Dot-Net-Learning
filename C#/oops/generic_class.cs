using System;

namespace GENERIC_CLASS
{
    public class Program<T>
    {
        T box;

        public Program(T a)
        {
            this.box = a;
        }

        public T show()
        {
            return box;
        }
    }


    public class test
    {
        public static void Main()
        {
            Program<int> obj = new Program<int>(10);
            Console.WriteLine("Interger object is calling .......");
            Console.WriteLine(obj.show());


            Program<string> obj1 = new Program<string>("Hello world");
            Console.WriteLine("String object calling .......");
            Console.WriteLine(obj1.show());
        }
    }
}