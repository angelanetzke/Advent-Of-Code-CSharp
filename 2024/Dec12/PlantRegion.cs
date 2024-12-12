namespace Day12;

internal class PlantRegion (string[] data)
{
	private readonly string[] data = data;
	private readonly HashSet<(int, int)> members = [];
	private static readonly HashSet<(int, int)> remaining = [];
	private static readonly (int, int)[] deltas = [(1, 0), (-1, 0), (0, 1), (0, -1)];

	public static void AddRemaining((int, int) newRemaining)
	{
		remaining.Add(newRemaining);
	}

	public static bool AreRemaining()
	{
		return remaining.Count > 0;
	}

	public void FindRegion()
	{
		if (remaining.Count == 0)
		{
			return;
		}
		(int, int) start = remaining.First();
		char startChar = data[start.Item1][start.Item2];
		Queue<(int, int)> queue = [];
		queue.Enqueue(start);
		while (queue.Count > 0)
		{
			(int, int) current = queue.Dequeue();
			remaining.Remove(current);
			members.Add(current);
			var nextPositions = deltas.Select(x => (current.Item1 + x.Item1, current.Item2 + x.Item2))
				.Where(x => remaining.Contains(x))
				.Where(x => data[x.Item1][x.Item2] == startChar)
				.Where(x => !members.Contains(x))
				.Where(x => !queue.Contains(x));
			foreach ((int, int) thisNextPosition in nextPositions)
			{
				queue.Enqueue(thisNextPosition);
			}
		}
	}

	public int GetArea()
	{
		return members.Count;
	}

	public int GetPerimeter()
	{
		int perimeterSize = 0;
		foreach ((int, int) thisMember in members)
		{
			perimeterSize += deltas.Select(x => (thisMember.Item1 + x.Item1, thisMember.Item2 + x.Item2))
				.Where(x => !members.Contains(x))
				.Count();
		}
		return perimeterSize;
	}

	public int GetSideCount()
	{
		return GetHorizontalSideCount() + GetVerticalSideCount();
	}

	private int GetHorizontalSideCount()
	{
		int sideCount = 0;
		int minRow = members.Select(x => x.Item1).Min();
		int maxRow = members.Select(x => x.Item1).Max() + 1;
		int minColumn = members.Select(x => x.Item2).Min();
		int maxColumn = members.Select(x => x.Item2).Max();
		for (int row = minRow; row <= maxRow; row++)
		{
			bool isLineHere = false;
			for (int column = minColumn; column <= maxColumn; column++)
			{
				if (isLineHere)
				{
					if (members.Contains((row, column)) && members.Contains((row - 1, column)))
					{
						isLineHere = false;
					}
					if (!members.Contains((row, column)) && !members.Contains((row - 1, column)))
					{
						isLineHere = false;
					}
					if (isLineHere)
					{
						if (members.Contains((row, column)) && !members.Contains((row, column - 1)))
						{
							sideCount++;
						}
						if (!members.Contains((row, column)) && members.Contains((row, column - 1)))
						{
							sideCount++;
						}
					}
				}
				else
				{
					if (members.Contains((row, column)) && !members.Contains((row - 1, column)))
					{
						sideCount++;
						isLineHere = true;
					}
					if (!members.Contains((row, column)) && members.Contains((row - 1, column)))
					{
						sideCount++;
						isLineHere = true;
					}
				}
			}
		}
		return sideCount;
	}

	private int GetVerticalSideCount()
	{
		int sideCount = 0;
		int minRow = members.Select(x => x.Item1).Min();
		int maxRow = members.Select(x => x.Item1).Max();
		int minColumn = members.Select(x => x.Item2).Min();
		int maxColumn = members.Select(x => x.Item2).Max() + 1;
		for (int column = minColumn; column <= maxColumn; column++)
		{
			bool isLineHere = false;
			for (int row = minRow; row <= maxRow; row++)
			{
				if (isLineHere)
				{
					if (members.Contains((row, column)) && members.Contains((row, column - 1)))
					{
						isLineHere = false;
					}
					if (!members.Contains((row, column)) && !members.Contains((row, column - 1)))
					{
						isLineHere = false;
					}
					if (isLineHere)
					{
						if (members.Contains((row, column)) && !members.Contains((row - 1, column)))
						{
							sideCount++;
						}
						if (!members.Contains((row, column)) && members.Contains((row - 1, column)))
						{
							sideCount++;
						}
					}
				}
				else
				{
					if (members.Contains((row, column)) && !members.Contains((row, column - 1)))
					{
						sideCount++;
						isLineHere = true;
					}
					if (!members.Contains((row, column)) && members.Contains((row, column - 1)))
					{
						sideCount++;
						isLineHere = true;
					}
				}
			}			
		}
		return sideCount;
	}
	
}