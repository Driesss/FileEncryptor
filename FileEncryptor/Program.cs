using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace FileEncryptor
{
    class Program
    {
        private int option = 0;
        private string filePath = "";
        private string oneTimePadPath = "";
        private byte[] file;
        private byte[] oneTimePad;
        private FilePadCombo fpc;
        private CryptoEngine cryptoEngine = new CryptoEngine();
        private StringBuilder breadCrumb = new StringBuilder("You are here: ");

        static void Main(string[] args)
        {
            Console.Title = "FileEncryptor by Dries Stelten";

            Program program = new Program();
        }

        public Program()
        {
            do
            {
                mainMenu();
            } while (option != 5);
        }

        private void mainMenu()
        {
            Console.Clear();
            breadCrumb.Append("Main menu");
            Console.WriteLine(breadCrumb);
            Console.WriteLine();
            Console.WriteLine("Welcome, please select mode:\n");
            Console.WriteLine("1) OneTimePad encryption");
            Console.WriteLine("2) AES encryption");
            Console.WriteLine("3) OneTimePad + AES encryption");
            Console.WriteLine("5) Exit\n");
            Console.WriteLine("Enter mode: [1-5]");
            option = getInput(5);

            switch (option)
            {
                case 1:
                    option = 0;
                    do
                    {
                        OTP();
                    } while (option != 3);
                    break;
                case 2:
                    option = 0;
                    do
                    {
                        AES();
                    } while (option != 3);
                    break;
                case 3:
                    option = 0;
                    do
                    {
                        OTPAES();
                    } while (option != 3);
                    break;
                case 5:
                    break;
                default:
                    Console.WriteLine("error in input, please enter a valid option.");
                    break;
            }
            breadCrumb.Remove(breadCrumb.Length - 9, 9);
        }

        private void OTP()
        {
            Console.Clear();
            breadCrumb.Append(" -> One time pad encryption");
            Console.WriteLine(breadCrumb);
            Console.WriteLine();
            Console.WriteLine("Do you want to:\n");
            Console.WriteLine("1) Encrypt");
            Console.WriteLine("2) Decrypt");
            Console.WriteLine("3) Back\n");
            Console.WriteLine("Enter mode: [1-3]");
            option = getInput(3);

            switch (option)
            {
                case 1:
                    breadCrumb.Append(" -> encryption");
                    Console.Clear();
                    Console.WriteLine(breadCrumb);
                    Console.WriteLine();
                    Console.WriteLine("Please provide path to the file you want to encrypt");
                    file = getFileFromInput();                    
                    Console.Clear();
                    Console.WriteLine(breadCrumb);
                    Console.WriteLine();
                    fpc = cryptoEngine.Encrypt(file);
                    Console.Clear();
                    Console.WriteLine(breadCrumb);
                    Console.WriteLine();
                    file = fpc.getFile();
                    oneTimePad = fpc.getOneTimePad();
                    Console.WriteLine("Writing files...");
                    File.WriteAllBytes(filePath + ".enc", file);
                    GC.Collect();
                    Console.WriteLine("Encrypted file written to:\n" + filePath + ".enc");
                    File.WriteAllBytes(filePath + ".otp", oneTimePad);
                    Console.WriteLine("OneTimePad written to:\n" + filePath + ".otp");
                    Console.WriteLine("Press any key to go back");
                    Console.ReadKey();
                    breadCrumb.Remove(breadCrumb.Length - 14, 14);
                    break;
                case 2:
                    breadCrumb.Append(" -> decryption");
                    Console.Clear();
                    Console.WriteLine(breadCrumb);
                    Console.WriteLine();
                    Console.WriteLine("Please provide path to file");
                    filePath = Console.ReadLine();
                    Console.WriteLine("Reading file...");
                    file = getFileFromInput();
                    Console.WriteLine("Please provile one time pad.");
                    oneTimePad = getFileFromInput();
                    Console.Clear();
                    Console.WriteLine(breadCrumb);
                    Console.WriteLine();
                    file = cryptoEngine.Encrypt(file, oneTimePad);
                    Console.Clear();
                    Console.WriteLine(breadCrumb);
                    Console.WriteLine();
                    Console.WriteLine("Writing to file...");
                    File.WriteAllBytes(filePath.Substring(0, filePath.Length - 4), file);
                    Console.WriteLine("Decrypted file written to:\n" + filePath.Substring(0, filePath.Length - 4));
                    Console.WriteLine("Press any key to go back");
                    Console.ReadKey();
                    breadCrumb.Remove(breadCrumb.Length - 14, 14);
                    break;
                case 3:
                    break;
                default:
                    break;
            }
            breadCrumb.Remove(breadCrumb.Length - 27, 27);
        }

        private void AES() {
            Console.Clear();
            breadCrumb.Append(" -> AES encryption");
            Console.WriteLine(breadCrumb);
            Console.WriteLine();
            Console.WriteLine("Do you want to:\n");
            Console.WriteLine("1) Encrypt");
            Console.WriteLine("2) Decrypt");
            Console.WriteLine("3) Back\n");
            Console.WriteLine("Enter mode: [1-3]");
            getInput(3);
            string passPhrase;

            switch (option)
            {
                case 1:
                    breadCrumb.Append(" -> encryption");
                    Console.Clear();
                    Console.WriteLine(breadCrumb);
                    Console.WriteLine();
                    Console.WriteLine("Please give path to file to encrypt.");
                    file = getFileFromInput();
                    Console.WriteLine("Please give passPhrase");
                    passPhrase = Console.ReadLine();
                    file = cryptoEngine.EncryptAES(file, passPhrase);
                    Console.WriteLine("Writing to file");
                    File.WriteAllBytes(filePath, file);
                    Console.WriteLine("File writen to {0}", filePath);
                    Console.WriteLine("Press any key to go back");
                    Console.ReadKey();
                    breadCrumb.Remove(breadCrumb.Length - 14, 14);
                    break;
                case 2: 
                    breadCrumb.Append(" -> decryption");
                    Console.Clear();
                    Console.WriteLine(breadCrumb);
                    Console.WriteLine();
                    Console.WriteLine("Please give path to file to decrypt.");
                    filePath = Console.ReadLine();
                    try
                    {
                        file = File.ReadAllBytes(filePath);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                    Console.WriteLine("Please give passPhrase");
                    passPhrase = Console.ReadLine();
                    file = cryptoEngine.DecryptAES(file, passPhrase);
                    Console.WriteLine("Writing to file");
                    File.WriteAllBytes(filePath, file);
                    Console.WriteLine("File writen to {0}", filePath);
                    Console.WriteLine("Press any key to go back");
                    Console.ReadKey();
                    breadCrumb.Remove(breadCrumb.Length - 14, 14);
                    break;
                case 3:
                    break;
                default:
                    break;
            }
            breadCrumb.Remove(breadCrumb.Length - 18, 18);

        }

        private void OTPAES() {
            Console.Clear();
            breadCrumb.Append(" -> One time pad + AES encryption");
            Console.WriteLine(breadCrumb);
            Console.WriteLine();
            Console.WriteLine("Hiding Gouvernment secrets, running from the NSA, too much free disk space,\nthis is the way to go!\n");
            Console.WriteLine("1) Encrypt");
            Console.WriteLine("2) Decrypt");
            Console.WriteLine("3) Back\n");
            Console.WriteLine("Enter mode: [1-3]");
            getInput(3);
            string passPhrase;

            switch (option)
            {
                case 1:
                    breadCrumb.Append(" -> encryption");
                    Console.Clear();
                    Console.WriteLine(breadCrumb);
                    Console.WriteLine();
                    Console.WriteLine("Please provide path to the file you want to encrypt");
                    filePath = Console.ReadLine();
                    try
                    {
                        Console.Clear();
                        Console.WriteLine("reading file...");
                        file = File.ReadAllBytes(filePath);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine(filePath);
                        Console.ReadKey();
                        //e.fileNotFoundError();
                        throw;
                    }
                    Console.Clear();
                    Console.WriteLine("Please give passPhrase to encrypt oneTimePad");
                    passPhrase = Console.ReadLine();
                    fpc = cryptoEngine.Encrypt(file);
                    Console.Clear();
                    file = fpc.getFile();
                    oneTimePad = fpc.getOneTimePad();
                    Console.WriteLine("Writing files...");
                    File.WriteAllBytes(filePath + ".enc", file);
                    Console.WriteLine("Encrypted file written to:\n" + filePath + ".enc");
                    oneTimePad = cryptoEngine.EncryptAES(oneTimePad, passPhrase);
                    File.WriteAllBytes(filePath + ".otp", oneTimePad);
                    Console.WriteLine("OneTimePad written to:\n" + filePath + ".otp");
                    Console.WriteLine("Press any key to go back");
                    Console.ReadKey();
                    breadCrumb.Remove(breadCrumb.Length - 14, 14);
                    break;
                case 2:
                    breadCrumb.Append(" -> decryption");
                    Console.Clear();
                    Console.WriteLine(breadCrumb);
                    Console.WriteLine();
                    Console.WriteLine("Please provide path to file");
                    filePath = Console.ReadLine();
                    Console.WriteLine("Reading file...");
                    try
                    {
                        file = File.ReadAllBytes(filePath);
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    Console.WriteLine("Please provile one time pad.");
                    oneTimePadPath = Console.ReadLine();
                    try
                    {
                        oneTimePad = File.ReadAllBytes(oneTimePadPath);
                    }
                    catch (Exception)
                    {
                        //e.fileNotFoundError();
                        throw;
                    }
                    Console.Clear();
                    Console.WriteLine("Please give passPhrase");
                    passPhrase = Console.ReadLine();
                    oneTimePad = cryptoEngine.DecryptAES(oneTimePad, passPhrase);
                    file = cryptoEngine.Encrypt(file, oneTimePad);
                    Console.Clear();
                    Console.WriteLine("Writing to file...");
                    File.WriteAllBytes(filePath.Substring(0, filePath.Length - 4), file);
                    Console.WriteLine("Decrypted file written to:\n" + filePath.Substring(0, filePath.Length - 4));
                    Console.WriteLine("Press any key to go back");
                    Console.ReadKey();
                    breadCrumb.Remove(breadCrumb.Length - 14, 14);
                    break;
                case 3:
                    break;
                default:
                    break;
            }
            breadCrumb.Remove(breadCrumb.Length - 33, 33);

        }

        private int getInput(int aant)
        {
            bool success = false;
            do
            {
                do
                {
                    try
                    {
                        option = Int32.Parse(Console.ReadLine());
                        success = true;
                    }
                    catch (FormatException ex)
                    {
                        Console.WriteLine(ex.Message);
                        success = false;
                    }
                } while(!success);

                if (!((option > 0) && (option < aant+1)))
                {
                    Console.WriteLine("please enter a number from 1 to " + aant);
                    success = false;
                }
                else
                {
                    success = true;
                }
            } while (!success);

            return option;
        }

        private byte[] getFileFromInput()
        {
            bool success = false;
            do
            {
                filePath = Console.ReadLine().Trim();
                try
                {
                    filePath = Path.GetFullPath(filePath);
                    if (File.Exists(filePath))
                    {
                        success = true;
                    }
                    else
                    {
                        success = false;
                        Console.WriteLine("Invalid path");
                    }
                }
                catch (Exception Ex)
                {
                    Console.WriteLine(Ex.Message);
                    success = false;
                }
            } while (!success);

            try
            {
                Console.Clear();
                Console.WriteLine(breadCrumb);
                Console.WriteLine();
                Console.WriteLine("reading file...");
                file = File.ReadAllBytes(filePath);
            }
            catch (Exception)
            {
                Console.WriteLine(filePath);
                Console.ReadKey();
                //e.fileNotFoundError();
                throw;
            }

            return file;
        }

    }
}
