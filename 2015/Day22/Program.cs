using Day22;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static int Part1(string[] allLines)
{
	return GameState.GetLeastMana(allLines);
}

static int Part2(string[] allLines)
{
	return GameState.GetLeastMana(allLines, true);
}