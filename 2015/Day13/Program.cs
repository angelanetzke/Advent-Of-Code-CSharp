string[] allLines = File.ReadAllLines("input.txt");
Dictionary<string, List<(string, int)>> happinessDeltas = [];
foreach (string thisLine in allLines)
{
	string[] parts = thisLine.Split(' ');
	int thisDelta = 0;
	if (parts[2] == "gain")
	{
		thisDelta = int.Parse(parts[3]);
	}
	else
	{
		thisDelta = -int.Parse(parts[3]);
	}
	if (happinessDeltas.TryGetValue(parts[0], out List<(string, int)>? listValue))
	{
		listValue.Add((parts[10][..^1], thisDelta));
	}
	else
	{
		happinessDeltas[parts[0]] = [ (parts[10][..^1], thisDelta) ];
	}
}
Console.WriteLine($"Part 1: {Part1(happinessDeltas)}");
Console.WriteLine($"Part 2: {Part2(happinessDeltas)}");

static int Part1(Dictionary<string, List<(string, int)>> happinessDeltas)
{	
	string[] names = [ ..happinessDeltas.Keys ];
	List<string[]> possibilities = [];
	GeneratePermutations(names, 0, possibilities);
	int happiest = int.MinValue;
	foreach (string[] thisPossibility in possibilities)
	{
		int thisHappinessScore = Evaluate(happinessDeltas, thisPossibility);
		happiest = Math.Max(happiest, thisHappinessScore);
	}
	return happiest;
}

static int Part2(Dictionary<string, List<(string, int)>> happinessDeltas)
{	
	happinessDeltas["Angie"] = [];
	foreach (string thisPersonKey in happinessDeltas.Keys)
	{
		happinessDeltas[thisPersonKey].Add(("Angie", 0));
		happinessDeltas["Angie"].Add((thisPersonKey, 0));	
	}
	string[] names = [ ..happinessDeltas.Keys ];
	List<string[]> possibilities = [];
	GeneratePermutations(names, 0, possibilities);
	int happiest = int.MinValue;
	foreach (string[] thisPossibility in possibilities)
	{
		int thisHappinessScore = Evaluate(happinessDeltas, thisPossibility);
		happiest = Math.Max(happiest, thisHappinessScore);
	}
	return happiest;
}

static void GeneratePermutations(string[] names, int index, List<string[]> possibilities)
{
	if (index == names.Length)
	{
		string[] thisPossibility = new string[names.Length];
		Array.Copy(names, thisPossibility, names.Length);
		possibilities.Add(thisPossibility);
	}
	else
	{
		for (int i = index; i < names.Length; i++)
		{
			string temp = names[index];
			names[index] = names[i];
			names[i] = temp;
			GeneratePermutations(names, index + 1, possibilities);
			temp = names[index];
			names[index] = names[i];
			names[i] = temp;
		}
	}
}

static int Evaluate(Dictionary<string, List<(string, int)>> happinessDeltas, string[] thisPossibility)
{
	int total = 0;
	for (int i = 0; i < thisPossibility.Length; i++)
	{
		int leftIndex = (i + thisPossibility.Length - 1) % thisPossibility.Length;
		int rightIndex = (i + 1) % thisPossibility.Length;
		total += happinessDeltas[thisPossibility[i]]
			.Where(x => x.Item1 == thisPossibility[leftIndex])
			.Select(x => x.Item2)
			.First();
		total += happinessDeltas[thisPossibility[i]]
			.Where(x => x.Item1 == thisPossibility[rightIndex])
			.Select(x => x.Item2)
			.First();	
	}
	return total;
}
