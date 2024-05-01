using Day19;
string input = File.ReadAllLines("input.txt")[0];
Console.WriteLine($"Part 1: {Part1(input)}");
Console.WriteLine($"Part 2: {Part2(input)}");

static int Part1(string input)
{
	ElfCircle circle = new(int.Parse(input));
	return circle.RunGame();
}

static int Part2(string input)
{
	ElfCircle circle = new(int.Parse(input));
	return circle.RunGame2();
}