using System.Text;

namespace Day11;

internal class State
{
	private readonly Dictionary<int, List<string>> configuration = [];
	private int elevatorFloor = 1;
	private long visitedValue = 0;
	private int priority = 0;

	public State(string[] allLines)
	{
		int thisFloorValue = 0;
		foreach (string thisLine in allLines)
		{
			string[] parts = thisLine.Split(' ');
			switch(parts[1])
			{
				case "first":
					thisFloorValue = 1;
					break;
				case "second":
					thisFloorValue = 2;
					break;
				case "third":
					thisFloorValue = 3;
					break;
				case "fourth":
					thisFloorValue = 4;
					break;
			}
			configuration[thisFloorValue] = [];
			for (int i = 1; i < parts.Length; i++)
			{
				if (parts[i].StartsWith("generator"))
				{
					configuration[thisFloorValue].Add(parts[i - 1] + " generator");
				}
				if (parts[i].StartsWith("microchip"))
				{
					configuration[thisFloorValue].Add(parts[i - 1].Split('-')[0] + " microchip");
				}
			}
		}
		SetCalculatedValues();
	}

	public State(State old)
	{
		foreach (KeyValuePair<int, List<string>> thisPair in old.configuration)
		{
			configuration[thisPair.Key] = new List<string>(thisPair.Value);
		}
		elevatorFloor = old.elevatorFloor;
	}

	public void AddItem(string itemName)
	{
		configuration[1].Add(itemName + " generator");
		configuration[1].Add(itemName + " microchip");
		SetCalculatedValues();
	}

	public int CountSteps()
	{
		Dictionary<State, State> parents = [];
		HashSet<long> visited = [];
		List<(State, int)> queue = [];
		queue.Add((this, 0));
		while (queue.Count > 0)
		{
			queue = [.. queue.OrderByDescending(x => x.Item1.priority)];
			(State thisState, int thisSteps) = queue[0];
			queue.RemoveAt(0);
			if (thisState.IsComplete())
			{
				return thisSteps;
			}
			visited.Add(thisState.visitedValue);
			foreach (State thisNeighbor in thisState.GetNeighbors())
			{
				if (!visited.Contains(thisNeighbor.visitedValue))
				{
					queue.Add((thisNeighbor, thisSteps + 1));
				}
			}
		}
		return -1;
	}	

	public List<State> GetNeighbors()
	{
		List<State> neighbors = [];		
		List<int> floorsToMoveTo = [elevatorFloor - 1, elevatorFloor + 1];
		List<(string, string)> itemPairs = [];
		for (int i = 0; i < configuration[elevatorFloor].Count - 1; i++)
		{
			for (int j = i + 1; j < configuration[elevatorFloor].Count; j++)
			{
				if (!IsDangerous(configuration[elevatorFloor][i], configuration[elevatorFloor][j]))
				{
					itemPairs.Add((configuration[elevatorFloor][i], configuration[elevatorFloor][j]));
				}
			}
		}
		bool canMoveTwoItemsUpstairs = false;
		bool canMoveOneItemDownstairs = false;
		// Move two items upstairs
		if (elevatorFloor < configuration.Keys.Max())
		{
			foreach ((string item1, string item2) in itemPairs)
			{
				State newState = new (this);
				newState.elevatorFloor = elevatorFloor + 1;
				newState.configuration[elevatorFloor].Remove(item1);
				newState.configuration[elevatorFloor].Remove(item2);
				newState.configuration[newState.elevatorFloor].Add(item1);
				newState.configuration[newState.elevatorFloor].Add(item2);
				if (newState.IsValidState())
				{
					newState.SetCalculatedValues();
					neighbors.Add(newState);
					canMoveTwoItemsUpstairs = true;
				}
			}
		}
		// Move one item upstairs
		if (!canMoveTwoItemsUpstairs && elevatorFloor < configuration.Keys.Max())
		{
			foreach (string thisItem in configuration[elevatorFloor])
			{
				State newState = new (this);
				newState.elevatorFloor = elevatorFloor + 1;
				newState.configuration[elevatorFloor].Remove(thisItem);
				newState.configuration[newState.elevatorFloor].Add(thisItem);
				if (newState.IsValidState())
				{
					newState.SetCalculatedValues();
					neighbors.Add(newState);
				}
			}
		}
		// Move one item downstairs
		if (elevatorFloor > configuration.Keys.Min())
		{
			foreach (string thisItem in configuration[elevatorFloor])
			{
				State newState = new (this);
				newState.elevatorFloor = elevatorFloor - 1;
				newState.configuration[elevatorFloor].Remove(thisItem);
				newState.configuration[newState.elevatorFloor].Add(thisItem);
				if (newState.IsValidState())
				{
					newState.SetCalculatedValues();
					neighbors.Add(newState);
					canMoveOneItemDownstairs = true;
				}
			}
		}
		// Move two items downstairs
		if (!canMoveOneItemDownstairs && elevatorFloor > configuration.Keys.Min())
		{
			foreach ((string item1, string item2) in itemPairs)
			{
				State newState = new (this);
				newState.elevatorFloor = elevatorFloor - 1;
				newState.configuration[elevatorFloor].Remove(item1);
				newState.configuration[elevatorFloor].Remove(item2);
				newState.configuration[newState.elevatorFloor].Add(item1);
				newState.configuration[newState.elevatorFloor].Add(item2);
				if (newState.IsValidState())
				{
					newState.SetCalculatedValues();
					neighbors.Add(newState);
				}
			}
		}
		return neighbors;
	}

	private static bool IsDangerous(string item1, string item2)
	{
		if (item1.EndsWith("generator") && item2.EndsWith("generator"))
		{
			return false;
		}
		if (item1.EndsWith("microchip") && item2.EndsWith("microchip"))
		{
			return false;
		}
		if (item1.Split(' ')[0] == item2.Split(' ')[0])
		{
			return false;
		}
		return true;
	}
	public bool IsComplete()
	{		
		List<KeyValuePair<int, List<string>>> nonEmpty = configuration.Where(x => x.Value.Count > 0).ToList();
		return nonEmpty.Count == 1 && nonEmpty[0].Key == configuration.Keys.Max();
	}

	public bool IsValidState()
	{
		foreach (int thisFloor in configuration.Keys)
		{
			List<string> microchips = configuration[thisFloor].Where(x => x.EndsWith("microchip")).ToList();
			List<string> generators = configuration[thisFloor].Where(x => x.EndsWith("generator")).ToList();
			if (microchips.Count == 0 || generators.Count == 0)
			{
				continue;
			}
			foreach (string thisMicrochip in microchips)
			{
				string microchipType = thisMicrochip.Split(' ')[0];
				if (generators.Any(x => x.StartsWith(microchipType)))
				{
					continue;
				}
				if (generators.Any(x => !x.StartsWith(microchipType)))
				{
					return false;
				}
			}
		}
		return true;
	}

	private void SetCalculatedValues()
	{
		visitedValue = elevatorFloor;
		Dictionary<string, int> itemFloors = [];
		foreach (KeyValuePair<int, List<string>> thisPair in configuration)
		{
			foreach (string thisItem in thisPair.Value)
			{
				itemFloors[thisItem] = thisPair.Key;
			}
		}
		List<int> pairValues = [];
		foreach (string thisItem in itemFloors.Keys)
		{
			if (thisItem.EndsWith("generator"))
			{
				int thisValue = itemFloors[thisItem] * 10;
				thisValue += itemFloors[thisItem.Replace("generator", "microchip")];
				pairValues.Add(thisValue);
			}
		}
		pairValues.Sort();
		foreach (int thisValue in pairValues)
		{
			visitedValue *= 100;
			visitedValue += thisValue;
		}
		if (IsComplete())
		{
			priority = int.MaxValue;
		}
		else
		{
			priority = configuration[4].Count * 100 + configuration[3].Count * 10 + configuration[2].Count;
		}		
	}

	public override string ToString()
	{
		StringBuilder builder = new ();
		foreach (int thisFloor in configuration.Keys)
		{
			configuration[thisFloor].Sort();
			builder.Insert(0, "\n");
			builder.Insert(0, string.Join(", ", configuration[thisFloor]));
			builder.Insert(0, $"Floor {thisFloor}: ");
		}
		builder.Append($"Elevator: {elevatorFloor}");
		return builder.ToString();
	}

	public override bool Equals(object? obj)
	{
		if (obj == null || GetType() != obj.GetType())
		{
			return false;
		}
		State other = (State)obj;
		return ToString() == other.ToString();
	}

	public override int GetHashCode()
	{
		return ToString().GetHashCode();
	}

}