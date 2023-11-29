var square = int.Parse(File.ReadAllLines("input.txt")[0]);
Part1(square);
Part2(square);

void Part1(int targetSquare)
{
	var result = GetCoordinates(targetSquare);
	Console.WriteLine($"Part 1: {Math.Abs(result.x) + Math.Abs(result.y)}");
}

void Part2(int maxValue)
{
	var writtenValues = new Dictionary<(int, int), int>();
	writtenValues[(0, 0)] = 1;
	var currentSquare = 2;	
	var currentValue = -1;
	while (currentValue <= maxValue)
	{
		currentValue = 0;
		var coordinates = GetCoordinates(currentSquare);
		for (int deltaX = -1; deltaX <= 1; deltaX++)
		{
			for (int deltaY = -1; deltaY <= 1; deltaY++)
			{
				if (deltaX == 0 && deltaY == 0)
				{
					continue;
				}
				if (writtenValues.ContainsKey((coordinates.x + deltaX, coordinates.y + deltaY)))
				{
					currentValue += writtenValues[(coordinates.x + deltaX, coordinates.y + deltaY)];
				}
			}
		}
		writtenValues[coordinates] = currentValue;
		currentSquare++;
	}
	Console.WriteLine($"Part 2: {currentValue}");
}

(int x, int y) GetCoordinates(int targetSquare)
{
	if (targetSquare == 1)
	{
		return (0, 0);
	}
	var x = 0;
	var y = 0;
	var layer = 0;
	var addends = new int[] { 2, 4, 6, 8 };
	var values = new int[] { 1, 1, 1, 1 };
	while (true)
	{	
		layer++;
		for (int i = 0; i < addends.Length; i++)
		{
			values[i] += addends[i];
			addends[i] += 8;
		}	
		var layerMin = values[0] - ((layer - 1) * 2 + 1);
		if (layerMin <= targetSquare && targetSquare <= values[0])
		{
			x = layer;
			y = layer - (values[0] - targetSquare);
			break;
		}
		if (values[0] < targetSquare && targetSquare <= values[1])
		{
			x = layer - (targetSquare - values[0]);
			y = layer;			
			break;
		}
		if (values[1] < targetSquare && targetSquare <= values[2])
		{
			x = -layer;
			y = -layer + (values[2] - targetSquare);
			break;
		}
		if (values[2] < targetSquare && targetSquare <= values[3])
		{
			x = -layer + (targetSquare - values[2]);
			y = -layer;
			break;
		}		
	}	
	return (x, y);
}
