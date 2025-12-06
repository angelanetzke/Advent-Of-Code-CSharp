using System.Diagnostics;
using System.Text.RegularExpressions;

Console.WriteLine("Advent of Code 2025, Day 6");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	Stopwatch timer = new();
	timer.Start();
	long result = 0L;
	List<List<long>> values = [];
	foreach (string s in allLines)
	{
		List<long> row = [];
		MatchCollection matches = new Regex(@"\d+").Matches(s);
		if (matches.Count > 0)
		{
			foreach (Match m in matches)
			{
				row.Add(long.Parse(m.Value));
			}
			values.Add(row);	
		}
		else
		{
			matches = new Regex(@"[+*]").Matches(s);
			int column = 0;
			foreach (Match m in matches)
			{
				long columnResult = m.Value == "+" ? 0L : 1L;
				for (int i = 0; i < values.Count; i++)
				{
					if (m.Value == "+")
					{
						columnResult += values[i][column];
					}
					else
					{
						columnResult *= values[i][column];
					}
				}
				result += columnResult;
				column++;
			}	
		}		
	}	
	timer.Stop();
	Console.WriteLine($"Part 1: {result} ({timer.ElapsedMilliseconds} ms)");
}

static void Part2(string[] allLines)
{
	Stopwatch timer = new();
	timer.Start();	
	long result = 0L;
	List<long> values = [];
	for (int column = allLines[0].Length - 1; column >=0; column--)
	{
		long currentValue = 0L;
		for (int row = 0; row < allLines.Length; row++)
		{
			if ('0' <= allLines[row][column] && allLines[row][column] <= '9')
			{
				currentValue = 10 * currentValue + long.Parse(allLines[row][column].ToString());
			}
			if (allLines[row][column] == '+')
			{
				values.Add(currentValue);
				result += values.Sum();
				values.Clear();
				currentValue = 0L;
			}
			if (allLines[row][column] == '*')
			{
				values.Add(currentValue);
				result += values.Aggregate(1L, (acc, x) => acc * x);
				values.Clear();
				currentValue = 0L;
			}
		}
		if (currentValue > 0)
		{
			values.Add(currentValue);	
		}		
	}	
	timer.Stop();
	Console.WriteLine($"Part 2: {result} ({timer.ElapsedMilliseconds} ms)");
}
