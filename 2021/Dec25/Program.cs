using Dec25;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");

static int Part1(string[] allLines)
{
	Region theRegion = new (allLines);
	return theRegion.CountSteps();
}
