using System.Text.RegularExpressions;
using System.Text;

var allLines = System.IO.File.ReadAllLines("input.txt");
var rules = new Dictionary<string, string>();
var inputStrings = new List<string>();
foreach (var thisLine in allLines)
{
	if (new Regex(@"^\d+:.+").IsMatch(thisLine))
	{
		string key = thisLine.Split(':')[0];
		string value = thisLine.Split(':')[1].Trim();
		rules[key] = value;
	}
	else if (thisLine.Length > 0)
	{
		inputStrings.Add(thisLine);
	}
}
Part1(rules, inputStrings);
Part2(rules, inputStrings);

static void Part1(Dictionary<string, string> rules, List<string> inputStrings)
{
	var theRegex = new Regex(@"^" + GetRegex(rules, rules["0"]) + "$");
	int matchCount = inputStrings.Count(thisString => theRegex.IsMatch(thisString));
	Console.WriteLine($"Part 1: {matchCount}");
}

static void Part2(Dictionary<string, string> rules, List<string> inputStrings)
{
	//rule changes:
	//8: 42 | 42 8
	//11: 42 31 | 42 11 31
	var rule42 = GetRegex(rules, rules["42"]);
	var rule31 = @GetRegex(rules, rules["31"]);
	int matchCount = 0;
	var theRegex = new Regex(@"^(" + rule42 + ")+(" + rule31 + ")+$");
	var start42Regex = new Regex(@"^(" + rule42 + "){1}");
	var start31Regex = new Regex(@"^(" + rule31 + "){1}");
	foreach (string thisString in inputStrings)
	{
		if (theRegex.IsMatch(thisString))
		{
			int rule42Count = 0;
			int rule31Count = 0;
			var countString = thisString;
			while(start42Regex.IsMatch(countString))
			{
				rule42Count++;
				countString = start42Regex.Replace(countString, String.Empty);
			}
			while (start31Regex.IsMatch(countString))
			{
				rule31Count++;
				countString = start31Regex.Replace(countString, String.Empty);
			}
			if (rule42Count > rule31Count)
			{
				matchCount++;
			}
		}
	}
	Console.WriteLine($"Part 2: {matchCount}");
}

static string GetRegex(Dictionary<string, string> ruleSet, string rule)
{
	rule = rule.Trim();
	if (rule[0] == '"')
	{
		return rule[1].ToString();
	}
	else if (rule.Contains('|'))
	{
		var builder = new StringBuilder();
		var components = rule.Split('|');
		for (int i = 0; i < components.Length; i++)
		{
			builder.Append('(');
			builder.Append(GetRegex(ruleSet, components[i]));
			builder.Append(')');
			if (i < components.Length - 1)
			{
				builder.Append('|');
			}
		}
		return builder.ToString();
	}
	else if (rule.Contains(' '))
	{
		var builder = new StringBuilder();
		var components = rule.Split(' ');
		for (int i = 0; i < components.Length; i++)
		{
			builder.Append('(');
			builder.Append(GetRegex(ruleSet, components[i]));
			builder.Append(')');
		}
		return builder.ToString();
	}
	else
	{
		return GetRegex(ruleSet, ruleSet[rule]);
	}
}

