using System;


namespace MyProject
{
    public class Calculator
    {
        int a, b, c;
        public void Accept()
        {
            //means input kaise lege apne 

            Console.WriteLine("Enter the First number");
            // a=Console.ReadLine()///agar yesa likhege to error kyuki Console.ReadLine Ek String leta hi combinateion of character but 
            //but a to apne yaha pr a to apne isko parse krna padega 

            //We Convert into a two way 
            //First way is these 
            a = int.Parse(Console.ReadLine());

            //second one is 
            //   int b = Convert.ToInt32(Console.ReadLine());   // same result as int.Parse here
            Console.WriteLine("Enter the Second number");
            b = int.Parse(Console.ReadLine());

        }
        public void Add()
        {
            c = a + b;
            Console.WriteLine($"Addition of {a} and {b} is {c}");
            Console.WriteLine("Addition of {0} and {1} is {2}", a, b, c);
        }

        public void Sub()
        {
            c = a - b;
            Console.WriteLine($"Substraction of {a} and {b} is {c}");
        }


        public void Mul()
        {
            c = a * b;
            Console.WriteLine($"Multiplication of {a} and {b} is {c}");
        }

        public void Divide()
        {
            c = a / b;
            Console.Write($"Divide of {a} and {b} is {c}");
        }

    }
}