using Dec18;

var allLines = File.ReadAllLines("input.txt");
var theMap = new VaultMap(allLines);
Part1(theMap);
Part2(theMap);

static void Part1(VaultMap theMap)
{
	var distance = theMap.GetDistance();
	Console.WriteLine($"Part 1: {distance}");
}

static void Part2(VaultMap theMap)
{
	var distance = theMap.GetDistanceQuad();
	Console.WriteLine($"Part 2: {distance}");
}


