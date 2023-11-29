using Day10;

var allLines = File.ReadAllLines("input.txt");
Part1AndPart2(allLines);

static void Part1AndPart2(string[] allLines)
{
	var lightList = new List<Light>();
	foreach (string thisLine in allLines)
	{
		lightList.Add(new Light(thisLine));
	}
	var minY = lightList.Select(thisLight => thisLight.GetY()).Min();
	var maxY = lightList.Select(thisLight => thisLight.GetY()).Max();
	var lastHeight = int.MaxValue;
	var thisHeight = maxY - minY + 1;
	int secondCount = -1;
	while (thisHeight < lastHeight)
	{
		secondCount++;
		lastHeight = thisHeight;
		foreach (Light thisLight in lightList)
		{
			thisLight.Update();
		}
		minY = lightList.Select(thisLight => thisLight.GetY()).Min();
		maxY = lightList.Select(thisLight => thisLight.GetY()).Max();
		thisHeight = maxY - minY + 1;
	}
	foreach (Light thisLight in lightList)
	{
		thisLight.Reverse();
	}
	var minX = lightList.Select(thisLight => thisLight.GetX()).Min();
	var maxX = lightList.Select(thisLight => thisLight.GetX()).Max();
	minY = lightList.Select(thisLight => thisLight.GetY()).Min();
	maxY = lightList.Select(thisLight => thisLight.GetY()).Max();
	var display = new bool[maxX - minX + 1, maxY - minY + 1];
	foreach (Light thisLight in lightList)
	{
		display[thisLight.GetX() - minX, thisLight.GetY() - minY] = true;
	}
	for (int y = minY; y <= maxY; y++)	
	{
		for (int x = minX; x <= maxX; x++)
		{
			if (display[x - minX, y - minY])
			{
				Console.Write('#');
			}
			else
			{
				Console.Write(' ');
			}
		}
		Console.WriteLine();
	}
	Console.WriteLine($"Part 2: {secondCount}");
}
