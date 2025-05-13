using Dec21;
using System.Diagnostics;

Console.WriteLine("Advent of Code 2019, Day 21");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result);
Console.WriteLine($"Part 1 answer: {part1Result}");
Part2(allLines, out long part2Result);
Console.WriteLine($"Part 2 answer: {part2Result}");

static void Part1(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	result = RunProgram("walkprogram.txt", allLines);
	timer.Stop();
	Console.WriteLine($"Part 1 time: {timer.ElapsedMilliseconds} ms");
}

static void Part2(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	result = RunProgram("runprogram.txt", allLines);
	timer.Stop();
	Console.WriteLine($"Part 2 time: {timer.ElapsedMilliseconds} ms");
}

static long RunProgram(string programName, string[] allLines)
{
	long result = -1L;
	string input = allLines[0];
	string[] program = File.ReadAllLines(programName);
	IntcodeComputer c = new ();
	c.SetMemory(input);
	foreach  (string thisProgramLine in program)
	{
		foreach (char thisChar in thisProgramLine)
		{
			c.AddInput(thisChar);
		}
		c.AddInput(10);
	}
	c.Run();
	List<long> output = c.GetOutput();
	Console.WriteLine($"output count: {output.Count}");
	foreach (long thisOutputValue in output)
	{
		if (thisOutputValue >= 256)
		{
			result = thisOutputValue;
			Console.Write(thisOutputValue);
		}
		else
		{
			Console.Write((char)thisOutputValue);
		}		
	}
	Console.WriteLine();
	return result;
}



