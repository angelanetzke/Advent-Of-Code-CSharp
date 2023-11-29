var text = File.ReadAllLines("input.txt")[0];
var moves = text.Split(',');
var x = 0;
var y = 0;
var positionSet = new HashSet<(int, int)>();
foreach (string thisMove in moves)
{
	switch (thisMove)
	{
		case "n":
			y -= 10;
			break;
		case "s":
			y += 10;
			break;
		case "nw":
			x -= 5;
			y -= 5;
			break;
		case "ne":
			x += 5;
			y -= 5;
			break;
		case "sw":
			x -= 5;
			y += 5;
			break;
		case "se":
			x += 5;
			y += 5;
			break;
	}
	positionSet.Add((x, y));
}
var minX = positionSet.Min(x => x.Item1);
var maxX = positionSet.Max(x => x.Item1);
var minY = positionSet.Min(x => x.Item2);
var maxY = positionSet.Max(x => x.Item2);
var offsets = new List<(int, int)>() { (0, -10), (0, 10), (-5, -5), (-5, 5), (5, -5), (5, 5) };
var distances = new Dictionary<(int, int), int>();
distances[(0, 0)] = 0;
var queue = new Queue<(int, int)>();
queue.Enqueue((0, 0));
var visited = new HashSet<(int, int)>();
while (queue.Count > 0)
{
	var current = queue.Dequeue();
	visited.Add(current);
	foreach (var thisOffset in offsets)
	{
		var next = (current.Item1 + thisOffset.Item1, current.Item2 + thisOffset.Item2);
		if (next.Item1 < minX || next.Item1 > maxX || next.Item2 < minY || next.Item2 > maxY)
		{
			continue;
		}
		if (distances.ContainsKey(next))
		{
			distances[next] = Math.Min(distances[next], distances[current] + 1);
		}
		else
		{
			distances[next] = distances[current] + 1;
		}
		if (!visited.Contains(next) && !queue.Contains(next))
		{
			queue.Enqueue(next);
		}			
	}
}
Console.WriteLine($"Part 1: {distances[(x, y)]}");
var maxDistance = distances.Where(x => positionSet.Contains(x.Key)).Max(x => x.Value);
Console.WriteLine($"Part 2 {maxDistance}");

