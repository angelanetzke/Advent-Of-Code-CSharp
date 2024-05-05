string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static int Part1(string[] allLines)
{
	int totalWrappingPaper = 0;
	List<int> dimensions = [];
	foreach (string thisLine in allLines)
	{
		dimensions = thisLine.Split('x').Select(int.Parse).ToList();
		dimensions.Sort();
		totalWrappingPaper += 3 * dimensions[0] * dimensions[1]
			+ 2 * dimensions[1] * dimensions[2] + 2 * dimensions[0] * dimensions[2];
	}
	return totalWrappingPaper;
}

static int Part2(string[] allLines)
{
	int totalRibbon = 0;
	List<int> dimensions = [];
	foreach (string thisLine in allLines)
	{
		dimensions = thisLine.Split('x').Select(int.Parse).ToList();
		dimensions.Sort();
		totalRibbon += 2 * dimensions[0] + 2 * dimensions[1]
			+ dimensions[0] * dimensions[1] * dimensions[2];
	}
	return totalRibbon;
}
