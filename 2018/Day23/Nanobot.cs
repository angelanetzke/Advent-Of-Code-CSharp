namespace Day23;

internal class Nanobot
{
	public int x { get; }
	public int y { get; }
	public int z { get; }
	public int radius { get; }

	public Nanobot((int x, int y, int z, int radius) definition)
	{
		this.x = definition.x;
		this.y = definition.y;
		this.z = definition.z;
		this.radius = definition.radius;
	}

	public bool ContainsPoint((int x, int y, int z) point)
	{
		return Math.Abs(point.x - x) + Math.Abs(point.y - y) + Math.Abs(point.z - z) <= radius;
	}

	public (int x, int y, int z)[] GetPoints()
	{
		var pointArray = new (int, int, int)[6]
		{
			(x - radius, y, z),
			(x - radius, y, z),
			(x, y - radius, z),
			(x, y + radius, z),
			(x, y, z - radius),
			(x, y, z + radius),
		};
		return pointArray;
	}
}