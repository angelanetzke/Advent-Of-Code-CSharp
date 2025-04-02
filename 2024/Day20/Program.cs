using Day20;
using System.Diagnostics;

Console.WriteLine("Advent of Code 2024, Day 20");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result);
Console.WriteLine($"Part 1: {part1Result}");
Part2(allLines, out long part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	Track t = new (allLines);
	result = t.Part1();
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static void Part2(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();

	result = -1L;
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}
