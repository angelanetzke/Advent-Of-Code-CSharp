string[] allLines = File.ReadAllLines("input.txt");
(int, int) result = Part1AndPart2(allLines);
Console.WriteLine($"Part 1: {result.Item1}");
Console.WriteLine($"Part 2: {result.Item2}");

static (int, int) Part1AndPart2(string[] allLines)
{
	Dictionary<string, List<(string, int)>> connections = [];
	foreach (string thisLine in allLines)
	{
		string[] parts = thisLine.Split(' ');
		string location1 = parts[0];
		string location2 = parts[2];
		int distance = int.Parse(parts[4]);
		if (connections.TryGetValue(location1, out List<(string, int)>? locationList1))
		{
			locationList1.Add((location2, distance));
		}
		else
		{
			connections[location1] = [(location2, distance)];
		}
		if (connections.TryGetValue(location2, out List<(string, int)>? locationList2))
		{
			locationList2.Add((location1, distance));
		}
		else
		{
			connections[location2] = [(location1, distance)];
		}
	}
	int locationCount = connections.Keys.Count;
	Queue<(string, int, HashSet<string>)> queue = [];
	foreach (string thisLocation in connections.Keys)
	{
		queue.Enqueue((thisLocation, 0, new HashSet<string>() { thisLocation }));
	}
	int shortestDistance = int.MaxValue;
	int longestDistance = int.MinValue;
	while (queue.Count > 0)
	{
		(string currentLocation, int totalDistance, HashSet<string> history) = queue.Dequeue();
		if (history.Count == locationCount)
		{
			shortestDistance = Math.Min(shortestDistance, totalDistance);
			longestDistance = Math.Max(longestDistance, totalDistance);
		}
		else
		{
			foreach ((string nextLocation, int nextDistance) in connections[currentLocation])
			{
				if (history.Contains(nextLocation))
				{
					continue;
				}
				HashSet<string> nextHistory = new (history);
				nextHistory.Add(nextLocation);
				queue.Enqueue((nextLocation, totalDistance + nextDistance, nextHistory));
			}
		}
	}
	return (shortestDistance, longestDistance);
}