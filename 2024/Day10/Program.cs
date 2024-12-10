using Day10;
using System.Diagnostics;

Console.WriteLine("Advent of Code 2024, Day 10");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result);
Console.WriteLine($"Part 1: {part1Result}");
Part2(allLines, out long part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	TopologicalMap tm = new (allLines);	
	result = tm.Part1();
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static void Part2(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	TopologicalMap tm = new (allLines);	
	result = tm.Part2();
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}
