using Dec15;

var inputLine = File.ReadAllLines("input.txt")[0];
	var theMap = new SystemMap(inputLine);
Part1(theMap);
Part2(theMap);

static void Part1(SystemMap theMap)
{
	var distance = theMap.GetDistanceToOxygenSystem();
	Console.WriteLine($"Part 1: {distance}");	
}

static void Part2(SystemMap theMap)
{
	var time = theMap.GetOxygenFillTime();
	Console.WriteLine($"Part 2: {time}");	
}