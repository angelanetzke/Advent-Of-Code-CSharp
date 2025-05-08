namespace Day23;

internal class Island
{
	private readonly Dictionary<(int, int), char> traversible = [];
	private readonly (int, int) start;
	private readonly (int, int) end;
	private int endID;
	private readonly Dictionary<int, List<(int, int)>> reduced = [];
	private static readonly Dictionary<(int, int), char> slopes = new ()
	{
		{(1, 0), 'v'},
		{(-1, 0), '^'},
		{(0, 1), '>'},
		{(0, -1), '<'}		
	};

	public Island(string[] allLines)
	{
		for (int row = 0; row < allLines.Length; row++)
		{
			for (int column = 0; column < allLines[row].Length; column++)
			{
				if (allLines[row][column] != '#')
				{
					traversible[(row, column)] = allLines[row][column];
					if (row == 0)
					{
						start = (row, column);
					}
					if (row == allLines.Length - 1)
					{
						end = (row, column);
					}
				}
			}
		}
	}

	public int Part1()
	{
		Reduce(true);
		return GetLongestDistance();
	}

	public int Part2()
	{
		Reduce(false);
		return GetLongestDistance();
	}

	private void Reduce(bool areSlopesSlippery)
	{
		// position, id
		Dictionary<(int, int), int> junctions = FindJunctions();
		foreach ((int, int) thisStartPosition in junctions.Keys)
		{
			reduced[junctions[thisStartPosition]] = [];
			Queue<((int, int), int)> queue = []; // position, distance
			queue.Enqueue((thisStartPosition, 0));
			HashSet<(int, int)> visited = [];
			while (queue.Count > 0)
			{
				((int, int) currentPosition, int currentDistance) = queue.Dequeue();
				visited.Add(currentPosition);
				if (currentPosition != thisStartPosition && junctions.ContainsKey(currentPosition))
				{
					reduced[junctions[thisStartPosition]].Add((junctions[currentPosition], currentDistance));
					continue;
				}
				if (currentPosition != thisStartPosition && currentPosition == end)
				{
					reduced[junctions[thisStartPosition]].Add((endID, currentDistance));
					continue;
				}
				foreach ((int, int) thisDirection in slopes.Keys)
				{
					(int, int) nextPosition = (currentPosition.Item1 + thisDirection.Item1, currentPosition.Item2 + thisDirection.Item2);
					if (!traversible.ContainsKey(nextPosition))
					{
						continue;
					}
					bool isTraversible;
					if (areSlopesSlippery)
					{
						isTraversible = traversible[nextPosition] == '.' || traversible[nextPosition] == slopes[thisDirection];
					}
					else
					{
						isTraversible = traversible.ContainsKey(nextPosition);
					}
					if (!isTraversible || visited.Contains(nextPosition))
					{
						continue;
					}
					queue.Enqueue((nextPosition, currentDistance + 1));
				}
			}
		}
	}

	private Dictionary<(int, int), int> FindJunctions()
	{
		Dictionary<(int, int), int> junctions = [];
		junctions[start] = 0;
		int nextID = 1;
		foreach ((int, int) thisPosition in traversible.Keys)
		{
			int directionCount = slopes.Keys
				.Count(x => traversible.ContainsKey((x.Item1 + thisPosition.Item1, x.Item2 + thisPosition.Item2)));
			if (directionCount > 2)
			{
				junctions[thisPosition] = nextID;
				nextID++;
			}
		}
		endID = nextID;
		return junctions;
	}

	private int GetLongestDistance()
	{
		Stack<(int, int, HashSet<int>)> stack = [];
		stack.Push((0, 0, []));
		int longestDistance = 0;
		while (stack.Count > 0)
		{
			(int currentID, int currentDistance, HashSet<int> visited) = stack.Pop();
			HashSet<int> nextVisited = [..visited];
			nextVisited.Add(currentID);
			if (!reduced.ContainsKey(currentID))
			{
				continue;
			}
			var toEnd = reduced[currentID].Where(x => x.Item1 == endID);
			if (toEnd.Any())
			{
				longestDistance = Math.Max(longestDistance, currentDistance + toEnd.First().Item2);
				continue;
			}
			foreach ((int, int) next in reduced[currentID])
			{
				if (nextVisited.Contains(next.Item1))
				{
					continue;
				}			
				stack.Push((next.Item1, currentDistance + next.Item2, nextVisited));
			}
		}
		return longestDistance;
	}

}