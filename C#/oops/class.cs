using System;

public class Student
{
    public string Name;//agar yaha pr public nhi to by default wo private hota hi and usko apne access nhi kr skte direct hi 
    public int Age;
    public double Marks;


    public void Introduce()
    {
        Console.WriteLine($"Hi, main {Name} hu, {Age} saal ka.");
    }

    public bool IsPassed()
    {
        return Marks >= 40;           // method field ko directly use kar sakta hai
    }
}

public class Program
{
    public static void Main()
    {
        // Object banana: ClassName variable = new ClassName();
        Student s1 = new Student();   // 'new' => heap pe memory allocate karta hai
        s1.Name = "Sourabh";          // dot (.) operator se field access
        s1.Age = 21;
        s1.Marks = 85.5;

        Student s2 = new Student();   // dusra object — apna alag data
        // s2.Name = "Rahul";
        s2.Age = 22;
        s2.Marks = 35;

        s1.Introduce();               // Hi, main Sourabh hu, 21 saal ka.
        s2.Introduce();               // Hi, main Rahul hu, 22 saal ka.

        Console.WriteLine(s1.IsPassed());   // True
        Console.WriteLine(s2.IsPassed());   // False
    }
}