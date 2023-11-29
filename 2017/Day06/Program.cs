using Day6;

var text = File.ReadAllLines("input.txt")[0];
Part1(text);
Part2(text);

void Part1(string text)
{
	var theMemory = new Memory(text);
	var result = theMemory.Execute();
	Console.WriteLine($"Part 1: {result.Item1}");
}

void Part2(string text)
{
	var theMemory = new Memory(text);
	var result = theMemory.Execute();
	Console.WriteLine($"Part 2: {result.Item2}");
}