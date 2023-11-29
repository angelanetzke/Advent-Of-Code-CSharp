using Day7;
using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

void Part1(string[] allLines)
{
	var programParents = new Dictionary<string, string?>();
	foreach (string thisLine in allLines)
	{
		var thisName = thisLine.Split(' ')[0];
		programParents[thisName] = null;
	}
	foreach (string thisLine in allLines)
	{
		if (!thisLine.Contains(" -> "))
		{
			continue;
		}
		var thisParent = thisLine.Split(' ')[0];
		var thisChildrenString = thisLine.Split(" -> ")[1];
		var thisChildrenList = thisChildrenString.Split(", ");
		foreach (var thisChild in thisChildrenList)
		{
			programParents[thisChild] = thisParent;
		}
	}
	var root = programParents.Where(x => x.Value == null).First().Key;
	Console.WriteLine($"Part 1: {root}");
}

void Part2(string[] allLines)
{
	var weightRegex = new Regex(@"(?<weight>\d+)");
	var tree = new Dictionary<string, Node>();
	foreach (string thisLine in allLines)
	{
		var thisName = thisLine.Split(' ')[0];
		var match = weightRegex.Match(thisLine);
		var thisWeight = int.Parse(match.Groups["weight"].Value);
		tree[thisName] = new Node(thisWeight);
	}
	foreach (string thisLine in allLines)
	{
		if (!thisLine.Contains(" -> "))
		{
			continue;
		}
		var thisParent = thisLine.Split(' ')[0];
		var thisChildrenString = thisLine.Split(" -> ")[1];
		var thisChildrenList = thisChildrenString.Split(", ");
		foreach (var thisChild in thisChildrenList)
		{
			tree[thisParent].children.Add(tree[thisChild]);
			tree[thisChild].parent = tree[thisParent];
		}
	}
	var root = tree.Values.Where(x => x.parent == null).First();
	var result = root.BalanceTree();
	if (result != null)
	{
		Console.WriteLine($"Part 2: {result}");
	}
}