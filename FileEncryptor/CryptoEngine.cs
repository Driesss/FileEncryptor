using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace FileEncryptor
{
    class CryptoEngine
    {

        private Random rnd = new Random();
        private int h = 0;
        private string Key;
        private string IV = "zxcvbnmdfrasdfgh";
        private byte[] salt = { 0, 1, 2, 3, 4, 5, 6, 7 };
        public byte[] Encrypt(byte[] file, byte[] oneTimePad)
        {
            h = file.Length / 50;
            Console.WriteLine("Decrypting file...");
            Console.WriteLine("File size: " + file.Length + " bytes.");
            Console.WriteLine("Progress:");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < file.Length; i++)
            {
                file[i] = (byte)((int)file[i] ^ (int)oneTimePad[i]);
                if (i % h == 0)
                {
                    Console.Write("\u2593");
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            return file;
            
        }

        public FilePadCombo Encrypt(byte[] file)
        {
            Console.WriteLine("Creating oneTimePad...");
            byte[] oneTimePad = new byte[file.Length];
            FilePadCombo fpc;

            rnd.NextBytes(oneTimePad);
            h = file.Length / 50;

            Console.WriteLine("Encrypting file...");
            Console.WriteLine("File size: " + file.Length + " bytes.");
            Console.WriteLine("Progress:");
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Blue;
            for (int i = 0; i < file.Length; i++)
            {
                file[i] = (byte)((int)file[i] ^ (int)oneTimePad[i]);
                if (i % h == 0)
                {
                    Console.Write("\u2593");
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            fpc = new FilePadCombo(file, oneTimePad);

            return fpc;
        }

        public byte[] EncryptAES(byte[] file, string pass)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            GenerateIVAndKeyFromPassphrase generator = new GenerateIVAndKeyFromPassphrase(32, 16, pass, 300, salt);
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.IV = generator.getIV();
            aes.Key = generator.getKey();
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform crypto = aes.CreateEncryptor(aes.Key, aes.IV);
            byte[] encrypted = crypto.TransformFinalBlock(file, 0, file.Length);
            crypto.Dispose();
            return encrypted;
        }

        public byte[] DecryptAES(byte[] file, string pass)
        {
            AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
            GenerateIVAndKeyFromPassphrase generator = new GenerateIVAndKeyFromPassphrase(32, 16, pass, 300, salt);
            aes.BlockSize = 128;
            aes.KeySize = 256;
            aes.Key = generator.getKey();
            aes.IV = generator.getIV();
            aes.Padding = PaddingMode.PKCS7;
            aes.Mode = CipherMode.CBC;
            ICryptoTransform crypto = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] secret = crypto.TransformFinalBlock(file, 0, file.Length);
            crypto.Dispose();
            return secret;

        }
    }
}
