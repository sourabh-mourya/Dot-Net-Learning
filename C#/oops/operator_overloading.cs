using System;

class Number
{
    public int x;

    public Number(int x)
    {
        this.x = x;
    }

    public static Number operator +(Number a, Number b)
    {
        return new Number(a.x + b.x);
    }
}

class Program
{
    static void Main()
    {
        Number n1 = new Number(10);
        Number n2 = new Number(20);

        Number n3 = n1 + n2;

        Console.WriteLine(n3.x);
    }
}