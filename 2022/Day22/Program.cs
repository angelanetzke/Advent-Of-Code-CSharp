using Day22;
using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
var theMap = GetMap(allLines);
Part1(theMap);
Part2(theMap);

static void Part1(MonkeyMap theMap)
{	
	var password = theMap.GetPassword(false);
	Console.WriteLine($"Part 1: {password}");
}

static void Part2(MonkeyMap theMap)
{	
	var password = theMap.GetPassword(true);
	Console.WriteLine($"Part 2: {password}");
}

static MonkeyMap GetMap(string[] allLines)
{
	var theMap = new MonkeyMap(allLines.Last());
	var mapLineRegex = new Regex("[ .#]+");
	foreach (string thisLine in allLines)
	{
		if (mapLineRegex.IsMatch(thisLine))
		{
			theMap.AddRow(thisLine);
		}
	}
	return theMap;
}
