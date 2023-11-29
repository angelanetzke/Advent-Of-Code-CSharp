var allLines = File.ReadAllLines("input.txt");
var depthRangePairs = new List<(int depth, int range)>();
foreach (string thisLine in allLines)
{
	var depth = int.Parse(thisLine.Split(": ")[0]);
	var range = int.Parse(thisLine.Split(": ")[1]);
	depthRangePairs.Add((depth, range));
}
Part1(depthRangePairs);
Part2(depthRangePairs);

void Part1(List<(int depth, int range)> depthRangePairs)
{
	var result = Execute(0, depthRangePairs);
	Console.WriteLine($"Part 1: {result.severity}");
}

void Part2(List<(int depth, int range)> depthRangePairs)
{
	var delay = 0;
	var result = Execute(delay, depthRangePairs);
	while (result.count > 0)
	{
		delay++;
		result = Execute(delay, depthRangePairs);
	}
	Console.WriteLine($"Part 2: {delay}");
}

(int severity, int count) Execute(int delay, List<(int depth, int range)> depthRangePairs)
{
	var severity = 0;
	var count = 0;	
	foreach ((int depth, int range) thisPair in depthRangePairs)
	{
		var interval = (thisPair.range - 1) * 2;
		if ((thisPair.depth + delay) % interval == 0)
		{
			severity += thisPair.depth * thisPair.range;
			count++;
		}
	}
	return (severity, count);
}