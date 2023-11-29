namespace Day23
{
	internal class ElfMap
	{
		private readonly List<(int, int)> elfLocations = new ();
		private readonly List<(int, int)> proposedLocations = new ();
		private readonly List<(int, int)> temp = new ();
		private int startDirection = 0;

		public ElfMap(string[] allLines)
		{
			for (int row = 0; row < allLines.Length; row++)
			{
				for (int column = 0; column < allLines[row].Length; column++)
				{
					if (allLines[row][column] == '#')
					{
						elfLocations.Add((row, column));
					}
				}
			}
		}

		public int Update()
		{
			int moveCount = 0;
			proposedLocations.Clear();
			elfLocations.ForEach(x => proposedLocations.Add(GetProposedLocation(x)));
			temp.Clear();
			for (int i = 0; i < proposedLocations.Count; i++)
			{
				if (proposedLocations.Where(x => x == proposedLocations[i]).Count() == 1)
				{
					temp.Add(proposedLocations[i]);
					if (proposedLocations[i] != elfLocations[i])
					{
						moveCount++;
					}
				}
				else
				{
					temp.Add(elfLocations[i]);
				}				
			}
			elfLocations.Clear();
			elfLocations.AddRange(temp);
			startDirection = (startDirection + 1) % 4;
			return moveCount;
		}

		public int CountOpenSpaces()
		{
			var minRow = elfLocations.Select(x => x.Item1).Min();
			var maxRow = elfLocations.Select(x => x.Item1).Max();
			var minColumn = elfLocations.Select(x => x.Item2).Min();
			var maxColumn = elfLocations.Select(x => x.Item2).Max();
			var count = 0;
			for (int row = minRow; row <= maxRow; row++)
			{
				for (int column = minColumn; column <= maxColumn; column++)
				{
					if (!elfLocations.Contains((row, column)))
					{
						count++;
					}
				}
			}
			return count;
		}

		private (int, int) GetProposedLocation((int, int) currentLocation)
		{
			var row = currentLocation.Item1;
			var column = currentLocation.Item2;
			var northClear = !elfLocations.Contains((row - 1, column - 1)) 
				&& !elfLocations.Contains((row - 1, column))
				&& !elfLocations.Contains((row - 1, column + 1));
			var southClear = !elfLocations.Contains((row + 1, column - 1)) 
				&& !elfLocations.Contains((row + 1, column))
				&& !elfLocations.Contains((row + 1, column + 1));
			var westClear = !elfLocations.Contains((row - 1, column - 1)) 
				&& !elfLocations.Contains((row, column - 1))
				&& !elfLocations.Contains((row + 1, column - 1));
			var eastClear = !elfLocations.Contains((row - 1, column + 1)) 
				&& !elfLocations.Contains((row, column + 1))
				&& !elfLocations.Contains((row + 1, column + 1));
			if (northClear && southClear && westClear && eastClear)
			{
				return currentLocation;	
			}
			int proposedDirection = startDirection;
			do
			{
				if (proposedDirection == 0 && northClear)
				{
					return (row - 1, column);
				}
				if (proposedDirection == 1 && southClear)
				{
					return (row + 1, column);
				}
				if (proposedDirection == 2 && westClear)
				{
					return (row, column - 1);
				}
				if (proposedDirection == 3 && eastClear)
				{
					return (row, column + 1);
				}
				proposedDirection = (proposedDirection + 1) % 4;
			} while (proposedDirection != startDirection);
			return currentLocation;
		}


	}
}