string[] allLines = File.ReadAllLines("input.txt");
List<(uint, uint)> whiteList = GetWhiteList(allLines);
whiteList = [ ..whiteList.OrderBy(x => x.Item1) ];
Console.WriteLine($"Part 1: {whiteList[0].Item1}");
uint count = 0;
foreach ((uint start, uint end) in whiteList)
{
	count += end - start + 1;
}
Console.WriteLine($"Part 2: {count}");

static List<(uint, uint)> GetWhiteList(string[] allLines)
{
	List<(uint, uint)> blackList = [];
	foreach (string thisLine in allLines)
	{
		string[] parts = thisLine.Split('-');
		blackList.Add((uint.Parse(parts[0]), uint.Parse(parts[1])));
	}
	List<(uint, uint)> whiteList = [(0, uint.MaxValue)];
	List<(uint, uint)> temp = [];
	foreach ((uint blackStart, uint blackEnd) in blackList)
	{
		temp.Clear();
		foreach ((uint whiteStart, uint whiteEnd) in whiteList)
		{
			if (Math.Max(whiteStart, blackStart) <= Math.Min(whiteEnd, blackEnd))
			{
				temp.AddRange(Subtract((whiteStart, whiteEnd), (blackStart, blackEnd)));
			}
			else
			{
				temp.Add((whiteStart, whiteEnd));
			}
		}
		whiteList = new (temp);
	}
	return whiteList;
}

static List<(uint, uint)> Subtract((uint, uint) first, (uint, uint) second)
{
	List<(uint, uint)> result = [];
	if (second.Item1 > first.Item1 && second.Item2 < first.Item2)
	{
		result.Add((first.Item1, second.Item1 - 1));
		result.Add((second.Item2 + 1, first.Item2));
	}
	else if (second.Item1 > first.Item1)
	{
		result.Add((first.Item1, second.Item1 - 1));
	}
	else if (second.Item2 < first.Item2)
	{
		result.Add((second.Item2 + 1, first.Item2));
	}
	return result;	
}