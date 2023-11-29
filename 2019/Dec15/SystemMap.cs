namespace Dec15
{
	internal class SystemMap
	{
		private readonly IntcodeComputer theComputer = new ();
		private (long, long) currentLocation = (0L, 0L);
		private readonly Dictionary<long, long> reverse = new ()
		{
			{ 1L, 2L },
			{ 2L, 1L },
			{ 3L, 4L },
			{ 4L, 3L }
		};
		private readonly HashSet<(long, long)> passages = new ();
		private (long, long) end = (0L, 0L);
		private readonly HashSet<(long, long)> visited = new ();
		private readonly Dictionary<(long, long), long> distances = new ();

		public SystemMap(string inputLine)
		{
			theComputer.SetMemory(inputLine);
		}

		public long GetDistanceToOxygenSystem()
		{
			currentLocation = (0L, 0L);
			visited.Clear();
			ExploreMap(0);
			visited.Clear();
			distances.Clear();
			var locationQueue = new Queue<(long, long)>();
			locationQueue.Enqueue((0L, 0L));
			distances[(0L, 0L)] = 0L;
			while (locationQueue.Count > 0)
			{
				currentLocation = locationQueue.Dequeue();
				visited.Add(currentLocation);
				for (int i = 1; i <= 4; i++)
				{
					var nextNeighbor = GetNeighbor(currentLocation, i);
					if (nextNeighbor == end)
					{
						return distances[currentLocation] + 1L;
					}
					if (passages.Contains(nextNeighbor)
						&& !visited.Contains(nextNeighbor)
						&& !locationQueue.Contains(nextNeighbor))
					{
						distances[nextNeighbor] = distances[currentLocation] + 1L;
						locationQueue.Enqueue(nextNeighbor);
					}
				}
			}
			return -1;
		}

		private void ExploreMap(long direction)
		{
			if (1 <= direction && direction <= 4)
			{
				Move(direction);
			}
			passages.Add(currentLocation);
			visited.Add(currentLocation);
			for (int i = 1; i <= 4; i++)
			{
				var nextResult = CheckDirection(i);
				var nextNeighbor = GetNeighbor(currentLocation, i);
				if (nextResult == 2)
				{
					end = nextNeighbor;
				}
				if (nextResult != 0 && !visited.Contains(nextNeighbor))
				{
					ExploreMap(i);
				}
			}
			if (1 <= direction && direction <= 4)
			{
				Move(reverse[direction]);
			}
		}

		public long GetOxygenFillTime()
		{
			if (passages.Count == 0)
			{
				ExploreMap(0);
			}
			visited.Clear();
			distances.Clear();
			var locationQueue = new Queue<(long, long)>();
			locationQueue.Enqueue(end);
			distances[end] = 0L;
			while (locationQueue.Count > 0)
			{
				currentLocation = locationQueue.Dequeue();
				visited.Add(currentLocation);
				for (int i = 1; i <= 4; i++)
				{
					var nextNeighbor = GetNeighbor(currentLocation, i);
					if (passages.Contains(nextNeighbor)
						&& !visited.Contains(nextNeighbor)
						&& !locationQueue.Contains(nextNeighbor))
					{
						distances[nextNeighbor] = distances[currentLocation] + 1L;
						locationQueue.Enqueue(nextNeighbor);
					}
				}
			}
			return distances.Values.Max();
		}

		private void Move(long direction)
		{
			theComputer.AddInput(direction);
			theComputer.Run();
			theComputer.ClearOutput();
			switch (direction)
			{
				case 1:
					currentLocation = (currentLocation.Item1 - 1, currentLocation.Item2);
					break;
				case 2:
					currentLocation = (currentLocation.Item1 + 1, currentLocation.Item2);
					break;
				case 3:
					currentLocation = (currentLocation.Item1, currentLocation.Item2 - 1);
					break;
				case 4:
					currentLocation = (currentLocation.Item1, currentLocation.Item2 + 1);
					break;
			}
		}

		private long CheckDirection(long direction)
		{
			theComputer.AddInput(direction);
			theComputer.Run();
			var result = theComputer.GetOutput()[0];
			if (result != 0)
			{
				theComputer.AddInput(reverse[direction]);
				theComputer.Run();
			}
			theComputer.ClearOutput();
			return result;
		}
		
		private (long, long) GetNeighbor((long, long) here, long direction)
		{
			switch (direction)
			{
				case 1:
					return (here.Item1 - 1, here.Item2);
				case 2:
					return (here.Item1 + 1, here.Item2);
				case 3:
					return (here.Item1, here.Item2 - 1);
				case 4:
					return (here.Item1, here.Item2 + 1);
				default:
					return here;
			}
		}
	}
}