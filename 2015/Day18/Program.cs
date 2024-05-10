using Day18;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static int Part1(string[] allLines)
{
	LightDisplay display = new (allLines);
	return display.Iterate(100);
}

static int Part2(string[] allLines)
{
	LightDisplay display = new (allLines);
	return display.Iterate2(100);
}
