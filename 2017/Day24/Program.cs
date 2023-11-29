using Day24;

var allLines = File.ReadAllLines("input.txt");
var components = new List<Component>();
foreach (string thisLine in allLines)
{
	components.Add(new Component(thisLine));
}
var startBridge = new Bridge(components, 0, 0, 0);
var list = startBridge.Extend();
Part1(list);
Part2(list);

static void Part1(List<Bridge> list)
{	
	var strongest = list.Max(x => x.GetStrength());
	Console.WriteLine($"Part 1: {strongest}");
}

static void Part2(List<Bridge> list)
{	
	var longestLength = list.Max(x => x.GetLength());
	var bestBridge = list.Where(x => x.GetLength() == longestLength)
		.OrderBy(x => x.GetStrength())
		.Reverse()
		.First();
	Console.WriteLine($"Part 2: {bestBridge.GetStrength()}");
}