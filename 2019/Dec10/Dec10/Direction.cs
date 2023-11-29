namespace Dec10
{
	internal class Direction : IComparable<Direction>
	{
		private readonly int deltaX;
		private readonly int deltaY;
		private readonly double angle;

		public Direction(int deltaX, int deltaY)
		{
			this.deltaX = deltaX;
			this.deltaY = deltaY;
			double subAngle = Math.Atan(Math.Abs((double)this.deltaY / (double)this.deltaX));
			if (deltaY == 0 && deltaX < 0)
			{
				angle = Math.PI * 1.5;
			}
			else if (deltaY == 0 && deltaX > 0)
			{
				angle = Math.PI * 0.5;
			}
			else if (deltaY < 0 && deltaX == 0)
			{
				angle = 0.0;
			}
			else if (deltaY > 0 && deltaX == 0)
			{
				angle = Math.PI;
			}
			else if (deltaY < 0 && deltaX < 0)
			{
				angle = Math.PI * 1.5 + subAngle;
			}
			else if (deltaY < 0 && deltaX > 0)
			{
				angle = Math.PI * 0.5 - subAngle;
			}
			else if (deltaY > 0 && deltaX < 0)
			{
				angle = Math.PI * 1.5 - subAngle;
			}
			else if (deltaY > 0 && deltaX > 0)
			{
				angle = Math.PI * 0.5 + subAngle;
			}
			else
			{
				angle = 0.0;
			}
		}

		public int GetDeltaX()
		{
			return deltaX;
		}

		public int GetDeltaY()
		{
			return deltaY;
		}

		public int CompareTo(Direction? other)
		{
			if (other == null)
			{
				return 1;
			}
			else
			{
				return angle.CompareTo(other.angle);
			}
		}

		public override bool Equals(object? obj)
		{
			if (obj is Direction other)
			{
				return angle == other.angle;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return angle.GetHashCode();
		}

	}

}
