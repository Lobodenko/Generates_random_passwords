using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Generates_random_passwords
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Random Password Generator");
            Console.WriteLine("========================");

            Console.Write("Enter the length of the password: ");
            if (int.TryParse(Console.ReadLine(), out int length) && length > 0)
            {
                Console.Write("Include digits? (yes/no): ");
                bool includeDigits = Console.ReadLine().Trim().ToLower() == "yes";

                Console.Write("Include uppercase letters? (yes/no): ");
                bool includeUpperCase = Console.ReadLine().Trim().ToLower() == "yes";

                Console.Write("Include lowercase letters? (yes/no): ");
                bool includeLowerCase = Console.ReadLine().Trim().ToLower() == "yes";

                Console.Write("Include symbols? (yes/no): ");
                bool includeSymbols = Console.ReadLine().Trim().ToLower() == "yes";

                string generatedPassword = GeneratePassword(length, includeDigits, includeUpperCase, includeLowerCase, includeSymbols);

                Console.WriteLine($"\nGenerated Password: {generatedPassword}");
                Thread.Sleep(4000);
            }
            else
            {
                Console.WriteLine("Invalid password length.");
            }
        }

        static string GeneratePassword(int length, bool includeDigits, bool includeUpperCase, bool includeLowerCase, bool includeSymbols)
        {
            const string digits = "0123456789";
            const string upperCase = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerCase = "abcdefghijklmnopqrstuvwxyz";
            const string symbols = "!@#$%^&*()-_=+[]{}|;:'\",.<>/?";

            StringBuilder allowedChars = new StringBuilder();

            if (includeDigits)
                allowedChars.Append(digits);

            if (includeUpperCase)
                allowedChars.Append(upperCase);

            if (includeLowerCase)
                allowedChars.Append(lowerCase);

            if (includeSymbols)
                allowedChars.Append(symbols);

            if (allowedChars.Length == 0)
                throw new ArgumentException("You must select at least one character type.");

            char[] password = new char[length];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[length];
                rng.GetBytes(randomBytes);

                for (int i = 0; i < length; i++)
                {
                    password[i] = allowedChars[randomBytes[i] % allowedChars.Length];
                }
            }

            return new string(password);
        }
    }
   
}