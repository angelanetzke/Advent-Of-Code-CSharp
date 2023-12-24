string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");

static int Part1(string[] garden)
{
	(int, int) start = (-1, -1);
	for (int row = 0; row < garden.Length; row++)
	{
		int startColumn = garden[row].IndexOf('S');
		if (startColumn > -1)
		{
			start = (row, startColumn);
		}
	}
	int gardenPlotCount = 0;
	HashSet<(int, int)> visited = [];
	Queue<(int, int)> queue = [];
	(int, int) current = start;
	queue.Enqueue(current);
	Dictionary<(int, int), int> distances = [];
	distances[start] = 0;
	List<(int, int)> offsets = [(-1, 0), (1, 0), (0, -1), (0, 1)];
	while (queue.Count > 0)
	{
		current = queue.Dequeue();
		visited.Add(current);
		if (distances[current] % 2 == 0)
		{
			gardenPlotCount++;
		}
		foreach ((int, int) thisOffset in offsets)
		{
			(int, int) next = (current.Item1 + thisOffset.Item1, current.Item2 + thisOffset.Item2);
			if (visited.Contains(next) || queue.Contains(next) || garden[next.Item1][next.Item2] == '#')
			{
				continue;
			}
			distances[next] = distances[current] + 1;
			if (distances[next] <= 64)
			{
				queue.Enqueue(next);
			}
		}
	}
	return gardenPlotCount;
}