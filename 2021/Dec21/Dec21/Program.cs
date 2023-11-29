using System;
using System.IO;

namespace Dec21
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] allLines = File.ReadAllLines("input.txt");
			int player1Start = int.Parse(allLines[0].Split(": ")[1]);
			int player2Start = int.Parse(allLines[1].Split(": ")[1]);
			Game theGame = new (player1Start, player2Start);
			while (theGame.GetPlayer1Score() < 1000 && theGame.GetPlayer2Score() < 1000)
			{
				theGame.Next();
			}
			long part1Answer;
			if (theGame.GetPlayer1Score() < theGame.GetPlayer2Score())
			{
				part1Answer = theGame.GetPlayer1Score() * theGame.GetRollCount();
			}
			else
			{
				part1Answer = theGame.GetPlayer2Score() * theGame.GetRollCount();
			}
			Console.WriteLine($"part 1: {part1Answer}");

			long[] winCounts = theGame.CountWins();
			if (winCounts[0] > winCounts[1])
			{
				Console.WriteLine($"part 2: {winCounts[0]}");
			}
			else
			{
				Console.WriteLine($"part 2: {winCounts[1]}");
			}
		}



	}
}
