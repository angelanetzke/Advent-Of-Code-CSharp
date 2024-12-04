using Day04;

Console.WriteLine("Advent of Code 2024, Day 4");
string[] allLines = File.ReadAllLines("input.txt");
WordSearch ws = new (allLines);
Part1(ws, out long part1Result);
Console.WriteLine($"Part 1: {part1Result}");
Part2(ws, out long part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(WordSearch ws, out long result)
{
	result = ws.Part1();
}

static void Part2(WordSearch ws, out long result)
{
	result = ws.Part2();
}
