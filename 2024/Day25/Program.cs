using System.Diagnostics;

Console.WriteLine("Advent of Code 2024, Day 25");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result);
Console.WriteLine($"Part 1: {part1Result}");

static void Part1(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	List<int[]> keys = [];
	List<int[]> locks = [];
	int i = 0;
	while (i < allLines.Length)
	{
		if (allLines[i] == "#####")
		{
			int[] thisLock = [0, 0, 0, 0, 0];
			for (int j = 1; j < 7; j++)
			{
				for (int k = 0; k < 5; k++)
				{
					if (allLines[i +j][k] == '#')
					{
						thisLock[k]++;
					}
				}
			}
			locks.Add(thisLock);
			i += 8;					
		}
		else if (allLines[i] == ".....")
		{
			int[] thisKey = [5, 5, 5, 5, 5];
			for (int j = 1; j < 7; j++)
			{
				for (int k = 0; k < 5; k++)
				{
					if (allLines[i +j][k] == '.')
					{
						thisKey[k]--;
					}
				}
			}
			keys.Add(thisKey);
			i += 8;		
		}
	}
	int count = 0;
	foreach (int[] thisKey in keys)
	{
		foreach (int[] thisLock in locks)
		{
			bool isMatch = true;
			for (int index = 0; index < 5; index++)
			{
				if (thisKey[index] + thisLock[index] > 5)
				{
					isMatch = false;
					break;
				}
			}
			count += isMatch ? 1 : 0;
		}
	}
	result = count;
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

