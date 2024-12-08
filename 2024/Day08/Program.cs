using System.Diagnostics;

Console.WriteLine("Advent of Code 2024, Day 8");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result);
Console.WriteLine($"Part 1: {part1Result}");
Part2(allLines, out long part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	List<(int, int, int)> antennae = [];
	for (int r = 0; r < allLines.Length; r++)
	{
		for (int c = 0; c < allLines[0].Length; c++)
		{
			if (allLines[r][c] != '.')
			{
				antennae.Add((allLines[r][c], r, c));
			}
		}
	}
	char[] frequencies = new char[10 + 26 + 26];
	char[] temp = Enumerable.Range('0', 10).Select(x => (char)x).ToArray();
	Array.Copy(temp, frequencies, temp.Length);	
	temp = Enumerable.Range('a', 26).Select(x => (char)x).ToArray();
	Array.Copy(temp, 0, frequencies, 10, temp.Length);
	temp = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();
	Array.Copy(temp, 0, frequencies, 10 + 26, temp.Length);
	HashSet<(int, int)> antinodes = [];
	foreach (char thisFrequency in frequencies)
	{
		List<(int, int, int)> antennaeAtThisFrequency = antennae.Where(x => x.Item1 == thisFrequency).ToList();
		for (int i = 0; i < antennaeAtThisFrequency.Count - 1; i++)
		{
			for (int j = i + 1; j < antennaeAtThisFrequency.Count; j++)
			{
				int rowDelta = antennaeAtThisFrequency[j].Item2 - antennaeAtThisFrequency[i].Item2;
				int columnDelta = antennaeAtThisFrequency[j].Item3 - antennaeAtThisFrequency[i].Item3;
				int antinodeRow = antennaeAtThisFrequency[i].Item2 - rowDelta;
				int antinodeColumn = antennaeAtThisFrequency[i].Item3 - columnDelta;
				if (0 <= antinodeRow && antinodeRow < allLines.Length
					&& 0 <= antinodeColumn && antinodeColumn < allLines[0].Length)
				{
					antinodes.Add((antinodeRow, antinodeColumn));	
				}				
				antinodeRow = antennaeAtThisFrequency[j].Item2 + rowDelta;
				antinodeColumn = antennaeAtThisFrequency[j].Item3 + columnDelta;
				if (0 <= antinodeRow && antinodeRow < allLines.Length
					&& 0 <= antinodeColumn && antinodeColumn < allLines[0].Length)
				{
					antinodes.Add((antinodeRow, antinodeColumn));	
				}
			}
		}
	}
	result = antinodes.Count;
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}

static void Part2(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	List<(int, int, int)> antennae = [];
	for (int r = 0; r < allLines.Length; r++)
	{
		for (int c = 0; c < allLines[0].Length; c++)
		{
			if (allLines[r][c] != '.')
			{
				antennae.Add((allLines[r][c], r, c));
			}
		}
	}
	char[] frequencies = new char[10 + 26 + 26];
	char[] temp = Enumerable.Range('0', 10).Select(x => (char)x).ToArray();
	Array.Copy(temp, frequencies, temp.Length);	
	temp = Enumerable.Range('a', 26).Select(x => (char)x).ToArray();
	Array.Copy(temp, 0, frequencies, 10, temp.Length);
	temp = Enumerable.Range('A', 26).Select(x => (char)x).ToArray();
	Array.Copy(temp, 0, frequencies, 10 + 26, temp.Length);
	HashSet<(int, int)> antinodes = [];
	foreach (char thisFrequency in frequencies)
	{
		List<(int, int, int)> antennaeAtThisFrequency = antennae.Where(x => x.Item1 == thisFrequency).ToList();
		for (int i = 0; i < antennaeAtThisFrequency.Count - 1; i++)
		{
			for (int j = i + 1; j < antennaeAtThisFrequency.Count; j++)
			{
				int antinodeRow = antennaeAtThisFrequency[i].Item2;
				int antinodeColumn = antennaeAtThisFrequency[i].Item3;
				int rowDelta = antennaeAtThisFrequency[j].Item2 - antennaeAtThisFrequency[i].Item2;
				int columnDelta = antennaeAtThisFrequency[j].Item3 - antennaeAtThisFrequency[i].Item3;		
				while (0 <= antinodeRow - rowDelta && antinodeRow - rowDelta < allLines.Length
					&& 0 <= antinodeColumn - columnDelta && antinodeColumn - columnDelta < allLines[0].Length)
				{
					antinodeRow -= rowDelta;
					antinodeColumn -= columnDelta;	
				}
				while (0 <= antinodeRow && antinodeRow < allLines.Length
					&& 0 <= antinodeColumn && antinodeColumn < allLines[0].Length)
				{
					antinodes.Add((antinodeRow, antinodeColumn));	
					antinodeRow += rowDelta;
					antinodeColumn += columnDelta;
				}
			}
		}
	}
	result = antinodes.Count;
	timer.Stop();
	Console.WriteLine(timer.Elapsed);
}
