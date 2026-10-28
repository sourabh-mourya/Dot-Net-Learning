using System;


namespace Multiple_Delegates
{
    public delegate void withoutParameterZied();
    public delegate void AdditionCalculation(int a,int b);
    public delegate void SquareCalculation(int a);

    class Program
    {


        public static void fun1()
        {
            Console.WriteLine("Without parameterized function is calling................");
        }


        public static void Add(int a, int b)
        {
            int c = a + b;
            Console.WriteLine("The addition of a = {0} &  b = {1}  is c = {2}", a, b, c);
        }


        public static void Square(int a)
        {
            int d = a * a;
            Console.WriteLine("Square of Number {0}", d);
        }



        public static void Main(String[] args)
        {

            withoutParameterZied wp = new withoutParameterZied(fun1);
            wp.Invoke();

            AdditionCalculation ac = Add;
            ac.Invoke(1, 2);
            ac(9, 0);


            SquareCalculation sc = Square;
            sc.Invoke(1);

        }
    }
}