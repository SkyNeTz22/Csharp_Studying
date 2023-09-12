using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cryptonite_Helper
{
    internal class AesGen
    {
        public static void GenerateAesKey()
        {
            // Create a new instance of the AES algorithm
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = 256; // 256 bits for a 32-byte key
                // Generate a random AES key
                aesAlg.GenerateKey();

                // The generated key is now available as aesAlg.Key
                byte[] aesKey = aesAlg.Key;

                // Print the key (for demonstration purposes)
                Console.WriteLine("Generated AES Key (Base64):");
                Console.WriteLine(Convert.ToBase64String(aesKey));
            }
        }

        public static void GenerateAesIV() 
        {
            // Create a new instance of the AES algorithm
            using (Aes aesAlg = Aes.Create())
            {
                // Generate a random IV (Initialization Vector)
                aesAlg.GenerateIV();

                // The generated IV is now available as aesAlg.IV
                byte[] iv = aesAlg.IV;

                // Print the IV (for demonstration purposes)
                Console.WriteLine("Generated IV (Base64):");
                Console.WriteLine(Convert.ToBase64String(iv));
            }
        }
    }
}
