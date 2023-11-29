using System.Text;
using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

void Part1(string[] allLines)
{
	var count = CountValidPhrases(allLines);
	Console.WriteLine($"Part 1: {count}");
}

void Part2(string[] allLines)
{
	var sortedLines = new string[allLines.Length];
	var builder = new StringBuilder();
	for (int i = 0; i < allLines.Length; i++)
	{
		builder.Clear();
		var words = allLines[i].Split(' ');
		foreach (string thisWord in words)
		{
			var wordCharArray = thisWord.ToArray();
			Array.Sort(wordCharArray);
			builder.Append(new String(wordCharArray) + " ");
		}
		sortedLines[i] = builder.ToString();
	}
	var count = CountValidPhrases(sortedLines);
	Console.WriteLine($"Part 2: {count}");
}

int CountValidPhrases(string[] allLines)
{
	var count = 0;
	foreach (string thisLine in allLines)
	{
		var isValid = true;
		var words = thisLine.Trim().Split(' ');
		foreach (string thisWord in words)
		{
			var wordRegex = new Regex(@"\b" + @thisWord + @"\b");
			var matchCount = wordRegex.Matches(thisLine).Count;
			if (matchCount > 1)
			{
				isValid = false;
				break;
			}
		}
		count += isValid ? 1 : 0;
	}
	return count;
}
