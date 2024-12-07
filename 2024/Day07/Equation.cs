namespace Day07;

internal class Equation
{
	private readonly long[] numbers;
	private readonly long total;
	private readonly Dictionary<string, long> resultCache = [];
	private static readonly char[] operatorOrder = ['+', '*', '|'];

	public Equation(string data)
	{
		string[] tokens = data.Split(": ");
		total = long.Parse(tokens[0]);
		numbers = tokens[1].Split(" ").Select(x => long.Parse(x)).ToArray();
	}

	public long Part1()
	{
		char[] operators = Enumerable.Repeat('+', numbers.Length - 1).ToArray();
		bool isSuccessful = true;
		while (isSuccessful)
		{
			long result = Evaluate(numbers.Length - 1, operators);
			if (result == total)
			{
				return total;
			}
			isSuccessful = IncrementOperators(operators);
		}
		return 0L;
	}

	public long Part2()
	{
		if (total < numbers.Sum())
		{
			return 0L;
		}
		char[] operators = Enumerable.Repeat('+', numbers.Length - 1).ToArray();
		bool isSuccessful = true;
		while (isSuccessful)
		{
			if (operators[^1] == '*' && total % numbers[^1] != 0)
			{
				isSuccessful = IncrementOperators2(operators);
				continue;
			}
			if (operators[^1] == '|' && total % 10 != numbers[^1] % 10)
			{
				isSuccessful = IncrementOperators2(operators);
				continue;
			}
			long result = Evaluate(numbers.Length - 1, operators);
			if (result == total)
			{
				return total;
			}
			isSuccessful = IncrementOperators2(operators);
		}
		return 0L;
	}

	private static bool IncrementOperators(char[] operators)
	{
		int carry = 1;
		int index = operators.Length - 1;
		while (carry == 1 && index >= 0)
		{
			carry = 0;
			operators[index] = operators[index] == '+' ? '*' : '+';
			if (operators[index] == '+')
			{
				carry = 1;
				index--;
			}		
		}
		return carry == 0;
	}

	private static bool IncrementOperators2(char[] operators)
	{
		int carry = 1;
		int index = operators.Length - 1;
		while (carry == 1 && index >= 0)
		{
			carry = 0;
			int currentOperator = Array.IndexOf(operatorOrder, operators[index]);
			operators[index] = operatorOrder[(currentOperator + 1) % operatorOrder.Length];
			if (operators[index] == '+')
			{
				carry = 1;
				index--;
			}		
		}
		return carry == 0;
	}

	private long Evaluate(int index, char[] operators)
	{
		string cacheKey = string.Join("", operators.Where((_, i) => i < index));
		if (resultCache.TryGetValue(cacheKey, out long cachedResult))
		{
			return cachedResult;
		}
		if (index == 0)
		{
			return numbers[0];
		}
		else
		{
			long previous = Evaluate(index - 1, operators);
			if (previous > total || previous == -1L)
			{
				resultCache[cacheKey] = -1L;
				return -1L;
			}
			if (operators[index - 1] == '+')
			{
				long result = previous + numbers[index];
				resultCache[cacheKey] = result;
				return result;
			}
			else if (operators[index - 1] == '*')
			{
				long result = previous * numbers[index];
				resultCache[cacheKey] = result;
				return result;
			}
			else
			{				
				long result = ConcatNumbers(previous, numbers[index]);
				resultCache[cacheKey] = result;
				return result;
			}
		}
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