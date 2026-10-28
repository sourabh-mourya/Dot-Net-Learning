
using System;

namespace variable
{
    public class func()
    {
        static int a=120;
        int b=30;

       
        public static void Main(String[] args)
        {
            //to using non-static value we used by using object
            func f=new func();

            Console.WriteLine($"The non static value is {f.b}");

            //to used static value we can used by class name

            Console.WriteLine($"The Static value is {func.a}");


            //We defined here local variables 
            int a=11;
        }       
       
    }
}