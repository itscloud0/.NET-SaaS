using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionDecryption
{
    //Developed by Ilia Sorokin
    public static class EncDec
    {
        // Secret key for AES encryption/decryption. It must be 16, 24, or 32 bytes long.
        private static readonly string Key = "CSE445YinongChenFall2024"; // Ensure the key length matches AES requirements

        // Encrypt the given plaintext string
        public static string Encrypt(string plainText)
        {
            // Using AES encryption algorithm
            using (Aes aes = Aes.Create())
            {
                // Set the AES key (must be 16, 24, or 32 bytes)
                aes.Key = Encoding.UTF8.GetBytes(Key);
                // Use a fixed Initialization Vector (IV) for simplicity. In practice, use a unique IV per encryption.
                aes.IV = new byte[16]; // 16-byte fixed IV

                // Create a memory stream to hold the encrypted data
                using (MemoryStream ms = new MemoryStream())
                {
                    // Create a CryptoStream that encrypts the data before writing it to the memory stream
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        // Write the plain text to the CryptoStream, which will encrypt it
                        using (StreamWriter sw = new StreamWriter(cs))
                        {
                            sw.Write(plainText);
                        }
                    }
                    // Return the encrypted data as a Base64 encoded string
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        // Decrypt the given encrypted string
        public static string Decrypt(string encryptedText)
        {
            // Using AES encryption algorithm
            using (Aes aes = Aes.Create())
            {
                // Set the AES key (must be 16, 24, or 32 bytes)
                aes.Key = Encoding.UTF8.GetBytes(Key);
                // Use the same fixed Initialization Vector (IV) used during encryption
                aes.IV = new byte[16]; // 16-byte fixed IV

                // Convert the encrypted text from Base64 back into a byte array
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(encryptedText)))
                {
                    // Create a CryptoStream that decrypts the data from the memory stream
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        // Read the decrypted data from the CryptoStream
                        using (StreamReader sr = new StreamReader(cs))
                        {
                            // Return the decrypted string
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}


