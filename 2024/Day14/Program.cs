using System.Diagnostics;
using System.Text.RegularExpressions;
using Day14;

Console.WriteLine("Advent of Code 2024, Day 14");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result, out Lobby l);
Console.WriteLine($"Part 1: {part1Result}");
Part2(l, out long part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string[] allLines, out long result, out Lobby l)
{
	Stopwatch timer = new ();
	timer.Start();
	Regex numberRegex = new (@"-?\d+");
	l = new ();
	for (int i = 0; i < allLines.Length; i++)
	{
		MatchCollection mc = numberRegex.Matches(allLines[i]);
		int x = int.Parse(mc[0].Value);
		int y = int.Parse(mc[1].Value);
		int xSpeed = int.Parse(mc[2].Value);
		int ySpeed = int.Parse(mc[3].Value);
		l.AddRobot(x, y, xSpeed, ySpeed);
	}
	result = l.Part1();
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static void Part2(Lobby l, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	result = l.Part2();
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}
