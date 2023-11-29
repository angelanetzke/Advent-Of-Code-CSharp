using Day16;

var text = File.ReadAllLines("input.txt")[0];
Part1(text);
Part2(text);

void Part1(string text)
{
	var danceLine = new DanceLine(16);
	danceLine.Dance(text);
	Console.WriteLine($"Part 1: {danceLine.ToString()}");
}

void Part2(string text)
{
	var danceIterations = 1000000000;
	var danceLine = new DanceLine(16);
	var cache = new Dictionary<string, int>();
	for (int i = 0; i < danceIterations; i++)
	{
		var thisDanceLineString = danceLine.ToString();
		if (cache.ContainsKey(thisDanceLineString))
		{
			var previous = cache[thisDanceLineString];
			var interval = i - previous;
			var equivalentI = (danceIterations - previous) % interval + previous;
			var equivalentString = cache
				.Where(x => x.Value == equivalentI)
				.Select(x => x.Key)
				.First();
			Console.WriteLine($"Part 2: {equivalentString}");
			break;
		}
		else
		{
			cache[thisDanceLineString] = i;
		}
		danceLine.Dance(text);
	}	
}
