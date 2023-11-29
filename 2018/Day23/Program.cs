using Day23;
using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
(int x, int y, int z, int radius)[] nanobots = new (int, int, int, int)[allLines.Length];
Part1(allLines, nanobots);
Part2(allLines, nanobots);

void Part1(string[] allLines, (int x, int y, int z, int radius)[] nanobots)
{
	var positionRegex = new Regex(@"pos=<(?<position>.+)>,");
	var radiusRegex = new Regex(@"r=(?<radius>\d+)");
	for (int i = 0; i < allLines.Length; i++)
	{
		var match = positionRegex.Match(allLines[i]);
		var positionString = match.Groups["position"].Value;
		var thisX = int.Parse(positionString.Split(',')[0]);
		var thisY = int.Parse(positionString.Split(',')[1]);
		var thisZ = int.Parse(positionString.Split(',')[2]);
		match = radiusRegex.Match(allLines[i]);
		var thisRadius = int.Parse(match.Groups["radius"].Value);
		nanobots[i] = (thisX, thisY, thisZ, thisRadius);
	}
	var largestRadius = nanobots.Select(x => x.radius).Max();
	var strongestNanobot = nanobots.Where(x => x.radius == largestRadius).First();
	var inRangeCount = 0;
	for (int i = 0; i < nanobots.Length; i++)
	{
		var thisDistance = Math.Abs(nanobots[i].x - strongestNanobot.x)
			+ Math.Abs(nanobots[i].y - strongestNanobot.y)
			+ Math.Abs(nanobots[i].z - strongestNanobot.z);
		inRangeCount += thisDistance <= largestRadius ? 1 : 0;
	}
	Console.WriteLine($"Part 1: {inRangeCount}");	
}

void Part2(string[] allLines, (int x, int y, int z, int radius)[] nanobots)
{
	var minX = nanobots.Select(n => n.x - n.radius).Min();
	var maxX = nanobots.Select(n => n.x + n.radius).Max();
	var minY = nanobots.Select(n => n.y - n.radius).Min();
	var maxY = nanobots.Select(n => n.y + n.radius).Max();
	var minZ = nanobots.Select(n => n.z - n.radius).Min();
	var maxZ = nanobots.Select(n => n.z + n.radius).Max();
	var nanobotList = new List<Nanobot>();
	for (int i = 0; i < nanobots.Length; i++)
	{
		nanobotList.Add(new Nanobot(nanobots[i]));
	}
	HashSet<(Section section, int count)> sectionSet = new ();
	sectionSet.Add((new Section(minX, maxX, minY, maxY, minZ, maxZ), 0));
	HashSet<(Section section, int count)> nextSectionSet = new ();
	while (true)
	{
		nextSectionSet.Clear();
		foreach (var thisSection in sectionSet)
		{
			var halfwayX = (thisSection.section.minX + thisSection.section.maxX) / 2;
			var minXValues = new int[] { thisSection.section.minX, halfwayX + 1 };
			var maxXValues = new int[] { halfwayX, thisSection.section.maxX };
			if (thisSection.section.minX == thisSection.section.maxX)
			{
				minXValues[1] = thisSection.section.minX;
				maxXValues[0] = thisSection.section.maxX;
			}
			var halfwayY = (thisSection.section.minY + thisSection.section.maxY) / 2;
			var minYValues = new int[] { thisSection.section.minY, halfwayY + 1 };
			var maxYValues = new int[] { halfwayY, thisSection.section.maxY };
			if (thisSection.section.minY == thisSection.section.maxY)
			{
				minYValues[1] = thisSection.section.minY;
				maxYValues[0] = thisSection.section.maxY;
			}
			var halfwayZ = (thisSection.section.minZ + thisSection.section.maxZ) / 2;
			var minZValues = new int[] { thisSection.section.minZ, halfwayZ + 1};
			var maxZValues = new int[] { halfwayZ, thisSection.section.maxZ };
			if (thisSection.section.minZ == thisSection.section.maxZ)
			{
				minZValues[1] = thisSection.section.minZ;
				maxZValues[0] = thisSection.section.maxZ;
			}
			var splitSectionsList = new List<Section>()
			{
				new Section(minXValues[0], maxXValues[0], minYValues[0], maxYValues[0], minZValues[0], maxZValues[0]),
				new Section(minXValues[0], maxXValues[0], minYValues[1], maxYValues[1], minZValues[0], maxZValues[0]),
				new Section(minXValues[0], maxXValues[0], minYValues[0], maxYValues[0], minZValues[1], maxZValues[1]),
				new Section(minXValues[0], maxXValues[0], minYValues[1], maxYValues[1], minZValues[1], maxZValues[1]),
				new Section(minXValues[1], maxXValues[1], minYValues[0], maxYValues[0], minZValues[0], maxZValues[0]),
				new Section(minXValues[1], maxXValues[1], minYValues[1], maxYValues[1], minZValues[0], maxZValues[0]),
				new Section(minXValues[1], maxXValues[1], minYValues[0], maxYValues[0], minZValues[1], maxZValues[1]),
				new Section(minXValues[1], maxXValues[1], minYValues[1], maxYValues[1], minZValues[1], maxZValues[1]),
			};
			foreach (Section thisSplitSection in splitSectionsList)
			{
				var thisCount = CountIntersections(thisSplitSection, nanobotList);
				nextSectionSet.Add((thisSplitSection, thisCount));
			}
		}
		var maxCount = nextSectionSet.Max(x => x.count);
		nextSectionSet = nextSectionSet.Where(x => x.count == maxCount).ToHashSet();
		var minDistanceOrder = nextSectionSet.Min(x => GetDistanceOrder(x.section));
		sectionSet = new(nextSectionSet.Where(x => GetDistanceOrder(x.section) == minDistanceOrder));
		if (sectionSet.Count == 1
			&& sectionSet.First().section.minX == sectionSet.First().section.maxX
			&& sectionSet.First().section.minY == sectionSet.First().section.maxY
			&& sectionSet.First().section.minZ == sectionSet.First().section.maxZ)
		{
			break;
		}
	}
	var solution = sectionSet.First().section;
	var distanceFromOrigin = solution.minX + solution.minY + solution.minZ;
	Console.WriteLine($"Part 2: {distanceFromOrigin}");
}

int CountIntersections(Section section, List<Nanobot> nanobotList)
{
	var count = 0;
	bool doesIntersect;
	for (int i = 0; i < nanobotList.Count; i++)
	{
		doesIntersect = false;
		var nanobotPoints = nanobotList[i].GetPoints();
		foreach ((int, int, int) thisPoint in nanobotPoints)
		{
			if (section.ContainsPoint(thisPoint))
			{
				doesIntersect = true;
				break;
			}
		}
		var sectionPoints = section.GetPoints();
		foreach ((int, int, int) thisPoint in sectionPoints)
		{
			if (nanobotList[i].ContainsPoint(thisPoint))
			{
				doesIntersect = true;
				break;
			}
		}
		count += doesIntersect ? 1 : 0;
	}
	return count;
}

int GetDistanceOrder(Section section)
{
	return (section.minX + section.maxX)
		+ (section.minY + section.maxY) 
		+ (section.minZ + section.maxZ);
}

