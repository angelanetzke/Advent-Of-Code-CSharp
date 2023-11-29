using Dec06;

var allLines = File.ReadAllLines("input.txt");
var system = new OrbitSystem(allLines);
Part1(system);
Part2(system);

static void Part1(OrbitSystem system)
{
	int orbitCount = system.CountOrbits();
	Console.WriteLine($"Part 1: {orbitCount}");
}

static void Part2(OrbitSystem system)
{
	int orbitCount = system.GetShortestPath();
	Console.WriteLine($"Part 2: {orbitCount}");
}
