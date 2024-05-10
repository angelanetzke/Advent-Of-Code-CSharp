using System.Text.RegularExpressions;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static string Part1(string[] allLines)
{
	List<(string, string)> sueFacts = new()
	{
		("children", "3"), ("cats", "7"), ("samoyeds", "2"), ("pomeranians", "3"), ("akitas", "0"),
		("vizslas", "0"), ("goldfish", "5"), ("trees", "3"), ("cars", "2"), ("perfumes", "1")
	};
	
	foreach (string thisLine in allLines)
	{
		bool isRightSue = true;
		foreach ((string thing, string quantity) in sueFacts)
		{
			Regex thisThingRegex = new Regex(@thing + @": (?<thisQuantity>\d+)");
			Match thingMatch = thisThingRegex.Match(thisLine);
			if (thingMatch.Success && thingMatch.Groups["thisQuantity"].Value != quantity)
			{
				isRightSue = false;
			}
		}
		if (isRightSue)
		{
			return thisLine.Split(':')[0];
		}
	}
	return "Aunt Sue not found";
}

static string Part2(string[] allLines)
{
	List<(string, int)> sueFacts = new()
	{
		("children", 3), ("cats", 7), ("samoyeds", 2), ("pomeranians", 3), ("akitas", 0),
		("vizslas", 0), ("goldfish", 5), ("trees", 3), ("cars", 2), ("perfumes", 1)
	};
	
	foreach (string thisLine in allLines)
	{
		bool isRightSue = true;
		foreach ((string thing, int quantity) in sueFacts)
		{
			Regex thisThingRegex = new Regex(@thing + @": (?<thisQuantity>\d+)");
			Match thingMatch = thisThingRegex.Match(thisLine);
			if (thingMatch.Success)
			{
				int thisQuantity = int.Parse(thingMatch.Groups["thisQuantity"].Value);
				if (thing == "cats" || thing == "tree")
				{
					if (thisQuantity <= quantity)
					{
						isRightSue = false;
					}
				}
				else if (thing == "pomeranians" || thing == "goldfish")
				{
					if (thisQuantity >= quantity)
					{
						isRightSue = false;
					}
				}
				else
				{
					if (thisQuantity != quantity)
					{
						isRightSue = false;
					}
				}				
			}
		}
		if (isRightSue)
		{
			return thisLine.Split(':')[0];
		}
	}
	return "Aunt Sue not found";
}