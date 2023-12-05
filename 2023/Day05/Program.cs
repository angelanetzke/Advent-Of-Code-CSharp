using Day05;
using System.Text.RegularExpressions;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static long Part1(string[] allLines)
{
	ValueMapper mapper = new ();
	List<long> values = [];
	foreach (string thisLine in allLines)
	{
		if (thisLine.StartsWith("seeds:"))
		{
			MatchCollection matches = Regex.Matches(thisLine, @" (?<thisValue>\d+)");
			values = matches.Select(x => long.Parse(x.Groups["thisValue"].Value)).ToList();
		}
		else if (thisLine.Contains(':'))
		{
			mapper.Clear();
		}
		else if (Regex.IsMatch(thisLine, @"^\d+ \d+ \d+$"))
		{
			mapper.AddRange(thisLine);
		}
		else if (thisLine.Length == 0 && mapper.GetSize() > 0)
		{
			values = mapper.MapToNextValues(values);
		}
	}
	values = mapper.MapToNextValues(values);
	return values.Min();
}

static long Part2(string[] allLines)
{
	ValueMapper mapper = new ();
	List<ValueRange> values = [];
	foreach (string thisLine in allLines)
	{
		if (thisLine.StartsWith("seeds:"))
		{
			List<Match> matches = Regex.Matches(thisLine, @" (?<thisValue>\d+)").ToList();
			for (int i = 0; i < matches.Count; i += 2)
			{
				values.Add(new ValueRange(long.Parse(matches[i].Value), long.Parse(matches[i + 1].Value)));
			}			
		}
		else if (thisLine.Contains(':'))
		{
			mapper.Clear();
		}
		else if (Regex.IsMatch(thisLine, @"^\d+ \d+ \d+$"))
		{
			mapper.AddRange(thisLine);
		}
		else if (thisLine.Length == 0 && mapper.GetSize() > 0)
		{
			values = mapper.MapToNextRanges(values);
		}
	}
	values = mapper.MapToNextRanges(values);
	values.Sort();
	return values[0].Min;
}
