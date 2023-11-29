var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

void Part1(string[] allLines)
{
	var total = 0;
	foreach (string thisLine in allLines)
	{
		if (thisLine[0] == '+')
		{
			total += int.Parse(thisLine[1..]);
		}
		else
		{
			total -= int.Parse(thisLine[1..]);
		}
	}
	Console.WriteLine($"Part 1: {total}");
}

void Part2(string[] allLines)
{
	var usedFrequencies = new HashSet<int>();
	int nextFrequency = 0;
	int i = 0;
	while (!usedFrequencies.Contains(nextFrequency))
	{
		usedFrequencies.Add(nextFrequency);
		if (allLines[i][0] == '+')
		{
			nextFrequency += int.Parse(allLines[i][1..]);
		}
		else
		{
			nextFrequency -= int.Parse(allLines[i][1..]);
		}
		i = (i + 1) % allLines.Length;		
	}
	Console.WriteLine($"Part 2: {nextFrequency}");
}