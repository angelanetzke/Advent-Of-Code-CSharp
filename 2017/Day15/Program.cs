using Day15;

var allLines = File.ReadAllLines("input.txt");
var initialValueA = long.Parse(allLines[0].Split(' ')[4]);
var initialValueB = long.Parse(allLines[1].Split(' ')[4]);
Part1(initialValueA, initialValueB);
Part2(initialValueA, initialValueB);

void Part1(long initialValueA, long initialValueB)
{
	var mask = 0xFFFF;
	var matchCount = 0;
	var generatorA = new Generator(16807L, initialValueA, 1);
	var generatorB = new Generator(48271L, initialValueB, 1);
	for (int i = 1; i <= 40_000_000; i++)
	{
		generatorA.Execute();
		generatorB.Execute();
		matchCount += (generatorA.GetValue() & mask) == (generatorB.GetValue() & mask) ? 1 : 0;
	}
	Console.WriteLine($"Part 1: {matchCount}");
}

void Part2(long initialValueA, long initialValueB)
{
	var mask = 0xFFFF;
	var matchCount = 0;
	var generatorA = new Generator(16807L, initialValueA, 4);
	var generatorB = new Generator(48271L, initialValueB, 8);
	for (int i = 1; i <= 5_000_000; i++)
	{
		generatorA.Execute();
		generatorB.Execute();
		matchCount += (generatorA.GetValue() & mask) == (generatorB.GetValue() & mask) ? 1 : 0;
	}
	Console.WriteLine($"Part 2: {matchCount}");
}