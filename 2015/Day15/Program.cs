string[] allLines = File.ReadAllLines("input.txt");
List<(int, int, int, int, int)> ingredientList = [];
foreach (string thisLine in allLines)
{
	string[] parts = thisLine.Split(' ');
	ingredientList.Add(
		(int.Parse(parts[2][..^1]), 
		int.Parse(parts[4][..^1]), 
		int.Parse(parts[6][..^1]), 
		int.Parse(parts[8][..^1]), 
		int.Parse(parts[10])));
}
Console.WriteLine($"Part 1: {Part1(ingredientList)}");
Console.WriteLine($"Part 2: {Part2(ingredientList)}");

static long Part1(List<(int, int, int, int, int)> ingredientList)
{
	long highestScore = long.MinValue;
	for (int a = 0; a <= 100; a++)
	{
		for (int b = 0; b <= 100 - a; b++)
		{
			for (int c = 0; c <= 100 - (a + b); c++)
			{
				int d = 100 - (a + b + c);
				long thisScore = Evaluate([a, b, c, d], ingredientList);
				highestScore = Math.Max(highestScore, thisScore);
			}
		}
	}
	return highestScore;
}

static long Part2(List<(int, int, int, int, int)> ingredientList)
{
	long highestScore = long.MinValue;
	for (int a = 0; a <= 100; a++)
	{
		for (int b = 0; b <= 100 - a; b++)
		{
			for (int c = 0; c <= 100 - (a + b); c++)
			{
				int d = 100 - (a + b + c);
				long thisScore = Evaluate([a, b, c, d], ingredientList, 500);
				highestScore = Math.Max(highestScore, thisScore);
			}
		}
	}
	return highestScore;
}

static long Evaluate(int[] combination, List<(int, int, int, int, int)> ingredientList, int calorieGoal = -1)
{
	long capacity = 0L;
	for (int i = 0; i < combination.Length; i++)
	{
		capacity += combination[i] * ingredientList[i].Item1;
	}
	if (capacity <= 0)
	{
		return 0;
	}
	long durability = 0L;
	for (int i = 0; i < combination.Length; i++)
	{
		durability += combination[i] * ingredientList[i].Item2;
	}
	if (durability <= 0)
	{
		return 0;
	}
	long flavor = 0L;
	for (int i = 0; i < combination.Length; i++)
	{
		flavor += combination[i] * ingredientList[i].Item3;
	}
	if (flavor <= 0)
	{
		return 0;
	}
	long texture = 0L;
	for (int i = 0; i < combination.Length; i++)
	{
		texture += combination[i] * ingredientList[i].Item4;
	}
	if (texture <= 0)
	{
		return 0;
	}
	if (calorieGoal >= 0)
	{	
		long calories = 0L;	
		for (int i = 0; i < combination.Length; i++)
		{
			calories += combination[i] * ingredientList[i].Item5;
		}
		if (calories != calorieGoal)
		{
			return 0;
		}
	}
	return capacity * durability * flavor * texture;	
}
