using Day15;
using System.Diagnostics;

Console.WriteLine("Advent of Code 2024, Day 15");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result, out Warehouse w);
Console.WriteLine($"Part 1: {part1Result}");
Part2(w, out long part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string[] allLines, out long result, out Warehouse w)
{
	Stopwatch timer = new ();
	timer.Start();
	w = new ();
	int row = 0;
	while (row < allLines.Length)
	{
		if (allLines[row].Length == 0)
		{
			break;
		}
		for (int column = 0; column < allLines[0].Length; column++)
		{
			if (allLines[row][column] == '#')
			{
				w.AddWall(row, column);
			}
			else if (allLines[row][column] == 'O')
			{
				w.AddBox(row, column);
			}
			else if (allLines[row][column] == '@')
			{
				w.SetRobotStartLocation(row, column);
			}
		}
		row++;
	}
	row++;
	while (row < allLines.Length)
	{
		w.AddDirectionLine(allLines[row]);
		row++;
	}
	result = w.Part1();
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static void Part2(Warehouse w, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	result = w.Part2();
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}
