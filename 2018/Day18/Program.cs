using Day18;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var theArea = new Area(allLines);
	var resourceValue = theArea.Iterate(10);
	Console.WriteLine($"Part 1: {resourceValue}");
}

static void Part2(string[] allLines)
{
	var theArea = new Area(allLines);
	var resourceValue = theArea.Iterate(1_000_000_000);
	Console.WriteLine($"Part 2: {resourceValue}");
}