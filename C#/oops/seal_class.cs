using System;

namespace SEAL_CLASSES
{
    public sealed class Program
    {
        public void show()
        {
            Console.WriteLine("Program class show method is calling.......");
        }
    }


    public class test:Program
    {
        public void show1()
        {
            Console.WriteLine("Test class show1 method is calling......");
        }


        public static void Main(String[] args)
        {
            test t=new test();
            t.show();
            t.show1();
        }
    }
}