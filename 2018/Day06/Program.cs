using Day06;

var allLines = File.ReadAllLines("input.txt");
var locationList = new List<Location>();
foreach (string thisLine in allLines)
{
	locationList.Add(new Location(thisLine));
}
Part1(locationList);
Part2(locationList);

static void Part1(List<Location> locationList)
{	
	var minX = locationList.Min(thisPoint => thisPoint.GetX());
	var maxX = locationList.Max(thisPoint => thisPoint.GetX());
	var minY = locationList.Min(thisPoint => thisPoint.GetY());
	var maxY = locationList.Max(thisPoint => thisPoint.GetY());
	for (int x = minX; x <= maxX; x++)
	{
		for (int y = minY; y <= maxY; y++)
		{
			var closest = locationList.OrderBy(thisPoint => thisPoint.GetDistanceToPoint(x, y)).ToList();
			if (closest[0].GetDistanceToPoint(x, y) == closest[1].GetDistanceToPoint(x, y))
			{
				continue;
			}
			closest[0].AddPoint(x, y);
			if (x == minX || x == maxX || y == minY || y == maxY)
			{
				closest[0].isInfinite = true;
			}
		}
	}
	var finiteLocations = locationList.Where(x => !x.isInfinite).ToList();
	var largestArea = finiteLocations.Max(x => x.GetPointCount());
	Console.WriteLine($"Part 1: {largestArea}");
}

static void Part2(List<Location> locationList)
{
	var areaSize = 0;
	var minX = locationList.Min(thisPoint => thisPoint.GetX());
	var maxX = locationList.Max(thisPoint => thisPoint.GetX());
	var minY = locationList.Min(thisPoint => thisPoint.GetY());
	var maxY = locationList.Max(thisPoint => thisPoint.GetY());
	for (int x = minX; x <= maxX; x++)
	{
		for (int y = minY; y <= maxY; y++)
		{
			if (locationList.Sum(thisPoint => thisPoint.GetDistanceToPoint(x, y)) < 10_000)
			{
				areaSize++;
			}
		}
	}
	Console.WriteLine($"Part 2: {areaSize}");
}