var allLines = System.IO.File.ReadAllLines("input.txt");
var numbers = new long[allLines.Length];
for (int i = 0; i < allLines.Length; i++)
{
	numbers[i] = long.Parse(allLines[i]);
}
long invalidNumber = Part1(numbers);
Part2(invalidNumber, numbers);

static long Part1(long[] numbers)
{
	
	int checkIndex = 25;	
	while(checkIndex < numbers.Length)
	{
		bool isValid = false;
		for (int i = checkIndex - 25; i <= checkIndex - 2; i++)
		{
			for (int j = checkIndex - 24; j <= checkIndex - 1; j++)
			{
				if (numbers[i] + numbers[j] == numbers[checkIndex])
				{
					isValid = true;
					break;
				}
			}
			if (isValid)
			{
				break;
			}
		}
		if (isValid)
		{
			checkIndex++;
		}
		else
		{
			long invalidNumber = numbers[checkIndex];
			Console.WriteLine($"Part 1: {invalidNumber}");
			return invalidNumber;
		}		
	}
	return -1L;
}

static void Part2(long invalidNumber, long[] numbers)
{
	//keys are the index of the first number in the set and values are the sum for the current setSize
	var sum = new Dictionary<int, long>();
	for (int i = 0; i < numbers.Length; i++)
	{
		sum[i] = numbers[i];
	}
	int setSize = 2;
	while (setSize < numbers.Length)
	{
		for (int i = 0; i <= numbers.Length - setSize; i++)
		{
			long newSum = sum[i] + numbers[i + setSize - 1];
			if (newSum == invalidNumber)
			{
				var solutionSet = new List<long>();
				for (int j = i; j <= i + setSize - 1; j++)
				{
					solutionSet.Add(numbers[j]);
				}
				solutionSet.Sort();
				long answer = solutionSet[0] + solutionSet[setSize - 1];
				Console.WriteLine($"Part 2: {answer}");
				return;
			}
			else
			{
				sum[i] = newSum;
			}
		}
		setSize++;
	}
}