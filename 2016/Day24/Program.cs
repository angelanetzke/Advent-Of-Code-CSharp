string[] allLines = File.ReadAllLines("input.txt");
(int, int) result = Part1AndPart2(allLines);
Console.WriteLine($"Part 1: {result.Item1}");
Console.WriteLine($"Part 2: {result.Item2}");

static (int, int) Part1AndPart2(string[] allLines)
{
	List<(char, int, int)> locations = []; // label, row, column
	HashSet<(int, int)> passages = [];
	for (int row = 0; row < allLines.Length; row++)
	{
		for (int column = 0; column < allLines[row].Length; column++)
		{
			if (allLines[row][column] != '#')
			{
				passages.Add((row, column));
			}
			if ('0' <= allLines[row][column] && allLines[row][column] <= '9')
			{
				locations.Add((allLines[row][column], row, column));
			}
		}
	}
	return GetShortestPath(locations, passages);
}

static (int, int) GetShortestPath(List<(char, int, int)> locations, HashSet<(int, int)> passages)
{
	Dictionary<char, List<(char, int)>> connections = GetConnections(locations, passages);
	Queue<(char, string, int)> queue = [];
	queue.Enqueue(('0', "0", 0));
	int shortestPath = int.MaxValue;
	int shortestCircularPath = int.MaxValue;
	while (queue.Count > 0)
	{
		(char currentLabel, string currentHistory, int currentDistance)	= queue.Dequeue();
		if (currentHistory.Length == locations.Count && currentDistance < shortestPath)
		{
			shortestPath = currentDistance;
		}
		if (currentHistory.Length == locations.Count + 1 && currentDistance < shortestCircularPath)
		{
			shortestCircularPath = currentDistance;
		}
		foreach ((char nextLabel, int distance) in connections[currentLabel])
		{
			if (currentHistory.Length == locations.Count && nextLabel == '0')
			{
				queue.Enqueue((nextLabel, currentHistory + nextLabel, currentDistance + distance));
			}
			if (!currentHistory.Contains(nextLabel))
			{
				queue.Enqueue((nextLabel, currentHistory + nextLabel, currentDistance + distance));
			}
		}
	}
	return (shortestPath, shortestCircularPath);
}

static Dictionary<char, List<(char, int)>> GetConnections(
	List<(char, int, int)> locations, HashSet<(int, int)> passages)
{
	Dictionary<char, List<(char, int)>> connections = [];
	(int, int)[] deltas = [ (0, 1), (0, -1), (1, 0), (-1, 0) ];
	foreach ((char startLabel, int startRow, int startColumn) in locations)
	{
		Queue<(int, int, int)> queue = [];
		queue.Enqueue((startRow, startColumn, 0));
		HashSet<(int, int)> visited = [];
		List<(char, int)> thisLabelConnections = [];
		while (queue.Count > 0)
		{
			(int row, int column, int distance) = queue.Dequeue();
			visited.Add((row, column));
			var labelsHere = locations.Where(x => x.Item2 == row && x.Item3 == column).Select(x => x.Item1);
			if (labelsHere.Count() == 1 && labelsHere.First() != startLabel)
			{
				thisLabelConnections.Add((labelsHere.First(), distance));
			}
			foreach ((int deltaRow, int deltaColumn) in deltas)
			{
				if (passages.Contains((row + deltaRow, column + deltaColumn)) 
					&& !visited.Contains((row + deltaRow, column + deltaColumn))
					&& !queue.Where(x => x.Item1 == row + deltaRow && x.Item2 == column + deltaColumn).Any())
				{
					queue.Enqueue((row + deltaRow, column + deltaColumn, distance + 1));
				}
			}
		}
		connections[startLabel] = thisLabelConnections;
	}
	return connections;
}