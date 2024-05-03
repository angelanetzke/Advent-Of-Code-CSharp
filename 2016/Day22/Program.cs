using System.Text.RegularExpressions;

string[] allLines = File.ReadAllLines("input.txt");
(int, int) result = Part1AndPart2(allLines);
Console.WriteLine($"Part 1: {result.Item1}");
Console.WriteLine($"Part 2: {result.Item2}");

static (int, int) Part1AndPart2(string[] allLines)
{
	(int, int) result = (-1, -1);
	Regex nodeDataRegex = new (@"x(?<x>\d+)-y(?<y>\d+)\s+(?<size>\d+)T\s+(?<used>\d+)T\s+(?<available>\d+)T\s+(?<percent>\d+)");
	List<(int, int, int, int, int, int)> allNodes = new (); // x, y, size, used, available, percent
	(int, int, int, int, int, int) emptyNode = (0, 0, 0, 0, 0, 0); // x, y, size, used, available, percent
	int maxX = int.MinValue;
	for (int i = 2; i < allLines.Length; i++)
	{
		Match match = nodeDataRegex.Match(allLines[i]);
		int x = int.Parse(match.Groups["x"].Value);
		int y = int.Parse(match.Groups["y"].Value);
		int size = int.Parse(match.Groups["size"].Value);
		int used = int.Parse(match.Groups["used"].Value);
		int available = int.Parse(match.Groups["available"].Value);
		int percent = int.Parse(match.Groups["percent"].Value);
		allNodes.Add((x, y, size, used, available, percent));
		if (allNodes[^1].Item4 == 0)
		{
			emptyNode = (x, y, size, used, available, percent);
		}
		maxX = Math.Max(maxX, x);
	}
	List<(int, int)> traversibleNodes = [ (emptyNode.Item1, emptyNode.Item2) ]; // x, y
	int viablePairs = 0;
	for (int i = 0; i < allNodes.Count; i++)
	{
		if (allNodes[i] == emptyNode)
		{
			continue;
		}
		if (allNodes[i].Item4 <= emptyNode.Item5)
		{
			traversibleNodes.Add((allNodes[i].Item1, allNodes[i].Item2));
			viablePairs++;
		}
	}
	result.Item1 = viablePairs;
	(int, int)[] deltas = [ (0, 1), (0, -1), (1, 0), (-1, 0) ];
	List<((int, int), (int, int), int, int)> queue = [];
	queue.Add(((maxX, 0), (emptyNode.Item1, emptyNode.Item2), 0, 0)); // data, empty, moves, priority
	HashSet<((int, int), (int, int))> visited = []; // data, empty
	while (queue.Count > 0)
	{
		queue = [ ..queue.OrderBy(x => x.Item4) ];
		((int, int) data, (int, int) empty, int moves, int priority) current = queue[0];
		queue.RemoveAt(0);
		visited.Add((current.data, current.empty));
		if (current.data == (0, 0))
		{
			result.Item2 = current.moves;
			break;
		}
		foreach ((int deltaX, int deltaY) in deltas)
		{
			if (current.data.Item1 + deltaX == current.empty.Item1
				&& current.data.Item2 + deltaY == current.empty.Item2
				&& !visited.Contains((current.empty, current.data))
				&& !queue.Where(x => x.Item1 == current.empty && x.Item2 == current.data).Any())
			{
				int priority;
				if (current.data.Item1 == 0 && current.data.Item2 == 0)
				{
					priority = 0;
				}
				else
				{
					priority = current.moves + 1 + current.empty.Item1 + current.empty.Item2;
				}
				queue.Add((current.empty, current.data, current.moves + 1, priority));
			}			
		}
		foreach ((int deltaX, int deltaY) in deltas)
		{
			int nextEmptyX = current.empty.Item1 + deltaX;
			int nextEmptyY = current.empty.Item2 + deltaY;
			if (!traversibleNodes.Contains((nextEmptyX, nextEmptyY))
			|| current.data.Item1 == nextEmptyX && current.data.Item2 == nextEmptyY)
			{
				continue;
			}
			if (!visited.Contains((current.data, (nextEmptyX, nextEmptyY)))
				&& !queue.Where(x => x.Item1 == current.data && x.Item2 == (nextEmptyX, nextEmptyY)).Any())
			{
				int priority = Math.Abs(current.data.Item1 - nextEmptyX) 
					+ Math.Abs(current.data.Item2 - nextEmptyY);
				queue.Add((current.data, (nextEmptyX, nextEmptyY), current.moves + 1, priority));
			}
		}
	}
	return result;
}

