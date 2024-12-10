namespace Day10;

internal class TopologicalMap(string[] data)
{
	private readonly List<(int, int)> startPositions = data
		.SelectMany((line, rowIndex) =>
			line.Select((character, columnIndex) => (Row: rowIndex, Column: columnIndex, Character: character))
		)
		.Where(x => x.Character == '0')
		.Select(x => (x.Row, x.Column))
		.ToList();
	private readonly int height = data.Length;
	private readonly int width = data[0].Length;
	private readonly string[] data = data;
	private static readonly (int, int)[] deltas = [(1, 0), (-1, 0), (0, 1), (0, -1)];

	public long Part1()
	{
		long totalScore = 0L;
		foreach ((int row, int column) thisStartPosition in startPositions)
		{
			long score = 0L;
			Queue<(int, int)> queue = [];
			HashSet<(int, int)> visited = [];
			queue.Enqueue(thisStartPosition);
			while (queue.Count > 0)
			{
				(int row, int column) current = queue.Dequeue();
				visited.Add(current);
				if (data[current.row][current.column] == '9')
				{
					score++;
				}
				var nextPositions = deltas
					.Select(x => (current.row + x.Item1, current.column + x.Item2))
					.Where(x => 0 <= x.Item1 && x.Item1 < height && 0 <= x.Item2 && x.Item2 < width)
					.Where(x => data[x.Item1][x.Item2] - data[current.row][current.column] == 1)
					.Where(x => !visited.Contains(x))
					.Where(x => !queue.Contains(x));
				foreach ((int, int) thisNextPosition in nextPositions)
				{
					queue.Enqueue(thisNextPosition);
				}			
			}
			totalScore += score;
		}
		return totalScore;
	}

	public long Part2()
	{
		long total = 0L;
		foreach ((int, int) thisStartPosition in startPositions)
		{
			total += CountPaths(thisStartPosition);
		}
		return total;
	}

	private long CountPaths((int row, int column) current)
	{
		if (data[current.row][current.column] == '9')
		{
			return 1L;
		}
		long count = 0L;
		var nextPositions = deltas
			.Select(x => (current.row + x.Item1, current.column + x.Item2))
			.Where(x => 0 <= x.Item1 && x.Item1 < height && 0 <= x.Item2 && x.Item2 < width)
			.Where(x => data[x.Item1][x.Item2] - data[current.row][current.column] == 1);
		foreach ((int, int) thisNextPosition in nextPositions)
		{
			count += CountPaths(thisNextPosition);
		}
		return count;
	}

}