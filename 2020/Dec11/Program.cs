using Dec11;

var allLines = System.IO.File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var theRoom = new Room(allLines);
	int occupiedCount = theRoom.CountOccupied();
	Console.WriteLine($"Part 1: {occupiedCount}");
}

static void Part2(string[] allLines)
{
	var theRoom = new Room(allLines);
	int occupiedCount = theRoom.CountOccupiedVisible();
	Console.WriteLine($"Part 2: {occupiedCount}");
}