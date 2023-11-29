var allLines = File.ReadAllLines("input.txt");
var line1 =  allLines[0];
var line2 = allLines[1];
var intersections = Part1(line1, line2);
Part2(line1, line2, intersections);

static List<(int, int)> Part1(string line1, string line2)
{
	//(x, y), number of lines that cross that point
	var points = new Dictionary<(int, int), int>();
	var x = 0;
	var y = 0;
	var line1Segments = line1.Split(',');
	foreach (string thisSegment in line1Segments)
	{
		char thisDirection = thisSegment.ToCharArray()[0];
		int thisLength = int.Parse(thisSegment[1..]);
		switch (thisDirection)
		{
			case 'U':
				for (int i = 0; i < thisLength; i++)
				{
					y++;
					points[(x, y)] = 1;
				}
				break;
			case 'D':
				for (int i = 0; i < thisLength; i++)
				{
					y--;
					points[(x, y)] = 1;
				}
				break;
			case 'R':
				for (int i = 0; i < thisLength; i++)
				{
					x++;
					points[(x, y)] = 1;
				}
				break;
			case 'L':
				for (int i = 0; i < thisLength; i++)
				{
					x--;
					points[(x, y)] = 1;
				}
				break;
		}
	}
	x = 0;
	y = 0;
	var line2Segments = line2.Split(',');
	foreach (string thisSegment in line2Segments)
	{
		char thisDirection = thisSegment.ToCharArray()[0];
		int thisLength = int.Parse(thisSegment[1..]);
		switch (thisDirection)
		{
			case 'U':
				for (int i = 0; i < thisLength; i++)
				{
					y++;
					if (points.ContainsKey((x, y)))
					{
						points[(x, y)] = 2;
					}
				}
				break;
			case 'D':
				for (int i = 0; i < thisLength; i++)
				{
					y--;
					if (points.ContainsKey((x, y)))
					{
						points[(x, y)] = 2;
					}
				}
				break;
			case 'R':
				for (int i = 0; i < thisLength; i++)
				{
					x++;
					if (points.ContainsKey((x, y)))
					{
						points[(x, y)] = 2;
					}
				}
				break;
			case 'L':
				for (int i = 0; i < thisLength; i++)
				{
					x--;
					if (points.ContainsKey((x, y)))
					{
						points[(x, y)] = 2;
					}
				}
				break;
		}
	}
	var intersections = new List<(int, int)>();
	int shortestDistance = int.MaxValue;
	foreach ((int, int) thisPoint in points.Keys)
	{
		if (points[thisPoint] == 2)
		{
			intersections.Add(thisPoint);
			int thisDistance = Math.Abs(thisPoint.Item1) + Math.Abs(thisPoint.Item2);
			shortestDistance = Math.Min(shortestDistance, thisDistance);
		}
	}
	Console.WriteLine($"Part 1: {shortestDistance}");
	return intersections;
}

static void Part2(string line1, string line2, List<(int, int)> intersections)
{
	//(x, y), steps to this point
	var line1Steps = new Dictionary<(int, int), int>();
	var line2Steps = new Dictionary<(int, int), int>();
	foreach((int, int) thisIntersection in intersections)
	{
		var x = 0;
		var y = 0;
		var steps = 0;
		var line1Segments = line1.Split(',');
		bool hasIntersectionBeenFound = false;
		int segmentNumber = 0;
		char thisDirection = 'U';
		int stepsToGo = 0;		
		while (!hasIntersectionBeenFound)
		{
			if (stepsToGo == 0)
			{
				thisDirection = line1Segments[segmentNumber].ToCharArray()[0];
				stepsToGo = int.Parse(line1Segments[segmentNumber][1..]);
				segmentNumber++;
			}
			steps++;
			stepsToGo--;
			switch (thisDirection)
			{
				case 'U':
					y++;
					break;
				case 'D':
					y--;
					break;
				case 'R':
					x++;
					break;
				case 'L':
					x--;
					break;
			}
			if (thisIntersection.Equals((x, y)))
			{
				hasIntersectionBeenFound = true;
			}
		}
		line1Steps[thisIntersection] = steps;
	}
	foreach ((int, int) thisIntersection in intersections)
	{
		var x = 0;
		var y = 0;
		var steps = 0;
		var line2Segments = line2.Split(',');
		bool hasIntersectionBeenFound = false;
		int segmentNumber = 0;
		char thisDirection = 'U';
		int stepsToGo = 0;
		while (!hasIntersectionBeenFound)
		{
			if (stepsToGo == 0)
			{
				thisDirection = line2Segments[segmentNumber].ToCharArray()[0];
				stepsToGo = int.Parse(line2Segments[segmentNumber][1..]);
				segmentNumber++;
			}
			steps++;
			stepsToGo--;
			switch (thisDirection)
			{
				case 'U':
					y++;
					break;
				case 'D':
					y--;
					break;
				case 'R':
					x++;
					break;
				case 'L':
					x--;
					break;
			}
			if (thisIntersection.Equals((x, y)))
			{
				hasIntersectionBeenFound = true;
			}
		}
		line2Steps[thisIntersection] = steps;
	}
	int fewestSteps = int.MaxValue;
	foreach((int, int) thisIntersection in intersections)
	{
		fewestSteps = Math.Min(fewestSteps, line1Steps[thisIntersection] + line2Steps[thisIntersection]);
	}
	Console.WriteLine($"Part 2: {fewestSteps}");
}
