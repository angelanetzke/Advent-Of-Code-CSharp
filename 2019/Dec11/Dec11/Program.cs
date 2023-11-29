using Dec11;

var inputLine = File.ReadAllText("input.txt");
Part1(inputLine);
Part2(inputLine);

static void Part1(string inputLine)
{
	var theRobot = new Robot();
	//(x, y), color (0 = black, 1 = white)
	var tiles = new Dictionary<(int, int), int>();
	var theComputer = new IntcodeComputer();
	theComputer.SetMemory(inputLine);
	theComputer.AddInput(0L);
	int paintCount = 0;
	IntcodeComputer.EXIT_STATUS currentStatus;
	do
	{
		theComputer.ClearOutput();
		currentStatus = theComputer.Run();
		if (currentStatus == IntcodeComputer.EXIT_STATUS.PAUSE)
		{
			if (!tiles.ContainsKey(theRobot.GetPosition()))
			{
				paintCount++;
			}
			tiles[theRobot.GetPosition()] = (int)theComputer.GetOutput()[0];
			theRobot.Rotate((int)theComputer.GetOutput()[1]);
			theRobot.Move();
			if (tiles.ContainsKey(theRobot.GetPosition()))
			{
				theComputer.AddInput(tiles[theRobot.GetPosition()]);
			}
			else
			{
				theComputer.AddInput(0);
			}			
		}
	} while (currentStatus != IntcodeComputer.EXIT_STATUS.HALT);
	Console.WriteLine($"Part 1: {paintCount}");
}

static void Part2(string inputLine)
{
	var theRobot = new Robot();
	//(x, y), color (0 = black, 1 = white)
	var tiles = new Dictionary<(int, int), int>();
	var minX = int.MaxValue;
	var maxX = int.MinValue;
	int minY = int.MaxValue;
	int maxY = int.MinValue;
	var theComputer = new IntcodeComputer();
	theComputer.SetMemory(inputLine);
	theComputer.AddInput(1L);
	tiles[(0, 0)] = 1;
	IntcodeComputer.EXIT_STATUS currentStatus;
	do
	{
		theComputer.ClearOutput();
		currentStatus = theComputer.Run();
		if (currentStatus == IntcodeComputer.EXIT_STATUS.PAUSE)
		{
			tiles[theRobot.GetPosition()] = (int)theComputer.GetOutput()[0];
			minX = Math.Min(minX, theRobot.GetPosition().Item1);
			maxX = Math.Max(maxX, theRobot.GetPosition().Item1);
			minY = Math.Min(minY, theRobot.GetPosition().Item2);
			maxY = Math.Max(maxY, theRobot.GetPosition().Item2);
			theRobot.Rotate((int)theComputer.GetOutput()[1]);
			theRobot.Move();
			if (tiles.ContainsKey(theRobot.GetPosition()))
			{
				theComputer.AddInput(tiles[theRobot.GetPosition()]);
			}
			else
			{
				theComputer.AddInput(0);
			}
		}
	} while (currentStatus != IntcodeComputer.EXIT_STATUS.HALT);
	for (int y = maxY; y >= minY; y--)
	{
		for (int x = minX; x <= maxX; x++)
		{
			if (tiles.ContainsKey((x, y)))
			{
				if (tiles[(x, y)] == 1)
				{
					Console.Write("*");
				}
				else
				{
					Console.Write(" ");
				}
			}
			else
			{
				Console.Write(" ");
			}
		}
		Console.WriteLine();
	}
}