namespace Dec24
{
	internal class Tile
	{
		private readonly int x;
		private readonly int y;

		public Tile(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public Tile[] GetNeighbors()
		{
			var neighbors = new Tile[6];
			neighbors[0] = new Tile(x + 10, y);
			neighbors[1] = new Tile(x + 5, y + 5);
			neighbors[2] = new Tile(x - 5, y + 5);
			neighbors[3] = new Tile(x - 10, y);
			neighbors[4] = new Tile(x + 5, y - 5);
			neighbors[5] = new Tile(x - 5, y - 5);
			return neighbors;
		}

		public override bool Equals(object? obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is Tile other)
			{
				return x == other.x && y == other.y;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return (x + y).GetHashCode();
		}

	}
}
