using System;
using System.Dynamic;
using System.Runtime.CompilerServices;

class Student
{
    private string[] names = new string[3];

    // Indexer
    public string this[int index]
    {
        get { return names[index]; }
        set { names[index] = value; }
    }
}

class Program
{
    static void Main()
    {
        Student s = new Student();

        s[0] = "Sourabh";    // set chalega
        s[1] = "Rahul";
        s[2] = "Amit";

        Console.WriteLine(s[0]);   // get chalega -> Sourabh

        for (int i = 0; i < 3; i++)
            Console.WriteLine(s[i]);
    }
}
