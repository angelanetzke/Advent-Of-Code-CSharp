using Day10;
using System.Diagnostics;

Console.WriteLine("Advent of Code 2025, Day 10");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	Stopwatch timer = new();
	timer.Start();
	long result = 0L;
	foreach (string s in allLines)
	{
		Machine m = new (s);
		result += m.CountPressesLights();
	}	
	timer.Stop();
	Console.WriteLine($"Part 1: {result} ({timer.ElapsedMilliseconds} ms)");
}

static void Part2(string[] allLines)
{
	Stopwatch timer = new();
	timer.Start();
	long result = 0L;
	foreach (string s in allLines)
	{
		Machine m = new (s);
		result += m.CountPressesJoltage();
	}
	timer.Stop();
	Console.WriteLine($"Part 2: {result} ({timer.ElapsedMilliseconds} ms)");
}
