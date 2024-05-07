using System.Text.RegularExpressions;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static int Part1(string[] allLines)
{
	List<Regex> regexList = [
		new Regex(@"\\x[0-9a-f]{2}"),
		new Regex(@"\\\\"),
		new Regex(@"\\""")
	];
	int codeCharacters = 0;
	int memoryCharacters = 0;
	foreach (string thisLine in allLines)
	{
		codeCharacters += thisLine.Length;
		string thisLineEdited = thisLine[1..^1];
		foreach (Regex thisRegex in regexList)
		{
			thisLineEdited = thisRegex.Replace(thisLineEdited, "_");
		}
		memoryCharacters += thisLineEdited.Length;
	}
	return codeCharacters - memoryCharacters;
}

static int Part2(string[] allLines)
{
	int codeCharacters = 0;
	int memoryCharacters = 0;
	foreach (string thisLine in allLines)
	{
		memoryCharacters += thisLine.Length;
		int escapeCount = thisLine.Count(x => x == '\\' || x == '"');
		codeCharacters += thisLine.Length + escapeCount + 2;
	}
	return codeCharacters - memoryCharacters;
}