using Dec24;
using System.Diagnostics;

Console.WriteLine("Advent of Code 2019, Day 24");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result);
Console.WriteLine($"Part 1 answer: {part1Result}");
Part2(allLines, out long part2Result);
Console.WriteLine($"Part 2 answer: {part2Result}");

static void Part1(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();	
	Grid g = new (allLines);
	result = g.Part1();
	timer.Stop();
	Console.WriteLine($"Part 1 time: {timer.ElapsedMilliseconds} ms");
}

static void Part2(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	RecursiveGrid rg = new (allLines);
	result = rg.Part2();
	timer.Stop();
	Console.WriteLine($"Part 2 time: {timer.ElapsedMilliseconds} ms");
}
