using System.Text.RegularExpressions;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static int Part1(string[] allLines)
{
	int value = 0;
	foreach (string thisLine in allLines)
	{
		int matchCount = GetMatchCount(thisLine);
		value += matchCount > 0 ? (int)Math.Round(Math.Pow(2, matchCount - 1)) : 0;
	}
	return value;
}

static int Part2(string[] allLines)
{
	Dictionary<int, int> values = [];
	Dictionary<int, int> counts = [];
	foreach (string thisLine in allLines)
	{
		int cardNumber = int.Parse(Regex.Match(thisLine, @"\d+").Value);
		counts[cardNumber] = 1;
		int matchCount = GetMatchCount(thisLine);
		values[cardNumber] = matchCount;
	}
	for (int i = counts.Keys.Min(); i <= counts.Keys.Max(); i++)
	{
		for (int j = i + 1; j <= i + values[i]; j++)
		{
			counts[j] += counts[i];
		}
	}
	return counts.Values.Sum();
}

static int GetMatchCount(string thisLine)
{
	int colonIndex = thisLine.IndexOf(':');
	int pipeIndex = thisLine.IndexOf('|');
	string winningNumberString = thisLine.Substring(colonIndex + 1, pipeIndex - colonIndex - 1);
	MatchCollection winningNumberMatches = Regex.Matches(winningNumberString, @"\d+");
	HashSet<int> winningNumbers = winningNumberMatches.Select(x => int.Parse(x.Value)).ToHashSet();
	string elfNumberString = thisLine.Substring(pipeIndex + 1);
	MatchCollection elfNumberMatches = Regex.Matches(elfNumberString, @"\d+");
	HashSet<int> elfNumbers = elfNumberMatches.Select(x => int.Parse(x.Value)).ToHashSet();
	return winningNumbers.Intersect(elfNumbers).Count();
}
