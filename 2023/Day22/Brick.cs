namespace Day22;

internal class Brick : IComparable<Brick>
{
	private static int stackMinX = int.MaxValue ;
	private static int stackMaxX = int.MinValue;
	private static int stackMinY = int.MaxValue;
	private static int stackMaxY = int.MinValue;
	private readonly int minX;
	private readonly int maxX;
	private readonly int minY;
	private readonly int maxY;
	private int minZ;
	private int maxZ;
	private readonly int id;
	private static int nextID = 0;

	public Brick(string data)
	{
		id = nextID;
		nextID++;
		int x1 = int.Parse(data.Split('~')[0].Split(',')[0]);
		int x2 = int.Parse(data.Split('~')[1].Split(',')[0]);
		minX = Math.Min(x1, x2);
		maxX = Math.Max(x1, x2);
		stackMinX = Math.Min(stackMinX, minX);
		stackMaxX = Math.Max(stackMaxX, maxX);
		int y1 = int.Parse(data.Split('~')[0].Split(',')[1]);
		int y2 = int.Parse(data.Split('~')[1].Split(',')[1]);
		minY = Math.Min(y1, y2);
		maxY = Math.Max(y1, y2);
		stackMinY = Math.Min(stackMinY, minY);
		stackMaxY = Math.Max(stackMaxY, maxY);
		int z1 = int.Parse(data.Split('~')[0].Split(',')[2]);
		int z2 = int.Parse(data.Split('~')[1].Split(',')[2]);
		minZ = Math.Min(z1, z2);
		maxZ = Math.Max(z1, z2);
	}

	public Brick(int minX, int maxX, int minY, int maxY, int minZ, int maxZ)
	{
		this.minX = minX;
		this.maxX = maxX;
		this.minY = minY;
		this.maxY = maxY;
		this.minZ = minZ;
		this.maxZ = maxZ;
	}

	public static Brick GetGround()
	{
		return new Brick(stackMinX, stackMaxX, stackMinY, stackMaxY, 0, 0);
	}

	public bool Contains(int x, int y, int z)
	{
		return minX <= x && x <= maxX
			&& minY <= y && y <= maxY
			&& minZ <= z && z <= maxZ;
	}

	public bool CanFall(Brick other)
	{
		for (int x = minX; x <= maxX; x++)
		{
			for (int y = minY; y <= maxY; y++)
			{
				if (other.Contains(x, y, minZ - 1))
				{
					return false;
				}
			}
		}
		return true;
	}

	public bool IsSupporting(Brick other)
	{
		for (int x = minX; x <= maxX; x++)
		{
			for (int y = minY; y <= maxY; y++)
			{
				if (other.Contains(x, y, maxZ + 1))
				{
					return true;
				}
			}
		}
		return false;
	}

	public void Fall()
	{
		minZ--;
		maxZ--;
	}

	public int CompareTo(Brick? other)
	{
		if (other is Brick b)
		{
			return -1 * maxZ.CompareTo(other.maxZ);
		}
		return 1;
	}

	public override bool Equals(object? obj)
	{
		if (obj is Brick other)
		{
			return id == other.id;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return id.GetHashCode();
	}

}