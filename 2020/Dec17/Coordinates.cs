namespace Dec17
{
	internal class Coordinates
	{
		private readonly int x;
		private readonly int y;
		private readonly int z;

		public Coordinates(int x, int y, int z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public List<Coordinates> GetNeighbors()
		{
			var neighbors = new List<Coordinates>();
			for (int deltaX = -1; deltaX <= 1; deltaX++)
			{
				for (int deltaY = -1; deltaY <= 1; deltaY++)
				{
					for (int deltaZ = -1; deltaZ <= 1; deltaZ++)
					{
						if (deltaX != 0 || deltaY != 0 || deltaZ != 0)
						{
							neighbors.Add(new Coordinates(x + deltaX, y + deltaY, z + deltaZ));
						}
					}
				}
			}
			return neighbors;
		}

		public int GetX()
		{
			return x;
		}

		public int GetY()
		{
			return y;
		}

		public int GetZ()
		{
			return z;
		}

		public override bool Equals(Object? obj)
		{
			if (obj == null)
			{
				return false;
			}
			else if (obj is Coordinates other)
			{
				return x == other.x && y == other.y && z == other.z;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return (x + y + z).GetHashCode();
		}

	}
}
