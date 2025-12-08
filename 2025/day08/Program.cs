using System.Diagnostics;

Console.WriteLine("Advent of Code 2025, Day 8");
string[] allLines = File.ReadAllLines("input.txt");

Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	Stopwatch timer = new();
	timer.Start();
	List<((long, long, long), (long, long, long), long)> distances = [];
	List<(long, long, long)> junctionBoxes 
		= [..allLines.Select(x => (long.Parse(x.Split(',')[0]), long.Parse(x.Split(',')[1]), long.Parse(x.Split(',')[2])))];
	List<HashSet<(long, long, long)>> circuits = [..junctionBoxes.Select(x => new HashSet<(long, long, long)>(){x})];
	for (int i = 0; i < junctionBoxes.Count - 1; i++)
	{
		for (int j = i + 1; j < junctionBoxes.Count; j++)
		{
			distances.Add((junctionBoxes[i], junctionBoxes[j], 
				(junctionBoxes[i].Item1 - junctionBoxes[j].Item1) * (junctionBoxes[i].Item1 - junctionBoxes[j].Item1)
				+ (junctionBoxes[i].Item2 - junctionBoxes[j].Item2) * (junctionBoxes[i].Item2 - junctionBoxes[j].Item2)
				+ (junctionBoxes[i].Item3 - junctionBoxes[j].Item3) * (junctionBoxes[i].Item3 - junctionBoxes[j].Item3)));
		}
	}
	distances = [..distances.OrderBy(x => x.Item3)];
	for (int i = 0; i < 1000; i++)
	{
		int first = circuits.FindIndex(x => x.Contains(distances[i].Item1));
		int second = circuits.FindIndex(x => x.Contains(distances[i].Item2));
		if (first == second)
		{
			continue;
		}
		circuits[first].UnionWith(circuits[second]);
		circuits.RemoveAt(second);
	}
	circuits = [..circuits.OrderByDescending(x => x.Count)];
	long result = circuits[0].Count * circuits[1].Count * circuits[2].Count;
	timer.Stop();
	Console.WriteLine($"Part 1: {result} ({timer.ElapsedMilliseconds} ms)");
}

static void Part2(string[] allLines)
{
	Stopwatch timer = new();
	timer.Start();
	List<((long, long, long), (long, long, long), long)> distances = [];
	List<(long, long, long)> junctionBoxes 
		= [..allLines.Select(x => (long.Parse(x.Split(',')[0]), long.Parse(x.Split(',')[1]), long.Parse(x.Split(',')[2])))];
	List<HashSet<(long, long, long)>> circuits = [..junctionBoxes.Select(x => new HashSet<(long, long, long)>(){x})];
	for (int i = 0; i < junctionBoxes.Count - 1; i++)
	{
		for (int j = i + 1; j < junctionBoxes.Count; j++)
		{
			distances.Add((junctionBoxes[i], junctionBoxes[j], 
				(junctionBoxes[i].Item1 - junctionBoxes[j].Item1) * (junctionBoxes[i].Item1 - junctionBoxes[j].Item1)
				+ (junctionBoxes[i].Item2 - junctionBoxes[j].Item2) * (junctionBoxes[i].Item2 - junctionBoxes[j].Item2)
				+ (junctionBoxes[i].Item3 - junctionBoxes[j].Item3) * (junctionBoxes[i].Item3 - junctionBoxes[j].Item3)));
		}
	}
	distances = [..distances.OrderBy(x => x.Item3)];
	long result = -1L;
	int k = 0;
	while (circuits.Count > 1)
	{
		int first = circuits.FindIndex(x => x.Contains(distances[k].Item1));
		int second = circuits.FindIndex(x => x.Contains(distances[k].Item2));
		result = distances[k].Item1.Item1 * distances[k].Item2.Item1;
		k++;
		if (first == second)
		{
			continue;
		}
		circuits[first].UnionWith(circuits[second]);
		circuits.RemoveAt(second);		
	}	
	timer.Stop();
	Console.WriteLine($"Part 2: {result} ({timer.ElapsedMilliseconds} ms)");
}
