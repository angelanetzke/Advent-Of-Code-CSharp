using Day21;

var allLines = File.ReadAllLines("input.txt");
Art.SetPatterns(allLines);
Part1();
Part2();

void Part1()
{	
	var art = new Art();
	for (int i = 1; i <= 5; i++)
	{
		art.Enhance();
	}
	Console.WriteLine($"Part 1: {art.CountOnPixels()}");
}

void Part2()
{
	Art.SetPatterns(allLines);
	var art = new Art();
	for (int i = 1; i <= 18; i++)
	{
		art.Enhance();
	}
	Console.WriteLine($"Part 1: {art.CountOnPixels()}");
}