using Day24;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

void Part1(string[] allLines)
{
	var theBattle = new Battle(allLines);
	var result = theBattle.DoBattle();
	Console.WriteLine($"Part 1: {result}");	
}

void Part2(string[] allLines)
{
	int result;
	var boost = 0;
	do
	{
		boost++;
		var theBattle = new Battle(allLines);
		theBattle.Boost(boost);
		result = theBattle.DoBattle();
	} while (result < 0);
	Console.WriteLine($"Part 2: {result} {boost}");
}