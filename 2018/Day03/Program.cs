using Day03;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var squareList = new List<Square>();
	foreach (string thisLine in allLines)
	{
		squareList.Add(new Square(thisLine));
	}
	var squareSet = new HashSet<(int, int)>();	
	for (int i = 0; i < squareList.Count - 1; i++)
	{
		for (int j = i + 1; j < squareList.Count; j++)
		{
			squareSet.UnionWith(squareList[i].GetOverlap(squareList[j]));
		}
	}
	Console.WriteLine($"Part 1: {squareSet.Count}");
}

static void Part2(string[] allLines)
{
	var squareList = new List<Square>();
	foreach (string thisLine in allLines)
	{
		squareList.Add(new Square(thisLine));
	}
	var id = "";
	for (int i = 0; i < squareList.Count; i++)
	{
		var hasOverlap = false;
		for (int j = 0; j < squareList.Count; j++)
		{
			if (i == j)
			{
				continue;
			}
			if (squareList[i].GetOverlap(squareList[j]).Count > 0)
			{
				hasOverlap = true;
				break;
			}
		}
		if (!hasOverlap)
		{
			id = squareList[i].GetID();
			break;
		}
	}
	Console.WriteLine($"Part 2: {id}");
}
