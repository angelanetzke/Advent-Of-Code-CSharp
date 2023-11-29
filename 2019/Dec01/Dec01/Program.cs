var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

void Part1(string[] allLines)
{
	long sum = 0L;
	foreach (string thisLine in allLines)
	{
		var thisMass = long.Parse(thisLine);
		var thisFuel = thisMass / 3 - 2;
		sum += thisFuel;
	}
	Console.WriteLine($"Part 1: {sum}");
}

void Part2(string[] allLines)
{
	long sum = 0L;
	foreach (string thisLine in allLines)
	{
		sum += CalculateTotalFuel(long.Parse(thisLine));
	}
	Console.WriteLine($"Part 2: {sum}");
}

long CalculateTotalFuel(long mass)
{
	var sum = 0L;
	var nextFuel = mass / 3 - 2;
	while (nextFuel > 0)
	{
		sum += nextFuel;
		nextFuel = nextFuel / 3 - 2;
	}
	return sum;
}