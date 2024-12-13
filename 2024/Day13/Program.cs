using Day13;
using System.Diagnostics;
using System.Text.RegularExpressions;

Console.WriteLine("Advent of Code 2024, Day 13");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result);
Console.WriteLine($"Part 1: {part1Result}");
Part2(allLines, out long part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	Regex valuesRegex = new (@"(?<a>\d+)[^\d]+(?<b>\d+)");
	long sum = 0L;
	for (int i = 0; i < allLines.Length; i+= 4)
	{
		Match m = valuesRegex.Match(allLines[i]);
		long a1 = long.Parse(m.Groups["a"].Value);
		long b1 = long.Parse(m.Groups["b"].Value);
		m = valuesRegex.Match(allLines[i + 1]);
		long a2 = long.Parse(m.Groups["a"].Value);
		long b2 = long.Parse(m.Groups["b"].Value);
		m = valuesRegex.Match(allLines[i + 2]);
		long a3 = long.Parse(m.Groups["a"].Value);
		long b3 = long.Parse(m.Groups["b"].Value);
		Matrix m1 = new ([a1, a2, b1, b2]);
		Matrix m2 = new ([a3, b3]);
		(long, long)? solution = Matrix.Solve(m1, m2);
		if (solution != null)
		{
			sum += 3 * solution.Value.Item1 + solution.Value.Item2;
		}
	}
	result = sum;
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static void Part2(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	Regex valuesRegex = new (@"(?<a>\d+)[^\d]+(?<b>\d+)");
	long adjustment = 10_000_000_000_000L;
	long sum = 0L;
	for (int i = 0; i < allLines.Length; i+= 4)
	{
		Match m = valuesRegex.Match(allLines[i]);
		long a1 = long.Parse(m.Groups["a"].Value);
		long b1 = long.Parse(m.Groups["b"].Value);
		m = valuesRegex.Match(allLines[i + 1]);
		long a2 = long.Parse(m.Groups["a"].Value);
		long b2 = long.Parse(m.Groups["b"].Value);
		m = valuesRegex.Match(allLines[i + 2]);
		long a3 = long.Parse(m.Groups["a"].Value) + adjustment;
		long b3 = long.Parse(m.Groups["b"].Value) + adjustment;
		Matrix m1 = new ([a1, a2, b1, b2]);
		Matrix m2 = new ([a3, b3]);
		(long, long)? solution = Matrix.Solve(m1, m2);
		if (solution != null)
		{
			sum += 3 * solution.Value.Item1 + solution.Value.Item2;
		}
	}
	result = sum;
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}
