using Day09;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static int Part1(string[] allLines)
{
	int sum = 0;
	foreach (string thisLine in allLines)
	{
		History newHistory = new (thisLine);
		sum += newHistory.GetNextValue();
	}
	return sum;
}

static int Part2(string[] allLines)
{
	int sum = 0;
	foreach (string thisLine in allLines)
	{
		History newHistory = new (thisLine);
		sum += newHistory.GetPreviousValue();
	}
	return sum;
}
