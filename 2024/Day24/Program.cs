using Day24;
using System.Diagnostics;

Console.WriteLine("Advent of Code 2024, Day 24");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result);
Console.WriteLine($"Part 1: {part1Result}");
Part2(allLines, out string part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	Device d = new (allLines);
	result = d.Part1();
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static void Part2(string[] allLines, out string result)
{
	Stopwatch timer = new ();
	timer.Start();
	Device d = new (allLines);
	result = d.Part2();
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}
