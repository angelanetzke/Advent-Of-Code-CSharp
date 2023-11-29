namespace Dec18
{
	public class VaultMap
	{		
		private Dictionary<char, List<(char, int)>> simpleMap = new ();
		private Dictionary<char, List<(char, int)>> simpleMapQuad = new ();
		private readonly string allKeysString;

		public VaultMap(string[] map)
		{
			var keyCount = 0;
			var startRow = -1;
			var startColumn = -1;
			List<(int, int)> keyLocations = new ();
			for (int row = 0; row < map.Length; row++)
			{
				for (int column = 0; column < map[0].Length; column++)
				{
					if (char.IsLetter(map[row][column]) || map[row][column] == '@')
					{
						keyLocations.Add((row, column));
					}
					if (char.IsLower(map[row][column]))
					{
						keyCount++;
					}
					if (map[row][column] == '@')
					{
						startRow = row;
						startColumn = column;
					}
				}
			}
			allKeysString = new String('1', keyCount);
			foreach ((int, int) thisLocation in keyLocations)
			{
				simpleMap[map[thisLocation.Item1][thisLocation.Item2]] 
					= GetNeighbors(thisLocation.Item1, thisLocation.Item2, map);
			}
			var mapRow = map[startRow - 1];
			var asCharArray = mapRow.ToCharArray();
			asCharArray[startColumn - 1] = '1';
			asCharArray[startColumn] = '#';
			asCharArray[startColumn + 1] = '2';
			map[startRow - 1] = new string(asCharArray);
			mapRow = map[startRow];
			asCharArray = mapRow.ToCharArray();
			asCharArray[startColumn - 1] = '#';
			asCharArray[startColumn] = '#';
			asCharArray[startColumn + 1] = '#';
			map[startRow] = new string(asCharArray);
			mapRow = map[startRow + 1];
			asCharArray = mapRow.ToCharArray();
			asCharArray[startColumn - 1] = '3';
			asCharArray[startColumn] = '#';
			asCharArray[startColumn + 1] = '4';
			map[startRow + 1] = new string(asCharArray);
			keyLocations.Clear();
			for (int row = 0; row < map.Length; row++)
			{
				for (int column = 0; column < map[0].Length; column++)
				{
					if (char.IsDigit(map[row][column]) || char.IsLetter(map[row][column]))
					{
						keyLocations.Add((row, column));
					}
				}
			}
			foreach ((int, int) thisLocation in keyLocations)
			{
				simpleMapQuad[map[thisLocation.Item1][thisLocation.Item2]] 
					= GetNeighbors(thisLocation.Item1, thisLocation.Item2, map);
			}
		}

		private static List<(char, int)> GetNeighbors(int row, int column, string[] map)
		{
			var neighborList = new List<(char, int)>();
			var visited = new Dictionary<(int, int), int>();
			visited[(row, column)] = 0;
			var queue = new Queue<(int, int)>();
			queue.Enqueue((row, column));
			while (queue.Count > 0)
			{
				var current = queue.Dequeue();
				foreach ((int, int) thisOffset in new (int, int)[]{ (0, -1), (0, 1), (-1, 0), (1, 0) })
				{
					var nextRow = current.Item1 + thisOffset.Item1;
					var nextColumn = current.Item2 + thisOffset.Item2;
					if (0 <= nextRow && nextRow < map.Length
						&& 0 <= nextColumn && nextColumn < map[0].Length
						&& !visited.ContainsKey((nextRow, nextColumn)))
					{
						visited[(nextRow, nextColumn)] = visited[current] + 1;
						var nextChar = map[nextRow][nextColumn];
						if (char.IsLetter(nextChar) || nextChar == '@' || char.IsDigit(nextChar))
						{
							neighborList.Add((nextChar, visited[(nextRow, nextColumn)]));
						}
						else if (nextChar == '.')
						{
							queue.Enqueue((nextRow, nextColumn));
						}
					}
				}
			}
			return neighborList;
		}

		public int GetDistance()
		{
			var visited = new Dictionary<(char, string), int>();
			visited[('@', new String('0', allKeysString.Length))] = 0;
			var queue = new Queue<(char, string)>();
			queue.Enqueue(('@', new String('0', allKeysString.Length)));
			while (queue.Count > 0)
			{
				var current = queue.Dequeue();
				var currentChar = current.Item1;
				var currentKeys = current.Item2;
				var distanceToHere = visited[(current)];				
				if (char.IsLower(currentChar) && currentKeys[currentChar - 'a'] == '0')
				{
					var asCharArray = currentKeys.ToCharArray();
					asCharArray[currentChar - 'a'] = '1';
					currentKeys = new string(asCharArray);
				}
				if (currentKeys == allKeysString)
				{
					return distanceToHere;
				}
				foreach ((char, int) thisNeighbor in simpleMap[currentChar])
				{
					var nextChar = thisNeighbor.Item1;
					var distanceToNext = thisNeighbor.Item2;
					if (char.IsUpper(nextChar) && currentKeys[nextChar - 'A'] == '0')
					{
						continue;
					}
					if (visited.ContainsKey((nextChar, currentKeys)))
					{
						visited[(nextChar, currentKeys)] = Math.Min(visited[(nextChar, currentKeys)],
							distanceToHere + distanceToNext);
					}
					else
					{						
						queue.Enqueue((nextChar, currentKeys));
						visited[(nextChar, currentKeys)] = distanceToHere + distanceToNext;
					}
				}
			}
			return -1;
		}

		public int GetDistanceQuad()
		{
			var visited = new Dictionary<(string, string), int>();
			visited[("1234", new string('0', allKeysString.Length))] = 0;
			var queue = new Queue<(string, string)>();
			queue.Enqueue(("1234", new string('0', allKeysString.Length)));
			while (queue.Count > 0)
			{
				var current = queue.Dequeue();
				var locations = current.Item1;
				var currentKeys = current.Item2;
				var distanceToHere = visited[current];
				for (int i = 0; i < locations.Length; i++)
				{
					if (char.IsLower(locations[i]) && currentKeys[locations[i] - 'a'] == '0')
					{
						var asCharArray = currentKeys.ToCharArray();
						asCharArray[locations[i] - 'a'] = '1';
						currentKeys = new string(asCharArray);
					}
				}
				if (currentKeys == allKeysString)
				{
					return distanceToHere;
				}
				for (int i = 0; i < locations.Length; i++)
				{
					var nextLocations = locations;
					foreach ((char, int) thisNeighbor in simpleMapQuad[locations[i]])
					{
						var nextChar = thisNeighbor.Item1;
						var distanceToNext = thisNeighbor.Item2;
						if (char.IsUpper(nextChar) && currentKeys[nextChar - 'A'] == '0')
						{
							continue;
						}
						var asCharArray = nextLocations.ToCharArray();
						asCharArray[i] = nextChar;
						nextLocations = new string(asCharArray);
						if (visited.ContainsKey((nextLocations, currentKeys)))
						{
							visited[(nextLocations, currentKeys)] = Math.Min(
								visited[(nextLocations, currentKeys)], distanceToHere + distanceToNext);
						}
						else
						{
							visited[(nextLocations, currentKeys)] = distanceToHere + distanceToNext;
							queue.Enqueue((nextLocations, currentKeys));
						}
					}
				}
			}			
			return -1;
		}
		
	}
}