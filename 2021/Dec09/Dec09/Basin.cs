using System;
using System.Collections.Generic;

namespace Dec09
{
	internal class Basin : IComparable
	{
		private readonly List<Point> points;
		public Basin()
		{
			points = new();
		}

		public void AddPoint(Point p)
		{
			points.Add(p);
		}

		public int GetSize()
		{
			return points.Count;
		}

		public int CompareTo(object obj)
		{
			if (obj is not Basin)
			{
				throw new ArgumentException();
			}
			else
			{
				Basin other = (Basin)obj;
				return GetSize().CompareTo(other.GetSize());
			}
		}
	}
}
