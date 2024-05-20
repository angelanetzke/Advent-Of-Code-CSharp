namespace Dec23;

internal class Burrow
{
	private readonly Dictionary<char, char[]> rooms = [];

	private readonly char[] hallway = Enumerable.Repeat('.', 7).ToArray();
	private int distance = 0;
	private string asString = "";
	private static readonly Dictionary<char, List<(int, int, List<int>)>> roomToHall = new ()
	{
		{ 'A', [ (0, 3, [1]), (1, 2, []), (2, 2, []), (3, 4, [2]), (4, 6, [2, 3]), (5, 8, [2, 3, 4]), (6, 9, [2, 3, 4, 5]) ]},
		{ 'B', [ (0, 5, [1, 2]), (1, 4, [2]), (2, 2, []), (3, 2, []), (4, 4, [3]), (5, 6, [3, 4]), (6, 7, [3, 4, 5]) ]},
		{ 'C', [ (0, 7, [1, 2, 3]), (1, 6, [2, 3]), (2, 4, [3]), (3, 2, []), (4, 2, []), (5, 4, [4]), (6, 5, [4, 5]) ]},
		{ 'D', [ (0, 9, [1, 2, 3, 4]), (1, 8, [2, 3, 4]), (2, 6, [3, 4]), (3, 4, [4]), (4, 2, []), (5, 2, []), (6, 3, [5]) ]},
	};

	private static readonly Dictionary<char, int> costs = new ()
	{
		{ 'A', 1 }, { 'B', 10 }, { 'C', 100 }, { 'D', 1000 }
	};

	private static int bestSoFar = int.MaxValue;
	private static Dictionary<string, int> cache = [];

	public Burrow(List<char[]> initialRooms)
	{
		for (int i = 0; i < initialRooms.Count; i++)
		{
			rooms[(char)('A' + i)] = initialRooms[i];
		}
	}

	public Burrow(Burrow old)
	{
		hallway = new char[old.hallway.Length];
		Array.Copy(old.hallway, hallway, old.hallway.Length);
		foreach (char thisRoomID in old.rooms.Keys)
		{
			rooms[thisRoomID] = new char[old.rooms[thisRoomID].Length];
			Array.Copy(old.rooms[thisRoomID], rooms[thisRoomID], old.rooms[thisRoomID].Length);
		}
	}

	public int CountSteps()
	{
		distance = 0;
		cache.Clear();
		bestSoFar = int.MaxValue;
		return CountStepsRecursive();
	}

	private int CountStepsRecursive()
	{
		if (cache.ContainsKey(ToString()))
		{
			return cache[ToString()];
		}
		if (IsComplete())
		{
			bestSoFar = Math.Min(bestSoFar, distance);
			cache[ToString()] = distance;
			return distance;
		}
		if (distance > bestSoFar)
		{
			cache[ToString()] = int.MaxValue;
			return int.MaxValue;
		}
		int stepCount = int.MaxValue;
		List<(Burrow, int)> neighbors = GetNeighbors();
		foreach ((Burrow next, int cost) in neighbors)
		{
			next.distance = distance + cost;
			int thisStepCount = next.CountStepsRecursive();
			stepCount = Math.Min(stepCount, thisStepCount);
		}
		cache[ToString()] = stepCount;
		return stepCount;
	}

	private List<(Burrow, int)> GetNeighbors()
	{
		List<(Burrow, int)> neighbors = [];
		for (int i = 0; i < hallway.Length; i++)
		{
			if (hallway[i] == '.')
			{
				continue;
			}
			foreach (char thisRoomID in rooms.Keys)
			{
				int roomEnterIndex = GetRoomEnterIndex(i, thisRoomID);
				if (roomEnterIndex == -1)
				{
					continue;
				}
				Burrow next = new (this);
				next.rooms[thisRoomID][roomEnterIndex] = hallway[i];
				next.hallway[i] = '.';
				int cost = (roomToHall[thisRoomID].Where(x => x.Item1 == i).First().Item2 + roomEnterIndex) 
					* costs[hallway[i]];
				neighbors.Add((next, cost));
			}
		}
		foreach (char thisRoomID in rooms.Keys)
		{
			(char, int)? nextFromRoom = GetNextFromRoom(thisRoomID);
			if (nextFromRoom == null)
			{
				continue;
			}
			for (int i = 0; i < hallway.Length; i++)
			{
				if (CanMoveRoomToHall(thisRoomID, i))
				{
					Burrow next = new (this);
					next.hallway[i] = nextFromRoom.Value.Item1;
					next.rooms[thisRoomID][nextFromRoom.Value.Item2] = '.';
					int cost = (roomToHall[thisRoomID].Where(x => x.Item1 == i).First().Item2 + nextFromRoom.Value.Item2) 
						* costs[nextFromRoom.Value.Item1];
					neighbors.Add((next, cost));
				}
			}			
		}
		return neighbors;
	}

	private (char, int)? GetNextFromRoom(char roomID)
	{
		if (rooms[roomID].All(x => x == '.'))
		{
			return null;
		}
		for (int i = 0; i < rooms[roomID].Length; i++)
		{
			if (rooms[roomID][i] != '.')
			{
				if (rooms[roomID][i] == roomID)
				{
					for (int j = i + 1; j < rooms[roomID].Length; j++)
					{
						if (rooms[roomID][j] != roomID)
						{
							return (rooms[roomID][i], i);
						}
					}
					return null;
				}
				else
				{
					return (rooms[roomID][i], i);
				}
			}
		}
		return null;
	}

	private int GetRoomEnterIndex(int hallwayIndex, char roomID)
	{
		if (hallway[hallwayIndex] != roomID)
		{
			return -1;
		}
		(int, int, List<int>) data = roomToHall[roomID].Where(x => x.Item1 == hallwayIndex).First();
		foreach (int thisPassingIndex in data.Item3)
		{
			if (hallway[thisPassingIndex] != '.')
			{
				return -1;
			}
		}
		for (int i = rooms[roomID].Length - 1; i >= 0; i--)
		{
			if (rooms[roomID][i] == '.')
			{
				return i;
			}
			if (rooms[roomID][i] != roomID)
			{
				return -1;
			}
		}
		return -1;
	}

	private bool CanMoveRoomToHall(char roomID, int hallwayIndex)
	{		
		if (hallway[hallwayIndex] != '.')
		{
			return false;
		}
		(int, int, List<int>) data = roomToHall[roomID].Where(x => x.Item1 == hallwayIndex).First();
		foreach (int thisPassingIndex in data.Item3)
		{
			if (hallway[thisPassingIndex] != '.')
			{
				return false;
			}
		}		
		return true;
	}

	private bool IsComplete()
	{
		foreach (char thisRoomID in rooms.Keys)
		{
			foreach (char thisOccupant in rooms[thisRoomID])
			{
				if (thisOccupant != thisRoomID)
				{
					return false;
				}
			}
		}
		return true;
	}

	public override bool Equals(object? obj)
	{
		if (obj is Burrow other)
		{
			return ToString() == other.ToString();
		}
		return false;
	}

	public override int GetHashCode()
	{
		return ToString().GetHashCode();
	}

	public override string ToString()
	{
		if (asString != "")
		{
			return asString;
		}
		System.Text.StringBuilder builder = new ();
		foreach (char thisHallLocation in hallway)
		{
			builder.Append(thisHallLocation);
		}
		foreach (char thisRoomID in rooms.Keys.OrderBy(x => x))
		{
			foreach (char thisOccupant in rooms[thisRoomID])
			{
				builder.Append(thisOccupant);
			}
		}
		builder.Append(distance);
		asString = builder.ToString();
		return asString;
	}

}