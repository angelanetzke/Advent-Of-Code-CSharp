using Day11;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static int Part1(string[] allLines)
{
	State start = new (allLines);
	return start.CountSteps();
}

static int Part2(string[] allLines)
{
	State start = new (allLines);
	start.AddItem("elerium");
	start.AddItem("dilithium");
	return start.CountSteps();
}