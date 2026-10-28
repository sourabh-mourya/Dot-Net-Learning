#:include internal.cs
using System;

namespace hello
{
    class Program
    {
        public static void Main(string[] args)
        {
            Access a = new Access();
            a.showInternal();   // OK: alag file hai, par same project hai
        }
    }
}