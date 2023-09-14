using System.Security.Cryptography;
using System.Text;

namespace ShopAPI.Core
{
    internal class EncryptionHelper
    {
        public static string EncryptString(string input)
        {
            using (Aes aesAlg = Aes.Create())
            {
                // Key and IV should be securely generated and stored.
                aesAlg.Key = Convert.FromBase64String("7IGQEiJlDV6Q/qd6AwsqJrLXaLkbietYE92MIJ6x5Qc="); ; // 16, 24, or 32 bytes
                aesAlg.IV = Convert.FromBase64String("TDxQ7ck3VEaG9aAH5u5j1g=="); // 16 bytes

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                byte[] encryptedBytes;
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(input);
                        }
                    }
                    encryptedBytes = msEncrypt.ToArray();
                }

                return Convert.ToBase64String(encryptedBytes);
            }
        }

        public static string DecryptString(string encryptedInput)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Convert.FromBase64String("7IGQEiJlDV6Q/qd6AwsqJrLXaLkbietYE92MIJ6x5Qc="); ; // 16, 24, or 32 bytes
                aesAlg.IV = Convert.FromBase64String("TDxQ7ck3VEaG9aAH5u5j1g=="); // 16 bytes

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                byte[] encryptedBytes = Convert.FromBase64String(encryptedInput);

                using (MemoryStream msDecrypt = new MemoryStream(encryptedBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
