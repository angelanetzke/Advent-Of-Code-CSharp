using Day15;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var theBattlefield = new Battlefield(allLines, 3);
	var isSuccessful = true;
	do
	{
		isSuccessful = theBattlefield.TakeTurn();
	} while (isSuccessful);
	var finalStats = theBattlefield.GetStats();
	Console.WriteLine($"Part 1: {finalStats.Item1 * finalStats.Item2}");
}

static void Part2(string[] allLines)
{
	var isSuccessful = true;
	var isBattleSuccessful = false;
	var elfAttackPower = 4;
	var theBattlefield = new Battlefield(allLines, elfAttackPower);
	while (!isBattleSuccessful)
	{
		var elfDeaths = 0;
		do
		{
			isSuccessful = theBattlefield.TakeTurn();
			elfDeaths = theBattlefield.GetStats().Item3;
		} while (isSuccessful && elfDeaths == 0);
		isBattleSuccessful = elfDeaths == 0;
		if (!isBattleSuccessful)
		{
			elfAttackPower++;
			theBattlefield = new Battlefield(allLines, elfAttackPower);
		}
		
	}	
	var finalStats = theBattlefield.GetStats();
	Console.WriteLine($"Part 2: {finalStats.Item1 * finalStats.Item2}");
}