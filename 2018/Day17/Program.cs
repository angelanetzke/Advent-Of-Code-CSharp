using Day17;
using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
var xRegex = new Regex(@"x=(?<x>\d+), y=(?<startY>\d+)..(?<endY>\d+)");
var yRegex = new Regex(@"y=(?<y>\d+), x=(?<startX>\d+)..(?<endX>\d+)");
var nodes = new Dictionary<(int, int), Node>();
var springPoint = (500, 0);
nodes[springPoint] = new Node (Node.NodeType.Spring);
foreach (string thisLine in allLines)
{
	if (thisLine[0] == 'x')
	{
		var match = xRegex.Match(thisLine);
		var x = int.Parse(match.Groups["x"].Value);
		var startY = int.Parse(match.Groups["startY"].Value);
		var endY = int.Parse(match.Groups["endY"].Value);
		for (int y = startY; y <= endY; y++)
		{
			nodes[(x, y)] = new Node(Node.NodeType.Clay);
		}			
	}
	else
	{
		var match = yRegex.Match(thisLine);
		var y = int.Parse(match.Groups["y"].Value);
		var startX = int.Parse(match.Groups["startX"].Value);
		var endX = int.Parse(match.Groups["endX"].Value);
		for (int x = startX; x <= endX; x++)
		{
			nodes[(x, y)] = new Node(Node.NodeType.Clay);
		}	
	}
}
var minY = nodes.Keys.Where(p => p != springPoint).Select(p => p.Item2).Min();
var maxY = nodes.Keys.Where(p => p != springPoint).Select(p => p.Item2).Max();
Node.allNodes = nodes;
Node.maxY = maxY;
Node.Execute((springPoint.Item1, minY), Node.FlowDirection.Down);
var waterCount = nodes.Values.Count(p => p.nodeType == Node.NodeType.SettledWater) 
	+ nodes.Values.Count(p => p.nodeType == Node.NodeType.UnsettledWater);
Console.WriteLine($"Part 1: {waterCount}");
waterCount = nodes.Values.Count(p => p.nodeType == Node.NodeType.SettledWater);
Console.WriteLine($"Part 2: {waterCount}");
