using System.Diagnostics;

Console.WriteLine("Advent of Code 2025, Day 7");
string[] allLines = File.ReadAllLines("input.txt");
(int, int) start = (-1, -1);
Dictionary<(int, int), char> manifold = [];
for (int row = 0; row < allLines.Length; row++)
{
	for (int column = 0; column < allLines[0].Length; column++)
	{
		manifold[(row, column)] = allLines[row][column];
		if (allLines[row][column] == 'S')
		{
			start = (row, column);
		}
	}
}
Part1(manifold, start);
Part2(manifold, start, allLines.Length  - 1);

static void Part1(Dictionary<(int, int), char> manifold, (int, int) start)
{
	Stopwatch timer = new();
	timer.Start();
	long result = 0L;	
	Queue<(int, int)> queue = [];
	queue.Enqueue(start);
	HashSet<(int, int)> visited = [];
	while (queue.Count > 0)
	{
		(int, int) current = queue.Dequeue();
		if (visited.Contains(current))
		{
			continue;
		}
		visited.Add(current);
		if (manifold[current] == '^')
		{
			(int, int) next1 = (current.Item1, current.Item2 - 1);
			(int, int) next2 = (current.Item1, current.Item2 + 1);
			result++;
			if (manifold.ContainsKey(next1) && !visited.Contains(next1))
			{
				queue.Enqueue(next1);
			}
			if (manifold.ContainsKey(next2) && !visited.Contains(next2))
			{
				queue.Enqueue(next2);
			}
		}
		else
		{
			(int, int) next = (current.Item1 + 1, current.Item2);
			if (manifold.ContainsKey(next) && !visited.Contains(next))
			{
				queue.Enqueue(next);
			}
		}
	}	
	timer.Stop();
	Console.WriteLine($"Part 1: {result} ({timer.ElapsedMilliseconds} ms)");
}

static void Part2(Dictionary<(int, int), char> manifold, (int, int) start, int endRow)
{
	Stopwatch timer = new();
	timer.Start();
	long result = CountTimelines(manifold, start, endRow, []);		
	timer.Stop();
	Console.WriteLine($"Part 2: {result} ({timer.ElapsedMilliseconds} ms)");
}

static long CountTimelines(Dictionary<(int, int), char> manifold, (int, int) current, int endRow, Dictionary<(int, int), long> cache)
{
	if (cache.TryGetValue(current, out long oldResult))
	{
		return oldResult;
	}
	if (manifold[current] == '^')
	{
		(int, int) next1 = (current.Item1, current.Item2 - 1);
		(int, int) next2 = (current.Item1, current.Item2 + 1);
		if (next1.Item1 > endRow)
		{
			cache[current] = 1L;
			return 1L;
		}
		if (next1.Item2 > endRow)
		{
			cache[current] = 1L;
			return 1L;
		}
		long total = 0L;
		if (manifold.ContainsKey(next1))
		{
			long nextValue1 = CountTimelines(manifold, next1, endRow, cache);
			total += nextValue1;
		}
		if (manifold.ContainsKey(next2))
		{
			long nextValue2 = CountTimelines(manifold, next2, endRow, cache);
			total += nextValue2;
		}
		cache[current] = total;
		return total;
	}
	else
	{
		long total = 0L;
		(int, int) next = (current.Item1 + 1, current.Item2);
		if (next.Item1 > endRow)
		{
			cache[current] = 1L;
			return 1L;
		}
		if (manifold.ContainsKey(next))
		{
			long nextValue2 = CountTimelines(manifold, next, endRow, cache);
			total += nextValue2;
		}
		cache[current] = total;
		return total;
	}
}
