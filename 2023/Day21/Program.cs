string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static long Part1(string[] garden)
{
	return GetCount(garden, 0, garden.Length / 2, garden.Length / 2, 64);
}

static long Part2(string[] garden)
{
	long targetSteps = 26501365;
	long layers = (targetSteps - garden.Length / 2) / garden.Length + 1;
	long typeACount = GetCount(garden, 0, garden.Length / 2, garden.Length / 2, garden.Length * 2 + 1);
	long typeBCount = GetCount(garden, 0, garden.Length / 2, garden.Length / 2, garden.Length * 2);
	long gardenPlotCount = typeACount;
	long distance = garden.Length / 2;
	for (int i = 1; i < layers - 1; i++)
	{
		gardenPlotCount += 4 * i * (i % 2 == 0 ? typeACount : typeBCount);
		distance += garden.Length;
	}
	long startDistance = distance + 1;
	gardenPlotCount += GetCount(garden, startDistance, garden.Length / 2, 0, targetSteps);
	gardenPlotCount += GetCount(garden, startDistance, garden.Length / 2, garden.Length - 1, targetSteps);
	gardenPlotCount += GetCount(garden, startDistance, 0, garden.Length / 2, targetSteps);
	gardenPlotCount += GetCount(garden, startDistance, garden.Length - 1, garden.Length / 2, targetSteps);
	long diagonals = (4 * (layers - 1) - 4) / 4;
	startDistance = distance - garden.Length / 2 + 1;
	gardenPlotCount += diagonals * GetCount(garden, startDistance, 0, 0, targetSteps);
	gardenPlotCount += diagonals * GetCount(garden, startDistance, 0, garden.Length - 1, targetSteps);
	gardenPlotCount += diagonals * GetCount(garden, startDistance, garden.Length - 1, 0, targetSteps);
	gardenPlotCount += diagonals * GetCount(garden, startDistance, garden.Length - 1, garden.Length - 1, targetSteps);
	diagonals++;
	startDistance = distance + garden.Length / 2 + 2;
	gardenPlotCount += diagonals * GetCount(garden, startDistance, 0, 0, targetSteps);
	gardenPlotCount += diagonals * GetCount(garden, startDistance, 0, garden.Length - 1, targetSteps);
	gardenPlotCount += diagonals * GetCount(garden, startDistance, garden.Length - 1, 0, targetSteps);
	gardenPlotCount += diagonals * GetCount(garden, startDistance, garden.Length - 1, garden.Length - 1, targetSteps);
	return gardenPlotCount;
}

static long GetCount(string[] garden, long startCount, int startRow, int startColumn, long targetSteps)
{
	if (startCount > targetSteps)
	{
		return 0;
	}
	(int, int) start = (startRow, startColumn);
	int gardenPlotCount = 0;
	HashSet<(int, int)> visited = [];
	Queue<(int, int, long)> queue = [];
	(int, int, long) current = (start.Item1, start.Item2, startCount);
	queue.Enqueue(current);
	List<(int, int)> offsets = [(-1, 0), (1, 0), (0, -1), (0, 1)];
	while (queue.Count > 0)
	{
		current = queue.Dequeue();
		visited.Add((current.Item1, current.Item2));
		if (current.Item3 % 2 == targetSteps % 2)
		{
			gardenPlotCount++;
		}
		foreach ((int, int) thisOffset in offsets)
		{
			(int, int, long) next 
				= (current.Item1 + thisOffset.Item1, current.Item2 + thisOffset.Item2, current.Item3 + 1);
			if (next.Item1 < 0 || next.Item1 >= garden.Length || next.Item2 < 0 || next.Item2 >= garden.Length)
			{
				continue;
			}
			if (visited.Any(v => v.Item1 == next.Item1 && v.Item2 == next.Item2)
				|| queue.Any(q => q.Item1 == next.Item1 && q.Item2 == next.Item2)
				|| garden[next.Item1][next.Item2] == '#')
			{
				continue;
			}
			if (next.Item3 <= targetSteps)
			{
				queue.Enqueue(next);
			}
		}
	}
	return gardenPlotCount;
}
