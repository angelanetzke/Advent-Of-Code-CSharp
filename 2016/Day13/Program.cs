string input = File.ReadAllLines("input.txt")[0];
(int, int) result = Part1AndPart2(input);
Console.WriteLine($"Part 1: {result.Item1}");
Console.WriteLine($"Part 2: {result.Item2}");

static (int, int) Part1AndPart2(string input)
{
	int maxDistance = 50;
	(int, int) result = (-1, -1);
	Dictionary<(int, int), bool> isWall = [];
	for (int i = 0; i <= 1; i++)
	{		
		int within50Count = 0;;
		int favoriteNumber = int.Parse(input);
		(int, int)[] deltas = [(0, 1), (0, -1), (1, 0), (-1, 0)];
		int endX = 31;
		int endY = 39;		
		HashSet<(int, int)> visited = [];
		List<(int, int, int, int)> queue = []; // x, y, distance, priority
		queue.Add((1, 1, 0, 0));
		while (queue.Count > 0)
		{
			queue = [.. queue.OrderBy(x => x.Item4)];			
			(int x, int y, int distance, int priority) = queue[0];
			queue.RemoveAt(0);
			visited.Add((x, y));
			if (i == 0 && x == endX && y == endY)
			{
				result.Item1 = distance;
				break;
			}
			if (i == 1 && distance <= maxDistance)
			{
				within50Count++;
			}			
			foreach ((int deltaX, int deltaY) in deltas)
			{
				int newX = x + deltaX;
				int newY = y + deltaY;
				bool newIsWall;
				if (isWall.ContainsKey((newX, newY)))
				{
					newIsWall = isWall[(newX, newY)];
				}
				else
				{
					newIsWall = GetIsWall(newX, newY, endX, endY, favoriteNumber, isWall);
					isWall[(newX, newY)] = newIsWall;
				}
				if (newIsWall)
				{
					continue;
				}
				if (!visited.Contains((newX, newY)) && !queue.Any(x => x.Item1 == newX && x.Item2 == newY))
				{
					int newDistance = distance + 1;
					int newPriority = newDistance + GetDistanceToEnd(newX, newY, endX, endY);
					queue.Add((newX, newY, newDistance, newPriority));
				}
			}
			if (i == 1 && !queue.Any(x => x.Item3 <= maxDistance))
			{
				result.Item2 = within50Count;
				break;
			}
		}
	}
	return result;
}

static int GetDistanceToEnd(int x, int y, int endX, int endY)
{
	return Math.Abs(endX - x) + Math.Abs(endY - y);
}

static bool GetIsWall(int x, int y, int endX, int endY, int favoriteNumber, Dictionary<(int, int), bool> isWall)
{
	if (x < 0 || y < 0)
	{
		return true;
	}
	int calculation = x * x + 3 * x + 2 * x * y + y + y * y + favoriteNumber;
	string binaryString = Convert.ToString(calculation, 2);
	int oneCount = binaryString.Count(x => x == '1');
	return oneCount % 2 == 1;
}