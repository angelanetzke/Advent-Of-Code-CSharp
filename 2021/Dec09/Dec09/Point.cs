using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dec09
{
	internal class Point
	{
		private readonly int row;
		private readonly int column;
		private readonly int height;
		public Point(int row, int column, int height)
		{
			this.row = row;
			this.column = column;
			this.height = height;
		}
		public int GetRow()
		{
			return row;
		}

		public int GetColumn()
		{
			return column;
		}

		public int getHeight()
		{
			return height;
		}
		public override bool Equals(object obj)
		{
			return obj is Point point &&
						 row == point.row &&
						 column == point.column &&
						 height == point.height;
		}
		public override int GetHashCode()
		{
			return HashCode.Combine(row, column, height);
		}
	}
}
