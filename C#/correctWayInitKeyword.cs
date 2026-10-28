class Student
{
    public string Name { get; init; }
    public int Age { get; init; }
}

class Program
{
    static void Main()
    {
        Student s = new Student
        {
            Name = "Sourabh",
            Age = 21
        };

        Console.WriteLine(s.Name);
        Console.WriteLine(s.Age);
    }
}