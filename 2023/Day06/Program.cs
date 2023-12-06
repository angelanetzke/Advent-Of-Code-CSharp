using System.Text.RegularExpressions;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static long Part1(string[] allLines)
{	
	List<(long time, long distance)> pairs = [];
	MatchCollection matches = Regex.Matches(allLines[0], @" (\d+)");
	long[] allTimes = matches.Select(x => long.Parse(x.Groups[0].Value)).ToArray();
	matches = Regex.Matches(allLines[1], @" (\d+)");
	long[] allDistances = matches.Select(x => long.Parse(x.Groups[0].Value)).ToArray();
	long product = 1;
	for (int i = 0; i < allTimes.Length; i++)
	{
		(long, long) endpoints = GetEndPoints(allTimes[i], allDistances[i]);
		product *= endpoints.Item2 - endpoints.Item1 + 1;
	}
	return product;
}

static long Part2(string[] allLines)
{
	string timeString = allLines[0].Split(':')[1];
	timeString = timeString.Replace(" ", "");
	string distanceString = allLines[1].Split(':')[1];
	distanceString = distanceString.Replace(" ", "");
	(long, long) endpoints = GetEndPoints(long.Parse(timeString), long.Parse(distanceString));
	return endpoints.Item2 - endpoints.Item1 + 1;
}

static (long, long) GetEndPoints(long time, long distance)
{
	long start = (long)Math.Ceiling((-time + Math.Sqrt(time * time - 4 * distance)) / -2 + .0001);
	long finish = (long)Math.Floor((-time - Math.Sqrt(time * time - 4 * distance)) / -2 - .0001);
	return (start, finish);
}

