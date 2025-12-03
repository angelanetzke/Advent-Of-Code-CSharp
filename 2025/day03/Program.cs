using System.Diagnostics;

Console.WriteLine("Advent of Code 2025, Day 3");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	Stopwatch timer = new();
	timer.Start();
	long result = 0L;
	foreach (string s in allLines)
	{
		int first = 0;
		int last = s.Length - 2;
		long joltage = 0L;
		for (int i = 1; i <= 2; i++)
		{
			(int, int) values = GetMaxDigit(s, first, last);
			joltage = 10 * joltage + values.Item1;
			first = values.Item2 + 1;
			last++;
		}
		result += joltage;
	}	
	timer.Stop();
	Console.WriteLine($"Part 1: {result} ({timer.ElapsedMilliseconds} ms)");
}

static void Part2(string[] allLines)
{
	Stopwatch timer = new();
	timer.Start();	
	long result = 0L;
	foreach (string s in allLines)
	{
		int first = 0;
		int last = s.Length - 12;
		long joltage = 0L;
		for (int i = 1; i <= 12; i++)
		{
			(int, int) values = GetMaxDigit(s, first, last);
			joltage = 10 * joltage + values.Item1;
			first = values.Item2 + 1;
			last++;
		}
		result += joltage;
	}	
	timer.Stop();
	Console.WriteLine($"Part 2: {result} ({timer.ElapsedMilliseconds} ms)");
}

static (int, int) GetMaxDigit(string bank, int first, int last)
{
	int largestDigit = -1;
	int largestIndex = -1;
	for (int i = first; i <= last; i++)
	{
		int digit = int.Parse(bank[i].ToString());
		if (digit > largestDigit)
		{
			largestDigit = digit;
			largestIndex = i;
		}
	}
	return (largestDigit, largestIndex);
}
