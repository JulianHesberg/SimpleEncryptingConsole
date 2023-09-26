using System;
using System.IO;
using System.Text;

namespace ConsoleApplication1
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to your very own secure message storage system!");

            while (true)
            {
                Console.WriteLine("Choose an option:");
                Console.WriteLine("1:  Safely store a message");
                Console.WriteLine("2:  Read the encrypted message");
                Console.WriteLine("3:  Exit");

                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        StoreMessage();
                        break;
                    case "2":
                        ReadMessage();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again..");
                        break;
                }
            }
        }

        static void StoreMessage()
        {
            Console.WriteLine("Enter the message to encrypt: ");
            var message = Console.ReadLine();

            Console.WriteLine("Enter a password: ");
            var password = Console.ReadLine();

            // Simple XOR-based encryption
            var encryptedMessage = Encrypt(message, password);

            File.WriteAllText("encrypted_message.txt", encryptedMessage);
            Console.WriteLine("\nMessage encrypted and stored successfully!");
            
            Console.WriteLine("\nPress Enter to continue...");
            Console.ReadLine();
        }

        static void ReadMessage()
        {
            Console.WriteLine("Enter the password to decrypt: ");
            var password = Console.ReadLine();

            try
            {
                var encryptedMessage = File.ReadAllText("encrypted_message.txt");

                // Simple XOR-based decryption
                var decryptedMessage = Decrypt(encryptedMessage, password);

                Console.WriteLine("\nDecrypted Message:");
                Console.WriteLine("\n" + decryptedMessage);
                
                Console.WriteLine("\nPress Enter to continue...");
                Console.ReadLine();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No encrypted message found.");
            }
        }
        static string Encrypt(string message, string password)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] encryptedBytes = new byte[messageBytes.Length];

            for (int i = 0; i < messageBytes.Length; i++)
            {
                encryptedBytes[i] = (byte)(messageBytes[i] ^ passwordBytes[i % passwordBytes.Length]);
            }

            return Convert.ToBase64String(encryptedBytes);
        }
        static string Decrypt(string encryptedMessage, string password)
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedMessage);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] decryptedBytes = new byte[encryptedBytes.Length];

            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                decryptedBytes[i] = (byte)(encryptedBytes[i] ^ passwordBytes[i % passwordBytes.Length]);
            }

            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}