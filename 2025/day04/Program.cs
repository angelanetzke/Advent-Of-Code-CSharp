using System.Diagnostics;

Console.WriteLine("Advent of Code 2025, Day 4");
string[] allLines = File.ReadAllLines("input.txt");
HashSet<(int, int)> paperRolls = [];
for (int row = 0; row < allLines.Length; row++)
{
	for (int column = 0; column < allLines[0].Length; column++)
	{
		if (allLines[row][column] == '@')
		{
			paperRolls.Add((row, column));
		}
	}
}
Part1(paperRolls);
Part2(paperRolls);

static void Part1(HashSet<(int, int)> paperRolls)
{
	Stopwatch timer = new();
	timer.Start();
	long result = 0L;
	foreach ((int, int) pr in paperRolls)
	{
		result += CountNeighbors(paperRolls, pr.Item1, pr.Item2) < 4 ? 1 : 0;
	}
	timer.Stop();
	Console.WriteLine($"Part 1: {result} ({timer.ElapsedMilliseconds} ms)");
}

static void Part2(HashSet<(int, int)> paperRolls)
{
	Stopwatch timer = new();
	timer.Start();	
	long result = 0L;
	HashSet<(int, int)> current = [..paperRolls];
	bool hasBeenRemoved = true;
	while (hasBeenRemoved)
	{
		hasBeenRemoved = false;
		HashSet<(int, int)> next = [];
		foreach ((int, int) pr in current)
		{
			if (CountNeighbors(current, pr.Item1, pr.Item2) < 4)
			{
				hasBeenRemoved = true;
				result++;
			}
			else
			{
				next.Add(pr);
			}
		}
		current = next;
	}
	timer.Stop();
	Console.WriteLine($"Part 2: {result} ({timer.ElapsedMilliseconds} ms)");
}

static int CountNeighbors(HashSet<(int, int)> paperRolls, int row, int column)
{
	int count = 0;
	for (int deltaRow = -1; deltaRow <= 1; deltaRow++)
	{
		for (int deltaColumn = -1; deltaColumn <= 1; deltaColumn++)
		{
			if (deltaRow == 0 && deltaColumn == 0)
			{
				continue;
			}
			count += paperRolls.Contains((row + deltaRow, column + deltaColumn)) ? 1 : 0;
		}
	}
	return count;
}
