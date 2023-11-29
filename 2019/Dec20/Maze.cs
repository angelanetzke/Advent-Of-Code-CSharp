using System.Text.RegularExpressions;

namespace Dec20
{
	internal class Maze
	{
		private Dictionary<string, List<(string, int)>> simpleMap = new ();

		public Maze(string[] allLines)
		{
			var keyPoints = new Dictionary<(int, int), string>();
			var characterRegex = new Regex(@"\w");
			var leftNameRegex = new Regex(@"\w{2}\.");
			var rightNameRegex = new Regex(@"\.\w{2}");
			var linesWithCharacters = new List<int>();
			for (int row = 0; row < allLines.Length; row++)
			{
				if (characterRegex.IsMatch(allLines[row]))
				{
					linesWithCharacters.Add(row);
				}
			}
			for (int i = 0; i < linesWithCharacters.Count; i++)
			{
				var thisRow = linesWithCharacters[i];
				if (i == 0)
				{
					var matches = characterRegex.Matches(allLines[thisRow]);
					foreach (Match thisMatch in matches)
					{
						var pointName = allLines[thisRow][thisMatch.Index].ToString() 
							+ allLines[thisRow + 1][thisMatch.Index];
						if (pointName != "AA" && pointName != "ZZ")
						{
							pointName +=  "-OUTER";
						}
						keyPoints[(thisRow + 2, thisMatch.Index)] = pointName;
					}
				}
				else if (i == linesWithCharacters.Count - 4)
				{
					var matches = characterRegex.Matches(allLines[thisRow]);
					foreach (Match thisMatch in matches)
					{
						var pointName = allLines[thisRow][thisMatch.Index].ToString() 
							+ allLines[thisRow + 1][thisMatch.Index];
						if (pointName != "AA" && pointName != "ZZ")
						{
							pointName  += "-INNER";
						}
						keyPoints[(thisRow + 2, thisMatch.Index)] = pointName;
					}
				}
				else if (i == 2)
				{
					var matches = characterRegex.Matches(allLines[thisRow]);
					foreach (Match thisMatch in matches)
					{
						var pointName = allLines[thisRow][thisMatch.Index].ToString() 
							+ allLines[thisRow + 1][thisMatch.Index];
						if (pointName != "AA" && pointName != "ZZ")
						{
							pointName += "-INNER";
						}
						keyPoints[(thisRow - 1, thisMatch.Index)] = pointName;
					}
				}
				else if (i == linesWithCharacters.Count - 2)
				{
					var matches = characterRegex.Matches(allLines[thisRow]);
					foreach (Match thisMatch in matches)
					{
						var pointName = allLines[thisRow][thisMatch.Index].ToString() 
							+ allLines[thisRow + 1][thisMatch.Index];
						if (pointName != "AA" && pointName != "ZZ")
						{
							pointName += "-OUTER";
						}
						keyPoints[(thisRow - 1, thisMatch.Index)] = pointName;
					}
				}
				else
				{
					var matches = leftNameRegex.Matches(allLines[thisRow]);
					foreach (Match thisMatch in matches)
					{
						var pointName = allLines[thisRow].Substring(thisMatch.Index, 2);
						if (pointName != "AA" && pointName != "ZZ")
						{
							if (thisMatch.Index == 0)
							{
								pointName += "-OUTER";
							}
							else
							{
								pointName += "-INNER";
							}
						}
						keyPoints[(thisRow, thisMatch.Index + 2)] = pointName;
					}
					matches = rightNameRegex.Matches(allLines[thisRow]);
					foreach (Match thisMatch in matches)
					{
						var pointName = allLines[thisRow].Substring(thisMatch.Index + 1, 2);
						if (pointName != "AA" && pointName != "ZZ")
						{
							if (thisMatch.Index == allLines[0].Length - 3)
							{
								pointName += "-OUTER";
							}
							else
							{
								pointName += "-INNER";
							}
						}
						keyPoints[(thisRow, thisMatch.Index)] = pointName;
					}
				}
			}
			foreach ((int, int) thisPoint in keyPoints.Keys)
			{
				if (simpleMap.ContainsKey(keyPoints[thisPoint]))
				{
					simpleMap[keyPoints[thisPoint]].AddRange(
						GetNeighbors(thisPoint, allLines, keyPoints));
				}
				else
				{
					simpleMap[keyPoints[thisPoint]] = 
						GetNeighbors(thisPoint, allLines, keyPoints);
				}
			}
			foreach (string thisPoint in simpleMap.Keys)
			{
				if (thisPoint.Substring(2) == "-INNER")
				{
					simpleMap[thisPoint].Add((thisPoint.Substring(0, 2) + "-OUTER", 1));
				}
				else if (thisPoint.Substring(2) == "-OUTER")
				{
					simpleMap[thisPoint].Add((thisPoint.Substring(0, 2) + "-INNER", 1));
				}
			}
		}

		private List<(string, int)> GetNeighbors(
			(int, int) point, string[] allLines, Dictionary<(int, int), string> keyPoints)
		{
			var neighborList = new List<(string, int)>();
			var visited = new Dictionary<(int, int), int>();
			visited[point] = 0;
			var queue = new Queue<(int, int)>();
			queue.Enqueue(point);
			while (queue.Count > 0)
			{
				var current = queue.Dequeue();
				foreach ((int, int) nextOffset in new (int, int)[] { (0, -1), (0, 1), (-1, 0), (1, 0) })
				{
					var thisRow = current.Item1 + nextOffset.Item1;
					var thisColumn = current.Item2 + nextOffset.Item2;
					if (visited.ContainsKey((thisRow, thisColumn)))
					{
						continue;
					}
					if (keyPoints.ContainsKey((thisRow, thisColumn)))
					{
						neighborList.Add((keyPoints[(thisRow, thisColumn)], visited[current] + 1));
					}
					else if (allLines[thisRow][thisColumn] == '.')
					{
						visited[(thisRow, thisColumn)] = visited[current] + 1;
						queue.Enqueue((thisRow, thisColumn));
					}
				}
			}
			return neighborList;
		}

		public int GetDistance()
		{
			var visited = new Dictionary<string, int>();
			visited["AA"] = 0;
			var queue = new Queue<string>();
			queue.Enqueue("AA");
			while (queue.Count > 0)
			{
				var current = queue.Dequeue();
				if (current == "ZZ")
				{
					return visited[current];
				}
				foreach ((string, int) thisNeighbor in simpleMap[current])
				{
					if (visited.ContainsKey(thisNeighbor.Item1))
					{
						visited[thisNeighbor.Item1] = 
							Math.Min(visited[thisNeighbor.Item1], visited[current] + thisNeighbor.Item2);
					}
					else
					{
						visited[thisNeighbor.Item1] = visited[current] + thisNeighbor.Item2;
						queue.Enqueue(thisNeighbor.Item1);
					}					
				}
			}
			return -1;
		}

		public int GetDistanceWithLevels()
		{
			var visited = new Dictionary<(string, string, int), int>();
			visited[("AA", "", 0)] = 0;
			var queue = new Queue<(string, string, int)>();
			queue.Enqueue(("AA", "", 0));
			while (queue.Count > 0)
			{
				var current = queue.Dequeue();
				var currentLocation = current.Item1;
				var lastLocation = current.Item2;
				var currentLevel = current.Item3;
				if (currentLocation == "ZZ" && currentLevel == 0)
				{
					return visited[current];
				}
				if (currentLocation.Contains("-INNER") 
					&& currentLocation.Substring(0, 2) != lastLocation.Substring(0, 2))
				{
					currentLevel++;
				}
				else if (currentLocation.Contains("-OUTER")
					&& currentLocation.Substring(0, 2) != lastLocation.Substring(0, 2))
				{
					currentLevel--;
				}
				if (currentLevel >= 0)
				{
					foreach ((string, int) thisNeighbor in simpleMap[currentLocation])
					{
						if (lastLocation != ""
							&& currentLocation.Substring(0, 2) != lastLocation.Substring(0, 2)
							&& thisNeighbor.Item1.Substring(0, 2) != currentLocation.Substring(0, 2))
						{
							continue;
						}
						if (lastLocation != ""
							&& currentLocation.Substring(0, 2) == lastLocation.Substring(0, 2)
							&& thisNeighbor.Item1.Substring(0, 2) == currentLocation.Substring(0, 2))
						{
							continue;
						}
						if (visited.ContainsKey((thisNeighbor.Item1, currentLocation, currentLevel)))
						{
							visited[(thisNeighbor.Item1, currentLocation, currentLevel)] = 
								Math.Min(visited[(thisNeighbor.Item1, currentLocation, currentLevel)], visited[current] + thisNeighbor.Item2);
						}
						else
						{
							visited[(thisNeighbor.Item1, currentLocation, currentLevel)] = visited[current] + thisNeighbor.Item2;
							queue.Enqueue((thisNeighbor.Item1, currentLocation, currentLevel));
						}	
					}
				}
			}
			return -1;
		}


	}
}