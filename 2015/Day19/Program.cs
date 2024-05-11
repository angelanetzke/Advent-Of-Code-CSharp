using System.Text;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static int Part1(string[] allLines)
{
	List<(string, string)> replacements = [];
	string startString = "";
	foreach (string thisLine in allLines)
	{
		if (thisLine.Length == 0)
		{
			continue;
		}
		if (thisLine.Contains("=>"))
		{
			string[] parts = thisLine.Split(" => ");
			replacements.Add((parts[0], parts[1]));
		}
		else
		{
			startString = thisLine;
		}
	}
	HashSet<string> newStrings = [];
	foreach ((string, string) thisReplacement in replacements)
	{
		newStrings = newStrings.Union(GetNewStrings(thisReplacement, startString, 0)).ToHashSet();
	}
	return newStrings.Count;
}

/* 
	Note: Because of randomization, this method may not terminate.
	If it doesn't terminate instantly, terminate the program and run it again.
*/
static int Part2(string[] allLines)
{
	Random RNG = new ();
	List<(string, string)> replacements = [];
	string startString = "";
	foreach (string thisLine in allLines)
	{
		if (thisLine.Length == 0)
		{
			continue;
		}
		if (thisLine.Contains("=>"))
		{
			string[] parts = thisLine.Split(" => ");
			replacements.Add((string.Join("", parts[1].Reverse()), string.Join("", parts[0].Reverse())));
		}
		else
		{
			startString = string.Join("", thisLine.Reverse());
		}
	}
	int stepCount = 0;
	string reducedString = startString;
	while (reducedString != "e")
	{
		replacements = replacements.OrderByDescending(x => RNG.Next()).ToList();
		foreach ((string, string) thisReplacement in replacements)
		{
			(string, int) result = Reduce(reducedString, thisReplacement);
			reducedString = result.Item1;
			stepCount += result.Item2;
		}
	}
	return stepCount;
}

static HashSet<string> GetNewStrings((string, string) replacement, string startString, int startIndex)
{
	int replacementIndex = startString[startIndex..].IndexOf(replacement.Item1);
	if (replacementIndex < 0)
	{
		return [];
	}
	replacementIndex += startIndex;
	int nextStartIndex = replacementIndex + replacement.Item1.Length;
	HashSet<string> returnSet;
	if (nextStartIndex < startString.Length)
	{
		returnSet = GetNewStrings(replacement, startString, nextStartIndex);
	}
	else
	{
		returnSet = [];
	}
	StringBuilder builder = new ();
	builder.Append(startString[..replacementIndex]);
	builder.Append(replacement.Item2);
	builder.Append(startString[nextStartIndex..]);
	returnSet.Add(builder.ToString());
	return returnSet;
}

static (string, int) Reduce(string reducedString, (string, string) replacement)
{
	if (reducedString.IndexOf(replacement.Item1) >= 0)
	{
		int count = reducedString.Split(replacement.Item1).Length - 1;
		string newString = reducedString.Replace(replacement.Item1, replacement.Item2);
		return (newString, count);
	}
	return (reducedString, 0);
}