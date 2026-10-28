
using System;

namespace InheritanceConstructor
{
    // BASE / PARENT CLASS
    public class BaseClass
    {
        // Base class constructor
        public BaseClass()
        {
            Console.WriteLine("Base class constructor is calling...");
        }

        public BaseClass(int a)
        {
            Console.WriteLine("The value of a = {0}",a);
        }
    }

    // DERIVED / CHILD CLASS
    public class DerivedClass : BaseClass
    {
        public DerivedClass():base(10)
        {
            Console.WriteLine("Derived class constructor is calling...");
        }

        /*
         Constructor Inheritance Concept:

         Jab hum DerivedClass ka object banate hain,
         tab sabse pehle BaseClass ka constructor call hota hai,
         uske baad DerivedClass ka constructor execute hota hai.

         Compiler implicitly base() call karta hai,
         agar base class ka accessible parameterless constructor available ho.

         Isko explicitly aise bhi likh sakte hain:

         public DerivedClass() : base()
         {
             Console.WriteLine("Derived class constructor is calling...");
         }

         Agar base class mein sirf parameterized constructor ho,
         toh derived constructor ko base() mein arguments dene honge.

         Example:
         public BaseClass(int age) { }

         public DerivedClass() : base(20)
         {
             Console.WriteLine("Derived class constructor...");
         }


         agar apne base(20) used kiya hi to parent class only single parameterized constructor call wihtout parameterized constructor call nhi hoga 
        */


        
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // DerivedClass ka object create kar rahe hain.
            DerivedClass dc = new DerivedClass();
        }
    }
}
