using System.Text;

namespace Day10;

internal class TunnelMap
{
	private readonly string[] mapData;
	private readonly (int, int) loopStart;
	private enum Direction { N, W, S, E };
	private static readonly (int, int) north = (-1, 0);
	private static readonly (int, int) west = (0, -1);
	private static readonly (int, int) south = (1, 0);
	private static readonly (int, int) east = (0, 1);
	private static readonly Dictionary<(char, Direction), (int, int)> nextStep = new ()
	{
		{ ('|', Direction.N), north },
		{ ('|', Direction.S), south },
		{ ('-', Direction.W), west },
		{ ('-', Direction.E), east },
		{ ('L', Direction.S), east },
		{ ('L', Direction.W), north },
		{ ('J', Direction.S), west },
		{ ('J', Direction.E), north },
		{ ('7', Direction.N), west },
		{ ('7', Direction.E), south },
		{ ('F', Direction.N), east },
		{ ('F', Direction.W), south },
	};

	private readonly Dictionary<(int, int), Direction> offsetToDirection = new ()
	{
		{north, Direction.N},
		{west, Direction.W},
		{south, Direction.S},
		{east, Direction.E}
	};
	private readonly HashSet<(int, int)> loopPositions = [];
	public TunnelMap(string[] mapData)
	{
		this.mapData = mapData;
		for (int row = 0; row < mapData.Length; row++)
		{
			int column = mapData[row].IndexOf('S');
			if (column >= 0)
			{
				loopStart = (row, column);
				break;
			}
		}
	}

	public int GetFarthestTileDistance()
	{		
		loopPositions.Add(loopStart);
		(int, int) currentPosition = loopStart;
		Direction currentDirection;
		(int, int) northOfStart = (loopStart.Item1 + north.Item1, loopStart.Item2 + north.Item2);
		(int, int) westOfStart = (loopStart.Item1 + west.Item1, loopStart.Item2 + west.Item2);
		(int, int) southOfStart = (loopStart.Item1 + south.Item1, loopStart.Item2 + south.Item2);
		if (IsInRangeMapData(northOfStart) && CanMove(northOfStart, Direction.N))
		{
			currentDirection = Direction.N;
			currentPosition = (currentPosition.Item1 + north.Item1, currentPosition.Item2 + north.Item2);
		}
		else if (IsInRangeMapData(westOfStart) && CanMove(northOfStart, Direction.W))
		{
			currentDirection = Direction.W;
			currentPosition = (currentPosition.Item1 + west.Item1, currentPosition.Item2 + west.Item2);
		}
		else if (IsInRangeMapData(southOfStart) && CanMove(northOfStart, Direction.S))
		{
			currentDirection = Direction.S;
			currentPosition = (currentPosition.Item1 + south.Item1, currentPosition.Item2 + south.Item2);
		}
		else
		{
			currentDirection = Direction.E;
			currentPosition = (currentPosition.Item1 + east.Item1, currentPosition.Item2 + east.Item2);
		}
		int count = 1;
		do
		{
			loopPositions.Add(currentPosition);
			count++;
			(int, int) offset = nextStep[(mapData[currentPosition.Item1][currentPosition.Item2], currentDirection)];
			currentPosition = (currentPosition.Item1 + offset.Item1, currentPosition.Item2 + offset.Item2);
			currentDirection = offsetToDirection[offset];
		} while (currentPosition != loopStart);
		return count / 2;
	}

	private bool IsInRangeMapData((int, int) position)
	{
		return 0 <= position.Item1
			&& position.Item1 < mapData.Length
			&& 0 <= position.Item2
			&& position.Item2 < mapData[0].Length;
	}

	private static bool IsInRangeExplodedMap((int, int) position, List<string> explodedMap)
	{
		return 0 <= position.Item1
			&& position.Item1 < explodedMap.Count
			&& 0 <= position.Item2
			&& position.Item2 < explodedMap[0].Length;
	}

	private bool CanMove((int, int) moveToPosition, Direction moveDirection)
	{
		char moveToChar = mapData[moveToPosition.Item1][moveToPosition.Item2];
		switch (moveDirection)
		{
			case Direction.N:
				return moveToChar == '|' || moveToChar == 'F' || moveToChar == '7';
			case Direction.W:
				return moveToChar == '-' || moveToChar == 'L' || moveToChar == 'F';
			case Direction.S:
				return moveToChar == '|' || moveToChar == 'L' || moveToChar == 'J';
			case Direction.E:
				return moveToChar == '-' || moveToChar == 'J' || moveToChar == '7';
		}
		return false;
	}

	public int CountInsideGround()
	{
		if (loopPositions.Count == 0)
		{
			return - 1;
		}
		List<string> explodedMap = [];
		explodedMap.Add(new string(' ', 3 * mapData[0].Length + 2));
		StringBuilder builder1 = new ();
		StringBuilder builder2 = new ();
		StringBuilder builder3 = new ();
		for (int row = 0; row < mapData.Length; row++)
		{
			builder1.Clear();
			builder2.Clear();
			builder3.Clear();
			builder1.Append(' ');
			builder2.Append(' ');
			builder3.Append(' ');
			for (int column = 0; column < mapData[0].Length; column++)
			{				
				if (loopPositions.Contains((row, column)))
				{
					switch(mapData[row][column])
					{
						case 'S':
							builder1.Append("###");
							builder2.Append("###");
							builder3.Append("###");
							break;
						case '|':
							builder1.Append(" # ");
							builder2.Append(" # ");
							builder3.Append(" # ");
							break;
						case '-':
							builder1.Append("   ");
							builder2.Append("###");
							builder3.Append("   ");
							break;
						case 'L':
							builder1.Append(" # ");
							builder2.Append(" ##");
							builder3.Append("   ");
							break;
						case 'J':
							builder1.Append(" # ");
							builder2.Append("## ");
							builder3.Append("   ");
							break;
						case 'F':
							builder1.Append("   ");
							builder2.Append(" ##");
							builder3.Append(" # ");
							break;
						case '7':
							builder1.Append("   ");
							builder2.Append("## ");
							builder3.Append(" # ");
							break;
					}
				}
				else
				{
					builder1.Append("   ");
					builder2.Append(" . ");
					builder3.Append("   ");
				}
			}
			builder1.Append(' ');
			builder2.Append(' ');
			builder3.Append(' ');
			explodedMap.Add(builder1.ToString());
			explodedMap.Add(builder2.ToString());
			explodedMap.Add(builder3.ToString());
		}
		explodedMap.Add(new string(' ', 3 * mapData[0].Length + 2));
		int outsideGroundCount = 0;
		int totalGroundCount = 0;
		explodedMap.ForEach(row => totalGroundCount += row.Count(column => column == '.'));
		(int, int) current;
		Queue<(int, int)> queue = new ();
		queue.Enqueue((0, 0));
		HashSet<(int, int)> visited = [];
		(int, int)[] nextOffsets = [north, west, south, east];
		while (queue.Count > 0)
		{
			current = queue.Dequeue();
			if (explodedMap[current.Item1][current.Item2] == '.')
			{
				outsideGroundCount++;
			}
			visited.Add(current);			
			foreach ((int, int) thisOffset in nextOffsets)
			{
				(int, int) thisNeighbor = (current.Item1 + thisOffset.Item1, current.Item2 + thisOffset.Item2);
				if (!queue.Contains(thisNeighbor)
					&& !visited.Contains(thisNeighbor)
					&& IsInRangeExplodedMap(thisNeighbor, explodedMap)
					&& explodedMap[thisNeighbor.Item1][thisNeighbor.Item2] != '#')
				{
					queue.Enqueue(thisNeighbor);
				}
			}
		}
		return totalGroundCount - outsideGroundCount;
	}

}