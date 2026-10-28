using System;

namespace Inheritance_Interface
{
    interface i1
    {
        void print();
    }

    interface i2
    {
        void print1();
    }

    // i3, i1 aur i2 dono ko inherit karta hai
    interface i3 : i1, i2
    {
        void print2();
    }

    class test : i3
    {
        public void print()
        {
            Console.WriteLine("Interface 1 is calling......");
        }

        public void print1()
        {
            Console.WriteLine("Interface 2 is calling.........");
        }

        public void print2()
        {
            Console.WriteLine("Interface 3 is calling..........");
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            test t = new test();

            // t.print();
            // t.print1();
            // t.print2();

            i1 i=new test();//we take an I1 interface as an refernces and child class and object
            i.print();
        }
    }
}