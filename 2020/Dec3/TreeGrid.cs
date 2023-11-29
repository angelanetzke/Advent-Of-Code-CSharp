using System.Collections.Generic;

namespace Dec3
{
	internal class TreeGrid
	{
		private readonly string[] rows;

		public TreeGrid(string[] rows)
		{
			this.rows = rows;
		}

		public bool IsTree(int x, int y)
		{
			if (0 <= x && 0 <= y && y <= rows.Length)
			{
				return rows[y][x % rows[0].Length] == '#';
			}
			else
			{
				return true;
			}
		}

		public long CountTrees(int xSlope, int ySlope)
		{
			int currentX = 0;
			int currentY = 0;
			long treeCount = 0L;
			while (currentY < rows.Length)
			{
				if (IsTree(currentX, currentY))
				{
					treeCount++;
				}
				currentX += xSlope;
				currentY += ySlope;
			}
			return treeCount;
		}
	}
}
