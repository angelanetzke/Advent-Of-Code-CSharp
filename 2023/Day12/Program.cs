using Day12;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static long Part1(string[] allLines)
{
	long possibilitySum = 0;
	foreach (string thisLine in allLines)
	{
		SpringRecord newRecord = new (thisLine, false);
		possibilitySum += newRecord.GetPossibilityCount();
	}
	return possibilitySum;
}

 static long Part2(string[] allLines)
{
	long possibilitySum = 0;
	foreach (string thisLine in allLines)
	{
		SpringRecord newRecord = new (thisLine, true);
		possibilitySum += newRecord.GetPossibilityCount();
	}
	return possibilitySum;
}








