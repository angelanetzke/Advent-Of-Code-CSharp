namespace Day24
{
	internal class ValleyMap
	{
		private readonly Dictionary<int, BlizzardMap> blizzardCycle = new ();
		private readonly int cycleLength;

		public ValleyMap(string[] allLines)
		{
			int height = allLines.Length - 2;
			int width = allLines[0].Length - 2;
			cycleLength = height * width;
			for (int step = 0; step < cycleLength; step++)
			{
				if (step == 0)
				{
					blizzardCycle[0] = new BlizzardMap(allLines);
				}
				else
				{
					blizzardCycle[step] = blizzardCycle[step - 1].Next();
				}
			}
		}

		public int GetTimeToEnd()
		{
			return GetTripTime(blizzardCycle[0].GetStart(), blizzardCycle[0].GetEnd(), 0);
		}

		public int GetTripTime((int, int) start, (int, int) end, int startCycleStep)
		{
			var visited = new HashSet<Node>();
			var nodeQueue = new Queue<Node>();
			nodeQueue.Enqueue(new Node(startCycleStep, 0, start.Item1, start.Item2));
			while (nodeQueue.Count > 0)
			{
				var current = nodeQueue.Dequeue();
				visited.Add(current);
				var neighborList = current.GetNeighbors();
				foreach ((int, int) thisNeighbor in neighborList)
				{
					var nextNode = new Node((current.GetCycleStep() + 1) % cycleLength, 
						current.GetMinute() + 1, thisNeighbor.Item1, thisNeighbor.Item2);
					if (nextNode.IsLocation(end))
					{
						return nextNode.GetMinute();
					}
					var nextNodeSpace = blizzardCycle[nextNode.GetCycleStep()].GetSquare(nextNode.GetLocation());
					if (!visited.Contains(nextNode) 
						&& !nodeQueue.Contains(nextNode)
						&& nextNodeSpace == BlizzardMap.SQUARE_TYPE.SPACE)
					{
						nodeQueue.Enqueue(nextNode);
					}
				}
			}
			return -1;
		}

		public int GetSnackRetrievalTime()
		{
			var totalTime = GetTripTime(blizzardCycle[0].GetStart(), blizzardCycle[0].GetEnd(), 0);
			var nextTime = -1;
			do
			{
				nextTime = GetTripTime(blizzardCycle[0].GetEnd(), blizzardCycle[0].GetStart(), totalTime % cycleLength);
				if (nextTime == -1)
				{
					totalTime++;
				}
			} while (nextTime == -1);
			totalTime += nextTime;
			nextTime = -1;
			do
			{
				nextTime = GetTripTime(blizzardCycle[0].GetStart(), blizzardCycle[0].GetEnd(), totalTime % cycleLength);
				if (nextTime == -1)
				{
					totalTime++;
				}
			} while (nextTime == -1);
			totalTime += nextTime;
			return totalTime;
		}

		private class Node
		{
			private readonly int cycleStep;
			private readonly int minute;
			private readonly int row;
			private readonly int column;			

			public Node(int cycleStep, int minute, int row, int column)
			{
				this.cycleStep = cycleStep;
				this.minute = minute;
				this.row = row;
				this.column = column;
			}

			public List<(int, int)> GetNeighbors()
			{
				var neighborList = new List<(int, int)>();
				neighborList.Add((row - 1, column));
				neighborList.Add((row + 1, column));
				neighborList.Add((row, column - 1));
				neighborList.Add((row, column + 1));
				neighborList.Add((row, column));
				return neighborList;
			}

			public int GetCycleStep()
			{
				return cycleStep;
			}

			public int GetMinute()
			{
				return minute;
			}

			public bool IsLocation((int, int) location)
			{
				return row == location.Item1 && column == location.Item2;
			}

			public (int, int) GetLocation()
			{
				return (row, column);
			}

			public override bool Equals(object? obj)
			{
				if (obj is Node other)
				{
					return cycleStep == other.cycleStep
						&& row == other.row
						&& column == other.column;
				}
				else
				{
					return false;
				}
			}

			public override int GetHashCode()
			{
				return (cycleStep.GetHashCode()
					+ row.GetHashCode()
					+ column.GetHashCode()).GetHashCode();
			}
		}

	}
}