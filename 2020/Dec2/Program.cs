using Dec2;

var allLines = System.IO.File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	int validCount = 0;
	foreach (string thisLine in allLines)
	{
		if ((new PolicyAndPassword(thisLine)).IsValid())
		{
			validCount++;
		}
	}
	Console.WriteLine($"Part 1: {validCount}");
}

static void Part2(string[] allLines)
{
	int validCount = 0;
	foreach (string thisLine in allLines)
	{
		if ((new PolicyAndPassword2(thisLine)).IsValid())
		{
			validCount++;
		}
	}
	Console.WriteLine($"Part 2: {validCount}");
}
