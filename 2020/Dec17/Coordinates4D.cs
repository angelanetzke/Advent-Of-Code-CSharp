namespace Dec17
{
	internal class Coordinates4D
	{
		private readonly int w;
		private readonly int x;
		private readonly int y;
		private readonly int z;		

		public Coordinates4D(int w, int x, int y, int z)
		{
			this.w = w;
			this.x = x;
			this.y = y;
			this.z = z;
		}

		public List<Coordinates4D> GetNeighbors()
		{
			var neighbors = new List<Coordinates4D>();
			for (int deltaW = -1; deltaW <= 1; deltaW++)
			{
				for (int deltaX = -1; deltaX <= 1; deltaX++)
				{
					for (int deltaY = -1; deltaY <= 1; deltaY++)
					{
						for (int deltaZ = -1; deltaZ <= 1; deltaZ++)
						{
							if (deltaW != 0 || deltaX != 0 || deltaY != 0 || deltaZ != 0)
							{
								neighbors.Add(new Coordinates4D(w + deltaW, x + deltaX, y + deltaY, z + deltaZ));
							}
						}
					}
				}
			}
			return neighbors;
		}

		public int GetW()
		{
			return w;
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
			else if (obj is Coordinates4D other)
			{
				return w == other.w && x == other.x && y == other.y && z == other.z;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return (w + x + y + z).GetHashCode();
		}

	}
}
