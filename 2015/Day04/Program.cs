using System.Security.Cryptography;
using System.Text;

string input = File.ReadAllLines("input.txt")[0];
Console.WriteLine($"Part 1: {Part1(input)}");
Console.WriteLine($"Part 2: {Part2(input)}");

static int Part1(string input)
{
	int value = -1;
	string hash;
	do
	{
		value++;
		hash = string.Join("", MD5.HashData(Encoding.ASCII.GetBytes(input + value))
			.Select(x => x.ToString("x2")));
	} while (!hash.StartsWith("00000"));
	return value;
}

static int Part2(string input)
{
	int value = -1;
	string hash;
	do
	{
		value++;
		hash = string.Join("", MD5.HashData(Encoding.ASCII.GetBytes(input + value))
			.Select(x => x.ToString("x2")));
	} while (!hash.StartsWith("000000"));
	return value;
}
