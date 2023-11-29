using Day23;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var theMap = new ElfMap(allLines);
	for (int round = 1; round <= 10; round++)
	{
		theMap.Update();
	}
	Console.WriteLine($"Part 1: {theMap.CountOpenSpaces()}");
}

static void Part2(string[] allLines)
{
	var theMap = new ElfMap(allLines);
	int round = 1;
	int moveCount;
	do
	{
		moveCount = theMap.Update();
		if (moveCount > 0)
		{
			round++;
		}		
	} while (moveCount > 0);
	Console.WriteLine($"Part 2: {round}");
}
