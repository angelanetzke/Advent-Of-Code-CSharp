namespace Day09;

internal class Rectangle((long, long) point1, (long, long) point2)
{
	private readonly long minX = long.Min(point1.Item1, point2.Item1);
	private readonly long minY = long.Min(point1.Item2, point2.Item2);
	private readonly long maxX = long.Max(point1.Item1, point2.Item1);
	private readonly long maxY = long.Max(point1.Item2, point2.Item2);

	public bool IsValid(List<Segment> segments)
	{
		List<(long, long)> corners = GetCorners();
		foreach ((long, long) c in corners)
		{
			if (!IsPointInPolygon(segments, c))
			{
				return false;
			}
		}
		List<Segment> sides = GetSides(corners);
		foreach (Segment s1 in sides)
		{
			foreach (Segment s2 in segments)
			{
				if (s1.IntersectsPerpendicular(s2))
				{
					return false;
				}
			}
		}
		return true;	
	}

	private List<(long, long)> GetCorners()
	{
		return [ (minX, minY), (minX, maxY), (maxX, maxY), (maxX, minY) ];
	}

	private static List<Segment> GetSides(List<(long, long)> corners)
	{
		List<Segment> sides = [];
		for (int i = 0; i < corners.Count; i++)
		{
			sides.Add(new Segment(corners[i], corners[(i + 1) % corners.Count]));
		}
		return sides;
	}

	private static bool IsPointInPolygon(List<Segment> segments, (long, long) point)
	{		
		foreach (Segment s in segments)
		{
			if (Segment.IsPointOnSegment(s, point))
			{
				return true;
			}
		}
		long count = 0L;
		foreach (Segment s in segments)
		{
			count += Segment.DoesRayIntersect(s, point) ? 1L : 0L;
		}
		return count % 2 == 1L;
	}
}