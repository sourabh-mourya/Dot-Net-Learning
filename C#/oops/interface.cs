using System;

namespace Interface
{
    interface Ia
    {
        void show();
    }
    interface Ib
    {
        void show();
    }

    class test : Ia, Ib
    {
        void Ia.show()
        {
            Console.WriteLine("Calling the Ia interface.......");
        }


        void Ib.show()
        {
            
            Console.WriteLine("Calling the Ib interface.......");
            
        }
    }


    class Program
    {
        public static void Main(String[] args)
        {
            Ia IaReferecnce=new test();
            IaReferecnce.show();

            Ib IbReference= new test();
            IbReference.show();
        }
    }
}