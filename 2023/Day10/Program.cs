using Day10;

string[] allLines = File.ReadAllLines("input.txt");
TunnelMap map = new (allLines);
Console.WriteLine($"Part 1: {Part1(map)}");
Console.WriteLine($"Part 2: {Part2(map)}");

static int Part1(TunnelMap map)
{	
	return map.GetFarthestTileDistance();
}

static int Part2(TunnelMap map)
{
	return map.CountInsideGround();
}
