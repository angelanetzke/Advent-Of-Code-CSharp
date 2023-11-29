namespace Day19;

using System.Net.Http.Headers;
using System.Text;

internal class Diagram
{
	private static Dictionary<char, (int row, int column)> move = new ()
	{
		{ 'N', (-1, 0) },
		{ 'S', (1, 0) },
		{ 'W', (0, -1) },
		{ 'E', (0, 1) }
	};
	char currentDirection = 'S';
	(int row, int column) currentPosition;
	private string[] map;

	public Diagram(string[] map)
	{
		this.map = map;
		currentPosition = (0, map[0].IndexOf('|'));
	}

	public (string code, int steps) Navigate()
	{
		var stepCount = 1;
		var builder = new StringBuilder();
		char? nextDirection = GetNextDirection();
		while (nextDirection != null)
		{
			currentDirection = (char)nextDirection;
			currentPosition = (currentPosition.row + move[currentDirection].row,
				currentPosition.column + move[currentDirection].column);
			if (char.IsLetter(map[currentPosition.row][currentPosition.column]))
			{
				builder.Append(map[currentPosition.row][currentPosition.column]);
			}
			nextDirection = GetNextDirection();
			stepCount++;
		}
		return (builder.ToString(), stepCount);
	}

	private bool CanMoveForward()
	{
		(int row, int column) nextPosition = (currentPosition.row + move[currentDirection].row,
			currentPosition.column + move[currentDirection].column);
		if (nextPosition.row < 0 || nextPosition.row >= map.Length)
		{
			return false;
		}
		if (nextPosition.column < 0 || nextPosition.column >= map[nextPosition.row].Length)
		{
			return false;
		}
		if (map[nextPosition.row][nextPosition.column] == ' ')
		{
			return false;
		}		
		return true;
	}

	private char? GetNextDirection()
	{
		var oldCurrentDirection = currentDirection;
		if (CanMoveForward())
		{
			return currentDirection;
		}
		switch(currentDirection)
		{
			case 'N':
			case 'S':
				currentDirection = 'W';
				if (CanMoveForward())
				{
					return 'W';
				}
				currentDirection = 'E';
				if (CanMoveForward())
				{
					return 'E';
				}
				break;			
			case 'W':
			case 'E':
				currentDirection = 'N';
				if (CanMoveForward())
				{
					return 'N';
				}
				currentDirection = 'S';
				if (CanMoveForward())
				{
					return 'S';
				}
				break;
		}
		currentDirection = oldCurrentDirection;
		return null;
	}
}