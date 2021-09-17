using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighScoreTrialAndError
{
    class Program
    {
        static void GetHighScore(string user, int val)
        {
            using (StreamReader reader = new StreamReader(@"C:\Users\will5606\source\repos\HighScoreTrialAndError\TextFile1.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line;
                    line = reader.ReadLine();

                    var values = line.Split(' ');

                    if (values[0] == user && val < int.Parse(values[1]))
                    {
                        string tempLine = user + " " + val;
                        var tempVar = tempLine.ToCharArray();
                        int tempInt = reader.ReadLine().IndexOf(values[0]);
                        reader.ReadLine().Remove(tempInt);
                    }
                }
            }
            using (StreamWriter writer = new StreamWriter(@"C:\Users\will5606\source\repos\HighScoreTrialAndError\TextFile1.txt", true))
            {
                writer.WriteLine(user + " " + val);
            }
        }

        static void ShowHighScore()
        {
            using (StreamReader reader = new StreamReader(@"C:\Users\will5606\source\repos\HighScoreTrialAndError\TextFile1.txt"))
            {
                var dictionary = new Dictionary<string, int>();

                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();

                    var values = line.Split(' ');

                    dictionary.Add(values[0], int.Parse(values[1]));
                }
                var items = from pair in dictionary
                           orderby pair.Value ascending
                           select pair;
                foreach (KeyValuePair<string, int> pair in items)
                {
                    Console.WriteLine("{0}: {1}", pair.Key, pair.Value);
                }
            }
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("-----HovedMenu-----\n1. Gæt et tal\n2. Se alle highscores\n3. Luk programmet");

                int guess = 0;

                ConsoleKey userInput = Console.ReadKey().Key;

                if (userInput == ConsoleKey.D1 || userInput == ConsoleKey.NumPad1)
                {
                    Console.Clear();

                    Console.Write("Gæt et tal: ");

                    Random rnd = new Random();

                    int rndNum = rnd.Next(10);

                    int y = int.Parse(Console.ReadLine());

                    while (y != rndNum)
                    {
                        guess++;
                        Console.Clear();
                        Console.WriteLine("Gæt: " + guess);
                        y = int.Parse(Console.ReadLine());
                    }

                    if (y == rndNum)
                    {
                        Console.Clear();
                        Console.WriteLine("-----Du gættede tallet!-----\nTallet blev gættet på " + guess + " Forsøg!");
                        Console.Write("Indtast dit navn: ");
                        string name = Console.ReadLine();

                        GetHighScore(name, guess);
                        Console.WriteLine(name + " " + guess); 
                    }

                }

                else if (userInput == ConsoleKey.D2 || userInput == ConsoleKey.NumPad2)
                {
                    Console.Clear();
                    ShowHighScore();
                }

                else if (userInput == ConsoleKey.D3 || userInput == ConsoleKey.NumPad3)
                {
                    Environment.Exit(1);
                }

                else
                {
                    Application.Restart();
                    Environment.Exit(1);
                }
                Console.ReadKey();
            }


        }
    }
}
