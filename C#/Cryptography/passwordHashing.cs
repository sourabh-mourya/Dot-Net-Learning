using System;
using System.Security.Cryptography; // PBKDF2, random salt, secure compare ke liye
using verify;                       // humare PasswordVerifier class ka namespace

class Program
{
    static void Main()
    {
        string password = "Example-password-for-demo";

        // Har password ke liye fresh random 16-byte salt.
        // Salt ki wajah se same password ka hash alag-alag users ke liye alag aata hai
        // (rainbow table attack se bachav).
        byte[] salt = RandomNumberGenerator.GetBytes(16);

        // PBKDF2 ko jaanbujhkar slow banane ke liye iterations zyada rakhte hain.
        // 600,000 = PBKDF2-SHA256 ke liye OWASP ki recommended value.
        int iterations = 600_000;

        // Output hash ki length bytes mein (32 = 256-bit)
        int outputLength = 32;

        // Password + salt + iterations se hash derive hota hai.
        // Same inputs = hamesha same hash, par hash se password wapas nahi nikal sakte.
        byte[] passwordHash = Rfc2898DeriveBytes.Pbkdf2(
            password: password,
            salt: salt,
            iterations: iterations,
            hashAlgorithm: HashAlgorithmName.SHA256,
            outputLength: outputLength
        );

        // Demo ke liye print kar rahe hain. Real app mein plain password kabhi log/print mat karna.
        Console.WriteLine("Plain Password :- " + password);
        Console.WriteLine($"Salt: {Convert.ToBase64String(salt)}");   // DB mein salt bhi store hota hai
        Console.WriteLine($"Hash: {Convert.ToBase64String(passwordHash)}");

        // Login jaisa step: entered password ko stored salt/hash/iterations se verify karo
        bool ans = PasswordVerifier.Verify(
            password,
            salt,
            passwordHash,
            iterations
        );

        Console.WriteLine("Ans :- " + ans); // true = password sahi hai
    }
}