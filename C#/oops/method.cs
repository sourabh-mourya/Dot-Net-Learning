// //1. Jab dono classes same namespace mein hain

// using System;

// namespace Method
// {
//     public class MethodClass
//     {
//         public void ShowMethod1()
//         {
//             Console.WriteLine("Hello");
//         }


//     }

//     public class SecondMethodClass
//     {
//         public static void Main(string[] args)
//         {
//             MethodClass m = new MethodClass();
//             m.ShowMethod1();
//         }
//     }
// }


// //2. Jab classes different namespaces mein hain


// namespace Method1
// {
//     public class MethodClass
//     {
//         public void ShowMethod2() { }
//     }
// }

// namespace Demo
// {
//     public class SecondMethodClass
//     {
//         public static void Main(string[] args)
//         {
//             Method1.MethodClass m = new Method1.MethodClass();
//             m.ShowMethod2();
//         }
//     }
// }


// //ab normal dekh staic method normal method 

// public class StaticMethod
// {
//     public void normalMethod()
//     {
//         Console.WriteLine("These is a normal method.........");

//     }

//     public static void sMethod()
//     {
//         Console.WriteLine("These is a static method calling by class name...");
//     }


//     public static void Main(String[] args)
//     {
//         StaticMethod sm=new StaticMethod();
//         sm.normalMethod();

//         StaticMethod.sMethod();
//     }
// }


//Passing an agrument in two different way k

public class passing_arguments
{
    
    public void add(int a, int b)
    {
        Console.WriteLine(a+b);
    }

    public void name_age(int age,string name)
    {
        Console.WriteLine("Age = {0} Name = {1}",age,name);
    }



    public static void Main(String[] args)
    {
        passing_arguments pa=new passing_arguments();
        pa.add(10,20);

        pa.name_age(20,"hello");

        //That's type of we are also calling function 

        pa.name_age(name:"World",age:12);

    }
}