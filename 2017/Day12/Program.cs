var allLines = File.ReadAllLines("input.txt");
var connections = new Dictionary<int, HashSet<int>>();
	foreach (string thisLine in allLines)
	{
		var thisID = int.Parse(thisLine.Split(" <-> ")[0]);
		connections[thisID] = new ();
	}
	foreach (string thisLine in allLines)
	{
		var thisID = int.Parse(thisLine.Split(" <-> ")[0]);
		var otherArray = thisLine.Split(" <-> ")[1].Split(", ");
		foreach (string thisOther in otherArray)
		{
			connections[thisID].Add(int.Parse(thisOther));
		}
	}
Part1(connections);
Part2(connections);

void Part1(Dictionary<int, HashSet<int>> connections)
{	
	var connectedTo0 = new HashSet<int>();
	var queue = new Queue<int>();
	queue.Enqueue(0);
	while (queue.Count > 0)
	{
		var current = queue.Dequeue();
		connectedTo0.Add(current);
		foreach (int next in connections[current])
		{
			if (!connectedTo0.Contains(next) && !queue.Contains(next))
			{
				queue.Enqueue(next);
			}
		}
	}
	Console.WriteLine($"Part 1: {connectedTo0.Count}");
}

void Part2(Dictionary<int, HashSet<int>> connections)
{
	var groups = new Dictionary<int, int>();
	var groupID = 0;
	foreach (int thisID in connections.Keys)
	{
		if (groups.ContainsKey(thisID))
		{
			continue;
		}
		var connectedToThisID = new HashSet<int>();
		var queue = new Queue<int>();
		queue.Enqueue(thisID);
		while (queue.Count > 0)
		{
			var current = queue.Dequeue();
			connectedToThisID.Add(current);
			foreach (int next in connections[current])
			{
				if (!connectedToThisID.Contains(next) && !queue.Contains(next))
				{
					queue.Enqueue(next);
				}
			}
		}
		foreach (int thisConnectedID in connectedToThisID)
		{
			groups[thisConnectedID] = groupID;
		}
		groupID++;
	}
	Console.WriteLine($"Part 2: {groupID}");
}