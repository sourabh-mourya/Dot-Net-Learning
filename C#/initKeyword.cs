class Student
{
    public string Name { get; set; }
    public int Age { get; init; }
}

class Program
{
    static void Main()
    {
        Student s = new Student();

        s.Name = "Sourabh"; // Allowed
        s.Age = 21;         // Error: object initialization ke baad

        Console.WriteLine(s.Name);
    }
}


