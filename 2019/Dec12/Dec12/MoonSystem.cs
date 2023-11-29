namespace Dec12
{
	internal class MoonSystem
	{
		private readonly List<(long, long, long)> startStates;
		private (long, long, long) periods;
		private readonly List<Moon> moons;
		private long step = 1L;

		public MoonSystem(List<Moon> moons)
		{
			startStates = new();
			foreach (Moon thisMoon in moons)
			{
				var startX = thisMoon.GetLocation().Item1;
				var startY = thisMoon.GetLocation().Item2;
				var startZ = thisMoon.GetLocation().Item3;
				startStates.Add((startX, startY, startZ));
			}
			periods = (0L, 0L, 0L);
			this.moons = new List<Moon>(moons);
		}

		public void NextStep()
		{
			foreach (Moon firstMoon in moons)
			{
				foreach (Moon secondMoon in moons)
				{
					firstMoon.CalculateGravity(secondMoon);
				}
			}
			foreach (Moon thisMoon in moons)
			{
				thisMoon.ApplyGravity();
			}
			int xMatches = 0;
			int yMatches = 0;
			int zMatches = 0;
			for (int i = 0; i < moons.Count; i++)
			{
				long thisX = moons[i].GetLocation().Item1;
				long thisY = moons[i].GetLocation().Item2;
				long thisZ = moons[i].GetLocation().Item3;
				long thisVelX = moons[i].GetVelocity().Item1;
				long thisVelY = moons[i].GetVelocity().Item2;
				long thisVelZ = moons[i].GetVelocity().Item3;
				if (thisX == startStates[i].Item1 && thisVelX == 0)
				{
					xMatches++;
				}
				if (thisY == startStates[i].Item2 && thisVelY == 0)
				{
					yMatches++;
				}
				if (thisZ == startStates[i].Item3 && thisVelZ == 0)
				{
					zMatches++;
				}
			}
			if (periods.Item1 == 0 && xMatches == moons.Count)
			{
				periods.Item1 = step;
			}
			if (periods.Item2 == 0 && yMatches == moons.Count)
			{
				periods.Item2 = step;
			}
			if (periods.Item3 == 0 && zMatches == moons.Count)
			{
				periods.Item3 = step;
			}
			step++;
		}

		public bool IsComplete()
		{
			if (periods.Item1 == 0)
			{
				return false;
			}
			if (periods.Item2 == 0)
			{
				return false;
			}
			if (periods.Item3 == 0)
			{
				return false;
			}
			return true;
		}

		public long GetLCM()
		{
			long lcm = GetLCM(periods.Item1, periods.Item2);
			lcm = GetLCM(lcm, periods.Item3);
			return lcm;
		}

		private static long GetLCM(long number1, long number2)
		{
			if (number1 == 0 || number2 == 0)
			{
				return 0;
			}
			long absNumber1 = Math.Abs(number1);
			long absNumber2 = Math.Abs(number2);
			long absHigherNumber = Math.Max(absNumber1, absNumber2);
			long absLowerNumber = Math.Min(absNumber1, absNumber2);
			long lcm = absHigherNumber;
			while (lcm % absLowerNumber != 0)
			{
				lcm += absHigherNumber;
			}
			return lcm;
		}

	}
}
