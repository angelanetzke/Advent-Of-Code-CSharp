var allLines = System.IO.File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	int startTime = int.Parse(allLines[0]);
	var buses = allLines[1].Split(',');
	int earliestBusNumber = -1;
	int earliestBusTime = int.MaxValue;
	foreach (string thisBusString in buses)
	{
		if (thisBusString != "x")
		{
			int thisBusNumber = int.Parse(thisBusString);
			if (startTime % thisBusNumber == 0)
			{
				earliestBusNumber = thisBusNumber;
				earliestBusTime = startTime;
				break;
			}
			else
			{
				int thisNextBusTime = thisBusNumber * (startTime / thisBusNumber + 1);
				if (thisNextBusTime < earliestBusTime)
				{
					earliestBusNumber = thisBusNumber;
					earliestBusTime = thisNextBusTime;
				}
			}
		}
	}
	int part1Answer = earliestBusNumber * (earliestBusTime - startTime);
	Console.WriteLine($"Part 1: {part1Answer}");
}

static void Part2(string[] allLines)
{
	var buses = allLines[1].Split(',');
	var divisors = new List<long>();
	var remainders = new List<long>();
	for (int i = 0; i < buses.Length; i++)
	{
		if (buses[i] != "x")
		{
			divisors.Add(long.Parse(buses[i]));
			remainders.Add(long.Parse(buses[i]) - i);
		}
	}
	long part2Answer = ChineseRemainderTheorem(divisors, remainders);
	Console.WriteLine($"Part 2: {part2Answer}");
}

static long ChineseRemainderTheorem(List<long> divisors, List<long> remainders)
{
	long product = divisors.Aggregate((x1, x2) => x1 * x2);
	var n = new long[divisors.Count];
	for (int i = 0; i < n.Length; i++)
	{
		n[i] = product / divisors[i];
	}
	var x = new long[n.Length];
	for (int i = 0; i < x.Length; i++)
	{
		x[i] = GetInverse(n[i], divisors[i]);
	}
	long sum = 0;
	for (int i = 0; i < remainders.Count; i++)
	{
		sum += remainders[i] * n[i] * x[i];
	}
	return sum % product;
}

static long GetInverse(long n, long divisor)
{
	long factor = 1;
	long checkValue = factor * n % divisor;
	while (checkValue != 1)
	{
		factor++;
		checkValue = factor * n % divisor;
	}
	return factor;
}
