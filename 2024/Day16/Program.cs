using Day16;
using System.Diagnostics;

Console.WriteLine("Advent of Code 2024, Day 16");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result, out Maze m);
Console.WriteLine($"Part 1: {part1Result}");
Part2(m, out long part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string[] allLines, out long result, out Maze m)
{
	Stopwatch timer = new ();
	timer.Start();
	m = new (allLines);
	result = m.Part1();
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static void Part2(Maze m, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	result = m.Part2();
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}
