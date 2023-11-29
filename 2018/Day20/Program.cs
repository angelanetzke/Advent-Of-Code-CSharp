using Day20;

var allText = File.ReadAllLines("input.txt")[0];
Room.Generate(allText);
Part1(allText);
Part2(allText);

static void Part1(string allText)
{	
	Console.WriteLine($"Part 1: {Room.GetLongestDistace()}");
}

static void Part2(string allText)
{	
	Console.WriteLine($"Part 2: {Room.Count1000DoorDistances()}");
}
