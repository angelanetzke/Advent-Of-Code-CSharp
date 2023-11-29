using Day16;

var allLines = File.ReadAllLines("input.txt");
Part1And2(allLines);

static void Part1And2(string[] allLines)
{	
	var isLastLineBlank = true;
	Device theDevice = new Device();
	var nextData = new List<string>();
	var sampleEndLine = -1;
	for (int i = 0; i < allLines.Length; i++)
	{
		var thisLine = allLines[i];
		if  (thisLine.Length == 0)
		{
			if (isLastLineBlank)
			{
				sampleEndLine = i;
				break;
			}
			theDevice.AddSample(nextData);
			nextData.Clear();
			isLastLineBlank = true;
		}
		else
		{
			nextData.Add(thisLine);
			isLastLineBlank = false;
		}
	}
	var atLeastThreeCount = theDevice.CountAtLeastThree();
	Console.WriteLine($"Part 1: {atLeastThreeCount}");
	var program = new List<string>();
	for (int i = sampleEndLine; i < allLines.Length; i++)
	{
		if (allLines[i].Length > 0)
		{
			program.Add(allLines[i]);
		}
	}
	var result = theDevice.Run(program);
	Console.WriteLine($"Part 2: {result}");
}