using System.Diagnostics;

Console.WriteLine("Advent of Code 2024, Day 18");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result);
Console.WriteLine($"Part 1: {part1Result}");
Part2(allLines, out string part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	result = -1L;
	HashSet<(int, int)> traversible = [];
	for (int row = 0; row <= 70; row++)
	{
		for (int col = 0; col <= 70; col++)
		{
			traversible.Add((row, col));
		}
	}
	HashSet<(int, int)> obstacles = [];
	for (int i = 0; i < 1024; i++)
	{
		string[] thisLine = allLines[i].Split(",");
		int row = int.Parse(thisLine[0]);
		int col = int.Parse(thisLine[1]);
		obstacles.Add((row, col));
	}
	foreach ((int, int) thisObstacle in obstacles)
	{
		traversible.Remove(thisObstacle);
	}
	result = GetDistance(traversible);
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static void Part2(string[] allLines, out string result)
{
	Stopwatch timer = new ();
	timer.Start();
	result = "";
	HashSet<(int, int)> traversible = [];
	for (int row = 0; row <= 70; row++)
	{
		for (int col = 0; col <= 70; col++)
		{
			traversible.Add((row, col));
		}
	}
	HashSet<(int, int)> obstacles = [];
	for (int i = 0; i < 1024; i++)
	{
		string[] thisLine = allLines[i].Split(",");
		int row = int.Parse(thisLine[0]);
		int col = int.Parse(thisLine[1]);
		obstacles.Add((row, col));
	}
	foreach ((int, int) thisObstacle in obstacles)
	{
		traversible.Remove(thisObstacle);
	}
	int obstacleIndex = 1024;
	int thisDistance = GetDistance(traversible);
	while (thisDistance > -1)
	{
		int obstacleRow = int.Parse(allLines[obstacleIndex].Split(",")[0]);
		int obstacleCol = int.Parse(allLines[obstacleIndex].Split(",")[1]);
		traversible.Remove((obstacleRow, obstacleCol));
		thisDistance = GetDistance(traversible);
		if (thisDistance < 0)
		{
			result = obstacleRow.ToString() + "," + obstacleCol.ToString();
		}
		else
		{
			obstacleIndex++;
		}		
	}
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static int GetDistance(HashSet<(int, int)> traversible)
{
	(int, int)[] deltas = [(0, 1), (0, -1), (1, 0), (-1, 0)];
	(int, int) start = (0, 0);
	(int, int) end = (70, 70);
	HashSet<(int, int)> visited = [];
	Queue<((int, int), int)> queue = [];
	queue.Enqueue((start, 0));
	while (queue.Count > 0)
	{
		((int, int), int) current = queue.Dequeue();
		visited.Add(current.Item1);
		if (current.Item1 == end)
		{
			return current.Item2;
		}
		foreach ((int, int) thisDelta in deltas)
		{
			(int, int) next = (current.Item1.Item1 + thisDelta.Item1, current.Item1.Item2 + thisDelta.Item2);
			if (!visited.Contains(next) && !queue.Any(x => x.Item1 == next) && traversible.Contains(next))
			{
				queue.Enqueue((next, current.Item2 + 1));
			}
		}
	}
	return -1;
}
