using System;
using System.IO;

namespace Dec22
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] allLines = File.ReadAllLines("input.txt"); 
			Reactor theReactor = new();
			for (int i = 0; i <= 19; i++)
			{
				theReactor.Next(allLines[i]);
			}
			long part1Answer = theReactor.CountCubes();
			Console.WriteLine($"part 1: {part1Answer}");

			Reactor2 theReactor2 = new();
			for (int i = 20; i < allLines.Length; i++)
			{
				theReactor2.Next(allLines[i]);
			}
			long part2Answer = theReactor2.CountCubes() + part1Answer;
			Console.WriteLine($"part 2: {part2Answer}");

		}



	}
}
