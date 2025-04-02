using System.Diagnostics;

Console.WriteLine("Advent of Code 2024, Day 19");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result);
Console.WriteLine($"Part 1: {part1Result}");
Part2(allLines, out long part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	string[] towels = allLines[0].Split(", ");
	Dictionary<string, bool> cache = [];
	long possibleCount = 0L;
	for (int i = 2; i < allLines.Length; i++)
	{
		possibleCount += IsPossible(allLines[i], 0, towels, cache) ? 1 : 0;
	}
	result = possibleCount;
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static void Part2(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	string[] towels = allLines[0].Split(", ");
	Dictionary<string, long> cache = [];
	long possibleCount = 0L;
	for (int i = 2; i < allLines.Length; i++)
	{
		possibleCount += CountPossible(allLines[i], 0, towels, cache);
	}
	result = possibleCount;
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static bool IsPossible(string pattern, int index, string[] towels, Dictionary<string, bool> cache)
{
	if (cache.TryGetValue(pattern[index..], out bool value))
	{
		return value;
	}
	foreach (string thisTowel in towels)
	{
		if (thisTowel.Length > pattern[index..].Length)
		{
			continue;
		}
		if (pattern[index..].StartsWith(thisTowel))
		{
			if (thisTowel.Length == pattern[index..].Length)
			{
				cache[pattern[index..]] = true;
				return true;
			}
			bool IsFuturePossible = IsPossible(pattern, index + thisTowel.Length, towels, cache);
			if (IsFuturePossible)
			{
				cache[pattern[index..]] = true;
				return true;
			}
		}
	}
	cache[pattern[index..]] = false;
	return false;
}

static long CountPossible(string pattern, int index, string[] towels, Dictionary<string, long> cache)
{
	if (cache.TryGetValue(pattern[index..], out long value))
	{
		return value;
	}
	long totalCount = 0L;
	foreach (string thisTowel in towels)
	{		
		if (thisTowel.Length > pattern[index..].Length)
		{
			continue;
		}
		if (pattern[index..].StartsWith(thisTowel))
		{
			if (thisTowel.Length == pattern[index..].Length)
			{
				totalCount += 1L;
			}
			else
			{
				totalCount += CountPossible(pattern, index + thisTowel.Length, towels, cache);
			}			
		}
	}
	cache[pattern[index..]] = totalCount;
	return totalCount;
}