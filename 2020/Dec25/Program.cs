var allLines = System.IO.File.ReadAllLines("input.txt");
Part1(allLines);

static void Part1(string[] allLines)
{
	uint cardPublicKey = uint.Parse(allLines[0]);
	uint doorPublicKey = uint.Parse(allLines[1]);
	ulong testValue = 1;
	int cardLoopSize = 0;
	while (testValue != cardPublicKey)
	{
		cardLoopSize++;
		testValue *= 7;
		testValue %= 20201227;
	}
	ulong encrytionKey = 1;
	for (int i = 0; i < cardLoopSize; i++)
	{
		encrytionKey *= doorPublicKey;
		encrytionKey %= 20201227;
	}
	Console.WriteLine($"Part 1: {encrytionKey}");
	
}
