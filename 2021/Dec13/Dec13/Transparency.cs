using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dec13
{
	internal class Transparency
	{
		private HashSet<Location> dots;

		public Transparency()
		{
			dots = new();
		}

		public void AddDot(string dotString)
		{
			int thisX = int.Parse(dotString.Split(",")[0]);
			int thisY = int.Parse(dotString.Split(",")[1]);
			dots.Add(new Location(thisX, thisY));
		}

		public int CountDots()
		{
			return dots.Count;
		}

		public void FoldUp(int foldY)
		{
			HashSet<Location> newDots = new();
			foreach (Location thisLocation in dots)
			{
				int thisX = thisLocation.GetX();
				int thisY = thisLocation.GetY();
				int newY = thisY > foldY ? foldY - (thisY - foldY) : thisY;
				newDots.Add(new Location(thisX, newY));
			}
			dots = newDots;
		}

		public void FoldLeft(int foldX)
		{
			HashSet<Location> newDots = new();
			foreach (Location thisLocation in dots)
			{
				int thisX = thisLocation.GetX();
				int thisY = thisLocation.GetY();
				int newX = thisX > foldX ? foldX - (thisX - foldX) : thisX;
				newDots.Add(new Location(newX, thisY));
			}
			dots = newDots;
		}

		public override string ToString()
		{
			StringBuilder builder = new();
			int minX = dots.Min<Location>(element => element.GetX());
			int minY = dots.Min<Location>(element => element.GetY());
			int maxX = dots.Max<Location>(element => element.GetX());
			int maxY = dots.Max<Location>(element => element.GetY());
			for (int y = minY; y <= maxY; y++)
			{
				for (int x = minX; x <= maxX; x++)
				{
					if (dots.Contains(new Location(x, y)))
					{
						builder.Append('#');
					}
					else
					{
						builder.Append(' ');
					}
				}
				builder.Append('\n');
			}
			return builder.ToString();
		}

	}
}
