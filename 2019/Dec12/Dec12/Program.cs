using Dec12;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var allMoons = new List<Moon>();
	foreach (string thisLine in allLines)
	{
		allMoons.Add(new Moon(thisLine));
	}
	int TOTAL_STEPS = 1000;
	for (int step = 0; step < TOTAL_STEPS; step++)
	{
		foreach (Moon firstMoon in allMoons)
		{
			foreach (Moon secondMoon in allMoons)
			{
				firstMoon.CalculateGravity(secondMoon);
			}
		}
		foreach (Moon thisMoon in allMoons)
		{
			thisMoon.ApplyGravity();
		}
	}
	long totalEnergy = 0L;
	foreach (Moon thisMoon in allMoons)
	{
		totalEnergy += thisMoon.GetTotalEnergy();
	}
	Console.WriteLine($"Part 1: {totalEnergy}");
}

static void Part2(string[] allLines)
{
	var allMoons = new List<Moon>();
	foreach (string thisLine in allLines)
	{
		allMoons.Add(new Moon(thisLine));
	}
	var theSystem = new MoonSystem(allMoons);
	bool isComplete = false;
	while (!isComplete)
	{
		theSystem.NextStep();
		isComplete = theSystem.IsComplete();
	}
	Console.WriteLine($"Part 2: {theSystem.GetLCM()}");
}