using Dec17;

var allLines = System.IO.File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var cubes = new Dictionary<Coordinates, bool>();
	for (int x = 0; x < allLines.Length; x++)
	{
		for (int y = 0; y < allLines[x].Length; y++)
		{
			if (allLines[x][y] == '#')
			{
				cubes[new Coordinates(x, y, 0)] = true;
			}
			else
			{
				cubes[new Coordinates(x, y, 0)] = false;
			}
		}
	}
	int minX = -1;
	int maxX = allLines.Length;
	int minY = -1;
	int maxY = allLines[0].Length;
	int minZ = -1;
	int maxZ = 1;
	for (int i = 0; i < 6; i++)
	{
		var previousCubes = new Dictionary<Coordinates, bool>(cubes);
		for (int x = minX; x <= maxX; x++)
		{
			for (int y = minY; y <= maxY; y++)
			{
				for (int z = minZ; z <= maxZ; z++)
				{
					bool isActive = false;
					if (cubes.TryGetValue(new Coordinates(x, y, z), out bool isCurrentActive))
					{
						isActive = isCurrentActive;
					}
					int activeNeighborCount = CountActiveNeighbors(x, y, z, previousCubes);
					if (isActive && (activeNeighborCount == 2 || activeNeighborCount == 3))
					{
						cubes[new Coordinates(x, y, z)] = true;
					}
					else if (isActive)
					{
						cubes[new Coordinates(x, y, z)] = false;
					}
					else if (!isActive && activeNeighborCount == 3)
					{
						cubes[new Coordinates(x, y, z)] = true;
					}
					else
					{
						cubes[new Coordinates(x, y, z)] = false;
					}
				}
			}
		}
		minX--;
		maxX++;
		minY--;
		maxY++;
		minZ--;
		maxZ++;
	}
	int activeCount = 0;
	foreach (Coordinates thisKey in cubes.Keys)
	{
		if (cubes[thisKey])
		{
			activeCount++;
		}
	}
	Console.WriteLine($"Part 1: {activeCount}");
}

static int CountActiveNeighbors(int x, int y, int z, Dictionary<Coordinates, bool> cubes)
{
	int count = 0;
	var allNeighbors = (new Coordinates(x, y, z)).GetNeighbors();
	foreach (Coordinates thisNeighbor in allNeighbors)
	{
		if (cubes.TryGetValue(thisNeighbor, out bool isNeighborActive))
		{
			if (isNeighborActive)
			{
				count++;
			}
		}
	}
	return count;
}

static void Part2(string[] allLines)
{
	var cubes = new Dictionary<Coordinates4D, bool>();
	for (int x = 0; x < allLines.Length; x++)
	{
		for (int y = 0; y < allLines[x].Length; y++)
		{
			if (allLines[x][y] == '#')
			{
				cubes[new Coordinates4D(0, x, y, 0)] = true;
			}
			else
			{
				cubes[new Coordinates4D(0, x, y, 0)] = false;
			}
		}
	}
	int minW = -1;
	int maxW = 1;
	int minX = -1;
	int maxX = allLines.Length;
	int minY = -1;
	int maxY = allLines[0].Length;
	int minZ = -1;
	int maxZ = 1;
	for (int i = 0; i < 6; i++)
	{
		var previousCubes = new Dictionary<Coordinates4D, bool>(cubes);
		for (int w = minW; w <= maxW; w++)
		{
			for (int x = minX; x <= maxX; x++)
			{
				for (int y = minY; y <= maxY; y++)
				{
					for (int z = minZ; z <= maxZ; z++)
					{
						bool isActive = false;
						if (cubes.TryGetValue(new Coordinates4D(w, x, y, z), out bool isCurrentActive))
						{
							isActive = isCurrentActive;
						}
						int activeNeighborCount = CountActiveNeighbors4D(w, x, y, z, previousCubes);
						if (isActive && (activeNeighborCount == 2 || activeNeighborCount == 3))
						{
							cubes[new Coordinates4D(w, x, y, z)] = true;
						}
						else if (isActive)
						{
							cubes[new Coordinates4D(w, x, y, z)] = false;
						}
						else if (!isActive && activeNeighborCount == 3)
						{
							cubes[new Coordinates4D(w, x, y, z)] = true;
						}
						else
						{
							cubes[new Coordinates4D(w, x, y, z)] = false;
						}
					}
				}
			}
		}
		minW--;
		maxW++;
		minX--;
		maxX++;
		minY--;
		maxY++;
		minZ--;
		maxZ++;
	}
	int activeCount = 0;
	foreach (Coordinates4D thisKey in cubes.Keys)
	{
		if (cubes[thisKey])
		{
			activeCount++;
		}
	}
	Console.WriteLine($"Part 2: {activeCount}");
}

static int CountActiveNeighbors4D(int w, int x, int y, int z, Dictionary<Coordinates4D, bool> cubes)
{
	int count = 0;
	var allNeighbors = (new Coordinates4D(w, x, y, z)).GetNeighbors();
	foreach (Coordinates4D thisNeighbor in allNeighbors)
	{
		if (cubes.TryGetValue(thisNeighbor, out bool isNeighborActive))
		{
			if (isNeighborActive)
			{
				count++;
			}
		}
	}
	return count;
}
