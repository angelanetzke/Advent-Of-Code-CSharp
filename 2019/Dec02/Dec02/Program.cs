using Dec02;

var inputLine = File.ReadAllText("input.txt");
Part1(inputLine);
Part2(inputLine);

static void Part1(string inputLine)
{
	var computer = new IntcodeComputer();
	computer.SetMemory(inputLine);
	computer.SetValue(1, 12L);
	computer.SetValue(2, 2L);
	computer.Run();
	var position0 = computer.GetValue(0);
	Console.WriteLine($"Part 1: {position0}");
}

static void Part2(string inputLine)
{
	var computer = new IntcodeComputer();
	bool answerFound = false;
	long answer = 0;
	for (int noun = 0; noun <= 99; noun++)
	{
		for (int verb = 0; verb <= 99; verb++)
		{
			computer.SetMemory(inputLine);
			computer.SetValue(1, noun);
			computer.SetValue(2, verb);
			computer.Run();
			if (computer.GetValue(0) == 19690720L)
			{
				answerFound = true;
				answer = 100 * noun + verb;
			}
		}
		if (answerFound)
		{
			break;
		}
	}
	Console.WriteLine($"Part 2: {answer}");	
}