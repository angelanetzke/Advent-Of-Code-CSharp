namespace Day07;

internal class Equation
{
	private readonly long[] numbers;
	private readonly long total;

	public Equation(string data)
	{
		string[] tokens = data.Split(": ");
		total = long.Parse(tokens[0]);
		numbers = tokens[1].Split(" ").Select(x => long.Parse(x)).ToArray();
	}

	public long Part1()
	{
		if (Evaluate(numbers[0], 1, true) == total)
		{
			return total;
		}
		return 0L;
	}

	public long Part2()
	{
		if (Evaluate(numbers[0], 1, false) == total)
		{
			return total;
		}
		return 0L;
	}

	private long Evaluate(long runningTotal, int index, bool isPart1)
	{
		if (index == numbers.Length - 1)
		{
			if (runningTotal + numbers[index] == total)
			{
				return total;
			}
			if (runningTotal * numbers[index] == total)
			{
				return total;
			}
			if (!isPart1 && ConcatNumbers(runningTotal, numbers[index]) == total)
			{
				return total;
			}
			return -1L;
		}
		if (Evaluate(runningTotal + numbers[index], index + 1, isPart1) == total)
		{
			return total;
		}
		if (Evaluate(runningTotal * numbers[index], index + 1, isPart1) == total)
		{
			return total;
		}
		if (!isPart1 && Evaluate(ConcatNumbers(runningTotal, numbers[index]), index + 1, isPart1) == total)
		{
			return total;
		}
		return -1L;
	}

	private static long ConcatNumbers(long l1, long l2)
	{
		long result = l1;
		for (long i = l2; i > 0; i /= 10)
		{
			result *= 10;
		}
		return result + l2;
	}

}