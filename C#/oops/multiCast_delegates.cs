using System;


namespace DELEGATES
{

    public delegate void Calculation(int a, int b);
    class Program
    {
        public static void Add(int a, int b)
        {
            int c = a + b;
            Console.WriteLine("The additon of a = {0}  b = {1} = c = {2}", a, b, c);
        }
        public static void Sub(int a, int b)
        {
            int c = a - b;
            Console.WriteLine("The substraction of a = {0}  b = {1} = c = {2}", a, b, c);
        }
        public static void Mul(int a, int b)
        {
            int c = a + b;
            Console.WriteLine("The multiplication of a = {0}  b = {1} = c = {2}", a, b, c);
        }
        public static void Div(int a, int b)
        {
            int c = a / b;
            Console.WriteLine("The division of a = {0}  b = {1} = c = {2}", a, b, c);
        }



        public static void Main(String[] args)
        {

            Calculation obj = new Calculation(Program.Add);
            obj +=Sub;
            obj+=Mul;
            obj+=Div;

            obj.Invoke(10,2);
        }

    }
}