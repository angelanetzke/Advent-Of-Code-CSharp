using System;
using System.Collections.Generic;

namespace Dec05
{
	internal class Segment
	{
		private readonly int x1;
		private readonly int y1;
		private readonly int x2;
		private readonly int y2;

		public Segment(int x1, int y1, int x2, int y2)
		{
			this.x1 = x1;
			this.y1 = y1;
			this.x2 = x2;
			this.y2 = y2;
		}

		public bool IsHorizontal()
		{
			return y1 == y2;
		}

		public bool IsVertical()
		{
			return x1 == x2;
		}

		public bool IsDiagonal()
		{
			return x1 != x2 && y1 != y2;
		}
		public List<Point> GetAllPoints()
		{
			Point thisPoint = new Point(x1, y1);
			Point endPoint = new Point(x2, y2);
			int deltaX = Math.Sign(x2 - x1);
			int deltaY = Math.Sign(y2 - y1);
			List<Point> result = new() { };
			bool complete = false;
			while(!complete)
			{
				result.Add(thisPoint);
				if (thisPoint.Equals(endPoint))
				{
					complete = true;
				}
				else
				{
					thisPoint = thisPoint.NextPoint(deltaX, deltaY);
				}
			}

			return result;
		}
		
		public override bool Equals(object obj)
		{
			if (obj is not Segment)
			{
				return false;
			}
			Segment other = (Segment)obj;
			if (x1 == other.x1 && y1 == other.y1 && x2 == other.x2 && y2 == other.y2)
			{
				return true;
			}
			if (x1 == other.x2 && y1 == other.y2 && x2 == other.x1 && y2 == other.y1)
			{
				return true;
			}
			return false;
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(x1 + x2, y1 + y2);
		}

		public override string ToString()
		{
			return x1 + "," + y1 + " -> " + x2 + "," + y2;
		}
	}
}
