using Day12;
using System.Diagnostics;

Console.WriteLine("Advent of Code 2024, Day 12");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result, out List<PlantRegion> allRegions);
Console.WriteLine($"Part 1: {part1Result}");
Part2(allRegions, out long part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string[] allLines, out long result, out List<PlantRegion> allRegions)
{
	Stopwatch timer = new ();
	timer.Start();
	for (int row = 0; row < allLines.Length; row++)
	{
		for (int column = 0; column < allLines[0].Length; column++)
		{
			PlantRegion.AddRemaining((row, column));
		}
	}
	allRegions = [];
	while (PlantRegion.AreRemaining())
	{
		PlantRegion newRegion = new (allLines);
		newRegion.FindRegion();
		allRegions.Add(newRegion);
	}
	long costSum = 0;
	allRegions.ForEach(x => costSum += x.GetArea() * x.GetPerimeter());
	result = costSum;
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static void Part2(List<PlantRegion> allRegions, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	long costSum = 0;
	allRegions.ForEach(x => costSum += x.GetArea() * x.GetSideCount());
	result = costSum;
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}
