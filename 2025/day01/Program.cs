using System.Diagnostics;

Console.WriteLine("Advent of Code 2025, Day 1");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	Stopwatch timer = new();
	timer.Start();
	int dialSize = 100;
	int current = 50;
	long count = 0L;
	foreach (string s in allLines)
	{
		char direction = s[0];
		int distance = (int.Parse(s[1..]) % dialSize) * (direction == 'L' ? -1 : 1);
		current = (current + distance + dialSize) % dialSize;
		count += current == 0 ? 1L : 0L;
	}	
	timer.Stop();
	Console.WriteLine($"Part 1: {count} ({timer.ElapsedMilliseconds} ms)");
}

static void Part2(string[] allLines)
{
	Stopwatch timer = new();
	timer.Start();	
	int dialSize = 100;
	int current = 50;
	long count = 0L;
	foreach (string s in allLines)
	{
		char direction = s[0];
		int distance = int.Parse(s[1..]);
		int distanceToZero;
		if (direction == 'L')
		{
			distanceToZero = current;		
		}
		else
		{
			distanceToZero = current == 0 ? 0 : 100 - current;
		}
		count += distance / dialSize;		
		if (current > 0 && distance % dialSize >= distanceToZero)
		{
			count += 1L;
		}
		int adjustedDistance = (distance % dialSize) * (direction == 'L' ? -1 : 1);
		current = (current + adjustedDistance + dialSize) % dialSize;				
	}	
	timer.Stop();
	Console.WriteLine($"Part 2: {count} ({timer.ElapsedMilliseconds} ms)");
}
