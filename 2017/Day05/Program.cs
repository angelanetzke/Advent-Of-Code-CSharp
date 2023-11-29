using Day5;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

void Part1(string[] allLines)
{
	var theInstructions = new Instructions(allLines);
	var result = theInstructions.Execute(false);
	Console.WriteLine($"Part 1: {result}");
}

void Part2(string[] allLines)
{
	var theInstructions = new Instructions(allLines);
	var result = theInstructions.Execute(true);
	Console.WriteLine($"Part 2: {result}");
}