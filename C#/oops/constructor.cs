using System;


public class ConstructorClass
{
    int age;
    string name;

    //agar apne koi constructor nhi bante hi to c# ka complier auto matically ek constructor bana deta like yesa 
    // ConstructorClass()
    // {
    // }


    ConstructorClass()
    {
        Console.WriteLine("Default Constructor are calling......");
    }



    ConstructorClass(int age, string name)
    {
        Console.WriteLine("Parameterized constructor are calling.....");
        this.age = age;
        this.name = name;

        //yaha pr this keyword class ke instances ko refer krta hi 

    }

    ConstructorClass(int age)
    {   
        Console.WriteLine("Single parameterize constructor are calling ......");
        this.age=age;
    }

    void ShowMethod()
    {
        Console.WriteLine("Age = {0}   Name = {1}", age, name);
    }

    public static void Main(String[] args)
    {
        ConstructorClass cc = new ConstructorClass();

        ConstructorClass cc2 = new ConstructorClass(12, "Hello world");

        //ConstructorClass cc2 = new ConstructorClass(name:"hello world",age:12);

         ConstructorClass cc3 =  new ConstructorClass(12);
         cc3.ShowMethod();
        cc2.ShowMethod();
    }
}