using System;

namespace Dec13
{
	internal class Location
	{
		private readonly int x;
		private readonly int y;

		public Location (int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		public int GetX()
		{
			return x;
		}

		public int GetY()
		{
			return y;
		}

		public override bool Equals(object obj)
		{
			return obj is Location location &&
						 x == location.x &&
						 y == location.y;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(x, y);
		}

	}
}
