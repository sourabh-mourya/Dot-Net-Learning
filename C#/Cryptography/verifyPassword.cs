using System;
using System.Security.Cryptography;

namespace verify
{
    // Class ka naam method se alag rakha (C# mein same naam allowed nahi)
    public class PasswordVerifier
    {
        // public static: taaki Program class se PasswordVerifier.Verify(...) call ho sake
        public static bool Verify(
            string enteredPassword,   // user ne login pe jo password dala
            byte[] storedSalt,        // DB mein saved salt
            byte[] storedHash,        // DB mein saved hash
            int storedIterations      // register karte waqt jitni iterations use hui thi
        )
        {
            // Entered password ka hash dobara nikaalte hain, bilkul wahi salt,
            // iterations aur algorithm use karke jo register ke time use hue the.
            byte[] candidateHash = Rfc2898DeriveBytes.Pbkdf2(
                password: enteredPassword,
                salt: storedSalt,
                iterations: storedIterations,
                hashAlgorithm: HashAlgorithmName.SHA256,
                outputLength: storedHash.Length // stored hash jitni hi length
            );

            // Constant-time comparison: timing attack se bachne ke liye
            // normal == ya SequenceEqual use nahi karte.
            return CryptographicOperations.FixedTimeEquals(
                candidateHash,
                storedHash
            );
        }
    }
}