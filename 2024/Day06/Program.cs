using Day06;

Console.WriteLine("Advent of Code 2024, Day 6");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result);
Console.WriteLine($"Part 1: {part1Result}");
Part2(allLines, out long part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string[] allLines, out long result)
{
	LabMap map = new(allLines);
	result = map.Part1();
}

static void Part2(string[] allLines, out long result)
{
	LabMap map = new(allLines);
	result = map.Part2();
}

