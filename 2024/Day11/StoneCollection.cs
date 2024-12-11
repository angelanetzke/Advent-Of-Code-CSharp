namespace Day11;

internal class StoneCollection (string data)
{
	private readonly List<long> stones = data.Split(' ')
		.Select(x => long.Parse(x))
		.ToList();
	private readonly Dictionary<(long, int), long> cache = [];
	
	public long Part1()
	{
		long count = 0L;
		stones.ForEach(x => count += 1L + CountStones(x, 25));
		return count;
	}

	public long Part2()
	{
		long count = 0L;
		stones.ForEach(x => count += 1L + CountStones(x, 75));
		return count;
	}

	private long CountStones(long start, int blinkCount)
	{
		if (cache.TryGetValue((start, blinkCount), out long cachedResult))
		{
			return cachedResult;
		}
		List<long> split = SplitDigits(start);
		if (blinkCount == 1)
		{			
			if (split.Count == 2)
			{
				cache[(start, blinkCount)] = 1L;
				return 1;
			}
			cache[(start, blinkCount)] = 0L;
			return 0;
		}
		if (start == 0)
		{
			long result = CountStones(1L, blinkCount - 1);
			cache[(start, blinkCount)] = result;
			return result;
		}
		else if (split.Count == 2)
		{
			long result = 1L + CountStones(split[0], blinkCount - 1) + CountStones(split[1], blinkCount - 1);
			cache[(start, blinkCount)] = result;
			return result;
		}
		else
		{
			long result = CountStones(start * 2024, blinkCount - 1);
			cache[(start, blinkCount)] = result;
			return result;
		}
	}

	private static List<long> SplitDigits(long l)
	{
		List<long> result = [];
		string s = l.ToString();
		if (s.Length % 2 == 1)
		{
			return result;
		}
		int middle = s.Length / 2;
		string firstHalf = s[..middle];
    string secondHalf = s[middle..];
		result.Add(long.Parse(firstHalf));
		result.Add(long.Parse(secondHalf));
		return result;
	}


}