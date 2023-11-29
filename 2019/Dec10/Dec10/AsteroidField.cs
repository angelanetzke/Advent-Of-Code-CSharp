namespace Dec10
{
	internal class AsteroidField
	{
		private readonly string[] field;
		private (int, int) monitoringStation = (0, 0);
		private readonly List<Direction> directions = new();
		private int nextDirection = 0;

		public AsteroidField(string[] field)
		{
			this.field = field;
		}

		public int GetMaxViewableAsteroids()
		{
			int max = 0;
			for (int x = 0; x < field[0].Length; x++)
			{
				for (int y = 0; y < field.Length; y++)
				{
					int asteroidsAtThisLocation = GetAsteroidsAtLocation(x, y);
					if (asteroidsAtThisLocation > max)
					{
						max = asteroidsAtThisLocation;
						monitoringStation = (x, y);
					}

				}
			}
			return max;
		}

		private int GetAsteroidsAtLocation(int x, int y)
		{
			if (field[y][x] != '#')
			{
				return 0;
			}
			else
			{
				var foundAsteroids = new HashSet<(int, int)>();
				var minDeltaX = 0 - x;
				var maxDeltaX = field[0].Length - 1 - x;
				var minDeltaY = 0 - y;
				var maxDeltaY = field.Length - 1 - y;
				for (int deltaX = minDeltaX; deltaX <= maxDeltaX; deltaX++)
				{
					for (int deltaY = minDeltaY; deltaY <= maxDeltaY; deltaY++)
					{
						if (IsInLowestTerms(deltaX, deltaY) && (deltaX != 0 || deltaY != 0))
						{
							var incrementX = deltaX;
							var incrementY = deltaY;
							var thisDeltaX = deltaX;
							var thisDeltaY = deltaY;
							while (0 <= x + thisDeltaX && x + thisDeltaX < field[0].Length
								&& 0 <= y + thisDeltaY && y + thisDeltaY < field.Length)
							{
								if (field[y + thisDeltaY][x + thisDeltaX] == '#')
								{
									foundAsteroids.Add((x + thisDeltaX, y + thisDeltaY));
									break;
								}
								thisDeltaX += incrementX;
								thisDeltaY += incrementY;
							}
						}
					}
				}
				return foundAsteroids.Count;
			}
		}

		public (int, int)? DestroyAsteroids(int count)
		{
			var minDeltaX = 0 - monitoringStation.Item1;
			var maxDeltaX = field[0].Length - 1 - monitoringStation.Item1;
			var minDeltaY = 0 - monitoringStation.Item2;
			var maxDeltaY = field.Length - 1 - monitoringStation.Item2;
			for (int deltaX = minDeltaX; deltaX <= maxDeltaX; deltaX++)
			{
				for (int deltaY = minDeltaY; deltaY <= maxDeltaY; deltaY++)
				{
					if (IsInLowestTerms(deltaX, deltaY) && (deltaX != 0 || deltaY != 0))
					{
						directions.Add(new Direction(deltaX, deltaY));
					}
				}
			}
			directions.Sort();
			int destroyedCount = 0;
			(int, int)? lastDestroyed = null;
			while (destroyedCount < count)
			{
				var thisDestroyed = TryDestroyNext();
				if (thisDestroyed != null)
				{
					lastDestroyed = thisDestroyed;
					destroyedCount++;
				}
			}
			return lastDestroyed;
		}

		private (int, int)? TryDestroyNext()
		{ 
			var incrementX = directions[nextDirection].GetDeltaX();
			var incrementY = directions[nextDirection].GetDeltaY();
			var thisDeltaX = directions[nextDirection].GetDeltaX();
			var thisDeltaY = directions[nextDirection].GetDeltaY();
			nextDirection = (nextDirection + 1) % directions.Count;
			var x = monitoringStation.Item1;
			var y = monitoringStation.Item2;
			while (0 <= x + thisDeltaX && x + thisDeltaX < field[0].Length
				&& 0 <= y + thisDeltaY && y + thisDeltaY < field.Length)
			{
				if (field[y + thisDeltaY][x + thisDeltaX] == '#')
				{
					
					var thisRow = field[y + thisDeltaY].ToCharArray();
					thisRow[x + thisDeltaX] = '.';
					field[y + thisDeltaY] = new string(thisRow);
					return (x + thisDeltaX, y + thisDeltaY);
				}
				thisDeltaX += incrementX;
				thisDeltaY += incrementY;
			}
			return null;
		}
		private static bool IsInLowestTerms(int x, int y)
		{
			if (x == 0 && Math.Abs(y) == 1)
			{
				return true;
			}
			else if (Math.Abs(x) == 1 && y == 0)
			{
				return true;
			}
			else if (x == 0 || y == 0)
			{
				return false;
			}
			else
			{
				var maxDivisor = Math.Min(Math.Abs(x), Math.Abs(y));
				for (int divisor = 2; divisor <= maxDivisor; divisor++)
				{
					if (Math.Abs(x) % divisor == 0 && Math.Abs(y) % divisor == 0)
					{
						return false;
					}
				}
				return true;
			}
		}


	}
}
