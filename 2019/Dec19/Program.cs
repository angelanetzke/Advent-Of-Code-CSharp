using Dec19;

var inputLine = File.ReadAllLines("input.txt")[0];
var theComputer = new IntcodeComputer();
theComputer.SetMemory(inputLine);
Part1(theComputer);
Part2(theComputer);

static void Part1(IntcodeComputer theComputer)
{

	var pulledCount = 0;
	for (long x = 0; x <= 49; x++)
	{
		for (long y = 0; y <= 49; y++)
		{
			if (IsPulling(theComputer, x, y))
			{
				pulledCount++;
			}
		}
	}
	Console.WriteLine($"Part 1: {pulledCount}");
}

static void Part2(IntcodeComputer theComputer)
{
	var minX = 0L;
	var maxX = 1000L;
	var x = (minX + maxX) / 2L;
	var minY = GetMinY(theComputer, x + 99);
	var maxY = GetMaxY(theComputer, x);
	var length = maxY - minY + 1;
	do
	{
		if (length < 100)
		{
			minX = x;
			x = (minX + maxX) / 2L;
		}
		else if (length > 100)
		{
			maxX = x;
			x = (minX + maxX) / 2L;
		}
		else
		{
			break;
		}
		x = (minX + maxX) / 2L;
		minY = GetMinY(theComputer, x + 99);
		maxY = GetMaxY(theComputer, x);
		length = maxY - minY + 1;
	} while (maxX - minX > 10);
	var answerX = -1L;
	var answerY = -1L;
	for (long thisX = minX; thisX <= maxX; thisX++)
	{
		minY = GetMinY(theComputer, thisX + 99);
		maxY = GetMaxY(theComputer, thisX);
		length = maxY - minY + 1;
		if (length >= 100)
		{
			answerX = thisX;
			answerY = maxY - 99;
			break;
		}
	}
	var answer = 10000L * answerX + answerY;
	Console.WriteLine($"Part 2: {answer}");
}

static long GetMinY(IntcodeComputer theComputer, long x)
{
	var thisY = 0L;
	while (!IsPulling(theComputer, x, thisY))
	{
		thisY++;
	}
	return thisY;
}

static long GetMaxY(IntcodeComputer theComputer, long x)
{
	var thisY = 0L;
	while (!IsPulling(theComputer, x, thisY))
	{
		thisY++;
	}
	while (IsPulling(theComputer, x, thisY))
	{
		thisY++;
	}
	return thisY - 1L;
}

static bool IsPulling(IntcodeComputer theComputer, long x, long y)
{
	theComputer.AddInput(x);
	theComputer.AddInput(y);
	theComputer.Run();
	var output = theComputer.GetOutput()[0];
	theComputer.Reset();
	return output == 1;
}

