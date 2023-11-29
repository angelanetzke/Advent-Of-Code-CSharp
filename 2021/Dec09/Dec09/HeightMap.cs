using System.Collections.Generic;
using System.Linq;

namespace Dec09
{
	internal class HeightMap
	{
		private readonly List<List<int>> heights;
		private readonly List<Point> lowPoints;
		private readonly List<Basin> basins;

		public HeightMap()
		{
			heights = new();
			lowPoints = new();
			basins = new();
		}

		public void AddRow(string row)
		{
			List<int> newHeightRow = new();
			foreach (char c in row)
			{
				newHeightRow.Add(int.Parse(c.ToString()));
			}
			heights.Add(newHeightRow);
		}

		public int GetTotalRiskLevel()
		{
			for (int row = 0; row < heights.Count; row++)
			{
				for (int column = 0; column < heights[0].Count; column++)
				{
					List<Point> localArea = new();
					int thisHeight = heights[row][column];
					Point thisPoint = new(row, column, thisHeight);					
					localArea.Add(thisPoint);
					localArea.AddRange(GetNeighbors(thisPoint));
					if (thisHeight == localArea.Min(element => element.getHeight())
						&& localArea.Count(element => element.getHeight() == thisHeight) == 1)
					{
						lowPoints.Add(new Point(row, column, heights[row][column]));
					}
				}
			}
			int total = lowPoints.Sum(element => element.getHeight()) + lowPoints.Count;
			return total;
		}

		public long GetTopBasinProduct()
		{
			foreach (Point thisPoint in lowPoints)
			{
				FindBasin(thisPoint);
			}
			basins.Sort();
			basins.Reverse();
			long product = 1L;
			for (int i = 0; i < 3; i++)
			{
				product *= (long)basins[i].GetSize();
			}
			return product;
		}

		private void FindBasin(Point startPoint)
		{
			Basin thisBasin = new();
			Queue<Point> toVisit = new();
			List<Point> visited = new();
			toVisit.Enqueue(startPoint);
			while (toVisit.Count > 0)
			{
				Point thisPoint = toVisit.Dequeue();
				visited.Add(thisPoint);
				thisBasin.AddPoint(thisPoint);
				List<Point> neighbors = GetNeighbors(thisPoint);
				foreach (Point thisNeighbor in neighbors)
				{
					if (!visited.Contains(thisNeighbor)
						&& !toVisit.Contains(thisNeighbor)
						&& thisNeighbor.getHeight() < 9)
					{
						toVisit.Enqueue(thisNeighbor);

					}
				}
			}
			basins.Add(thisBasin);
		}
		private List<Point> GetNeighbors(Point p)
		{
			List<Point> neighbors = new();
			if (p.GetRow() > 0)
			{
				neighbors.Add(new Point(p.GetRow() - 1, p.GetColumn(), heights[p.GetRow() - 1][p.GetColumn()]));
			}
			if (p.GetRow() < heights.Count - 1)
			{
				neighbors.Add(new Point(p.GetRow() + 1, p.GetColumn(), heights[p.GetRow() + 1][p.GetColumn()]));
			}
			if (p.GetColumn() > 0)
			{
				neighbors.Add(new Point(p.GetRow(), p.GetColumn() - 1, heights[p.GetRow()][p.GetColumn() - 1]));
			}
			if (p.GetColumn() < heights[0].Count - 1)
			{
				neighbors.Add(new Point(p.GetRow(), p.GetColumn() + 1, heights[p.GetRow()][p.GetColumn() + 1]));
			}
			return neighbors;
		}




	}

}
