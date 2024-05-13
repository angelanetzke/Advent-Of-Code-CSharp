string input = File.ReadAllLines("input.txt")[0];
Console.WriteLine($"Part 1: {Part1(input)}");
Console.WriteLine($"Part 2: {Part2(input)}");

static int Part1(string input)
{
	int target = int.Parse(input);
	int house = 1;
	while (true)
	{
		int sum = SumOfFactors(house) * 10;
		if (sum >= target)
		{
			return house;
		}
		house++;
	}
}

static int Part2(string input)
{
	int target = int.Parse(input);
	int[] houses = Enumerable.Repeat(0, target + 1).ToArray();
	for (int elf = 1; elf <= target; elf++)
	{
		for (int houseindex = elf; houseindex <= 50 * elf; houseindex += elf)
		{
			if (houseindex > target)
			{
				break;
			}
			houses[houseindex] += elf * 11;
		}
	}
	int i = 1;
	while (houses[i] < target)
	{
		i++;
	}
	return i;
}

static int SumOfFactors(int number)
{
	int sum = 0;
	for (int i = 1; i * i <= number; i++)
	{
		if (number % i == 0)
		{
			sum += i;
			if (i * i != number)
			{
				sum += number / i;
			}
		}
	}
	return sum;
}

