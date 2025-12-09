using Day09;
using System.Diagnostics;

Console.WriteLine("Advent of Code 2025, Day 9");
string[] allLines = File.ReadAllLines("input.txt");
List<(long, long)> tiles = [..allLines.Select(x => (long.Parse(x.Split(',')[0]), long.Parse(x.Split(',')[1])))];
Part1(tiles, out List<(Rectangle, long)> rectangles);
Part2(tiles, rectangles);

static void Part1(List<(long, long)> tiles, out List<(Rectangle, long)> rectangles)
{
	Stopwatch timer = new();
	timer.Start();
	long result = -1L;
	rectangles = [];
	for (int i = 0; i < tiles.Count - 1; i++)
	{
		for (int j = i + 1; j < tiles.Count; j++)
		{
			long size = (Math.Abs(tiles[i].Item1 - tiles[j].Item1) + 1) * (Math.Abs(tiles[i].Item2 - tiles[j].Item2) + 1);
			rectangles.Add((new Rectangle(tiles[i], tiles[j]), size));
			result = long.Max(result, size);
		}
	}	
	timer.Stop();
	Console.WriteLine($"Part 1: {result} ({timer.ElapsedMilliseconds} ms)");
}

static void Part2(List<(long, long)> tiles, List<(Rectangle, long)> rectangles)
{
	Stopwatch timer = new();
	timer.Start();
	long result = -1L;
	rectangles = [..rectangles.OrderByDescending(x => x.Item2)];
	List<Segment> segments = [];
	int i;
	for (i = 0; i < tiles.Count; i++)
	{
		segments.Add(new Segment(tiles[i], tiles[(i + 1) % tiles.Count]));
	}
	foreach ((Rectangle, long) r in rectangles)
	{
		if (!r.Item1.IsValid(segments))
		{
			continue;
		}
		result = r.Item2;
		break;		
	}
	timer.Stop();
	Console.WriteLine($"Part 2: {result} ({timer.ElapsedMilliseconds} ms)");
}





