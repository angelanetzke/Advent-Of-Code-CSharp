using System.Text;

var allLines = File.ReadAllLines("input.txt");
var spread = new Dictionary<string, char>();
for (int i = 2; i < allLines.Length; i++)
{
	var inputString = allLines[i].Split(" => ")[0];
	var outputChar = allLines[i].Split(" => ")[1][0];
	spread[inputString] = outputChar;
}
Part1(allLines, spread);
Part2(allLines, spread);

static void Part1(string[] allLines, Dictionary<string, char> spread)
{
	var plantSum = RunSimulation(allLines, 20, spread);
	Console.WriteLine($"Part 1: {plantSum}");
}

static void Part2(string[] allLines, Dictionary<string, char> spread)
{
	var plantSum = RunSimulation(allLines, 50_000_000_000, spread);
	Console.WriteLine($"Part 2: {plantSum}");
}

static long RunSimulation(string[] allLines, long generationCount, Dictionary<string, char> spread)
{	
	var plants = new HashSet<long>();
	var nextPlants = new HashSet<long>();
	var seen = new HashSet<string>();	
	for (int i = 15; i < allLines[0].Length; i++)
	{
		if (allLines[0][i] == '#')
		{
			plants.Add(i - 15);
		}
	}
	var builder = new StringBuilder();
	for (long generationNumber = 1L; generationNumber <= generationCount; generationNumber++)
	{
		var plantPattern = GetPlantPattern(plants);
		if (seen.Contains(plantPattern))
		{
			return plants.Sum() + plants.Count() * (generationCount - generationNumber + 1);
		}
		seen.Add(plantPattern);
		nextPlants.Clear();
		for (long plantNumber = plants.Min() - 2; plantNumber <= plants.Max() + 2; plantNumber++)
		{
			builder.Clear();
			for (int neighbor = -2; neighbor <= 2; neighbor++)
			{
				if (plants.Contains(plantNumber + neighbor))
				{
					builder.Append('#');
				}
				else
				{
					builder.Append('.');
				}
			}
			if (spread.ContainsKey(builder.ToString()) && spread[builder.ToString()] == '#')
			{
				nextPlants.Add(plantNumber);
			}
		}
		plants = new HashSet<long>(nextPlants);
	}
	return plants.Sum();
}

static string GetPlantPattern(HashSet<long> plants)
{
	var builder = new StringBuilder();
	for (long i = plants.Min(); i <= plants.Max(); i++)
	{
		if (plants.Contains(i))
		{
			builder.Append('#');
		}
		else
		{
			builder.Append('.');
		}
	}
	return builder.ToString();
}
