using Day04;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var sectorIDSum = 0;
	foreach (string thisLine in allLines)
	{
		var thisRoom = new Room(thisLine);
		sectorIDSum += thisRoom.IsReal() ? thisRoom.GetSectorID() : 0;
	}
	Console.WriteLine($"Part 1: {sectorIDSum}");
}

static void Part2(string[] allLines)
{
	foreach (string thisLine in allLines)
	{
		var thisRoom = new Room(thisLine);
		if (thisRoom.IsReal())
		{
			var realName = thisRoom.Decrpyt();
			if (realName.ToLower().Contains("north"))
			{
				Console.WriteLine($"{realName} {thisRoom.GetSectorID()}");
			}			
		}		
	}
}