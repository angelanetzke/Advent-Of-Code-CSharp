using Day21;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static string Part1(string[] allLines)
{
	Password password = new("abcdefgh");
	return password.Scramble(allLines);
}

static string Part2(string[] allLines)
{
	Password password = new("fbgdceah");
	return password.Unscramble(allLines);
}