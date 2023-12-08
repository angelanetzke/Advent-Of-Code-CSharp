string[] allLines = File.ReadAllLines("input.txt");
Dictionary<string, string[]> nodes = [];
for (int i = 2; i < allLines.Length; i++)
{
	string thisKey = allLines[i][..3];
	string[] thisValue = allLines[i][7..15].Split(", ");
	nodes[thisKey] = thisValue;
}
string directions = allLines[0];
Console.WriteLine($"Part 1: {Part1(directions, nodes)}");
Console.WriteLine($"Part 2: {Part2(directions, nodes)}");

static int Part1(string directions, Dictionary<string, string[]> nodes)
{	
	return GetMoveCount(nodes, directions, "AAA", ["ZZZ"]);
}

static long Part2(string directions, Dictionary<string, string[]> nodes)
{	
	List<string> startNodes = nodes.Keys.Where(x => x.EndsWith('A')).ToList();
	List<string> endNodes = nodes.Keys.Where(x => x.EndsWith('Z')).ToList();
	List<int> moveCounts = [];
	foreach (string thisStart in startNodes)
	{
		//GetCycle(nodes, directions, thisStart, endNodes);
		moveCounts.Add(GetMoveCount(nodes, directions, thisStart, endNodes));
	}
	long lcm = moveCounts[0];
	for (int i = 1; i < moveCounts.Count; i++)
	{
		lcm = GetLCM(lcm, moveCounts[i]);
	}
	return lcm;
}

static int GetMoveCount(Dictionary<string, string[]> nodes, string directions, string start, List<string> end)
{
	string current = start;
	int moveCount = 0;
	int step = 0;
	while (!end.Contains(current))
	{
		moveCount++;
		char thisDirection = directions[step];
		current = thisDirection == 'L' ? nodes[current][0] : nodes[current][1];
		step = (step + 1) % directions.Length;
	}
	return moveCount;
}

static long GetLCM(long number1, long number2)
{
	return Math.Abs(number1 * number2) / GetGCD(number1, number2);
}

static long GetGCD(long number1, long number2)
{
	while (number2 != 0)
	{
		long temp = number2;
		number2 = number1 % number2;
		number1 = temp;
	}
	return number1;
}

/*
static void GetCycle(Dictionary<string, string[]> nodes, string directions, string start, List<string> end)
{
	string current = start;
	Console.Write($"{current}: ");
	int endFoundCount = 0;
	int step = 0;
	int moveCount = 0;
	int lastEnd = 0;
	while (endFoundCount < 7)
	{
		moveCount++;
		char thisDirection = directions[step];
		current = thisDirection == 'L' ? nodes[current][0] : nodes[current][1];
		if (current.EndsWith('Z'))
		{
			Console.Write($"{moveCount - lastEnd} ");
			lastEnd = moveCount;
			endFoundCount++;
		}
		step = (step + 1) % directions.Length;
	}
	Console.WriteLine();
}
*/

