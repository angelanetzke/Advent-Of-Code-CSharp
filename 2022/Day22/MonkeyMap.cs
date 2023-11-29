using System.Text.RegularExpressions;

namespace Day22
{
	internal class MonkeyMap
	{
		private readonly List<string> mapData = new ();
		private readonly string directions;
		private enum Facing { RIGHT, DOWN, LEFT, UP };
		private static readonly int facingCount = 4;
		private static readonly Regex tokenRegex = new Regex(@"^(\d+|[RL]{1})");
		private static readonly int SIZE = 50;

		public MonkeyMap(string directions)
		{
			this.directions = directions;
		}

		public void AddRow(string newRow)
		{
			mapData.Add(newRow);
		}

		public int GetPassword(bool isCube)
		{
			var currentPosition = (0, GetMinColumn(0));
			Facing currentFacing = Facing.RIGHT;
			var tokenIndex = 0;
			(int, int, Facing) nextData;
			while (tokenIndex < directions.Length)
			{
				var nextToken = tokenRegex.Match(directions.Substring(tokenIndex)).Value;
				if (nextToken == "L")
				{
					currentFacing = (Facing)(((int)currentFacing + facingCount - 1) % facingCount);
				}
				else if (nextToken == "R")
				{
					currentFacing = (Facing)(((int)currentFacing + 1) % facingCount);
				}
				else
				{
					var stepCount = int.Parse(nextToken);
					for (int thisStep = 1; thisStep <= stepCount; thisStep++)
					{
						if (isCube)
						{
							nextData = GetNextCubeData(currentPosition, currentFacing);
						}
						else
						{
							nextData = GetNextFlatData(currentPosition, currentFacing);
						}
						if (mapData[nextData.Item1][nextData.Item2] == '.')
						{
							currentPosition = (nextData.Item1, nextData.Item2);
							currentFacing = nextData.Item3;
						}
					}
				}
				tokenIndex += nextToken.Length;
			}
			return 1000 * (currentPosition.Item1 + 1) + 4 * (currentPosition.Item2 + 1) + (int)currentFacing;
		}		

		private (int, int, Facing) GetNextFlatData((int, int) currentPosition, Facing currentFacing)
		{
			var row = currentPosition.Item1;
			var column = currentPosition.Item2;
			switch (currentFacing)
			{
				case Facing.RIGHT:
					if (column == GetMaxColumn(row))
					{
						return (row, GetMinColumn(row), currentFacing);
					}
					else
					{
						return (row, column + 1, currentFacing);
					}
				case Facing.DOWN:
					if (row == GetMaxRow(column))
					{
						return (GetMinRow(column), column, currentFacing);
					}
					else
					{
						return (row + 1, column, currentFacing);
					}
				case Facing.LEFT:
					if (column == GetMinColumn(row))
					{
						return (row, GetMaxColumn(row), currentFacing);
					}
					else
					{
						return (row, column - 1, currentFacing);
					}
				case Facing.UP:
					if (row == GetMinRow(column))
					{
						return (GetMaxRow(column), column, currentFacing);
					}
					else
					{
						return (row - 1, column, currentFacing);
					}
			}
			return (currentPosition.Item1, currentPosition.Item2, currentFacing);
		}		

		private (int, int, Facing) GetNextCubeData((int, int) currentPosition, Facing currentFacing)
		{
			var row = currentPosition.Item1;
			var column = currentPosition.Item2;
			int newRow;
			int newColumn;
			Facing newFacing = currentFacing;
			switch (currentFacing)
			{
				case Facing.RIGHT:
					if (column == GetMaxColumn(row))
					{
						if (0 <= row && row <= SIZE - 1)
						{
							newFacing = Facing.LEFT;
							newRow = SIZE * 2 + SIZE - 1 - row;
							newColumn = GetMaxColumn(newRow);
						}
						else if (SIZE <= row && row <= SIZE * 2 - 1)
						{
							newFacing = Facing.UP;
							newColumn = SIZE * 2 + row - SIZE;
							newRow = GetMaxRow(newColumn);
						}
						else if (SIZE * 2 <= row && row <= SIZE * 3 - 1)
						{
							newFacing = Facing.LEFT;
							newRow = SIZE * 3 - 1 - row;
							newColumn = GetMaxColumn(newRow);
						}
						else
						{
							newFacing = Facing.UP;
							newColumn = SIZE + row - SIZE * 3;
							newRow = GetMaxRow(newColumn);
						}
						return (newRow, newColumn, newFacing);
					}
					else
					{
						return (row, column + 1, newFacing);
					}
				case Facing.DOWN:
					if (row == GetMaxRow(column))
					{
						if (0 <= column && column <= SIZE - 1)
						{
							newFacing = Facing.DOWN;
							newColumn = SIZE * 2 + column;
							newRow = GetMinRow(newColumn);
						}
						else if (SIZE <= column && column <= SIZE * 2 - 1)
						{
							newFacing = Facing.LEFT;
							newRow = SIZE * 3 + column - SIZE;
							newColumn = GetMaxColumn(newRow);
						}
						else 
						{
							newFacing = Facing.LEFT;
							newRow = SIZE + column - SIZE * 2;
							newColumn = GetMaxColumn(newRow);
						}
						return (newRow, newColumn, newFacing);
					}
					else
					{
						return (row + 1, column, newFacing);
					}
				case Facing.LEFT:
					if (column == GetMinColumn(row))
					{
						if (0 <= row && row <= SIZE - 1)
						{
							newFacing = Facing.RIGHT;
							newRow = SIZE * 2 + SIZE - 1 - row;
							newColumn = GetMinColumn(newRow);
						}
						else if (SIZE <= row && row <= SIZE * 2 - 1)
						{
							newFacing = Facing.DOWN;
							newColumn = row - SIZE;
							newRow = GetMinRow(newColumn);
						}
						else if (SIZE * 2 <= row && row <= SIZE * 3 - 1)
						{
							newFacing = Facing.RIGHT;
							newRow = SIZE * 3 - 1 - row;
							newColumn = GetMinColumn(newRow);
						}
						else
						{
							newFacing = Facing.DOWN;
							newColumn = SIZE + row - SIZE * 3;
							newRow = GetMinRow(newColumn);
						}
						return (newRow, newColumn, newFacing);
					}
					else
					{
						return (row, column - 1, newFacing);
					}
				case Facing.UP:
					if (row == GetMinRow(column))
					{
						if (0 <= column && column <= SIZE - 1)
						{
							newFacing = Facing.RIGHT;
							newRow = SIZE + column;
							newColumn = GetMinColumn(newRow);
						}
						else if (SIZE <= column && column <= SIZE * 2 - 1)
						{
							newFacing = Facing.RIGHT;
							newRow = SIZE * 3 + column - SIZE;
							newColumn = GetMinColumn(newRow);
						}
						else 
						{
							newFacing = Facing.UP;
							newColumn = column - SIZE * 2;
							newRow = GetMaxRow(newColumn);
						}
						return (newRow, newColumn, newFacing);
					}
					else
					{
						return (row - 1, column, newFacing);
					}
			}
			return (currentPosition.Item1, currentPosition.Item2, currentFacing);
		}

		private int GetMinColumn(int row)
		{
			if (mapData[row].IndexOf('#') == -1)
			{
				return mapData[row].IndexOf('.');
			}
			else if (mapData[row].IndexOf('.') == -1)
			{
				return mapData[row].IndexOf('#');
			}
			else
			{
				return Math.Min(mapData[row].IndexOf('.'), mapData[row].IndexOf('#'));
			}
		}
		private int GetMaxColumn(int row)
		{
			return mapData[row].Length - 1;
		}

		private int GetMinRow(int column)
		{
			var columnString = new string(mapData.Where(x => column < x.Length).Select(x => x[column]).ToArray());
			if (columnString.IndexOf('#') == -1)
			{
				return columnString.IndexOf('.');
			}
			else if (columnString.IndexOf('.') == -1)
			{
				return columnString.IndexOf('#');
			}
			else
			{
				return Math.Min(columnString.IndexOf('.'), columnString.IndexOf('#'));
			}
		}

		private int GetMaxRow(int column)
		{
			var columnString = new string(mapData.Where(x => column < x.Length).Select(x => x[column]).ToArray());
			return columnString.Length - 1;
		}

	}
}