using Day19;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static int Part1(string[] allLines)
{
	int i = 0;
	string thisLine = allLines[0];
	while (thisLine.Length > 0)
	{
		Workflow newWorkflow = new (thisLine);
		i++;
		thisLine = allLines[i];
	}
	i++;
	int valueSum = 0;
	while (i < allLines.Length)
	{
		valueSum += Workflow.GetAcceptedValue(allLines[i]);
		i++;
	}
	return valueSum;
}

// Note: must run Part1 first to setup Worflow.workflowList
static long Part2(string[] allLines)
{
	return Workflow.GetAcceptableCount();
}
