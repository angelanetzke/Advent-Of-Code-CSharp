namespace Dec25;

internal class Region
{
	private HashSet<(int, int)> eastboundHerd = [];
	private HashSet<(int, int)> southboundHerd = [];
	private readonly int maxRow;
	private readonly int maxColumn;	

	public Region(string[] allLines)
	{
		maxRow = allLines.Length - 1;
		maxColumn = allLines[0].Length - 1;
		for (int row = 0; row < allLines.Length; row++)
		{
			for (int column = 0; column < allLines[row].Length; column++)
			{
				if (allLines[row][column] == '>')
				{
					eastboundHerd.Add((row, column));
				}
				if (allLines[row][column] == 'v')
				{
					southboundHerd.Add((row, column));
				}
			}
		}
	}

	public int CountSteps()
	{
		int steps = 1;
		while (Advance())
		{
			steps++;
		}
		return steps;
	}

	private bool Advance()
	{
		bool didAdvance = false;
		HashSet<(int, int)> nextEastbound = [];
		HashSet<(int, int)> nextSouthbound = [];
		foreach ((int, int) thisCucumber in eastboundHerd)
		{
			(int, int) moveTo = (thisCucumber.Item1, thisCucumber.Item2 + 1);
			if (moveTo.Item2 > maxColumn)
			{
				moveTo.Item2 = 0;
			}
			if (!eastboundHerd.Contains(moveTo) && !southboundHerd.Contains(moveTo))
			{
				didAdvance = true;
				nextEastbound.Add(moveTo);
			}
			else
			{
				nextEastbound.Add((thisCucumber.Item1, thisCucumber.Item2));
			}
		}
		foreach ((int, int) thisCucumber in southboundHerd)
		{
			(int, int) moveTo = (thisCucumber.Item1 + 1, thisCucumber.Item2);
			if (moveTo.Item1 > maxRow)
			{
				moveTo.Item1 = 0;
			}
			if (!nextEastbound.Contains(moveTo) && !southboundHerd.Contains(moveTo))
			{
				didAdvance = true;
				nextSouthbound.Add(moveTo);
			}
			else
			{
				nextSouthbound.Add((thisCucumber.Item1, thisCucumber.Item2));
			}
		}
		eastboundHerd = nextEastbound;
		southboundHerd = nextSouthbound;
		return didAdvance;
	}


}	
