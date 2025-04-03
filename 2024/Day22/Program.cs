using System.Diagnostics;

Console.WriteLine("Advent of Code 2024, Day 22");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result);
Console.WriteLine($"Part 1: {part1Result}");
Part2(allLines, out long part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	long sum = 0L;
	foreach (string thisLine in allLines)
	{
		long secretNumber = long.Parse(thisLine);
		for (int i = 1; i <= 2000; i++)
		{
			secretNumber = GetNextSecretNumber(secretNumber);
		}
		sum += secretNumber;
	}
	result = sum;
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static void Part2(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();	
	Dictionary<(int, int, int, int), int> bananas = [];
	Dictionary<int, HashSet<(int, int, int, int)>> seen = [];
	List<int> lastFour = [];
	for (int i = 0; i < allLines.Length; i++)
	{
		lastFour.Clear();
		seen[i] = [];
		long secretNumber = long.Parse(allLines[i]);
		int previousDigit = (int)(secretNumber % 10);
		for (int j = 1; j <= 2000; j++)
		{			
			secretNumber = GetNextSecretNumber(secretNumber);
			if (lastFour.Count == 4)
			{
				lastFour.RemoveAt(0);
			}
			int thisDigit = (int)(secretNumber % 10);
			lastFour.Add(thisDigit - previousDigit);
			previousDigit = thisDigit;
			if (lastFour.Count == 4)
			{
				(int, int, int, int) bananaKey = (lastFour[0], lastFour[1], lastFour[2], lastFour[3]);
				if (seen[i].Contains(bananaKey))
				{
					continue;
				}
				seen[i].Add(bananaKey);
				if (bananas.TryGetValue(bananaKey, out int oldCount))
				{
					bananas[bananaKey] = oldCount + thisDigit;
				}
				else
				{
					bananas[bananaKey] = thisDigit;
				}
			}			
		}
	}
	result = bananas.Values.Max();
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static long GetNextSecretNumber(long current)
{
	long result = ((current * 64) ^ current) % 16777216;
	result = ((result / 32) ^ result) % 16777216;
	result = ((result * 2048) ^ result) % 16777216;
	return result;
}
