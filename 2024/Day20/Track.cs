namespace  Day20;

internal class Track
{
	private readonly (int, int) start;
	private readonly HashSet<(int, int)> traversible = [];
	private readonly List<(int, int)> walls = [];
	private static readonly (int, int)[] deltas = [(0, 1), (0, -1), (1, 0), (-1, 0)];
	private readonly Dictionary<(int, int), int> distances = [];

	public Track(string[] allLines)
	{
		for (int row = 0; row < allLines.Length; row++)
		{
			for (int column = 0; column < allLines[row].Length; column++)
			{
				if (allLines[row][column] == 'S')
				{
					start = (row, column);
					traversible.Add((row, column));
				}
				else if (allLines[row][column] == 'E')
				{
					traversible.Add((row, column));
				}
				else if (allLines[row][column] == '.')
				{					
					traversible.Add((row, column));
				}
				else if (row != 0 && row != allLines.Length - 1 && column != 0 && column != allLines[row].Length - 1)
				{
					walls.Add((row, column));
				}
			}
		}
	}

	public int Part1()
	{
		int count = 0;
		int targetImprovement = 100;
		SetDistances();
		foreach ((int, int) thisCheatWall in walls)
		{
			count += GetImprovement(thisCheatWall) >= targetImprovement ? 1 : 0;
		}
		return count;
	}

	private void SetDistances()
	{
		HashSet<(int, int)> visited = [];
		Queue<((int, int), int)> queue = [];
		queue.Enqueue((start, 0));
		while (queue.Count > 0)
		{
			((int, int), int) current = queue.Dequeue();
			visited.Add(current.Item1);
			distances[current.Item1] = current.Item2;
			foreach ((int, int) thisDelta in deltas)
			{
				(int, int) next = (current.Item1.Item1 + thisDelta.Item1, current.Item1.Item2 + thisDelta.Item2);
				if (traversible.Contains(next) && !visited.Contains(next) && !queue.Any(x => x.Item1 == next))
				{
					queue.Enqueue((next, current.Item2 + 1));
				}
			}
		}
	}

	private int GetImprovement((int, int) cheatWall)
	{
		(int, int) left = (cheatWall.Item1, cheatWall.Item2 - 1);
		(int, int) right = (cheatWall.Item1, cheatWall.Item2 + 1);
		if (distances.TryGetValue(left, out int leftDistance) && distances.TryGetValue(right, out int rightDistance))
		{
			return Math.Abs(rightDistance - leftDistance) - 1;
		}
		(int, int) up = (cheatWall.Item1 - 1, cheatWall.Item2);
		(int, int) down = (cheatWall.Item1 + 1, cheatWall.Item2);
		if (distances.TryGetValue(up, out int upDistance) && distances.TryGetValue(down, out int downDistance))
		{
			return Math.Abs(upDistance - downDistance) - 1;
		}
		return 0;
	}
}