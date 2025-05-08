using Day23;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static int Part1(string[] allLines)
{
	Island i = new (allLines);
	return i.Part1();
}

static int Part2(string[] allLines)
{
	Island i = new (allLines);
	return i.Part2();
}