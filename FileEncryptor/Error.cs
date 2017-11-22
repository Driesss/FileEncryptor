using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileEncryptor
{
    class Error
    {
        Music m = new Music();
        Random rnd = new Random();
        public void fileNotFoundError()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Can't open file, Enjoy this music!");
            switch (rnd.Next(3))
            {
                case 0:
                    m.playMario();
                    break;
                case 1:
                    m.playTetris();
                    break;
                case 2:
                    m.playTitanic();
                    break;
            }
            Environment.Exit(0);
        }
    }
}
