using System;

namespace InheritanceTypes
{
    // 1. Single Inheritance
    class Animal
    {
        public void Eat()
        {


            Console.WriteLine("Animal is eating");
        }
    }

    class Dog : Animal
    {
        public void Bark()
        {
            Console.WriteLine("Dog is barking");
        }
    }

    // 2. Multilevel Inheritance
    class LivingBeing
    {
        public void Breathe()
        {
            Console.WriteLine("Living being is breathing");
        }
    }

    class Mammal : LivingBeing
    {
        public void Walk()
        {
            Console.WriteLine("Mammal is walking");
        }
    }

    class Human : Mammal
    {
        public void Think()
        {
            Console.WriteLine("Human is thinking");
        }
    }

    // 3. Hierarchical Inheritance
    class Shape
    {
        public void Display()
        {
            Console.WriteLine("This is a shape");
        }
    }

    class Circle : Shape
    {
        public void DrawCircle()
        {
            Console.WriteLine("Drawing a circle");
        }
    }

    class Rectangle : Shape
    {
        public void DrawRectangle()
        {
            Console.WriteLine("Drawing a rectangle");
        }
    }

    // 4. Hybrid Inheritance
    // Combination of class inheritance and interfaces
    interface IPrintable
    {
        void Print();
    }

    class Device
    {
        public void Start()
        {
            Console.WriteLine("Device started");
        }
    }

    class Printer : Device, IPrintable
    {
        public void Print()
        {
            Console.WriteLine("Printer is printing");
        }
    }

    class Scanner : Device, IPrintable
    {
        public void Print()
        {
            Console.WriteLine("Scanner output is printing");
        }
    }

    // Main method
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Single Inheritance ===");
            Dog d = new Dog();
            d.Eat();
            d.Bark();

            Console.WriteLine("\n=== Multilevel Inheritance ===");
            Human h = new Human();
            h.Breathe();
            h.Walk();
            h.Think();

            Console.WriteLine("\n=== Hierarchical Inheritance ===");
            Circle c = new Circle();
            c.Display();
            c.DrawCircle();

            Rectangle r = new Rectangle();
            r.Display();
            r.DrawRectangle();

            Console.WriteLine("\n=== Hybrid Inheritance ===");
            Printer p = new Printer();
            p.Start();
            p.Print();

            Scanner s = new Scanner();
            s.Start();
            s.Print();
        }
    }
}