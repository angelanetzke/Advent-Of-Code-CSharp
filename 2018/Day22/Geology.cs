namespace Day22;

internal class Geology
{
	public static int depth { get; set;}
	public static (int x, int y) targetLocation { get; set; }
	private static Dictionary<(int x, int y), int> geologicCache = new ();
	private static Dictionary<(int x, int y), int> erosionCache = new ();
	
	public static int GetGeologicIndex(int x, int y)
	{
		int value;
		if (geologicCache.ContainsKey((x, y)))
		{
			return geologicCache[(x, y)];
		}
		if (x == 0 && y == 0)
		{
			geologicCache[(0, 0)] = 0;
			return 0;
		}
		if (x == targetLocation.x && y == targetLocation.y)
		{
			geologicCache[targetLocation] = 0;
			return 0;
		}
		if (y == 0)
		{
			value = x * 16807;
			geologicCache[(x, y)] = value;
			return value;
		}
		if (x == 0)
		{
			value = y * 48271;
			geologicCache[(x, y)] = value;
			return value;
		}
		value = GetErosionLevel(x - 1, y) * GetErosionLevel(x, y - 1);
		geologicCache[(x, y)] = value;
		return value;
	}
	
	public static int GetErosionLevel(int x, int y)
	{
		if (erosionCache.ContainsKey((x, y)))
		{
			return erosionCache[(x, y)];
		}
		var value = (GetGeologicIndex(x, y) + depth) % 20183;
		erosionCache[(x, y)] = value;
		return value;
	}
}