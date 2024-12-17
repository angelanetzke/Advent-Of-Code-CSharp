namespace Day15;

internal class Warehouse
{
	private readonly List<string> directions = [];
	private readonly HashSet<(int, int)> wallsStart = [];
	private HashSet<(int, int)> wallsCurrent = [];
	private readonly HashSet<(int, int)> boxesStart = [];
	private HashSet<(int, int)> boxesCurrent = [];
	private int robotStartRow = -1;
	private int robotStartColumn = -1;

	private int robotCurrentRow = -1;
	private int robotCurrentColumn = -1;
	private static readonly Dictionary<char, (int, int)> deltas = new ()
	{
		['^'] = (-1, 0),
		['v'] = (1, 0),
		['<'] = (0, -1),
		['>'] = (0, 1)
	};

	public void AddDirectionLine(string directionLine)
	{
		directions.Add(directionLine);
	}
	public void AddWall(int row, int column)
	{
		wallsStart.Add((row, column));
	}

	public void AddBox(int row, int column)
	{
		boxesStart.Add((row, column));
	}

	public void SetRobotStartLocation(int row, int column)
	{
		robotStartRow = row;
		robotStartColumn = column;
	}

	public long Part1()
	{
		robotCurrentRow = robotStartRow;
		robotCurrentColumn = robotStartColumn;
		boxesCurrent = [.. boxesStart];
		wallsCurrent = [..wallsStart];
		foreach (string d in directions)
		{
			foreach (char c in d)
			{
				(int, int) thisDelta = deltas[c];
				(int, int) nextLocation = (robotCurrentRow + thisDelta.Item1, robotCurrentColumn + thisDelta.Item2);
				if (wallsCurrent.Contains(nextLocation))
				{
					continue;
				}
				else if (boxesCurrent.Contains(nextLocation))
				{
					List<(int, int)>? boxesToMove = GetBoxesToMove(nextLocation, c);
					if (boxesToMove != null)
					{
						foreach ((int, int) thisBox in boxesToMove)
						{
							boxesCurrent.Remove(thisBox);
							boxesCurrent.Add((thisBox.Item1 + thisDelta.Item1, thisBox.Item2 + thisDelta.Item2));
						}
						robotCurrentRow = nextLocation.Item1;
						robotCurrentColumn = nextLocation.Item2;
					}
				}
				else
				{
					robotCurrentRow = nextLocation.Item1;
					robotCurrentColumn = nextLocation.Item2;
				}
			}
		}
		return boxesCurrent.Select(b => 100 * b.Item1 + b.Item2).Sum();
	}

	public long Part2()
	{
		robotCurrentRow = robotStartRow;
		robotCurrentColumn = robotStartColumn * 2;
		boxesCurrent = [];
		foreach ((int, int) thisBox in boxesStart)
		{
			boxesCurrent.Add((thisBox.Item1, 2 * thisBox.Item2));
		}
		wallsCurrent = [];
		foreach ((int, int) thisWall in wallsStart)
		{
			wallsCurrent.Add((thisWall.Item1, 2 * thisWall.Item2));
			wallsCurrent.Add((thisWall.Item1, 2 * thisWall.Item2 + 1));
		}
		foreach (string d in directions)
		{
			foreach (char c in d)
			{
				(int, int) thisDelta = deltas[c];
				(int, int) nextLocation = (robotCurrentRow + thisDelta.Item1, robotCurrentColumn + thisDelta.Item2);
				if (wallsCurrent.Contains(nextLocation))
				{
					continue;
				}
				List<(int, int)>? boxAtLocation = GetWideBox(nextLocation);
				if (boxAtLocation == null)
				{
					robotCurrentRow = nextLocation.Item1;
					robotCurrentColumn = nextLocation.Item2;
				}
				else
				{
					HashSet<(int, int)>? boxesToMove = GetBoxesToMoveWide(nextLocation, c);
					if (boxesToMove != null)
					{
						foreach ((int, int) thisBox in boxesToMove)
						{
							boxesCurrent.Remove(thisBox);
							boxesCurrent.Add((thisBox.Item1 + thisDelta.Item1, thisBox.Item2 + thisDelta.Item2));
						}
						robotCurrentRow = nextLocation.Item1;
						robotCurrentColumn = nextLocation.Item2;
					}
				}
			}
		}
		return boxesCurrent.Select(b => 100 * b.Item1 + b.Item2).Sum();
	}

	private List<(int, int)>? GetBoxesToMove((int, int) current, char facing)
	{
		if (wallsCurrent.Contains(current))
		{
			return null;
		}
		if (!boxesCurrent.Contains(current))
		{
			return [];
		}
		List<(int, int)>? toMove 
			= GetBoxesToMove((current.Item1 + deltas[facing].Item1, current.Item2 + deltas[facing].Item2), facing);
		if (toMove == null)
		{
			return null;
		}
		toMove.Add(current);
		return toMove;
	}

	private HashSet<(int, int)>? GetBoxesToMoveWide((int, int) current, char facing)
	{
		if (wallsCurrent.Contains(current))
		{
			return null;
		}
		List<(int, int)>? boxAtLocation = GetWideBox(current);
		if (boxAtLocation == null)
		{
			return [];
		}				
		if (deltas[facing].Item1 == 0) // left or right
		{
			HashSet<(int, int)>? toMove 
				= GetBoxesToMoveWide((current.Item1, current.Item2 + 2 * deltas[facing].Item2), facing);
			if (toMove == null)
			{
				return null;
			}
			toMove.Add(boxAtLocation[0]);			
			return toMove;
		}
		else // up or down
		{
			HashSet<(int, int)>? toMove 
				= GetBoxesToMoveWide((boxAtLocation[0].Item1 + deltas[facing].Item1, boxAtLocation[0].Item2), facing);
			HashSet<(int, int)>? toMove2 
				= GetBoxesToMoveWide((boxAtLocation[1].Item1 + deltas[facing].Item1, boxAtLocation[1].Item2), facing);
			if (toMove == null || toMove2 == null)
			{
				return null;
			}
			List<(int, int)> combined = [];
			combined.AddRange(toMove);
			combined.AddRange(toMove2);
			combined.Add(boxAtLocation[0]);
			return [.. combined];
		}		
	}

	private List<(int, int)>? GetWideBox((int, int) location)
	{
		if (boxesCurrent.Contains(location))
		{
			return [location, (location.Item1, location.Item2 + 1)];
		}
		if (boxesCurrent.Contains((location.Item1, location.Item2 - 1)))
		{
			return [(location.Item1, location.Item2 - 1), location];
		}
		return null;
	}

}