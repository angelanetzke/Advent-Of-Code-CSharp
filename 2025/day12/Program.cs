using System.Diagnostics;

Console.WriteLine("Advent of Code 2025, Day 12");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines);

static void Part1(string[] allLines)
{
	Stopwatch timer = new();
	timer.Start();
	long result = 0L;
	List<int> blockCounts = [];
	int currentCount = 0;
	foreach (string s in allLines)
	{
		if (s.Length == 0)
		{
			blockCounts.Add(currentCount);
		}
		else if (s[1] == ':')
		{
			currentCount = 0;
		}
		else if (s.Contains('#'))
		{
			currentCount += s.Count(x => x == '#');
		}		 
		else if (s.Contains('x'))
		{
			string[] tokens = s.Split(": ");
			int width = int.Parse(tokens[0].Split('x')[0]);
			int height = int.Parse(tokens[0].Split('x')[1]);
			int[] presentCounts = [..tokens[1].Split(' ').Select(x => int.Parse(x))];
			int totalBlocks = 0;
			for (int i = 0; i < blockCounts.Count; i++)
			{
				totalBlocks += presentCounts[i] * blockCounts[i];
			}
			result += width * height >= totalBlocks ? 1L : 0L;				
		}
		
	}
	timer.Stop();
	Console.WriteLine($"Part 1: {result} ({timer.ElapsedMilliseconds} ms)");
}

