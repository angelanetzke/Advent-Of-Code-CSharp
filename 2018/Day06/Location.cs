namespace Day06
{
	internal class Location
	{
		private readonly int x;
		private readonly int y;
		public bool isInfinite { set; get; } = false;
		private readonly HashSet<(int, int)> points = new ();

		public Location(string data)
		{
			x = int.Parse(data.Split(", ")[0]);
			y = int.Parse(data.Split(", ")[1]);
		}

		public int GetX()
		{
			return x;
		}

		public int GetY()
		{
			return y;
		}

		public int GetDistanceToPoint(int otherX, int otherY)
		{
			return Math.Abs(x - otherX) + Math.Abs(y - otherY);
		}

		public void AddPoint(int x, int y)
		{
			points.Add((x, y));
		}

		public int GetPointCount()
		{
			return points.Count;
		}

	}
}