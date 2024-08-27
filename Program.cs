using System.IO;
using System.Reflection;
using System.Transactions;

namespace FileReading
{
    class Program
    {
        private static string? username;
        static void Main(string[] args)
        {
            string path = Path.Combine(Environment.CurrentDirectory, "coolFile.txt");
            bool runningProgram = true;

            Console.WriteLine("What's your name?");

            username = Console.ReadLine();
            while (string.IsNullOrEmpty(username))
            {
                Console.WriteLine();
                Console.WriteLine("Please enter your name");
                username = Console.ReadLine();
            }

            Console.WriteLine();
            Console.WriteLine($"Welcome {username}");

            while (runningProgram)
            {

                var choice = DisplayMenu();
                switch (choice)
                {
                    case ConsoleKey.D1:
                        WriteToFile(path);
                        break;
                    case ConsoleKey.D2:
                        ReadFile(path);
                        break;
                    case ConsoleKey.D3:
                        EmptyFile(path);
                        break;
                    case ConsoleKey.Escape:
                        runningProgram = false;
                        break;
                    default:
                        Console.WriteLine("Invalid, please enter one of the following.");
                        break;
                }
            }
            Console.WriteLine("Thanks for using my app!");
            Console.Read();
        }

        private static ConsoleKey DisplayMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1. Write inside the file");
            Console.WriteLine("2. Read the content of the file");
            Console.WriteLine("3. Remove all the text");
            Console.WriteLine("esc. Leave the program");
            Console.WriteLine();

            var choice = Console.ReadKey(intercept: true).Key;
            return choice;

        }
        private static void WriteToFile(string path)
        {
            bool isWriting = true;
            while (isWriting)
            {
                Console.WriteLine();
                Console.WriteLine("You are writing now!");
                using (StreamWriter sw = new StreamWriter(path, append: true))
                {
                    Console.WriteLine();
                    string? content = Console.ReadLine();
                    sw.WriteLine(content);
                    Console.WriteLine();
                }

                Console.WriteLine("press escape to leave, anything to stay");
                var pressedKey = Console.ReadKey(intercept: true).Key;

                if (pressedKey == ConsoleKey.Escape)
                {
                    isWriting = false;
                }
            }
        }
        private static void ReadFile(string path)
        {
            bool reading = true;
            using (StreamReader sr = File.OpenText(path))
            {

                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("Press escape to leave");

                while (reading)
                {
                    if (Console.ReadKey().Key == ConsoleKey.Escape)
                    {
                        reading = false;
                    }
                }


            }

        }
        private static void EmptyFile(string path)
        {
            File.WriteAllText(path, string.Empty);
            Console.WriteLine("File's empty now!");

        }
    }
}