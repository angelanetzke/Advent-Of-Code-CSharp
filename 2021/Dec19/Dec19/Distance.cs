using System;

namespace Dec19
{
	internal class Distance
	{
		private readonly int manhattan;
		private readonly int straightSquared;

		public Distance(Beacon beacon1, Beacon beacon2)
		{
			manhattan = Math.Abs(beacon1.GetX() - beacon2.GetX())
				+ Math.Abs(beacon1.GetY() - beacon2.GetY())
				+ Math.Abs(beacon1.GetZ() - beacon2.GetZ());
			straightSquared = (beacon1.GetX() - beacon2.GetX()) * (beacon1.GetX() - beacon2.GetX())
				+ (beacon1.GetY() - beacon2.GetY()) * (beacon1.GetY() - beacon2.GetY())
				+ (beacon1.GetZ() - beacon2.GetZ()) * (beacon1.GetZ() - beacon2.GetZ());
		}

		public override bool Equals(object obj)
		{
			return obj is Distance distance &&
						 manhattan == distance.manhattan &&
						 straightSquared == distance.straightSquared;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(manhattan, straightSquared);
		}
	}


}
