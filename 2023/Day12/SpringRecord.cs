namespace Day12;

internal class SpringRecord
{
	private readonly string pattern;
	private readonly int[] damagedSprings;
	private readonly Dictionary<(string, int), long> cache = [];

	public SpringRecord(string data, bool isExtended)
	{
		pattern = data.Split(' ')[0];
		damagedSprings = data.Split(' ')[1].Split(',').Select(x => int.Parse(x)).ToArray();
		if (isExtended)
		{
			pattern = string.Join('?', Enumerable.Repeat(pattern, 5));
			damagedSprings = Enumerable.Repeat(damagedSprings, 5).SelectMany(x => x).ToArray();
		}
	}

	public long GetPossibilityCount()
	{
		return GetPossibilityCount(pattern, 0);
	}

	private long GetPossibilityCount(string substring, int springIndex)
	{
		long result = 0;
		if (cache.ContainsKey((substring, springIndex)))
		{
			return cache[(substring, springIndex)];
		}
		if (springIndex >= damagedSprings.Length)
		{
			result = substring.Contains('#') ? 0 : 1;
			cache[(substring, springIndex)] = result;
			return result;
		}
		if (substring.Length == 0)
		{
			cache[(substring, springIndex)] = 0;
			return 0;
		}
		char nextChar = substring[0];
		switch(nextChar)
		{
			case '.':
				result = GetPossibilityCount(substring[1..], springIndex);;
				cache[(substring, springIndex)] = result;
				return result;
			case '#':
				if (substring.Length < damagedSprings[springIndex])
				{
					cache[(substring, springIndex)] = 0;
					return 0;
				}
				string groupString = substring[..damagedSprings[springIndex]];
				if (groupString.Contains('.'))
				{
					cache[(substring, springIndex)] = 0;
					return 0;
				}			
				if (substring.Length == groupString.Length)
				{
					result = springIndex == damagedSprings.Length - 1 ? 1 : 0;
					cache[(substring, springIndex)] = result;
					return result;
				}
				if (substring.Length >= groupString.Length + 1 && substring[groupString.Length] != '#')
				{
					result = GetPossibilityCount(substring[(groupString.Length + 1)..], springIndex + 1);
					cache[(substring, springIndex)] = result;
					return result;
				}
				cache[(substring, springIndex)] = 0;						
				return 0;
			case '?':
				string option1 = '.' + substring[1..];
				string option2 = '#' + substring[1..];
				result = GetPossibilityCount(option1,springIndex) + GetPossibilityCount(option2, springIndex);
				cache[(substring, springIndex)] = result;
				return result;
		}
		cache[(substring, 0)] = 0;
		return 0;
	}

}