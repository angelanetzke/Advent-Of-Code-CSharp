using System.Text;

namespace Dec24;

internal class Grid
{
	private readonly int rowCount;
	private readonly int columnCount;
	private HashSet<(int, int)> current = [];
	private readonly HashSet<string> history = [];
	private readonly StringBuilder builder = new ();

	public Grid(string[] allLines)
	{
		rowCount = allLines.Length;
		columnCount = allLines[0].Length;
		for (int row = 0; row < allLines.Length; row++)
		{
			for (int column = 0; column < allLines[0].Length; column++)
			{
				if (allLines[row][column] == '#')
				{
					current.Add((row, column));
				}
			}
		}
	}

	public long Part1()
	{
		history.Clear();
		string thisEntry = GetHistoryString();
		while (!history.Contains(thisEntry))
		{
			history.Add(thisEntry);
			Iterate();
			thisEntry = GetHistoryString();
		}
		return GetScore();
	}

	private void Iterate()
	{
		HashSet<(int, int)> next = [];
		for (int row = 0; row < rowCount; row++)
		{
			for (int column = 0; column < columnCount; column++)
			{
				int neighborCount = CountNeighbors(row, column);
				if (current.Contains((row, column)))
				{
					if (neighborCount == 1)
					{
						next.Add((row, column));
					}
				}
				else
				{
					if (neighborCount == 1 || neighborCount == 2)
					{
						next.Add((row, column));
					}
				}
			}
		}
		current = next;
	}

	private int CountNeighbors(int row, int column)
	{
		int count = 0;
		foreach ((int, int) thisDelta in new (int, int)[] {(1, 0), (-1, 0), (0, 1), (0, -1)})
		{
			(int, int) thisNeighbor = (row + thisDelta.Item1, column + thisDelta.Item2);
			count += current.Contains(thisNeighbor) ? 1 : 0;
		}
		return count;
	}

	private string GetHistoryString()
	{
		builder.Clear();
		for (int row = 0; row < rowCount; row++)
		{
			for (int column = 0; column < columnCount; column++)
			{
				if (current.Contains((row, column)))
				{
					builder.Append('#');
				}
				else
				{
					builder.Append('.');
				}
			}
		}
		return builder.ToString();
	}

	private long GetScore()
	{
		long value = 1;
		long score = 0;
		for (int row = 0; row < rowCount; row++)
		{
			for (int columm	 = 0; columm < columnCount; columm++)
			{
				score += current.Contains((row, columm)) ? value: 0;
				value *= 2;
			}
		}
		return score;
	}
}