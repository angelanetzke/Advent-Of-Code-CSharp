namespace Dec23;

internal class NAT
{
	private static (long, long)? lastReceivedValue = null;
	private static long? lastSentY = null;
	private static long part2Solution = -1L;

	public static void Receive(long x, long y)
	{
		lastReceivedValue = (x, y);
	}

	public static void Send()
	{
		IntcodeComputer.SendToGlobalOutput((0, lastReceivedValue!.Value.Item1, lastReceivedValue!.Value.Item2));
		if (lastReceivedValue!.Value.Item2 == lastSentY)
		{
			part2Solution = lastSentY.Value;
		}
		lastSentY = lastReceivedValue!.Value.Item2;
	}

	public static long GetPart2Solution()
	{
		return part2Solution;
	}

}