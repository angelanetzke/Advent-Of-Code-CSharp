namespace Day11;

internal class Reactor
{
	private readonly Dictionary<string, List<string>> devices = [];
	private readonly Dictionary<string, long> cache = [];

	public Reactor(string[] allLines)
	{
		foreach (string s in allLines)
		{
			string[] tokens = s.Split(": ");
			devices[tokens[0]] = [..tokens[1].Split(' ')];
		}
	}

	public long Part1()
	{
		return CountPaths("you", "out");
	}

	public long Part2()
	{
		long first = CountPaths("svr", "dac") * CountPaths("dac", "fft") * CountPaths("fft", "out");
		long second = CountPaths("svr", "fft") * CountPaths("fft", "dac") * CountPaths("dac", "out");
		return first + second;
	}

	private long CountPaths(string start, string end)
	{
		cache.Clear();
		cache[end] = 1L;
		return CountPathsRecursive(start);
	}

	private long CountPathsRecursive(string current)
	{
		if (cache.TryGetValue(current, out long cachedValue))
		{
			return cachedValue;
		}
		if (!devices.ContainsKey(current))
		{
			return 0L;
		}
		long total = 0L;
		foreach (string child in devices[current])
		{
			total += CountPathsRecursive(child);
		}
		cache[current] = total;
		return total;
	}

}