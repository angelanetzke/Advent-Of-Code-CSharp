namespace Day23;

internal class Section
{
	public int minX { get; }
	public int maxX { get; }
	public int minY { get; }
	public int maxY { get; }
	public int minZ { get; }
	public int maxZ { get; }

	public Section(int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
	{
		this.minX = minX;
		this.maxX = maxX;
		this.minY = minY;
		this.maxY = maxY;
		this.minZ = minZ;
		this.maxZ = maxZ;
	}

	public bool ContainsPoint((int x, int y, int z) point)
	{
		return minX <= point.x && point.x <= maxX
			&& minY <= point.y && point.y <= maxY
			&& minZ <= point.z && point.z <= maxZ;
	}

	public (int x, int y, int z)[] GetPoints()
	{
		var pointArray = new (int, int, int)[8]
		{
			(minX, minY, minZ),
			(minX, maxY, minZ),
			(minX, minY, maxZ),
			(minX, maxY, maxZ),
			(maxX, minY, minZ),
			(maxX, maxY, minZ),
			(maxX, minY, maxZ),
			(maxX, maxY, maxZ),
		};
		return pointArray;
	}

	public override bool Equals(object? obj)
	{
		if (obj != null && obj is Section other)
		{
			return minX == other.minX
				&& maxX == other.maxX
				&& minY == other.minY
				&& maxY == other.maxY
				&& minZ == other.minZ
				&& maxZ == other.maxZ;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return (minX.GetHashCode() + maxX.GetHashCode()
			+ minY.GetHashCode() + maxY.GetHashCode()
			+ minZ.GetHashCode() + maxZ.GetHashCode())
			.GetHashCode();
	}
}