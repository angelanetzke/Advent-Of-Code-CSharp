using Dec23;
using System.Diagnostics;

Console.WriteLine("Advent of Code 2019, Day 23");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result);
Console.WriteLine($"Part 1 answer: {part1Result}");
Part2(allLines, out long part2Result);
Console.WriteLine($"Part 2 answer: {part2Result}");

static void Part1(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	int computerCount = 50;
	IntcodeComputer[] network = new IntcodeComputer[computerCount];
	for (int i = 0; i < computerCount; i++)
	{
		network[i] = new IntcodeComputer();
		network[i].SetMemory(allLines[0]);
		network[i].SetID(i);
	}
	while (IntcodeComputer.GetPart1Solution() == -1)
	{
		foreach (IntcodeComputer c in network)
		{
			c.Run();
		}
	}
	result = IntcodeComputer.GetPart1Solution();
	timer.Stop();
	Console.WriteLine($"Part 1 time: {timer.ElapsedMilliseconds} ms");
}

static void Part2(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	int computerCount = 50;
	IntcodeComputer[] network = new IntcodeComputer[computerCount];
	for (int i = 0; i < computerCount; i++)
	{
		network[i] = new IntcodeComputer();
		network[i].SetMemory(allLines[0]);
		network[i].SetID(i);
	}
	while (NAT.GetPart2Solution() == -1)
	{
		bool areAllIdle = true;
		foreach (IntcodeComputer c in network)
		{
			c.Run();
			areAllIdle = areAllIdle && c.IsIdle();
		}
		if (areAllIdle)
		{
			NAT.Send();
		}
	}
	result = NAT.GetPart2Solution();
	timer.Stop();
	Console.WriteLine($"Part 2 time: {timer.ElapsedMilliseconds} ms");
}
