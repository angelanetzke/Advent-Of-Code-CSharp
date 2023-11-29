using Dec07;

var inputLine = File.ReadAllText("input.txt");
Part1(inputLine);
Part2(inputLine);

static void Part1(string inputLine)
{
	var permutations = File.ReadAllLines("permutations.txt");
	long maxOutput = long.MinValue;
	foreach (string thisPermutation in permutations)
	{
		var thisOutput = 0L;
		for (int i = 0; i < 5; i++)
		{
			var thisAmplifier = new IntcodeComputer();
			thisAmplifier.SetMemory(inputLine);
			thisAmplifier.AddInput(long.Parse(thisPermutation[i].ToString()));
			thisAmplifier.AddInput(thisOutput);
			thisAmplifier.Run();
			thisOutput = thisAmplifier.GetOutput()[0];
		}
		maxOutput = Math.Max(maxOutput, thisOutput);
	}
	Console.WriteLine($"Part 1: {maxOutput}");
}

static void Part2(string inputLine)
{
	var permutations = File.ReadAllLines("permutations5-9.txt");
	long maxOutput = long.MinValue;
	foreach (string thisPermutation in permutations)
	{
		var amplifiers = new IntcodeComputer[5];
		for (int i = 0; i < amplifiers.Length; i++)
		{
			amplifiers[i] = new IntcodeComputer();
			amplifiers[i].SetMemory(inputLine);
			amplifiers[i].AddInput(long.Parse(thisPermutation[i].ToString()));

		}
		var thisOutput = 0L;
		bool isComplete = false;
		var amplifierIndex = 0;
		while (!isComplete)
		{
			amplifiers[amplifierIndex].ClearOutput();
			amplifiers[amplifierIndex].AddInput(thisOutput);
			isComplete = IntcodeComputer.EXIT_REASON.HALT == amplifiers[amplifierIndex].Run();
			if (!isComplete)
			{
				thisOutput = amplifiers[amplifierIndex].GetOutput()[0];
				amplifierIndex = (amplifierIndex + 1) % amplifiers.Length;
			}			
		}
		maxOutput = Math.Max(maxOutput, thisOutput);
	}
	Console.WriteLine($"Part 2: {maxOutput}");
}