using System;
using System.Collections.Generic;
using System.IO;

namespace TheHangmanGame
{
	class Program
	{
		static string capital;
		static string capitalDashed;
		static string currentCapital;
		static string country;

		List<char> notInWord = new List<char>();

		int lifePoints = 5;

		static void Main(string[] args)
		{
			NewGame();


		}

		static void NewGame()
		{
			var dict = FileToDictionary();
			capital = GetRandomCapital(dict);

		}

		static public Dictionary<int, Dictionary<string, string>> FileToDictionary()
		{
			string path = Directory.GetCurrentDirectory() + "countries_and_capitals.txt";
			var countriesAndCapitals = new Dictionary<int, Dictionary<string, string>>();

			using (StreamReader reader = new StreamReader(path))
			{
				string line;
				int i = 0;

				while ((line = reader.ReadLine()) != null)
				{
					var list = line.Split('|');
					var tempDict = new Dictionary<string, string>();
					tempDict[list[0]] = list[1];
					countriesAndCapitals[i] = tempDict;
					i++;
				}
			}
			return countriesAndCapitals;
		}

		static string GetRandomCapital(Dictionary<int, Dictionary<string, string>> dict)
		{
			string capital = "";
			int max = dict.Count;

			Random rand = new Random();
			var i = rand.Next(max);

			var tempDict = dict[i];
			capital = tempDict.Values.ToString();
			return capital;
		}

		static string DashCapital(string str)
		{
			string temp = "";

			Console.WriteLine("Drawn capital:\n");
			for (int i = 0; i < str.Length; i++)
				temp += '_';

			return temp;
		}

		static void GuessCapital(string capital)
		{
			string guessedCapital;
			Console.WriteLine("Guess capital");
			guessedCapital = Console.ReadLine();
			if (guessedCapital == capital)
			{
				Console.WriteLine("You won!");
			}
			else
			{
				lifePoints -= 1;
				Console.WriteLine($"It's not {guessedCapital}!");
				Menu();
			}
		}

		static void GuessLetter(string capital)
		{
			Console.WriteLine("Type a letter");
			char letter = Console.ReadKey().KeyChar;

			bool contains = false;
			int i = 0;

			string newCapital = "";

			foreach (char c in capital)
			{
				if (c.Equals(letter))
				{
					contains = true;
					newCapital += letter;
				}
				else
					newCapital += '_';
				i++;
			}

			if (contains == false)
			{
				Console.WriteLine($"There's no {letter} in capital");
				lifePoints -= 1;
				notInWord.Add(letter);
			}
			else
			{
				Console.WriteLine(newCapital);
			}

		}


		static void Menu()
		{
			Console.WriteLine($"Your life points: {lifePoints}\n");
			Console.WriteLine("Menu:\n");
			Console.WriteLine("1. To guess capital type 1");
			Console.WriteLine("2. To guess letter in capital type 2");
			string key = Console.ReadLine();
			try
			{
				int result = Int32.Parse(key);
				if (result == 1)
					GuessCapital(capital);
				else if (result == 2)
					GuessLetter(capital);
				else
				{
					Console.WriteLine("Incorrect input");
					Menu();
				}
			}
			catch (FormatException)
			{
				Console.WriteLine("Incorrect input");
			}
		}


	}
}
