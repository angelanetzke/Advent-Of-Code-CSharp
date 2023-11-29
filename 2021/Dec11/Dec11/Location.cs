using System;

namespace Dec11
{
	internal class Location
	{
		public readonly int row;
		public readonly int column;

		public Location(int row, int column)
		{
			this.row = row;
			this.column = column;
		}

		public override bool Equals(object obj)
		{
			return obj is Location location &&
						 row == location.row &&
						 column == location.column;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(row, column);
		}
	}
}
