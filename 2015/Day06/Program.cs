string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static int Part1(string[] allLines)
{
	int size = 1000;
	bool[,] lights = new bool[size, size];
	foreach (string thisLine in allLines)
	{
		string[] parts = thisLine.Split(' ');
		if (parts[0] == "turn")
		{
			int minX = int.Parse(parts[2].Split(',')[0]);
			int minY = int.Parse(parts[2].Split(',')[1]);
			int maxX = int.Parse(parts[4].Split(',')[0]);
			int maxY = int.Parse(parts[4].Split(',')[1]);
			bool isLightOn = parts[1] == "on";
			for (int x = minX; x <= maxX; x++)
			{
				for (int y = minY; y <= maxY; y++)
				{
					lights[x, y] = isLightOn;
				}
			}
		}
		else if (parts[0] == "toggle")
		{
			int minX = int.Parse(parts[1].Split(',')[0]);
			int minY = int.Parse(parts[1].Split(',')[1]);
			int maxX = int.Parse(parts[3].Split(',')[0]);
			int maxY = int.Parse(parts[3].Split(',')[1]);
			for (int x = minX; x <= maxX; x++)
			{
				for (int y = minY; y <= maxY; y++)
				{
					lights[x, y] = !lights[x, y];
				}
			}
		}
	}
	int lightOnCount = 0;
	for (int x = 0; x < size; x++)
	{
		for (int y = 0; y < size; y++)
		{
			lightOnCount += lights[x, y] ? 1 : 0;
		}
	}
	return lightOnCount;
}

static int Part2(string[] allLines)
{
	int size = 1000;
	int[,] lights = new int[size, size];
	foreach (string thisLine in allLines)
	{
		string[] parts = thisLine.Split(' ');
		if (parts[0] == "turn")
		{
			int minX = int.Parse(parts[2].Split(',')[0]);
			int minY = int.Parse(parts[2].Split(',')[1]);
			int maxX = int.Parse(parts[4].Split(',')[0]);
			int maxY = int.Parse(parts[4].Split(',')[1]);
			int brightnessDelta = parts[1] == "on" ? 1 : -1;
			for (int x = minX; x <= maxX; x++)
			{
				for (int y = minY; y <= maxY; y++)
				{
					lights[x, y] += brightnessDelta;
					lights[x, y] = Math.Max(lights[x, y], 0);
				}
			}
		}
		else if (parts[0] == "toggle")
		{
			int minX = int.Parse(parts[1].Split(',')[0]);
			int minY = int.Parse(parts[1].Split(',')[1]);
			int maxX = int.Parse(parts[3].Split(',')[0]);
			int maxY = int.Parse(parts[3].Split(',')[1]);
			for (int x = minX; x <= maxX; x++)
			{
				for (int y = minY; y <= maxY; y++)
				{
					lights[x, y] += 2;
				}
			}
		}
	}
	int totalBrightness = 0;
	for (int x = 0; x < size; x++)
	{
		for (int y = 0; y < size; y++)
		{
			totalBrightness += lights[x, y];
		}
	}
	return totalBrightness;
}