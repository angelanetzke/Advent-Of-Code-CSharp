string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static int Part1(string[] allLines)
{
	int[] capacities = allLines.Select(x => int.Parse(x)).ToArray();
	bool[] isIncluded = Enumerable.Repeat(false, capacities.Length).ToArray();
	bool isComplete = false;
	int validCount = 0;
	while (!isComplete)
	{
		validCount += IsValid(capacities, isIncluded, 150) ? 1 : 0;
		if (isIncluded.All(x => x))
		{
			isComplete = true;
			break;
		}
		for (int i = isIncluded.Length - 1; i >= 0; i--)
		{
			isIncluded[i] = !isIncluded[i];
			if (isIncluded[i])
			{
				break;
			}			
		}
	}
	return validCount;
}

static int Part2(string[] allLines)
{
	Dictionary<int, int> containerCounts = []; // number of containers, count
	int[] capacities = allLines.Select(x => int.Parse(x)).ToArray();
	bool[] isIncluded = Enumerable.Repeat(false, capacities.Length).ToArray();
	bool isComplete = false;
	while (!isComplete)
	{
		int thisContainerCount = isIncluded.Count(x => x);
		bool isValidCombination = IsValid(capacities, isIncluded, 150);
		if (isValidCombination)
		{
			if (containerCounts.ContainsKey(thisContainerCount))
			{
				containerCounts[thisContainerCount]++;
			}
			else
			{
				containerCounts[thisContainerCount] = 1;
			}
		}
		if (isIncluded.All(x => x))
		{
			isComplete = true;
			break;
		}
		for (int i = isIncluded.Length - 1; i >= 0; i--)
		{
			isIncluded[i] = !isIncluded[i];
			if (isIncluded[i])
			{
				break;
			}			
		}
	}
	int minContainers = containerCounts.Keys.Min();
	return containerCounts[minContainers];
}

static bool IsValid(int[] capacities, bool[] isIncluded, int targetTotal)
{
	int thisTotal = 0;
	{
		for (int i = 0; i < capacities.Length; i++)
		{
			thisTotal += isIncluded[i] ? capacities[i] : 0;
		}
	}
	return thisTotal == targetTotal;
}