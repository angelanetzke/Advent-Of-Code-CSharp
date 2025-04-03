using Day21;
using System.Diagnostics;

Console.WriteLine("Advent of Code 2024, Day 21");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result);
Console.WriteLine($"Part 1: {part1Result}");
Part2(allLines, out long part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	long sum = 0L;
	foreach (string thisLine in allLines)
	{
		Dictionary<string, long> directions = Keypad.GetDirections(thisLine);
		directions = Robot.GetDirections(directions);
		directions = Robot.GetDirections(directions);
		sum += directions.Sum(x => x.Key.Length * x.Value) * int.Parse(thisLine[..^1]);
	}
	result = sum;
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static void Part2(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	long sum = 0L;
	foreach (string thisLine in allLines)
	{
		Dictionary<string, long> directions = Keypad.GetDirections(thisLine);
		for (int i = 1; i <= 25; i++)
		{
			directions = Robot.GetDirections(directions);
		}
		long thisComplexity = 0L;
		foreach (string thisKey in directions.Keys)
		{
			thisComplexity += thisKey.Length * directions[thisKey]; 
		}
		thisComplexity *= int.Parse(thisLine[..^1]);
		sum += thisComplexity;
	}
	result = sum;
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}
