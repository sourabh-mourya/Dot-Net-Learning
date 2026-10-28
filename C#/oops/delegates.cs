using System;


namespace DELEGATES
{

        public delegate void Calculation(int a,int b);
    class Program
    {
        public static void Add(int a, int b)
        {
            int c=a+b;
            Console.WriteLine("The additon of a = {0}  b = {1} = c = {2}",a,b,c);
        }



        public static void Main(String[] args)
        {
            
            Calculation obj=new Calculation(Program.Add);
            Console.WriteLine("Calling Delegate through Invoking method......");
            obj.Invoke(10,20);
            Console.WriteLine("Calling Delegate through obj() ....");
            obj(20,30);


            Console.WriteLine("Create Delegates object in different way ..... ");
            Calculation obj1=Program.Add;
            Console.WriteLine("Calling Delegate through Invoking method obj1()......");
            obj1.Invoke(10,20);
            Console.WriteLine("Calling Delegate through obj1() ....");
            obj1(20,30);
        }

    }
}
