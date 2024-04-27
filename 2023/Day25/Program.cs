string[] allLines = File.ReadAllLines("input.txt");
Dictionary<string, List<string>> components = [];
foreach (string thisLine in allLines)
{
	string thisName = thisLine.Split(": ")[0];
	string[] neighborNames = thisLine.Split(": ")[1].Split(' ');
	foreach (string thisNeighborName in neighborNames)
	{
		if (components.TryGetValue(thisName, out List<string>? thisList))
		{
			thisList.Add(thisNeighborName);
		}
		else
		{
			components[thisName] = [ thisNeighborName ];
		}
		if (components.TryGetValue(thisNeighborName, out List<string>? thisNeighborList))
		{
			thisNeighborList.Add(thisName);
		}
		else
		{
			components[thisNeighborName] = [ thisName ];
		}
	}
}
Random RNG = new ();	
Dictionary<string, List<string>> temp = CopyDictionary(components);
List<HashSet<string>> groups = [];
while (temp[temp.Keys.First()].Count != 3)
{
	temp = CopyDictionary(components);
	groups.Clear();
	foreach (string thisName in temp.Keys)
	{
		HashSet<string> thisGroup = [thisName];
		groups.Add(thisGroup);
	}	
	while (temp.Count > 2)
	{
		string destinationComponent = temp.Keys.ElementAt(RNG.Next(temp.Count)); 
		List<string> destinationList = temp[destinationComponent];
		string sourceComponent = destinationList[RNG.Next(destinationList.Count)];
		List<string> sourceList = temp[sourceComponent];
		destinationList.AddRange(sourceList);
		temp.Remove(sourceComponent);
		foreach (var neighbor in sourceList)
		{
			List<string> neighborList = temp[neighbor];
			for (int i = 0; i < neighborList.Count; i++)
			{
				if (neighborList[i] == sourceComponent)
				{
					neighborList[i] = destinationComponent;
				}
			}
		}
		destinationList.RemoveAll(n => n == destinationComponent);
		destinationList.Remove(sourceComponent);
		HashSet<string>? sourceGroup = groups.Find(g => g.Contains(sourceComponent));
		HashSet<string>? destinationGroup = groups.Find(g => g.Contains(destinationComponent));
		if (sourceGroup != null && destinationGroup != null)
		{
			destinationGroup.UnionWith(sourceGroup);
			groups.Remove(sourceGroup);
		}
	}
}
int answer = groups[0].Count * groups[1].Count;
Console.WriteLine($"Part 1: {answer}");

static Dictionary<string, List<string>> CopyDictionary(Dictionary<string, List<string>> originalDictionary)
{
	Dictionary<string, List<string>> copiedDictionary = [];	
	foreach (var thisPair in originalDictionary)
	{
		List<string> copiedList = new (thisPair.Value);
		copiedDictionary.Add(thisPair.Key, copiedList);
	}	
	return copiedDictionary;
}

