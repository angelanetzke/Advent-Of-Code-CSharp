using System.Text.RegularExpressions;

string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	Regex singleDigit = new (@"\d{1}");
	int sum = 0;
	foreach (string thisLine in allLines)
	{
		MatchCollection matches = singleDigit.Matches(thisLine);
		int firstDigit = int.Parse(matches.First().ToString());
		int lastDigit = int.Parse(matches.Last().ToString());
		int value = 10 * firstDigit + lastDigit;
		sum += value;
	}
	Console.WriteLine($"Part 1: {sum}");	
}

static void Part2(string[] allLines)
{
	Dictionary<string, int> stringToDigit = new ()
	{
		{ "one", 1 },
		{ "two", 2 },
		{ "three", 3 },
		{ "four", 4 },
		{ "five", 5 },
		{ "six", 6 },
		{ "seven", 7 },
		{ "eight", 8 },
		{ "nine", 9 },
	};
	Regex singleDigit = new (@"\d{1}");
	int sum = 0;
	foreach (string thisLine in allLines)
	{
		MatchCollection matches = singleDigit.Matches(thisLine);
		string firstDigitString = matches.First().ToString();
		int firstDigitIndex = matches.First().Index;
		string lastDigitString = matches.Last().ToString();
		int lastDigitIndex = matches.Last().Index;
		foreach (string thisDigitString in stringToDigit.Keys)
		{
			int index = thisLine.IndexOf(thisDigitString);
			if (index < 0)
			{
				continue;
			}
			if (index < firstDigitIndex)
			{
				firstDigitIndex = index;
				firstDigitString = thisDigitString;
			}
			index = thisLine.LastIndexOf(thisDigitString);
			if (index > lastDigitIndex)
			{
				lastDigitIndex = index;
				lastDigitString = thisDigitString;
			}
		}
		int firstDigit;
		int lastDigit;
		try
		{
			firstDigit = int.Parse(firstDigitString);
		}
		catch
		{
			firstDigit = stringToDigit[firstDigitString];
		}
		try
		{
			lastDigit = int.Parse(lastDigitString);
		}
		catch
		{
			lastDigit = stringToDigit[lastDigitString];
		}
		int value = 10 * firstDigit + lastDigit;
		sum += value;
	}
	Console.WriteLine($"Part 2: {sum}");	
}
