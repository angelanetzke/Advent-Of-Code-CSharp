using System.Text.RegularExpressions;

Console.WriteLine("Advent of Code 2024, Day 1");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result, out List<long> leftList, out List<long> rightList);
Console.WriteLine($"Part 1: {part1Result}");
Part2(allLines, out long part2Result, leftList, rightList);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string[] allLines, out long result, out List<long> leftList, out List<long> rightList)
{
	Regex locationsRegex = new(@"^(?<left>\d+) +(?<right>\d+)$");
	leftList = [];
	rightList = [];
	foreach (string thisLine in allLines)
	{
		Match m = locationsRegex.Match(thisLine);
		leftList.Add(long.Parse(m.Groups["left"].Value));
		rightList.Add(long.Parse(m.Groups["right"].Value));
	}
	leftList.Sort();
	rightList.Sort();
	long differenceSum = 0;
	for (int i = 0; i < leftList.Count; i++)
	{
		differenceSum += Math.Abs(leftList[i] - rightList[i]);
	}
	result = differenceSum;
}

static void Part2(string[] allLines, out long result, List<long> leftList, List<long> rightList)
{
	long similarityScore = 0L;
	foreach (long l in leftList)
	{
		similarityScore += l * rightList.Count(x => x == l);
	}
	result = similarityScore;
}
