using Dec12;

var allLines = System.IO.File.ReadAllLines("input.txt");

Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var theFerry = new Ferry();
	foreach(string thisLine in allLines)
	{
		theFerry.Next(thisLine);
	}
	int[] location = theFerry.GetCurrentLocation();
	int distance = Math.Abs(location[0]) + Math.Abs(location[1]);
	Console.WriteLine($"Part 1: {distance}");
}

static void Part2(string[] allLines)
{
	var theFerry = new FerryWithWaypoint();
	foreach (string thisLine in allLines)
	{
		theFerry.Next(thisLine);
	}
	int[] location = theFerry.GetCurrentLocation();
	int distance = Math.Abs(location[0]) + Math.Abs(location[1]);
	Console.WriteLine($"Part 2: {distance}");
}

