using Dec20;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var theMaze = new Maze(allLines);
	var distance = theMaze.GetDistance();
	Console.WriteLine($"Part 1: {distance}");
}

static void Part2(string[] allLines)
{
	var theMaze = new Maze(allLines);
	var distance = theMaze.GetDistanceWithLevels();
	Console.WriteLine($"Part 2: {distance}");
}