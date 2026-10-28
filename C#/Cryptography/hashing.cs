using System;

using System.Security.Cryptography;
using System.Text;
class Program
{
    static void Main()
    {
        string message = "Hello World";
        Console.WriteLine("Simple plane message :-  " + message);

        byte[] input = Encoding.UTF8.GetBytes(message);
        byte[] hash = SHA256.HashData(input);
        string hexHash = Convert.ToHexString(hash);

        //Agar SHA12 used krna hi to apne iss trh used krenge
        ///SHA12.HashData(input) yesa kr dege...

        Console.WriteLine($"SHA-256: {hexHash}");
        Console.WriteLine($"Hash length: {hash.Length} bytes");
    }
}