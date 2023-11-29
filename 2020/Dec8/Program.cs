using Dec8;

var allLines = System.IO.File.ReadAllLines("input.txt");
var theBootCode = new BootCode(allLines);
Part1(theBootCode);
Part2(theBootCode);

static void Part1(BootCode bc)
{
	int acc = bc.GetLastAcc();
	Console.WriteLine($"Part 1: {acc}");
}

static void Part2(BootCode bc)
{
	int acc = bc.Debug();
	Console.WriteLine($"Part 2: {acc}");
}
