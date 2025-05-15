namespace Dec24;

internal class RecursiveGrid
{
	private HashSet<(int, int, int)> current = [];
	private static readonly Dictionary<(int, int, int), List<(int, int)>> neighbors = new ()
	{
		{(-1, 1, 0), [(3, 2)]},
		{(-1, -1, 0), [(1, 2)]},
		{(-1, 0, 1), [(2, 3)]},
		{(-1, 0, -1), [(2, 1)]},
		{(1, 1, 0), [(0, 0), (0, 1), (0, 2), (0, 3), (0, 4)]},
		{(1, -1, 0), [(4, 0), (4, 1), (4, 2), (4, 3), (4, 4)]},
		{(1, 0, 1), [(0, 0), (1, 0), (2, 0), (3, 0), (4, 0)]},
		{(1, 0, -1), [(0, 4), (1, 4), (2, 4), (3, 4), (4, 4)]}
	};

	public RecursiveGrid(string[] allLines)
	{
		for (int row = 0; row < 5; row++)
		{
			for (int column = 0; column < 5; column++)
			{
				if (allLines[row][column] == '#')
				{
					current.Add((0, row, column));
				}
			}
		}
	}

	public long Part2()
	{
		for (int i = 1; i <= 200; i++)
		{
			Iterate();
		}
		return current.Count;
	}

	private void Iterate()
	{
		HashSet<(int, int, int)> next = [];
		int minLevel = current.Select(x => x.Item1).Min();
		int maxLevel = current.Select(x => x.Item1).Max();
		for (int level = minLevel - 1; level <= maxLevel + 1; level++)
		{
			for (int row = 0; row < 5; row++)
			{
				for (int column = 0; column < 5; column++)
				{
					if (row == 2 && column == 2)
					{
						continue;
					}
					int neighborCount = CountNeighbors(level, row, column);
					if (current.Contains((level, row, column)))
					{
						if (neighborCount == 1)
						{
							next.Add((level, row, column));
						}
					}
					else
					{
						if (neighborCount == 1 || neighborCount == 2)
						{
							next.Add((level, row, column));
						}
					}
				}
			}
		}
		current = next;
	}

	private int CountNeighbors(int level, int row, int column)
	{
		int count = 0;
		foreach ((int, int) thisDelta in new (int, int)[] {(1, 0), (-1, 0), (0, 1), (0, -1)})
		{
			(int, int, int) thisNeighbor = (level, row + thisDelta.Item1, column + thisDelta.Item2);
			if (thisNeighbor.Item2 < 0 || thisNeighbor.Item2 > 4
				|| thisNeighbor.Item3 < 0 || thisNeighbor.Item3 > 4)
			{
				foreach ((int, int) thisNeighbor2 in neighbors[(-1, thisDelta.Item1, thisDelta.Item2)])
				{
					count += current.Contains((level - 1 , thisNeighbor2.Item1, thisNeighbor2.Item2)) ? 1 : 0;
				}
			}
			else if (thisNeighbor.Item2 == 2 && thisNeighbor.Item3 == 2)
			{
				foreach ((int, int) thisNeighbor2 in neighbors[(1, thisDelta.Item1, thisDelta.Item2)])
				{
					count += current.Contains((level + 1, thisNeighbor2.Item1, thisNeighbor2.Item2)) ? 1 : 0;
				}
			}
			else
			{
				count += current.Contains(thisNeighbor) ? 1 : 0;
			}
		}
		return count;
	}

}