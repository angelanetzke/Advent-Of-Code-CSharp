using System.Diagnostics;

Console.WriteLine("Advent of Code 2024, Day 23");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result);
Console.WriteLine($"Part 1: {part1Result}");
Part2(allLines, out string part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	Dictionary<string, List<string>> network = [];
	foreach (string thisLine in allLines)
	{
		string firstComputer = thisLine.Split('-')[0];
		string secondComputer = thisLine.Split('-')[1];
		if (network.TryGetValue(firstComputer, out List<string>? neighbors))
		{
			neighbors.Add(secondComputer);
		}
		else
		{
			network[firstComputer] = [secondComputer];
		}
		if (network.TryGetValue(secondComputer, out neighbors))
		{
			neighbors.Add(firstComputer);
		}
		else
		{
			network[secondComputer] = [firstComputer];
		}
	}
	List<string> tComputers = [..network.Keys.Where(x => x.StartsWith('t'))];
	HashSet<string> cliques = [];
	foreach (string thisComputer in tComputers)
	{
		if (network[thisComputer].Count < 2)
		{
			continue;
		}
		for (int i = 0; i < network[thisComputer].Count - 1; i++)
		{
			for (int j = i + 1; j < network[thisComputer].Count; j++)
			{
				string computer1 = network[thisComputer][i];
				string computer2 = network[thisComputer][j];
				if (network[computer1].Contains(computer2))
				{
					List<string> thisClique = [thisComputer, computer1, computer2];
					thisClique.Sort();
					cliques.Add(string.Join("", thisClique));
				}
			}
		}
	}
	result = cliques.Count;
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static void Part2(string[] allLines, out string result)
{
	Stopwatch timer = new ();
	timer.Start();
	Dictionary<string, HashSet<string>> network = [];
	foreach (string thisLine in allLines)
	{
		string firstComputer = thisLine.Split('-')[0];
		string secondComputer = thisLine.Split('-')[1];
		if (network.TryGetValue(firstComputer, out HashSet<string>? neighbors))
		{
			neighbors.Add(secondComputer);
		}
		else
		{
			network[firstComputer] = [secondComputer];
		}
		if (network.TryGetValue(secondComputer, out neighbors))
		{
			neighbors.Add(firstComputer);
		}
		else
		{
			network[secondComputer] = [firstComputer];
		}
	}
	List<List<string>> cliques = [];	
	BronKerbosch([], [..network.Keys], [], network, cliques);
	cliques.Sort((x, y) => -1 * x.Count.CompareTo(y.Count));
	List<string> largest = cliques[0];
	largest.Sort();
	result = string.Join(',', largest);
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static void BronKerbosch(HashSet<string> R, HashSet<string> P, HashSet<string> X,
	Dictionary<string, HashSet<string>> network, List<List<string>> cliques)
{
	if (P.Count == 0 && X.Count == 0)
	{
			cliques.Add([..R]);
			return;
	}
	foreach (string v in P.ToList())
	{
			HashSet<string> neighbors = network[v];
			HashSet<string> nextR = [..R, v];
			HashSet<string> nextP = [..P.Intersect(neighbors)];
			HashSet<string> nextX = [..X.Intersect(neighbors)];
			BronKerbosch(nextR, nextP, nextX, network, cliques);
			P.Remove(v);
			X.Add(v);
	}
}

