namespace Day24
{
	internal class BlizzardMap
	{
		private readonly List<(int, int)> northBlizzards = new ();
		private readonly List<(int, int)> southBlizzards = new ();
		private readonly List<(int, int)> westBlizzards = new ();
		private readonly List<(int, int)> eastBlizzards = new ();
		private readonly int minRow;
		private readonly int maxRow;
		private readonly int minColumn;
		private readonly int maxColumn;
		public enum SQUARE_TYPE { START, END, BLIZZARD, SPACE, WALL };
		private readonly (int, int) start;
		private readonly (int, int) end;

		public BlizzardMap(int minRow, int maxRow, int minColumn, int maxColumn, 
			(int, int) start, (int, int) end)
		{
			this.minRow = minRow;
			this.maxRow = maxRow;
			this.minColumn = minColumn;
			this.maxColumn = maxColumn;
			this.start = start;
			this.end = end;
		}

		public BlizzardMap(string[] allLines)
		{
			for (int row = 0; row < allLines.Length; row++)
			{
				for (int column = 0; column < allLines[row].Length; column++)
				{
					switch (allLines[row][column])
					{
						case '^':
							northBlizzards.Add((row, column));
							break;
						case 'v':
							southBlizzards.Add((row, column));
							break;
						case '<':
							westBlizzards.Add((row, column));
							break;
						case '>':
							eastBlizzards.Add((row, column));
							break;
						case '.':
							if (row == 0)
							{
								start = (row, column);
							}
							if (row == allLines.Length - 1)
							{
								end = (row, column);
							}
							break;
					}
				}
			}
			minRow = 1;
			maxRow = allLines.Length - 2;
			minColumn = 1;
			maxColumn = allLines[0].Length - 2;
		}

		public BlizzardMap Next()
		{
			var newMap = new BlizzardMap(minRow, maxRow, minColumn, maxColumn, start, end);
			foreach ((int, int) thisBlizzard in northBlizzards)
			{
				if (thisBlizzard.Item1 == minRow)
				{
					newMap.northBlizzards.Add((maxRow, thisBlizzard.Item2));
				}
				else
				{
					newMap.northBlizzards.Add((thisBlizzard.Item1 - 1, thisBlizzard.Item2));
				}
			}
			foreach ((int, int) thisBlizzard in southBlizzards)
			{
				if (thisBlizzard.Item1 == maxRow)
				{
					newMap.southBlizzards.Add((minRow, thisBlizzard.Item2));
				}
				else
				{
					newMap.southBlizzards.Add((thisBlizzard.Item1 + 1, thisBlizzard.Item2));
				}
			}
			foreach ((int, int) thisBlizzard in westBlizzards)
			{
				if (thisBlizzard.Item2 == minColumn)
				{
					newMap.westBlizzards.Add((thisBlizzard.Item1, maxColumn));
				}
				else
				{
					newMap.westBlizzards.Add((thisBlizzard.Item1, thisBlizzard.Item2 - 1));
				}
			}
			foreach ((int, int) thisBlizzard in eastBlizzards)
			{
				if (thisBlizzard.Item2 == maxColumn)
				{
					newMap.eastBlizzards.Add((thisBlizzard.Item1, minColumn));
				}
				else
				{
					newMap.eastBlizzards.Add((thisBlizzard.Item1, thisBlizzard.Item2 + 1));
				}
			}		
			return newMap;
		}

		public SQUARE_TYPE GetSquare((int, int) location)
		{
			var row = location.Item1;
			var column = location.Item2;
			if (start == (row, column))
			{
				return SQUARE_TYPE.START;
			}
			if (end == (row, column))
			{
				return SQUARE_TYPE.END;
			}
			if (northBlizzards.Contains((row, column)))
			{
				return SQUARE_TYPE.BLIZZARD;
			}
			if (southBlizzards.Contains((row, column)))
			{
				return SQUARE_TYPE.BLIZZARD;
			}
			if (westBlizzards.Contains((row, column)))
			{
				return SQUARE_TYPE.BLIZZARD;
			}
			if (eastBlizzards.Contains((row, column)))
			{
				return SQUARE_TYPE.BLIZZARD;
			}
			if (minRow <= row && row <= maxRow && minColumn <= column && column <= maxColumn)
			{
				return SQUARE_TYPE.SPACE;
			}
			return SQUARE_TYPE.WALL;
		}

		public (int, int) GetStart()
		{
			return start;
		}

		public (int, int) GetEnd()
		{
			return end;
		}

	}
}