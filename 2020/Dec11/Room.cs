namespace Dec11
{
	internal class Room
	{
		private readonly int width;
		private readonly int height;
		private readonly List<char> layout = new();
		private List<char> previousLayout = new();
		private readonly Dictionary<int, List<int>> neighbors = new();
		private readonly Dictionary<int, List<int>> neighborsVisible = new();

		public Room(string[] initialLayout)
		{
			height = initialLayout.Length;
			width = initialLayout[0].Length;
			foreach (string thisLine in initialLayout)
			{
				foreach (char thisLocation in thisLine)
				{
					layout.Add(thisLocation);
				}
			}
			PopulateNeighbors();
			PopulateNeighborsVisible();
		}

		private void PopulateNeighbors()
		{
			for (int i = 0; i < layout.Count; i++)
			{
				if (layout[i] == '.')
				{
					continue;
				}
				int thisRow = i / width;
				int thisColumn = i % width;
				var theseNeighbors = new List<int>();
				for (int neighborRowDelta = -1; neighborRowDelta <= 1; neighborRowDelta++)
				{
					for (int neighborColumnDelta = -1; neighborColumnDelta <= 1; neighborColumnDelta++)
					{
						if (neighborRowDelta != 0 || neighborColumnDelta != 0)
						{
							int neighborRow = thisRow + neighborRowDelta;
							int neighborColumn = thisColumn + neighborColumnDelta;
							int neighborIndex = neighborRow * width + neighborColumn;
							if (0 <= neighborRow && neighborRow < height
								&& 0 <= neighborColumn && neighborColumn < width
								&& layout[neighborIndex] != '.')
							{
								theseNeighbors.Add(neighborIndex);
							}
						}
					}
				}
				neighbors.Add(i, theseNeighbors);
			}
		}

		private void PopulateNeighborsVisible()
		{
			for (int i = 0; i < layout.Count; i++)
			{
				if (layout[i] == '.')
				{
					continue;
				}
				int thisRow = i / width;
				int thisColumn = i % width;
				var theseNeighbors = new List<int>();
				for (int neighborRowDelta = -1; neighborRowDelta <= 1; neighborRowDelta++)
				{
					for (int neighborColumnDelta = -1; neighborColumnDelta <= 1; neighborColumnDelta++)
					{
						if (neighborRowDelta != 0 || neighborColumnDelta != 0)
						{
							int neighborRow = thisRow + neighborRowDelta;
							int neighborColumn = thisColumn + neighborColumnDelta;
							int neighborIndex = neighborRow * width + neighborColumn;
							while (0 <= neighborRow && neighborRow < height
								&& 0 <= neighborColumn && neighborColumn < width
								&& layout[neighborIndex] == '.')
							{
								neighborRow += neighborRowDelta;
								neighborColumn += neighborColumnDelta;
								neighborIndex = neighborRow * width + neighborColumn;
							}
							if (0 <= neighborRow && neighborRow < height
								&& 0 <= neighborColumn && neighborColumn < width
								&& layout[neighborIndex] != '.')
							{
								theseNeighbors.Add(neighborIndex);
							}
						}
					}
				}
				neighborsVisible.Add(i, theseNeighbors);
			}
		}

			public int CountOccupied()
		{
			while (previousLayout.Count == 0 || !layout.SequenceEqual(previousLayout))
			{
				previousLayout = new List<char>(layout);
				for (int i = 0; i < previousLayout.Count; i++)
				{
					layout[i] = GetNewValue(i);
				}
			}
			return GetCurrentOccupied();
		}

		public int CountOccupiedVisible()
		{
			while (previousLayout.Count == 0 || !layout.SequenceEqual(previousLayout))
			{
				previousLayout = new List<char>(layout);
				for (int i = 0; i < previousLayout.Count; i++)
				{
					layout[i] = GetNewValueVisible(i);
				}
			}
			return GetCurrentOccupied();
		}

		private int GetCurrentOccupied()
		{
			int occupiedCount = 0;
			foreach (char thisLocation in layout)
			{
				if (thisLocation == '#')
				{
					occupiedCount++;
				}
			}
			return occupiedCount;
		}

		private char GetNewValue(int index)
		{
			if (neighbors.TryGetValue(index, out List<int>? theseNeighbors))
			{
				char currentValue = previousLayout[index];				
				int occupiedNeighborCount = 0;
				foreach (int thisNeighborIndex in theseNeighbors)
				{
					if (previousLayout[thisNeighborIndex] == '#')
					{
						occupiedNeighborCount++;
					}
				}
				if (currentValue == 'L' && occupiedNeighborCount == 0)
				{
					return '#';
				} 
				else if (currentValue == '#' && occupiedNeighborCount >= 4)
				{
					return 'L';
				}
				else
				{
					return currentValue;
				}
			}
			else
			{
				return '.';
			}
		}

		private char GetNewValueVisible(int index)
		{
			if (neighborsVisible.TryGetValue(index, out List<int>? theseNeighbors))
			{
				char currentValue = previousLayout[index];
				int occupiedNeighborCount = 0;
				foreach (int thisNeighborIndex in theseNeighbors)
				{
					if (previousLayout[thisNeighborIndex] == '#')
					{
						occupiedNeighborCount++;
					}
				}
				if (currentValue == 'L' && occupiedNeighborCount == 0)
				{
					return '#';
				}
				else if (currentValue == '#' && occupiedNeighborCount >= 5)
				{
					return 'L';
				}
				else
				{
					return currentValue;
				}
			}
			else
			{
				return '.';
			}
		}



	}
}
