using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
var screen = new bool[50, 6];
var buffer = new Dictionary<int, bool>();
foreach (string thisLine in allLines)
{
	if (thisLine.StartsWith("rect"))
	{
		var match = new Regex(@"rect (?<x>\d+)x(?<y>\d+)").Match(thisLine);
		var width = int.Parse(match.Groups["x"].Value);
		var height = int.Parse(match.Groups["y"].Value);
		for (int x = 0; x < width; x++)
		{
			for (int y = 0; y < height; y++)
			{
				screen[x, y] = true;
			}
		}
	}
	else if (thisLine.StartsWith("rotate row"))
	{
		buffer.Clear();
		var match = new Regex(@"rotate row y=(?<y>\d+) by (?<distance>\d+)").Match(thisLine);
		var y = int.Parse(match.Groups["y"].Value);
		var distance = int.Parse(match.Groups["distance"].Value);
		for (int x = screen.GetLength(0) - 1; x >= 0; x--)
		{
			buffer[(x + distance) % screen.GetLength(0)] = screen[x, y];
		}
		foreach (int x in buffer.Keys)
		{
			screen[x, y] = buffer[x];
		}
	}
	else if (thisLine.StartsWith("rotate column"))
	{
		buffer.Clear();
		var match = new Regex(@"rotate column x=(?<x>\d+) by (?<distance>\d+)").Match(thisLine);
		var x = int.Parse(match.Groups["x"].Value);
		var distance = int.Parse(match.Groups["distance"].Value);
		for (int y = screen.GetLength(1) - 1; y >= 0; y--)
		{
			buffer[(y + distance) % screen.GetLength(1)] = screen[x, y];
		}
		foreach (int y in buffer.Keys)
		{
			screen[x, y] = buffer[y];
		}
	}
}
var pixelsOnCount = 0;
for (int y = 0; y < screen.GetLength(1); y++)
{
	for (int x = 0; x < screen.GetLength(0); x++)
	{
		if (screen[x, y])
		{
			Console.Write("#");
			pixelsOnCount++;
		}
		else
		{
			Console.Write(" ");
		}
	}
	Console.WriteLine();
}
Console.WriteLine($"Part 1: {pixelsOnCount}");





