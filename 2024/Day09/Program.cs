using System.Diagnostics;
using Day09;

Console.WriteLine("Advent of Code 2024, Day 9");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines[0], out long part1Result);
Console.WriteLine($"Part 1: {part1Result}");
Part2(allLines[0], out long part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string inputLine, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	int headFileNumber = 0;
	int tailFileNumber = inputLine.Length / 2;
	int headFileIndex = 0;
	int tailFileIndex = inputLine.Length - 1;
	long checkSum = 0L;
	bool isFile = true;
	int position = 0;
	int remainder = int.Parse(inputLine[tailFileIndex].ToString());
	while (headFileIndex < tailFileIndex)
	{
		if (isFile)
		{
			int fileSize = int.Parse(inputLine[headFileIndex].ToString());
			for (int i = 1; i <= fileSize; i++)
			{
				checkSum += position * headFileNumber;
				position++;
			}
			headFileNumber++;
		}
		else
		{
			int spaceAvailable = int.Parse(inputLine[headFileIndex].ToString());
			while (spaceAvailable > 0)
			{								
				checkSum += position * tailFileNumber;
				remainder--;
				position++;
				spaceAvailable--;
				if (remainder == 0)
				{
					tailFileIndex -= 2;
					tailFileNumber--;
					remainder = int.Parse(inputLine[tailFileIndex].ToString());
				}
			}
		}
		headFileIndex++;
		isFile = !isFile;
	}
	for (int i = 1; i <= remainder; i++)
	{
		checkSum += position * tailFileNumber;
		position++;
	}
	result = checkSum;
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static void Part2(string inputLine, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	DiskMap dm = new (inputLine);
	dm.Sort();
	result = dm.GetCheckSum();
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}
