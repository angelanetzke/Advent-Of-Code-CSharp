using System.Text.RegularExpressions;

namespace Dec12
{
	internal class Moon
	{
		private (long, long, long) location;
		private (long, long, long) velocity;
		public Moon(string data)
		{
			var regex = new Regex(@"<x=(?<x>-{0,1}\d+), y=(?<y>-{0,1}\d+), z=(?<z>-{0,1}\d+)>");
			var matches = regex.Match(data);
			var locationX = long.Parse(matches.Groups["x"].Value);
			var locationY = long.Parse(matches.Groups["y"].Value);
			var locationZ = long.Parse(matches.Groups["z"].Value);
			location = (locationX, locationY, locationZ);
			velocity = (0L, 0L, 0L);
		}

		public void CalculateGravity(Moon other)
		{
			if (this != other)
			{
				if (location.Item1 < other.location.Item1)
				{
					velocity.Item1 += 1L;
				}
				else if (location.Item1 > other.location.Item1)
				{
					velocity.Item1 -= 1L;
				}
				if (location.Item2 < other.location.Item2)
				{
					velocity.Item2 += 1L;
				}
				else if (location.Item2 > other.location.Item2)
				{
					velocity.Item2 -= 1L;
				}
				if (location.Item3 < other.location.Item3)
				{
					velocity.Item3 += 1L;
				}
				else if (location.Item3 > other.location.Item3)
				{
					velocity.Item3 -= 1L;
				}
			}
		}

		public void ApplyGravity()
		{
			location.Item1 += velocity.Item1;
			location.Item2 += velocity.Item2;
			location.Item3 += velocity.Item3;
		}

		public long GetPotentialEnergy()
		{
			return Math.Abs(location.Item1) + Math.Abs(location.Item2) + Math.Abs(location.Item3);
		}

		public long GetKineticEnergy()
		{
			return Math.Abs(velocity.Item1) + Math.Abs(velocity.Item2) + Math.Abs(velocity.Item3);
		}

		public long GetTotalEnergy()
		{
			return GetPotentialEnergy() * GetKineticEnergy();
		}

		public (long, long, long) GetLocation()
		{
			return location;
		}

		public (long, long, long) GetVelocity()
		{
			return velocity;
		}


	}
}
