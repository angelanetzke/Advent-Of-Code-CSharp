using System.Text.RegularExpressions;

string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	int maxRed = 12;
	int maxGreen = 13;
	int maxBlue = 14;
	int gameIDSum = 0;
	foreach (string thisLine in allLines)
	{
		Match match = Regex.Match(thisLine, @"Game (?<id>\d+):");
		int gameID = int.Parse(match.Groups["id"].Value);
		bool isPossible = true;
		MatchCollection matches = Regex.Matches(thisLine, @"(?<count>\d+) red");
		isPossible &= GetIsPossible(matches, maxRed);
		matches = Regex.Matches(thisLine, @"(?<count>\d+) green");
		isPossible &= GetIsPossible(matches, maxGreen);
		matches = Regex.Matches(thisLine, @"(?<count>\d+) blue");
		isPossible &= GetIsPossible(matches, maxBlue);
		gameIDSum += isPossible ? gameID : 0;
	}
	Console.WriteLine($"Part 1: {gameIDSum}");
}

static bool GetIsPossible(MatchCollection matches, int max)
{
	foreach (Match thisMatch in matches)
	{
		int thisCount = int.Parse(thisMatch.Groups["count"].Value);
		if (thisCount > max)
		{
			return false;
		}
	}
	return true;
}

static void Part2(string[] allLines)
{
	int gamePowerSum = 0;
	foreach (string thisLine in allLines)
	{
		int gamePower = 1;
		MatchCollection matches = Regex.Matches(thisLine, @"(?<count>\d+) red");
		gamePower *= GetMaxOfColor(matches);
		matches = Regex.Matches(thisLine, @"(?<count>\d+) green");
		gamePower *= GetMaxOfColor(matches);
		matches = Regex.Matches(thisLine, @"(?<count>\d+) blue");
		gamePower *= GetMaxOfColor(matches);
		gamePowerSum += gamePower;
	}
	Console.WriteLine($"Part 2: {gamePowerSum}");
}

static int GetMaxOfColor(MatchCollection matches)
{
	int max = 0;
	foreach (Match thisMatch in matches)
	{
		int count = int.Parse(thisMatch.Groups["count"].Value);
		max = int.Max(max, count);
	}
	return max;
}