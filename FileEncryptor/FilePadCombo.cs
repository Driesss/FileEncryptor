using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileEncryptor
{
    class FilePadCombo
    {
        private byte[] file;
        private byte[] oneTimePad;

        public FilePadCombo(byte[] file, byte[] pad)
        {
            this.file = file;
            this.oneTimePad = pad;
        }

        public byte[] getFile()
        {
            return file;
        }

        public byte[] getOneTimePad()
        {
            return oneTimePad;
        }
    }
}
