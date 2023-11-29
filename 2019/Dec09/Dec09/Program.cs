using Dec09;

var inputLine = File.ReadAllText("input.txt");
Part1(inputLine);
Part2(inputLine);

static void Part1(string inputLine)
{
	var computer = new IntcodeComputer();
	computer.SetMemory(inputLine);
	computer.AddInput(1L);
	computer.Run();
	var keycode = computer.GetOutput()[0];
	Console.WriteLine($"Part 1: {keycode}");
}

static void Part2(string inputLine)
{
	var computer = new IntcodeComputer();
	computer.SetMemory(inputLine);
	computer.AddInput(2L);
	computer.Run();
	var keycode = computer.GetOutput()[0];
	Console.WriteLine($"Part 2: {keycode}");
}




