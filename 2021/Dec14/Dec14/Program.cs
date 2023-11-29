using System;
using System.IO;

namespace Dec14
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] allLines = File.ReadAllLines("input.txt");
			Polymer thePolymer = new(allLines[0]);
			for (int i = 2; i < allLines.Length; i++)
			{
				thePolymer.AddRule(allLines[i]);
			}
			int part1Iterations = 10;
			int part2Iterations = 40;
			thePolymer.Execute(10);
			Console.WriteLine($"part 1: {thePolymer.GetAnswer()}");
			thePolymer.Execute(part2Iterations - part1Iterations);
			Console.WriteLine($"part 2: {thePolymer.GetAnswer()}");
		}
	}
}
