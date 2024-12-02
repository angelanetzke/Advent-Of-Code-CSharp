Console.WriteLine("Advent of Code 2024, Day 2");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result, out List<string> unsafeReportList);
Console.WriteLine($"Part 1: {part1Result}");
Part2(unsafeReportList, out long part2Result);
Console.WriteLine($"Part 2: {part1Result + part2Result}");

static void Part1(string[] allLines, out long result, out List<string> unsafeReportList)
{
	long safeCount = 0L;
	unsafeReportList = [];
	foreach (string thisLine in allLines)
	{
		bool isSafe = IsSafe(thisLine, -1);
		if (isSafe)
		{
			safeCount++;
		}
		else
		{
			unsafeReportList.Add(thisLine);
		}
	}
	result = safeCount;
}

static void Part2(List<string> unsafeReportList, out long result)
{
	long safeCount = 0L;
	foreach (string thisReport in unsafeReportList)
	{
		bool isSafe = false;
		for (int i = 0; i < thisReport.Length; i++)
		{
			isSafe = isSafe || IsSafe(thisReport, i);
			if (isSafe)
			{
				break;
			}
		}
		safeCount += isSafe ? 1 : 0;
	}
	result = safeCount;
}

static bool IsSafe(string report, int skip)
{
	int[] levels;
	levels = report.Split(" ").Select(x => int.Parse(x)).ToArray();
	if (skip >= 0)
	{
		levels = levels.Where((value, index) => index != skip).ToArray();
	}
	int direction = 0;
	for (int i = 1; i < levels.Length; i++)
	{
		int delta = levels[i - 1] - levels[i];
		if (delta == 0)
		{
			return false;
		}
		if (direction == 0)
		{
			direction = delta / Math.Abs(delta);
		}
		if (delta / Math.Abs(delta) != direction)
		{
			return false;
		}
		if (Math.Abs(delta) < 1 || Math.Abs(delta) > 3)
		{
			return false;
		}
	}
	return true;
}
