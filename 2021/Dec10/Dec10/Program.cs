using System;
using System.Collections.Generic;
using System.IO;

namespace Dec10
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] allLines = File.ReadAllLines("input.txt");
			int part1Score = 0;
			List<long> part2Scores = new();
			foreach (string thisLine in allLines)
			{
				Line thisSubsystemLine = new(thisLine);
				int thisPart1Score = thisSubsystemLine.GetPart1Score();
				part1Score += thisPart1Score;
				if (thisPart1Score == 0)
				{
					part2Scores.Add((thisSubsystemLine.GetPart2Score()));
				}

			}
			Console.WriteLine($"part 1: {part1Score}");
			part2Scores.Sort();
			Console.WriteLine($"part 2: {part2Scores[part2Scores.Count / 2]}");
		}
	}
}
