using Day11;

string input = File.ReadAllLines("input.txt")[0];
(string, string) result = Part1AndPart2(input);
Console.WriteLine($"Part 1: {result.Item1}");
Console.WriteLine($"Part 2: {result.Item2}");

static (string, string) Part1AndPart2(string input)
{
	(string, string) result = ("", "");
	Password nextPassword = new (input);
	while (!nextPassword.IsValid())
	{
		nextPassword.Increment();
	}
	result.Item1 = nextPassword.ToString();
	do
	{
		nextPassword.Increment();
	} while (!nextPassword.IsValid());
	result.Item2 = nextPassword.ToString();
	return result;
}