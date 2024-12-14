using System.Text;

namespace Day14;

internal class Lobby
{
	private static readonly int width = 101;
	private static readonly int height = 103;
	private readonly List<(int, int, int, int)> robots = [];
	private readonly List<(long, long)> currentPositions = [];
	private readonly StringBuilder builder = new ();
	private static readonly (int, int)[] deltas = [(1, 0), (-1, 0), (0, 1), (0, -1)];

	public void AddRobot(int x, int y, int xSpeed, int ySpeed)
	{
		robots.Add((x, y, xSpeed, ySpeed));
	}

	public long Part1()
	{
		return Simulate(100);
	}

	public long Part2()
	{
		// Print current positions to see if result has a tree.
		// If result does not have a tree, adjust starting time to time at result.
		long time = 0L;
		while (!IsPossibleTree())
		{
			time++;
			Simulate(time);
		}
		Console.WriteLine(this);
		return time;
	}

	private long Simulate(long seconds)
	{
		currentPositions.Clear();
		foreach ((int x, int y, int xSpeed, int ySpeed) thisRobot in robots)
		{
			long newX = (thisRobot.x + seconds * (thisRobot.xSpeed + width)) % width;
			long newY = (thisRobot.y + seconds * (thisRobot.ySpeed + height)) % height;
			currentPositions.Add((newX, newY));
		}
		long midX = width / 2;
		long midY = height / 2;
		long safetyFactor = 1L;
		safetyFactor *= currentPositions.Count(r => r.Item1 < midX && r.Item2 < midY);
		safetyFactor *= currentPositions.Count(r => r.Item1 > midX && r.Item2 < midY);
		safetyFactor *= currentPositions.Count(r => r.Item1 < midX && r.Item2 > midY);
		safetyFactor *= currentPositions.Count(r => r.Item1 > midX && r.Item2 > midY);
		return safetyFactor;
	}

	private bool IsPossibleTree()
	{
		// Goal is arbitrary.
		int goal = 20;
		foreach ((long x, long y) thisCurrentPosition in currentPositions)
		{
			if (CountAdjacentRobots(thisCurrentPosition.x, thisCurrentPosition.y) >= goal)
			{
				return true;
			}
		}
		return false;
	}

	private int CountAdjacentRobots(long startX, long startY)
	{
		if (!currentPositions.Contains((startX, startY)))
		{
			return 0;
		}
		int count = 0;
		Queue<(long, long)> queue = [];
		HashSet<(long, long)> visited = [];
		queue.Enqueue((startX, startY));
		while (queue.Count > 0)
		{
			(long, long) current = queue.Dequeue();
			visited.Add(current);
			count++;
			var nextPositions = deltas.Select(x => (current.Item1 + x.Item1, current.Item2 + x.Item2))
				.Where(x => currentPositions.Contains(x))
				.Where(x => !queue.Contains(x))
				.Where(x => !visited.Contains(x));
			foreach ((long, long) thisNextPosition in nextPositions)
			{
				queue.Enqueue(thisNextPosition);
			}
		}
		return count;
	}

	public override string ToString()
	{
		builder.Clear();
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				if (currentPositions.Contains((x, y)))
				{
					builder.Append('*');
				}
				else
				{
					builder.Append('.');
				}
			}
			builder.Append('\n');
		}
		return builder.ToString();
	}

}