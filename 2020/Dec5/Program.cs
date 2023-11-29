using Dec5;

var allLines = System.IO.File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	int maxSeat = 0;
	foreach(string thisLine in allLines)
	{
		var thisSeat = new Seat(thisLine);
		maxSeat = Math.Max(maxSeat, thisSeat.GetSeatID());
	}
	Console.WriteLine($"Part 1: {maxSeat}");
}
static void Part2(string[] allLines)
{
	const int MAX_POSSIBLE = 127 * 8 + 7;
	int maxSeat = 0;
	int minSeat = MAX_POSSIBLE;
	bool[] isSeatTaken = new bool[MAX_POSSIBLE];
	foreach (string thisLine in allLines)
	{
		var thisSeat = new Seat(thisLine);
		int thisSeatID = thisSeat.GetSeatID();
		maxSeat = Math.Max(maxSeat, thisSeatID);
		minSeat = Math.Min(minSeat, thisSeatID);
		isSeatTaken[thisSeatID] = true;
	}
	int mySeatID = -1;
	for (int i = minSeat + 1; i <= maxSeat + 1; i++)
	{
		if (isSeatTaken[i - 1] && !isSeatTaken[i] && isSeatTaken[i + 1])
		{
			mySeatID = i;
			break;
		}
	}
	Console.WriteLine($"Part 1: {mySeatID}");
}
