using Day11;
using System.Diagnostics;

Console.WriteLine("Advent of Code 2025, Day 11");
string[] allLines = File.ReadAllLines("input.txt");
Reactor r = new (allLines);
Part1(r);
Part2(r);

static void Part1(Reactor r)
{
	Stopwatch timer = new();
	timer.Start();
	long result = r.Part1();	
	timer.Stop();
	Console.WriteLine($"Part 1: {result} ({timer.ElapsedMilliseconds} ms)");
}

static void Part2(Reactor r)
{
	Stopwatch timer = new();
	timer.Start();
	long result = r.Part2();	
	timer.Stop();
	Console.WriteLine($"Part 2: {result} ({timer.ElapsedMilliseconds} ms)");
}
