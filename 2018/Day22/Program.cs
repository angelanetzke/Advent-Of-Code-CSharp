using Day22;

var allLines = File.ReadAllLines("input.txt");
var depth = int.Parse(allLines[0].Split(' ')[1]);
var targetX = int.Parse(allLines[1].Split(' ')[1].Split(',')[0]);
var targetY = int.Parse(allLines[1].Split(' ')[1].Split(',')[1]);
Part1(depth, targetX, targetY);
Part2();

static void Part1(int depth, int targetX, int targetY)
{
	Geology.depth = depth;
	Geology.targetLocation = (targetX, targetY);
	var totalRisk = 0;
	for (int x = 0; x <= targetX; x++)
	{
		for (int y = 0; y <= targetY; y++)
		{
			totalRisk += Geology.GetErosionLevel(x, y) % 3;
		}
	}
	Console.WriteLine($"Part 1: {totalRisk}");
}

static void Part2()
{
	var queue = new HashSet<Region>();
	queue.Add(new Region());
	var visited = new HashSet<Region>();
	while (queue.Count > 0)
	{
		var current = queue.Min();
		if (current.IsEnd())
		{
			Console.WriteLine($"Part 2: {current.GetDistanceToHere()}");
			break;
		}
		queue.Remove(current);
		visited.Add(current);
		var neighbors = current.GetNeighbors();
		foreach (Region thisNeighbor in neighbors)
		{
			if (visited.Contains(thisNeighbor))
			{
				continue;
			}
			var previous = queue.Where(x => x.Equals(thisNeighbor));
			if (previous.Count() > 0
				&& thisNeighbor.GetDistanceToHere() < previous.First().GetDistanceToHere())
			{
				queue.Remove(previous.First());
			}
			queue.Add(thisNeighbor);
		}
	}
}
