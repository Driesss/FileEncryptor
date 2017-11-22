using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FileEncryptor
{
    class GenerateIVAndKeyFromPassphrase
    {
        private int keyLength;
        private int IVLength;
        private int iterations;
        private string passPhrase;
        private byte[] generatedBytes;
        private byte[] IV;
        private byte[] key;

        public GenerateIVAndKeyFromPassphrase(int keyLength, int IVLength, string passPhrase, int iterations, byte[] salt)
        {
            this.keyLength = keyLength;
            this.IVLength = IVLength;
            this.iterations = iterations;
            this.passPhrase = passPhrase;
            this.IV = new byte[IVLength];
            this.key = new byte[keyLength];

            Rfc2898DeriveBytes generator = new Rfc2898DeriveBytes(passPhrase, salt, iterations);
            generatedBytes = generator.GetBytes(keyLength + IVLength);
        }

        public byte[] getIV()
        {

            for (int i = 0; i < IVLength; i++)
            {
                IV[i] = generatedBytes[i];
            }

            return IV;
        }

        public byte[] getKey()
        {
            for (int i = 0; i < keyLength; i++)
            {
                key[i] = generatedBytes[IVLength + i];
            }

            return key;
        }

    }
}
