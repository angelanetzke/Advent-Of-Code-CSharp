using Day24;

var allLines = File.ReadAllLines("input.txt");
var theMap = new ValleyMap(allLines);
Part1(theMap);
Part2(theMap);

static void Part1(ValleyMap theMap)
{	
	var time = theMap.GetTimeToEnd();
	Console.WriteLine($"Part 1: {time}");
}

static void Part2(ValleyMap theMap)
{
	var time = theMap.GetSnackRetrievalTime();
	Console.WriteLine($"Part 2: {time}");
}

