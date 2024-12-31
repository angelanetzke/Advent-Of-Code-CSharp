using System.Net.Http.Headers;

namespace Day16;

internal class Maze
{
	private readonly (int, int) start;
	private readonly (int, int) end;
	private readonly HashSet<(int, int)> traversible;
	private readonly Dictionary<char, char> rotateLeft = new ()
	{
		['N'] = 'W',
		['S'] = 'E',
		['W'] = 'S',
		['E'] = 'N'
	};
	private readonly Dictionary<char, char> rotateRight = new ()
	{
		['N'] = 'E',
		['S'] = 'W',
		['W'] = 'N',
		['E'] = 'S'
	};
	private readonly Dictionary<char, (int, int)> deltas = new ()
	{
		['N'] = (-1, 0),
		['S'] = (1, 0),
		['W'] = (0, -1),
		['E'] = (0, 1)
	};
	long lowestScore = long.MaxValue;

	public Maze(string[] data)
	{
		start = data
			.SelectMany((row, rowIndex)
				=> row.Select((x, columnIndex) => new {Value = x, Row = rowIndex, Column = columnIndex})
			)
			.Where(x => x.Value == 'S')
			.Select(x => (x.Row, x.Column))
			.FirstOrDefault();
		end = data
			.SelectMany((row, rowIndex)
				=> row.Select((x, columnIndex) => new {Value = x, Row = rowIndex, Column = columnIndex})
			)
			.Where(x => x.Value == 'E')
			.Select(x => (x.Row, x.Column))
			.FirstOrDefault();
		traversible = data
			.SelectMany((row, rowIndex)
				=> row.Select((x, columnIndex) => new {Value = x, Row = rowIndex, Column = columnIndex})
			)
			.Where(x => x.Value == '.' || x.Value == 'S' || x.Value == 'E')
			.Select(x => (x.Row, x.Column))
			.ToHashSet();
	}

	public long Part1()
	{
		HashSet<(int, int)> visited = [];
		List<(int, int, char)> queue = [];
		queue.Add((start.Item1, start.Item2, 'E'));
		Dictionary<(int, int), long> distances = new ()
		{
			[start] = 0L
		};
		while (queue.Count > 0)
		{
			queue = [.. queue.OrderBy(x => distances[(x.Item1, x.Item2)])];
			(int, int, char) current = queue[0];			
			if (current.Item1 == end.Item1 && current.Item2 == end.Item2)
			{
				lowestScore = distances[(current.Item1, current.Item2)];
				return lowestScore;
			}
			queue.RemoveAt(0);
			visited.Add((current.Item1, current.Item2));
			List<(int, int, char)> next = 
			[
				(current.Item1 + deltas[current.Item3].Item1, current.Item2 + deltas[current.Item3].Item2, current.Item3),
				(current.Item1 + deltas[rotateLeft[current.Item3]].Item1, current.Item2 + deltas[rotateLeft[current.Item3]].Item2, rotateLeft[current.Item3]),
				(current.Item1 + deltas[rotateRight[current.Item3]].Item1, current.Item2 + deltas[rotateRight[current.Item3]].Item2, rotateRight[current.Item3])
			];
			foreach ((int, int, char) thisNext in next)
			{
				if (visited.Contains((thisNext.Item1, thisNext.Item2)) || !traversible.Contains((thisNext.Item1, thisNext.Item2)))
				{
					continue;
				}
				long nextDistance = distances[(current.Item1, current.Item2)] + 1;
				if (thisNext.Item3 != current.Item3)
				{
					nextDistance += 1000L;
				}
				var previous = queue.Where(x => x.Item1 == thisNext.Item1 && x.Item2 == thisNext.Item2);
				if (previous.Any())
				{
					if (nextDistance < distances[(thisNext.Item1, thisNext.Item2)])
					{
						queue.Remove(previous.First());
						queue.Add(thisNext);
						distances[(thisNext.Item1, thisNext.Item2)] = nextDistance;
					}
				}
				else
				{
					queue.Add(thisNext);
					distances[(thisNext.Item1, thisNext.Item2)] = nextDistance;
				}				
			}
		}
		return -1L;
	}

	public long Part2()
	{
		if (lowestScore == -1L)
		{
			Part1();
		}
		Dictionary<(int, int, char), List<(int, int, char)>> parents = new()
		{
			[(start.Item1, start.Item2, 'E')] = []
		};
		HashSet<(int, int, char)> visited = [];
		List<(int, int, char)> queue = [];
		queue.Add((start.Item1, start.Item2, 'E'));
		Dictionary<(int, int, char), long> distances = new ()
		{
			[(start.Item1, start.Item2, 'E')] = 0L
		};
		while (queue.Count > 0)
		{
			queue = [.. queue.OrderBy(x => distances[x])];
			(int, int, char) current = queue[0];			
			if (current.Item1 == end.Item1 && current.Item2 == end.Item2)
			{
				if (distances[current] == lowestScore)
				{
					return CountUsedTiles(parents);
				}
				else
				{
					break;
				}
			}
			queue.RemoveAt(0);
			visited.Add(current);
			List<(int, int, char)> next = 
			[
				(current.Item1 + deltas[current.Item3].Item1, current.Item2 + deltas[current.Item3].Item2, current.Item3),
				(current.Item1 + deltas[rotateLeft[current.Item3]].Item1, current.Item2 + deltas[rotateLeft[current.Item3]].Item2, rotateLeft[current.Item3]),
				(current.Item1 + deltas[rotateRight[current.Item3]].Item1, current.Item2 + deltas[rotateRight[current.Item3]].Item2, rotateRight[current.Item3])
			];
			foreach ((int, int, char) thisNext in next)
			{
				if (visited.Contains(thisNext) || !traversible.Contains((thisNext.Item1, thisNext.Item2)))
				{
					continue;
				}
				long nextDistance = distances[current] + 1;
				if (thisNext.Item3 != current.Item3)
				{
					nextDistance += 1000L;
				}
				var previous = queue.Where(x => x == thisNext);
				if (previous.Any())
				{
					if (nextDistance == distances[thisNext])
					{
						queue.Remove(previous.First());
						queue.Add(thisNext);
						distances[thisNext] = nextDistance;
						parents[thisNext].Add(current);
					}
					else if (nextDistance < distances[thisNext])
					{
						queue.Remove(previous.First());
						queue.Add(thisNext);
						distances[thisNext] = nextDistance;
						parents[thisNext] = [current];
					}
				}
				else
				{
					queue.Add(thisNext);
					distances[thisNext] = nextDistance;
					parents[thisNext] = [current];
				}				
			}
		}
		return -1L;		
	}

	private long CountUsedTiles(Dictionary<(int, int, char), List<(int, int, char)>> parents)
	{
		HashSet<(int, int, char)> visited = [];
		HashSet<(int, int)> usedTiles = [];
		var endPoints = parents.Keys.Where(x => x.Item1 == end.Item1 && x.Item2 == end.Item2);
		Queue<(int, int, char)> queue = [];
		foreach (var thisEndPoint in endPoints)
		{
			queue.Enqueue(thisEndPoint);
		}
		while (queue.Count > 0)
		{
			var current = queue.Dequeue();

			visited.Add(current);
			usedTiles.Add((current.Item1, current.Item2));
			var next = parents[current];
			foreach (var thisNext in next)
			{
				if (visited.Contains(thisNext) || queue.Contains(thisNext))
				{
					continue;
				}
				queue.Enqueue(thisNext);
			}
		}
		return usedTiles.Count;
	}
		
}