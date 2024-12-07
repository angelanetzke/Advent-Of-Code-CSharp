using System.Diagnostics;
using Day07;

Console.WriteLine("Advent of Code 2024, Day 7");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result, out List<Equation> allEquations);
Console.WriteLine($"Part 1: {part1Result}");
Part2(allEquations, out long part2Result);
Console.WriteLine($"Part 2: {part1Result + part2Result}");

static void Part1(string[] allLines, out long result, out List<Equation> allEquations)
{
	Stopwatch timer = new ();
	timer.Start();
	long sum = 0L;
	allEquations = [];
	foreach (string thisLine in allLines)
	{
		Equation e = new (thisLine);
		long thisValue = e.Part1();
		if (thisValue == 0L)
		{
			allEquations.Add(e);
		}		
		sum += thisValue;
	}
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
	result = sum;
}

static void Part2(List<Equation> allEquations, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	long sum = 0L;
	foreach (Equation e in allEquations)
	{
		sum += e.Part2();
	}
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
	result = sum;
}
