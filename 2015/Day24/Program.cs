using Day24;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static long Part1(string[] allLines)
{
	PresentSorter sorter = new (allLines);
	return sorter.GetQuantumEntanglement();
}

static long Part2(string[] allLines)
{
	PresentSorter sorter = new (allLines, true);
	return sorter.GetQuantumEntanglement();
}


