using Dec05;

var inputLine = File.ReadAllText("input.txt");
var computer = new IntcodeComputer();
Part1(computer, inputLine);
Part2(computer, inputLine);

static void Part1(IntcodeComputer computer, string inputLine)
{
	computer.SetMemory(inputLine);
	computer.SetInput(new long[] { 1 });
	computer.Run();
	long diagnosticCode = computer.GetOutput().Last();
	Console.WriteLine($"Part 1: {diagnosticCode}");	
}

static void Part2(IntcodeComputer computer, string inputLine)
{
	computer.ClearOutput();
	computer.SetMemory(inputLine);
	computer.SetInput(new long[] { 5 });
	computer.Run();
	long diagnosticCode = computer.GetOutput().Last();
	Console.WriteLine($"Part 2: {diagnosticCode}");
}