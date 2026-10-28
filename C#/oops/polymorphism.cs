using System;


namespace Polymorphism
{



    public class Poly
    {

        public void Add()
        {
            Console.WriteLine("In wihtout parameterize function ");
            int a = 10;
            int b = 20;

            int c = a + b;
            Console.WriteLine("The additon of two value a = {0}  and b = {1}  is c = {2}", a, b, c);

        }

        public void Add(int a, int b)
        {
            Console.WriteLine("In parameterzie function..");
            int c = a + b;

            Console.WriteLine("The additon of two value a = {0}  and b = {1}  is c = {2}", a, b, c);
        }



        public static void Main(String[] args)
        {
            Poly p = new Poly();

            p.Add();
            p.Add(19, 90);

        }


    }

}