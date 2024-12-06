namespace Day06;

internal class LabMap(string[] data)
{
	private readonly string[] data = data;
	private readonly Dictionary<char, char> turnRight = new()
	{
		['N'] = 'E',
		['E'] = 'S',
		['S'] = 'W',
		['W'] = 'N'
	};
	private readonly HashSet<(char, int, int)> cache = [];

	public long Part1()
	{
		Execute(-1, -1, out long visitedCount);
		return visitedCount;
	}

	public long Part2()
	{
		long count = 0;
		for (int row = 0; row < data.Length; row++)
		{
			for (int column = 0; column < data[0].Length; column++)
			{
				if (data[row][column] != '.')
				{
					continue;
				}
				if (!Execute(row, column, out long _))
				{
					count++;
				}
			}
		}
		return count;
	}

	private bool Execute(int obstacleRow, int obstacleColumn, out long visitedCount)
	{
		cache.Clear();
		visitedCount = -1L;
		if (obstacleRow >= 0 && obstacleColumn >= 0)
		{
			data[obstacleRow] = data[obstacleRow][..obstacleColumn] + '#' + data[obstacleRow][(obstacleColumn + 1)..];
		}
		var location = data
			.Select((line, rowIndex) => new { Row = rowIndex, Column = line.IndexOf('^') })
			.FirstOrDefault(x => x.Column != -1);
		int row = location!.Row;
		int column = location!.Column;
		char direction = 'N';
		HashSet<(int, int)> visited = [];
		char? nextChar = '.';
		while (nextChar != null)
		{
			visited.Add((row, column));
			if (!cache.Add((direction, row, column)))
			{
				if (obstacleRow >= 0 && obstacleColumn >= 0)
				{
					data[obstacleRow] = data[obstacleRow][..obstacleColumn] + '.' + data[obstacleRow][(obstacleColumn + 1)..];
				}
				return false;
			}
			nextChar = GetNext(direction, row, column);
			if (nextChar != null)
			{
				if (nextChar == '#')
				{
					direction = turnRight[direction];
				}
				else
				{
					Advance(direction, row, column, out row, out column);
				}
			}
		}
		visitedCount = visited.Count;
		if (obstacleRow >= 0 && obstacleColumn >= 0)
		{
			data[obstacleRow] = data[obstacleRow][..obstacleColumn] + '.' + data[obstacleRow][(obstacleColumn + 1)..];
		}
		return true;
	}

	private char? GetNext(char direction, int row, int column)
	{
		int nextRow;
		int nextColumn;
		switch(direction)
		{		
			case 'N':
				nextRow = row - 1;
				return nextRow >= 0 ? data[nextRow][column] : null;
			case 'E':
				nextColumn = column + 1;
				return nextColumn < data[0].Length ? data[row][nextColumn] : null;
			case 'S':
				nextRow = row + 1;
				return nextRow < data.Length ? data[nextRow][column] : null;
			case 'W':
				nextColumn = column - 1;
				return nextColumn >= 0 ? data[row][nextColumn] : null;
		}
		return null;
	}

	private static void Advance(char direction, int row, int column, out int nextRow, out int nextColumn)
	{
		switch (direction)
		{
			case 'N':
				nextRow = row - 1;
				nextColumn = column;
				return;
			case 'E':
				nextRow = row;
				nextColumn = column + 1;
				return;
			case 'S':
				nextRow = row + 1;
				nextColumn = column;
				return;
			case 'W':
				nextRow = row;
				nextColumn = column - 1;
				return;
		}
		nextRow = row;
		nextColumn = column;
	}

}