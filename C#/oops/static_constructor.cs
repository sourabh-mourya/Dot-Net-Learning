using System;

namespace static_constructor
{
    
    class Person
    {
        public static string personName;
        public static int personAge;

        static Person()
        {
            personAge=12;
            personName="Hello world";

            Console.WriteLine("Static Constructor is invoked !!!!!");
        }


        public Person()
        {
            Console.WriteLine("Default constructor...");
        }

        public static void getDetails()
        {
            Console.WriteLine("The person age = {0}  and name = {1}",personAge,personName);
        }


        public static void Main(String[] args)
        {
            
                Person obj1=new Person();
                // Console.ReadLine();
                Person.getDetails();

                Person obj2=new Person();
                Person.getDetails();

        }


    }

}