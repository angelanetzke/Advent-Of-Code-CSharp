var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var infected = new HashSet<(int, int)>();
	for (int row = 0; row < allLines.Length; row++)
	{
		for (int column = 0; column < allLines[row].Length; column++)
		{
			if (allLines[row][column] == '#')
			{
				infected.Add((row, column));
			}
		}
	}
	(int row, int column) currentLocation = (allLines.Length / 2, allLines[0].Length / 2);
	var currectDirection = 'N';
	var nextMove = new Dictionary<char, (int row, int column)>()
	{
		{ 'N', (-1, 0) },
		{ 'S', (1, 0) },
		{ 'W', (0, -1) },
		{ 'E', (0, 1) }
	};
	var turnRight = new Dictionary<char, char>()
	{
		{ 'N', 'E' },
		{ 'S', 'W' },
		{ 'W', 'N' },
		{ 'E', 'S' }
	};
	var turnLeft = new Dictionary<char, char>()
	{
		{ 'N', 'W' },
		{ 'S', 'E' },
		{ 'W', 'S' },
		{ 'E', 'N' }
	};
	var infectedCount = 0;
	for (int i = 1; i <= 10_000; i++)
	{
		if (infected.Contains(currentLocation))
		{
			currectDirection = turnRight[currectDirection];
			infected.Remove(currentLocation);
		}
		else
		{
			currectDirection = turnLeft[currectDirection];
			infected.Add(currentLocation);
			infectedCount++;
		}
		currentLocation = (currentLocation.row + nextMove[currectDirection].row
			, currentLocation.column + nextMove[currectDirection].column);
	}
	Console.WriteLine($"Part 1: {infectedCount}");
}

static void Part2(string[] allLines)
{
	var weakened = new HashSet<(int, int)>();
	var infected = new HashSet<(int, int)>();
	var flagged = new HashSet<(int, int)>();
	for (int row = 0; row < allLines.Length; row++)
	{
		for (int column = 0; column < allLines[row].Length; column++)
		{
			if (allLines[row][column] == '#')
			{
				infected.Add((row, column));
			}
		}
	}
	(int row, int column) currentLocation = (allLines.Length / 2, allLines[0].Length / 2);
	var currectDirection = 'N';
	var nextMove = new Dictionary<char, (int row, int column)>()
	{
		{ 'N', (-1, 0) },
		{ 'S', (1, 0) },
		{ 'W', (0, -1) },
		{ 'E', (0, 1) }
	};
	var turnRight = new Dictionary<char, char>()
	{
		{ 'N', 'E' },
		{ 'S', 'W' },
		{ 'W', 'N' },
		{ 'E', 'S' }
	};
	var turnLeft = new Dictionary<char, char>()
	{
		{ 'N', 'W' },
		{ 'S', 'E' },
		{ 'W', 'S' },
		{ 'E', 'N' }
	};
	var reverse = new Dictionary<char, char>()
	{
		{ 'N', 'S' },
		{ 'S', 'N' },
		{ 'W', 'E' },
		{ 'E', 'W' }
	};
	var infectedCount = 0;
	for (int i = 1; i <= 10_000_000; i++)
	{
		if (weakened.Contains(currentLocation))
		{
			weakened.Remove(currentLocation);
			infected.Add(currentLocation);
			infectedCount++;
		}
		else if (infected.Contains(currentLocation))
		{
			currectDirection = turnRight[currectDirection];
			infected.Remove(currentLocation);
			flagged.Add(currentLocation);
		}
		else if (flagged.Contains(currentLocation))
		{
			currectDirection = reverse[currectDirection];
			flagged.Remove(currentLocation);
		}
		else // clean
		{
			currectDirection = turnLeft[currectDirection];
			weakened.Add(currentLocation);
		}
		currentLocation = (currentLocation.row + nextMove[currectDirection].row
			, currentLocation.column + nextMove[currectDirection].column);
	}
	Console.WriteLine($"Part 2: {infectedCount}");
}
