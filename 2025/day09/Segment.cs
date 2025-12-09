internal class Segment((long, long) point1, (long, long) point2)
{
	private readonly long x1 = long.Min(point1.Item1, point2.Item1);
	private readonly long y1 = long.Min(point1.Item2, point2.Item2);
	private readonly long x2 = long.Max(point1.Item1, point2.Item1);
	private readonly long y2 = long.Max(point1.Item2, point2.Item2);

	public bool IntersectsPerpendicular(Segment other)
	{
		if (x1 == x2 && other.y1 == other.y2)
		{
			return other.x1 < x1 && x1 < other.x2 && y1 < other.y1 && other.y1 < y2;
		}
		else if (y1 == y2 && other.x1 == other.x2)
		{
			return x1 < other.x1 && other.x1 < x2 && other.y1 < y1 && y1 < other.y2;
		}
		return false;
	}

	public static bool IsPointOnSegment(Segment s, (long, long) point)
	{
		if (s.x1 == s.x2)
		{
			return s.x1 == point.Item1 && s.y1 <= point.Item2 && point.Item2 <= s.y2;
		}
		else
		{
			return s.y1 == point.Item2 && s.x1 <= point.Item1 && point.Item1 <= s.x2;
		}
	}

	public static bool DoesRayIntersect(Segment s, (long, long) rayStart)
	{
		if (s.y1 == s.y2)
		{
			return false;
		}
		if (rayStart.Item1 > s.x1)
		{
			return false;
		}
		return s.y1 <= rayStart.Item2 && rayStart.Item2 < s.y2;	
	}
}