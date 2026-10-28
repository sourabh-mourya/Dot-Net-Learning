using System;

class Student
{
    private int _age;

    public int Age
    {
        get
        {
            return _age;  // Value read karega
        }

        set
        {
            _age = value; // Value assign karega
        }
        
    }
}

class Program
{
    static void Main()
    {
        Student s = new Student();

        s.Age = 20;                  // set chalega
        Console.WriteLine(s.Age);    // get chalega
    }
}