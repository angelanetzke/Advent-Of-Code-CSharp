using Dec13;

var allText = File.ReadAllText("input.txt");
Part1(allText);
Part2(allText);

static void Part1(string allText)
{
	var theComputer = new IntcodeComputer();
	theComputer.SetMemory(allText);
	theComputer.Run();
	var theOutput = theComputer.GetOutput();
	var blockCount = 0;
	for (int i = 2; i < theOutput.Count; i += 3)
	{
		if (theOutput[i] == 2L)
		{
			blockCount++;
		}
	}
	Console.WriteLine($"Part 1: {blockCount}");
}

static void Part2(string allText)
{
	var blocks = new HashSet<(long, long)>();
	var theComputer = new IntcodeComputer();
	allText = "2" + allText[1..];
	theComputer.SetMemory(allText);
	var theOutput = new List<long>();
	var paddleX = -1L;
	var ballX = -1L;
	var score = -1L;
	do
	{
		theComputer.ClearOutput();
		theComputer.Run();
		theOutput = theComputer.GetOutput();
		for (int i = 0; i < theOutput.Count; i += 3)
		{
			if (theOutput[i + 2] == 0L)
			{
				blocks.Remove((theOutput[i], theOutput[i + 1]));
			}
			if (theOutput[i + 2] == 2L)
			{
				blocks.Add((theOutput[i], theOutput[i + 1]));
			}
			if (theOutput[i + 2] == 3L)
			{
				paddleX = theOutput[i];
			}
			if (theOutput[i + 2] == 4L)
			{
				ballX = theOutput[i];
			}
			if (theOutput[i] == -1 && theOutput[i + 1] == 0)
			{
				score = theOutput[i + 2];
			}
		}
		var paddleDirection = ballX < paddleX ? -1L : ballX > paddleX ? 1L : 0L;
		theComputer.AddInput(paddleDirection);
	} while (blocks.Count > 0);
	Console.WriteLine($"Part 2: {score}");
}
