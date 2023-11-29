var allLines = System.IO.File.ReadAllLines("input.txt");
var numbers = new List<int>();
foreach (string thisLine in allLines)
{
	numbers.Add(int.Parse(thisLine));
}
Part1(numbers);
Part2(numbers);

static void Part1(List<int> numbers)
{	
	numbers.Sort();
	int jolt1DifferenceCount = 0;
	//Start at 1 for device.
	int jolt3DifferenceCount = 1;
	//Count difference between outlet and first adapter.
	if (numbers[0] == 1)
	{
		jolt1DifferenceCount++;
	}
	if (numbers[0] == 3)
	{
		jolt3DifferenceCount++;
	}
	for (int i = 1; i < numbers.Count; i++)
	{
		if (numbers[i] - numbers[i - 1] == 1)
		{
			jolt1DifferenceCount++;
		}
		if (numbers[i] - numbers[i - 1] == 3)
		{
			jolt3DifferenceCount++;
		}
	}
	int answer = jolt1DifferenceCount * jolt3DifferenceCount;
	Console.WriteLine($"Part 1: {answer}");
}

static void Part2(List<int> numbers)
{
	numbers.Sort();
	//Add device.
	numbers.Add(numbers[numbers.Count - 1] + 3);
	//Add outlet
	numbers.Add(0);
	numbers.Sort();
	var cache = new Dictionary<int, long>();
	long answer = CountConnections(0, numbers, cache);
	Console.WriteLine($"Part 2: {answer}");
}

static long CountConnections(int index, List<int> numbers, Dictionary<int, long> cache)
{
	if (index == numbers.Count - 1)
	{
		return 1L;
	}
	else
	{
		if (cache.TryGetValue(index, out long connectionCount))
		{
			return connectionCount;
		}
		else
		{
			long sum = 0L;
			for (int i = 1; i <= 3; i++)
			{
				if (index + i < numbers.Count && numbers[index + i] - numbers[index] <= 3)
				{
					sum += CountConnections(index + i, numbers, cache);
				}
			}
			cache[index] = sum;
			return sum;
		}
	}
}
