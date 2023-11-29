using Day23;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2();

static void Part1(string[] allLines)
{
	var coprocesspor = new Coprocessor();
	var mulCount = coprocesspor.Execute(allLines);
	Console.WriteLine($"Part 1: {mulCount}");
}

static void Part2()
{
	var coprocesspor = new Coprocessor();
	var h = coprocesspor.Execute2();
	Console.WriteLine($"Part 2: {h}");
}
