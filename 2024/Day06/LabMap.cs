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
	private static readonly HashSet<(int, int)> visited = [];

	public long Part1()
	{
		Execute(-1, -1, out long visitedCount);
		return visitedCount;
	}

	public long Part2()
	{
		if (visited.Count == 0)
		{
			Part1();
		}
		long count = 0;
		foreach ((int, int) thisVisited in visited)
		{
			if (data[thisVisited.Item1][thisVisited.Item2] == '^')
			{
				continue;
			}
			if (!Execute(thisVisited.Item1, thisVisited.Item2, out long _))
			{
				count++;
			}
		}
		return count;
	}

	private bool Execute(int obstacleRow, int obstacleColumn, out long visitedCount)
	{
		cache.Clear();
		visitedCount = -1L;
		var location = data
			.Select((line, rowIndex) => new { Row = rowIndex, Column = line.IndexOf('^') })
			.FirstOrDefault(x => x.Column != -1);
		int row = location!.Row;
		int column = location!.Column;
		char direction = 'N';
		char? nextChar = '.';
		bool isObstaclePlaced = obstacleRow >= 0 && obstacleColumn >= 0;
		while (nextChar != null)
		{
			if (!isObstaclePlaced)
			{
				visited.Add((row, column));
			}			
			if (!cache.Add((direction, row, column)))
			{
				return false;
			}
			nextChar = GetNextChar(direction, row, column, obstacleRow, obstacleColumn);
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
		return true;
	}

	private char? GetNextChar(char direction, int row, int column, int obstacleRow, int obstacleColumn)
	{
		int nextRow = -1;
		int nextColumn = -1;
		switch(direction)
		{		
			case 'N':
				nextRow = row - 1;
				nextColumn = column;
				break;
			case 'E':
				nextRow = row;
				nextColumn = column + 1;
				break;
			case 'S':
				nextRow = row + 1;
				nextColumn = column;
				break;
			case 'W':
				nextRow = row;
				nextColumn = column - 1;
				break;
		}
		if (nextRow == obstacleRow && nextColumn == obstacleColumn)
		{
			return '#';
		}
		if (nextRow < 0 || nextRow >= data.Length || nextColumn < 0 || nextColumn >= data[0].Length)
		{
			return null;
		}
		return data[nextRow][nextColumn];
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