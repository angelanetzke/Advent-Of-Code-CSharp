namespace Day20
{
	internal class Room
	{
		private readonly static Dictionary<(int x, int y), Room> allRooms = new ();
		private enum Direction { N, S, W, E }
		private Dictionary<Direction, bool> doors = new ()
		{
			{ Direction.N, false },
			{ Direction.S, false },
			{ Direction.W, false },
			{ Direction.E, false }
		};
		private readonly (int x, int y) location;
		private static Dictionary<(int x, int y), int> distances = new ();

		private Room((int, int) location)
		{
			this.location = location;
		}

		private (int, int) Move(Direction direction)
		{
			int nextX = location.x;
			int nextY = location.y;
			switch (direction)
			{
				case Direction.N:
					nextY--;
					break;
				case Direction.S:
					nextY++;
					break;
				case Direction.W:
					nextX--;
					break;
				case Direction.E:
					nextX++;
					break;
			}
			if (!allRooms.Keys.Contains((nextX, nextY)))
			{
				allRooms[(nextX, nextY)] = new Room((nextX, nextY));
			}
			switch (direction)
			{
				case Direction.N:
					doors[Direction.N] = true;
					allRooms[(nextX, nextY)].doors[Direction.S] = true;
					break;
				case Direction.S:
					doors[Direction.S] = true;
					allRooms[(nextX, nextY)].doors[Direction.N] = true;
					break;
				case Direction.W:
					doors[Direction.W] = true;
					allRooms[(nextX, nextY)].doors[Direction.E] = true;
					break;
				case Direction.E:
					doors[Direction.E] = true;
					allRooms[(nextX, nextY)].doors[Direction.W] = true;
					break;
			}			
			return (nextX, nextY);
		}

		private List<(int, int)> GetNeighbors()
		{
			var neighborList = new List<(int, int)>();
			if (doors[Direction.N])
			{
				neighborList.Add((location.x, location.y - 1));
			}
			if (doors[Direction.S])
			{
				neighborList.Add((location.x, location.y + 1));
			}
			if (doors[Direction.W])
			{
				neighborList.Add((location.x - 1, location.y));
			}
			if (doors[Direction.E])
			{
				neighborList.Add((location.x + 1, location.y));
			}
			return neighborList;
		}

		public static void Generate(string directions)
		{
			directions = directions.Substring(1, directions.Length - 2);
			(int x, int y) currentLocation = (0, 0);
			allRooms[(currentLocation)] = new Room(currentLocation);
			var locationStack = new Stack<(int, int)>();
			foreach (char thisDirection in directions)
			{
				switch(thisDirection)
				{
					case 'N':
						currentLocation = allRooms[currentLocation].Move(Direction.N);
						break;
					case 'S':
						currentLocation = allRooms[currentLocation].Move(Direction.S);
						break;
					case 'W':
						currentLocation = allRooms[currentLocation].Move(Direction.W);
						break;
					case 'E':
						currentLocation = allRooms[currentLocation].Move(Direction.E);
						break;
					case '(':
						locationStack.Push(currentLocation);
						break;
					case ')':
						currentLocation = locationStack.Pop();
						break;
					case '|':
						currentLocation = locationStack.Peek();
						break;
				}
			}
		}

		private static void CalculateDistances()
		{
			distances.Clear();
			var queue = new Queue<(int x, int y)>();
			var visited = new HashSet<(int x, int y)>();
			distances[(0, 0)] = 0;
			queue.Enqueue((0, 0));
			visited.Add((0, 0));
			while (queue.Count > 0)
			{
				var current = queue.Dequeue();
				var neighbors = Room.allRooms[current].GetNeighbors();
				foreach ((int x, int y) thisNeighbor in neighbors)
				{
					if (!visited.Contains(thisNeighbor))
					{
						visited.Add(thisNeighbor);
						distances[thisNeighbor] = distances[current] + 1;
						queue.Enqueue(thisNeighbor);
					}
				}
			}
		}

		public static int GetLongestDistace()
		{
			if (distances.Count == 0)
			{
				CalculateDistances();
			}
			return distances.Select(x => x.Value).Max();
		}

		public static int Count1000DoorDistances()
		{
			if (distances.Count == 0)
			{
				CalculateDistances();
			}
			return distances.Select(x => x.Value).Count(x => x >= 1000);
		}

	}
}