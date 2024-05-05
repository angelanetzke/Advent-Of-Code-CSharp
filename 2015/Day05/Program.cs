using System.Globalization;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static int Part1(string[] allLines)
{
	int niceCount = 0;
	List<char> vowels = ['a', 'e', 'i', 'o', 'u'];
	List<string> naughtyStrings = ["ab", "cd", "pq", "xy"];
	foreach (string thisLine in allLines)
	{
		int vowelCount = thisLine.Count(x => vowels.Contains(x));
		if (vowelCount < 3)
		{
			continue;
		}
		bool hasTwoInARow = false;
		for (int i = 1; i < thisLine.Length; i++)
		{
			if (thisLine[i] == thisLine[i - 1])
			{
				hasTwoInARow = true;
				break;
			}
		}
		if (!hasTwoInARow)
		{
			continue;
		}
		bool hasNaughtyString = false;
		foreach (string thisNaughtyString in naughtyStrings)
		{
			if (thisLine.Contains(thisNaughtyString))
			{
				hasNaughtyString = true;
				break;
			}
		}
		if (hasNaughtyString)
		{
			continue;
		}
		niceCount++;
	}
	return niceCount;
}

static int Part2(string[] allLines)
{
	int niceCount = 0;
	foreach (string thisLine in allLines)
	{
		bool hasTwoPairs = false;
		for (int i = 1; i <= thisLine.Length - 3; i++)
		{
			for (int j = i + 2; j < thisLine.Length; j++)
			{
				if (thisLine[i] == thisLine[j] && thisLine[i - 1] == thisLine[j - 1])
				{
					hasTwoPairs = true;
					break;
				}
			}
			if (hasTwoPairs)
			{
				break;
			}
		}
		if (!hasTwoPairs)
		{
			continue;
		}
		bool hasSplitPair = false;
		for (int i = 2; i < thisLine.Length; i++)
		{
			if (thisLine[i] == thisLine[i - 2])
			{
				hasSplitPair = true;
				break;
			}
		}
		if (!hasSplitPair)
		{
			continue;
		}
		niceCount++;
	}
	return niceCount;
}