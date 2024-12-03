using System.Text.RegularExpressions;

Console.WriteLine("Advent of Code 2024, Day 3");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result);
Console.WriteLine($"Part 1: {part1Result}");
Part2(allLines, out long part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string[] allLines, out long result)
{
	int productSum = 0;
	Regex mulRegex = new(@"mul\(\d{1,3},\d{1,3}\)");
	foreach (string thisLine in allLines)
	{
		MatchCollection mc = mulRegex.Matches(thisLine);
		foreach (Match m in mc)
		{
			string argumentString = m.Value;
			argumentString = argumentString.Replace("mul(", "");
			argumentString = argumentString.Replace(")", "");
			int[] arguments = argumentString.Split(',').Select(x => int.Parse(x)).ToArray();
			productSum += arguments[0] * arguments[1];
		}
	}
	result = productSum;
}

static void Part2(string[] allLines, out long result)
{
	int productSum = 0;
	Regex mulRegex = new(@"mul\(\d{1,3},\d{1,3}\)");
	Regex doRegex = new(@"do\(\)");
	Regex dontRegex = new(@"don't\(\)");
	bool isEnabled = true;
	foreach (string thisLine in allLines)
	{
		List<(int, string)> reduced = [];
		MatchCollection mc = mulRegex.Matches(thisLine);
		foreach (Match m in mc)
		{
			string argumentString = m.Value;
			argumentString = argumentString.Replace("mul(", "");
			argumentString = argumentString.Replace(")", "");			
			int[] arguments = argumentString.Split(',').Select(x => int.Parse(x)).ToArray();
			reduced.Add((m.Index, (arguments[0] * arguments[1]).ToString()));			
		}
		mc = doRegex.Matches(thisLine);
		foreach (Match m in mc)
		{
			reduced.Add((m.Index, "true"));
		}
		mc = dontRegex.Matches(thisLine);
		foreach (Match m in mc)
		{
			reduced.Add((m.Index, "false"));
		}
		reduced = [.. reduced.OrderBy(x => x.Item1)];
		foreach ((int, string) x in reduced)
		{
			if (x.Item2 == "true")
			{
				isEnabled = true;
			}
			else if (x.Item2 == "false")
			{
				isEnabled = false;
			}
			else if (isEnabled)
			{
				productSum += int.Parse(x.Item2);
			}		
		}
	}
	result = productSum;
}
