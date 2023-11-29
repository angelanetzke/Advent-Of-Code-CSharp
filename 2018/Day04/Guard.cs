namespace Day04
{
	internal class Guard
	{
		private readonly Dictionary<int, int> counts = new ();
		private int lastEvent = 0;		
		
		public void NewShift()
		{
			lastEvent = 0;
		}

		public void AddEvent(int eventTime, bool isAwake)
		{
			if (isAwake)
			{
				for (int thisMinute = lastEvent; thisMinute < eventTime; thisMinute++)
				{
					AddToCount(thisMinute);
				}
			}
			lastEvent = eventTime;
		}

		public void AddToCount(int minute)
		{
			if (counts.ContainsKey(minute))
			{
				counts[minute]++;
			}
			else
			{
				counts[minute] = 1;
			}
		}

		public (int, int) GetSleepStats()
		{
			if (counts.Keys.Count == 0)
			{
				return (0, 0);
			}
			var totalSleepTime = counts.Values.Sum();
			var mostCommonSleepMinute = counts.MaxBy(x => x.Value).Key;
			return (totalSleepTime, mostCommonSleepMinute);
		}

		public (int, int) GetSleepStats2()
		{
			if (counts.Keys.Count == 0)
			{
				return (0, 0);
			}
			var mostCommonSleepMinute = counts.MaxBy(x => x.Value).Key;
			var minuteSleepTime = counts[mostCommonSleepMinute];
			return (minuteSleepTime, mostCommonSleepMinute);
		}

	}
}