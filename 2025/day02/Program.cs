using System.Diagnostics;

Console.WriteLine("Advent of Code 2025, Day 2");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	Stopwatch timer = new();
	timer.Start();
	long sum = 0L;
	string[] ranges = allLines[0].Split(',');
	foreach (string s in ranges)
	{
		string minString = s.Split('-')[0];
		string maxString = s.Split('-')[1];
		if (minString.Length == maxString.Length && minString.Length % 2 == 0)
		{
			sum += GetInvalidSum(minString, maxString);
		}		
		else if (minString.Length == maxString.Length && minString.Length % 2 == 1)
		{
			continue;
		}
		else if (minString.Length % 2 == 0 && maxString.Length % 2 == 1)
		{
			maxString = new string('9', minString.Length);
			sum += GetInvalidSum(minString, maxString);
		}
		else if (minString.Length % 2 == 1 && maxString.Length % 2 == 0)
		{
			minString = $"1{new string('0', maxString.Length - 1)}";
			sum += GetInvalidSum(minString, maxString);
		}
	}	
	timer.Stop();
	Console.WriteLine($"Part 1: {sum} ({timer.ElapsedMilliseconds} ms)");
}

static long GetInvalidSum(string minString, string maxString)
{
	long sum = 0L;
	long minLong = long.Parse(minString);
	long maxLong = long.Parse(maxString);	
	for (long i = long.Parse(minString[..(minString.Length / 2)]); i <= long.Parse(maxString[..(maxString.Length / 2)]); i++)
	{
		long target = long.Parse($"{i}{i}");
		if (minLong <= target && target <= maxLong)
		{
			sum += target;
		}
	}	
	return sum;
}

static void Part2(string[] allLines)
{
	Stopwatch timer = new();
	timer.Start();	
	long sum = 0L;
	string[] ranges = allLines[0].Split(',');
	foreach (string s in ranges)
	{
		string minString = s.Split('-')[0];
		string maxString = s.Split('-')[1];
		sum += GetInvalidSum2(minString, maxString);
	}
	timer.Stop();
	Console.WriteLine($"Part 2: {sum} ({timer.ElapsedMilliseconds} ms)");
}

static long GetInvalidSum2(string minString, string maxString)
{
	long sum = 0L;
	long minLong = long.Parse(minString);
	long maxLong = long.Parse(maxString);
	HashSet<long> history = [];
	for (long i = 1; i <= long.Parse(new string('9', maxString.Length / 2)); i++)
	{
		int j = int.Max(minString.Length / i.ToString().Length, 2);
		long target = long.Parse(string.Concat(Enumerable.Repeat(i.ToString(), j)));
		while (target <= maxLong)
		{
			if (minLong <= target && !history.Contains(target))
			{
				sum += target;
				history.Add(target);
			}
			j++;
			target = long.Parse(string.Concat(Enumerable.Repeat(i.ToString(), j)));
		}		
	}	
	return sum;
}
