using System;
using System.Security.Cryptography; // HMAC, random bytes, secure compare ke liye
using System.Text;                  // string <-> byte[] conversion ke liye

class Program
{
    static void Main()
    {
        // 32 bytes (256-bit) ki cryptographically secure random secret key banata hai.
        // Real app mein yeh key server pe safe store hoti hai (env var / key vault),
        // har baar random generate nahi hoti.
        byte[] key = RandomNumberGenerator.GetBytes(32);

        // Jis data ko sign/verify karna hai, use UTF-8 bytes mein convert kar rahe hain,
        // kyunki HMAC string pe nahi, bytes pe kaam karta hai.
        byte[] message = Encoding.UTF8.GetBytes(
            "orderId=101&status=paid"
        );

        // MAC (Message Authentication Code) yahan store hoga
        byte[] mac;

        // HMAC-SHA256 object secret key ke saath banaya.
        // 'using' lagaya taaki kaam ke baad key material memory se properly dispose ho jaye.
        using (var hmac = new HMACSHA256(key))
        {
            // Message + key se 32-byte ka signature (tag) calculate hota hai.
            // Same key + same message = hamesha same MAC.
            // Message ya key badli to MAC poori tarah alag aayega.
            mac = hmac.ComputeHash(message);
        }

        // MAC ko readable hex string mein print karta hai
        Console.WriteLine(Convert.ToHexString(mac));

        // Demo ke liye hum apna hi MAC "received" maan rahe hain.
        // Real case mein yeh client/webhook request ke header se aayega.
        byte[] receivedMac = mac; // Demo only: real request se aayega

        // Fixed-time comparison: dono MAC compare karta hai, lekin time hamesha same lagta hai
        // chahe pehla byte hi mismatch ho ya aakhri. Isse timing attack se bachav hota hai.
        // Normal == ya SequenceEqual use mat karna, woh jaldi return karte hain aur leak karte hain.
        bool valid = CryptographicOperations.FixedTimeEquals(
            mac,
            receivedMac
        );

        // true = message authentic hai aur tamper nahi hua
        Console.WriteLine($"Valid: {valid}");
    }
}