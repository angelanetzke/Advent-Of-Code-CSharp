using Day18;

var allLines = File.ReadAllLines("input.txt");
SingleProgram.commands = allLines;
Part1();
Part2();

void Part1()
{
	var program = new SingleProgram(0);
	var result = program.Execute(true);
	Console.WriteLine($"Part 1: {result}");	
}

void Part2()
{
	var program0 = new SingleProgram(0);
	var program1 = new SingleProgram(1);
	program0.partner = program1;
	program1.partner = program0;
	int queue0Count;
	int queue1Count;
	var sendCount = -1L;
	do
	{
		program0.Execute(false);
		sendCount = program1.Execute(false);
		queue0Count = program0.GetQueueCount();
		queue1Count = program1.GetQueueCount();
	} while (queue0Count > 0 || queue1Count > 0);
	Console.WriteLine($"Part 2: {sendCount}");
}

