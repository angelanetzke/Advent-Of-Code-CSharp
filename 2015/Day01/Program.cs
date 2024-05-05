string input = File.ReadAllLines("input.txt")[0];
Console.WriteLine($"Part 1: {Part1(input)}");
Console.WriteLine($"Part 2: {Part2(input)}");

static int Part1(string input)
{
	int floor = 0;
	foreach (char thisChar in input)
	{
		if (thisChar == '(')
		{
			floor++;
		}
		else if (thisChar == ')')
		{
			floor--;
		}
	}
	return floor;
}

static int Part2(string input)
{
	int floor = 0;
	for (int i = 0; i < input.Length; i++)
	{
		if (input[i] == '(')
		{
			floor++;
		}
		else if (input[i] == ')')
		{
			floor--;
		}
		if (floor == -1)
		{
			return i + 1;
		}
	}
	return -1;
}