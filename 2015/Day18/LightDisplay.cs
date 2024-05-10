namespace Day18;

internal class LightDisplay
{
	private HashSet<(int, int)> onLights = []; // row, column
	private readonly int height;
	private readonly int width;
	private HashSet<(int, int)> permanentlyOnLights = [];

	public LightDisplay(string[] allLines)
	{
		height = allLines.Length;
		width = allLines[0].Length;
		permanentlyOnLights = [ (0, 0), (0, width - 1), (height - 1, 0), (height - 1, width - 1)];
		for (int row = 0; row < height; row++)
		{
			for (int column = 0; column < width; column++)
			{
				if (allLines[row][column] == '#')
				{
					onLights.Add((row, column));
				}
			}
		}
	}

	public int Iterate(int count)
	{
		for (int i = 1; i <= count; i++)
		{
			Iterate();
		}
		return onLights.Count;
	}

	public int Iterate2(int count)
	{
		foreach ((int row, int column) in permanentlyOnLights)
		{
			onLights.Add((row, column));
		}
		for (int i = 1; i <= count; i++)
		{
			Iterate2();
		}
		return onLights.Count;
	}

	private void Iterate()
	{
		HashSet<(int, int)> temp = [];
		for (int row = 0; row < height; row++)
		{
			for (int column = 0; column < width; column++)
			{
				int onNeighbors = CountOnNeighbors(row, column);
				if (onLights.Contains((row, column)))
				{
					if (onNeighbors == 2 || onNeighbors == 3)
					{
						temp.Add((row, column));
					}
				}
				else
				{
					if (onNeighbors == 3)
					{
						temp.Add((row, column));
					}
				}
			}
		}
		onLights = temp;
	}

	private void Iterate2()
	{
		HashSet<(int, int)> temp = [];
		for (int row = 0; row < height; row++)
		{
			for (int column = 0; column < width; column++)
			{
				if (permanentlyOnLights.Contains((row, column)))
				{
					temp.Add((row, column));
					continue;
				}
				int onNeighbors = CountOnNeighbors(row, column);
				if (onLights.Contains((row, column)))
				{
					if (onNeighbors == 2 || onNeighbors == 3)
					{
						temp.Add((row, column));
					}
				}
				else
				{
					if (onNeighbors == 3)
					{
						temp.Add((row, column));
					}
				}
			}
		}
		onLights = temp;
	}

	private int CountOnNeighbors(int row, int column)
	{
		int count = 0;
		for (int deltaRow = -1; deltaRow <= 1; deltaRow++)
		{
			for (int deltaColumn = -1; deltaColumn <= 1; deltaColumn++)
			{
				if (deltaRow == 0 && deltaColumn == 0)
				{
					continue;
				}
				int nextRow = row + deltaRow;
				int nextColumn = column + deltaColumn;
				if (nextRow < 0 || nextRow >= height || nextColumn < 0 || nextColumn >= width)
				{
					continue;
				}
				count += onLights.Contains((nextRow, nextColumn)) ? 1 : 0;
			}
		}
		return count;	
	}

}