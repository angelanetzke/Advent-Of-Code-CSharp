var inputLine = File.ReadAllLines("input.txt")[0];
Part1(inputLine);
Part2(inputLine);

static void Part1(string inputLine)
{
	var gridSize = 300;
	var squareSize = 3;
	var serialNumber = int.Parse(inputLine);
	var highestPowerLevel = int.MinValue;
	var highestX = -1;
	var highestY = -1;
	for (int x = 1; x <= gridSize - squareSize + 1; x++)
	{
		for (int y = 1; y <= gridSize - squareSize + 1; y++)
		{
			var totalPowerLevel = 0;
			for (int deltaX = 0; deltaX <= squareSize - 1; deltaX++)
			{
				for (int deltaY = 0; deltaY <= squareSize - 1; deltaY++)
				{
					totalPowerLevel += GetPowerLevel(x + deltaX, y + deltaY, serialNumber);
				}
			}
			if (totalPowerLevel > highestPowerLevel)
			{
				highestPowerLevel = totalPowerLevel;
				highestX = x;
				highestY = y;
			}
		}
	}
	Console.WriteLine($"Part 1: {highestX},{highestY}");
}

static void Part2(string inputLine)
{
	var gridSize = 300;
	var serialNumber = int.Parse(inputLine);
	var sums = new Dictionary<(int, int), int>();
	for (int x = 1; x <= gridSize; x++)
	{
		for (int y = 1; y <= gridSize; y++)
		{
			var thisSum = GetPowerLevel(x, y, serialNumber);
			if (x >= 2)
			{
				thisSum += sums[(x - 1, y)];
			}
			if (y >= 2)
			{
				thisSum += sums[(x, y - 1)];
			}
			if (x >= 2 && y >= 2)
			{
				thisSum -= sums[(x - 1, y - 1)];
			}
			sums[(x, y)] = thisSum;
		}
	}
	var highestPowerLevel = int.MinValue;
	var highestX = -1;
	var highestY = -1;
	var highestSize = -1;
	for (int squareSize = 1; squareSize <= gridSize; squareSize++)
	{
		for (int x = 1; x <= gridSize - squareSize + 1; x++)
		{
			for (int y = 1; y <= gridSize - squareSize + 1; y++)
			{
				var thisPowerLevel = sums[(x + squareSize - 1, y + squareSize - 1)];
				if (x >= 2)
				{
					thisPowerLevel -= sums[(x - 1, y + squareSize - 1)];
				}
				if (y >= 2)
				{
					thisPowerLevel -= sums[(x + squareSize - 1, y - 1)];
				}
				if (x >= 2 && y >= 2)
				{
					thisPowerLevel += sums[(x - 1, y - 1)];
				}
				if (thisPowerLevel > highestPowerLevel)
				{
					highestPowerLevel = thisPowerLevel;
					highestX = x;
					highestY = y;
					highestSize = squareSize;
				}
			}
		}
	}
	Console.WriteLine($"Part 2: {highestX},{highestY},{highestSize}");
}

static int GetPowerLevel(int x, int y, int serialNumber)
{
	return ((((x + 10) * y + serialNumber) * (x + 10)) / 100) % 10 - 5;
}