using System.Diagnostics;

Console.WriteLine("Advent of Code 2025, Day 5");
string[] allLines = File.ReadAllLines("input.txt");
List<(long, long)> ranges = [];
string s = allLines[0];
int i = 0;
while (s.Length > 0)
{
	string[] tokens = s.Split('-');
	ranges.Add((long.Parse(tokens[0]), long.Parse(tokens[1])));
	i++;
	s = allLines[i];
}
List<long> ingredients = [];
for (int j = i + 1; j < allLines.Length; j++)
{
	ingredients.Add(long.Parse(allLines[j]));
}
Part1(ranges, ingredients);
Part2(ranges);

static void Part1(List<(long, long)> ranges, List<long> ingredients)
{
	Stopwatch timer = new();
	timer.Start();
	long result = 0L;	
	foreach (long x in ingredients)
	{
		foreach((long, long) y in ranges)
		{
			if (y.Item1 <= x && x <= y.Item2)
			{
				result++;
				break;
			}
		}
	}	
	timer.Stop();
	Console.WriteLine($"Part 1: {result} ({timer.ElapsedMilliseconds} ms)");
}

static void Part2(List<(long, long)> ranges)
{
	Stopwatch timer = new();
	timer.Start();
	long result = 0L;
	ranges = [..ranges.OrderBy(x => x.Item1)];
	List<(long, long)> mergedRanges = [];
	int i = 0;
	while (i < ranges.Count)
	{
		(long, long) newRange = (ranges[i].Item1, ranges[i].Item2);
		int j = i + 1;
		while (j < ranges.Count)
		{
			if (ranges[j].Item1 > newRange.Item2)
			{
				break;
			}
			newRange = (newRange.Item1, long.Max(ranges[j].Item2, newRange.Item2));
			i++;
			j++;
		}
		mergedRanges.Add(newRange);
		i++;
	}
	foreach ((long, long) r in mergedRanges)
	{
		result += r.Item2 - r.Item1 + 1;
	}	
	timer.Stop();
	Console.WriteLine($"Part 2: {result} ({timer.ElapsedMilliseconds} ms)");
}
