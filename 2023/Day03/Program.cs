using System.Text.RegularExpressions;

string[] allLines = File.ReadAllLines("input.txt");
Part1And2(allLines);

static void Part1And2(string[] allLines)
{
	int partNumberSum = 0;
	HashSet<(int, int)> symbolNeighbors = [];
	Dictionary<(int, int), HashSet<int>> gears = [];
	for (int row = 0; row < allLines.Length; row++)
	{
		int currentNumber = 0;
		bool isNumberInProgress = false;
		symbolNeighbors.Clear();
		for (int column = 0; column < allLines[row].Length; column++)
		{
			if (char.IsDigit(allLines[row][column]))
			{
				currentNumber = currentNumber * 10 + int.Parse(allLines[row][column].ToString());
				isNumberInProgress = true;
				GetSymbolNeighbors(allLines, row, column).ForEach(x => symbolNeighbors.Add(x));			
			}
			else if (isNumberInProgress)
			{
				if (symbolNeighbors.Count > 0)
				{
					partNumberSum += currentNumber;
					AddToGears(symbolNeighbors, gears, currentNumber);
				}
				currentNumber = 0;
				isNumberInProgress = false;
				symbolNeighbors.Clear();
			}
		}
		if (isNumberInProgress && symbolNeighbors.Count > 0)
		{
			partNumberSum += currentNumber;
			AddToGears(symbolNeighbors, gears, currentNumber);
		}		
	}
	Console.WriteLine($"Part 1: {partNumberSum}");
	int gearRatioSum = 0;
	foreach ((int, int) thisSymbol in gears.Keys)
	{
		List<int> thisPartList = gears[thisSymbol].ToList();
		if (thisPartList.Count == 2)
		{
			gearRatioSum += thisPartList[0] * thisPartList[1];
		}
	}
	Console.WriteLine($"Part 2: {gearRatioSum}");
}

static void AddToGears(HashSet<(int, int)> symbolNeighbors,
	Dictionary<(int, int), HashSet<int>> gears, int partNumber)
{
	foreach ((int, int) thisNeighbor in symbolNeighbors)
	{
		if (gears.ContainsKey(thisNeighbor))
		{
			gears[thisNeighbor].Add(partNumber);
		}
		else
		{
			gears[thisNeighbor] = [partNumber];
		}
	}
}

static List<(int, int)> GetSymbolNeighbors(string[] allLines, int row, int column)
{
	List<(int, int)> symbolNeighbors = [];
	for (int deltaRow = -1; deltaRow <= 1; deltaRow++)
	{
		for (int deltaColumn = -1; deltaColumn <= 1; deltaColumn++)
		{
			if (row + deltaRow < 0 || row + deltaRow >= allLines.Length
				|| column + deltaColumn < 0 || column + deltaColumn >= allLines[0].Length )
			{
				continue;
			}
			if (deltaRow == 0 && deltaColumn == 0)
			{
				continue;
			}
			if (!Regex.IsMatch(allLines[row + deltaRow][column + deltaColumn].ToString(), @"[0-9\.]"))
			{
				symbolNeighbors.Add((row + deltaRow, column + deltaColumn));
			}
		}
	}
	return symbolNeighbors;
}