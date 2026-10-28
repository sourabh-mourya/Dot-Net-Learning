using System;

abstract class Vehicle
{
    public string brand = "Honda";

    public abstract void Start();

    public void Display()
    {
        Console.WriteLine(brand);
    }
}

class Car : Vehicle
{
    public override void Start()
    {
        Console.WriteLine("Car starts with a key");
    }
}

class Bike : Vehicle
{
    public override void Start()
    {
        Console.WriteLine("Bike starts with a button");
    }
}

class Program
{
    static void Main()
    {
        Car c = new Car();
        c.Display();
        c.Start();

        Bike b = new Bike();
        b.Display();
        b.Start();

        // Vehicle v = new Vehicle(); // Error
    }
}