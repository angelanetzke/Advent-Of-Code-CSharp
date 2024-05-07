using System.Text;

Console.WriteLine("q");
string input = File.ReadAllLines("input.txt")[0];
Console.WriteLine($"Part 1: {Part1(input)}");
Console.WriteLine($"Part 2: {Part2(input)}");

static int Part1(string input)
{
	for (int i = 1; i <= 40; i++)
	{
		input = GetNextSequence(input);
	}
	return input.Length;
}

static int Part2(string input)
{
	for (int i = 1; i <= 50; i++)
	{
		input = GetNextSequence(input);
	}
	return input.Length;
}

static string GetNextSequence(string input)
{
	StringBuilder builder = new ();
	char currentChar = input[0];
	int currentCount = 0;
	for (int i = 0; i < input.Length; i++)
	{
		if (input[i] == currentChar)
		{
			currentCount++;
		}
		else
		{
			builder.Append(currentCount);
			builder.Append(currentChar);
			currentChar = input[i];
			currentCount = 1;
		}
	}
	builder.Append(currentCount);
	builder.Append(currentChar);
	return builder.ToString();
}