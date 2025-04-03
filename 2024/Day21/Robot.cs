using System.Text;

namespace Day21;

internal class Robot
{
	private static readonly Dictionary<char, (int, int)> locations = new ()
	{
		{'^', (0, 1)},
		{'A', (0, 2)},
		{'<', (1, 0)},
		{'v', (1, 1)},
		{'>', (1, 2)}	
	};
	private static readonly (int, int) voidLocation = (0, 0);
	private static readonly Dictionary<(char, char), string> segments = [];
	private static readonly StringBuilder builder = new ();

	public static Dictionary<string, long> GetDirections(Dictionary<string, long> previous)
	{
		Dictionary<string, long> result = [];
		foreach (string thisPrevious in previous.Keys)
		{
			char currentButton = 'A';
			foreach (char c in thisPrevious)
			{
				if (segments.TryGetValue((currentButton, c), out string? thisSegment))
				{
					if (result.TryGetValue(thisSegment, out long oldCount))
					{
						result[thisSegment] =  oldCount + previous[thisPrevious];
					}
					else
					{
						result[thisSegment] = previous[thisPrevious];
					}
				}
				else
				{
					SetSegment(currentButton, c);
					thisSegment = segments[(currentButton, c)];
					if (result.TryGetValue(thisSegment, out long oldCount))
					{
						result[thisSegment] =  oldCount + previous[thisPrevious];
					}
					else
					{
						result[thisSegment] = previous[thisPrevious];
					}
				}
				currentButton = c;
			}
		}
		return result;
	}

	private static void SetSegment(char start, char end)
	{
		builder.Clear();
		(int, int) currentLocation = locations[start];
		(int, int) nextLocation = locations[end];
		string? nextPart = MoveLeft(currentLocation, nextLocation);
		if (nextPart != null)
		{
			builder.Append(nextPart);
			currentLocation = (currentLocation.Item1, currentLocation.Item2 - nextPart.Length);
		}
		nextPart = MoveDown(currentLocation, nextLocation);
		if (nextPart != null)
		{
			builder.Append(nextPart);
			currentLocation = (currentLocation.Item1 + nextPart.Length, currentLocation.Item2);
		}
		nextPart = MoveUp(currentLocation, nextLocation);
		if (nextPart != null)
		{
			builder.Append(nextPart);
			currentLocation = (currentLocation.Item1 - nextPart.Length, currentLocation.Item2);
		}
		nextPart = MoveRight(currentLocation, nextLocation);
		if (nextPart != null)
		{
			builder.Append(nextPart);
			currentLocation = (currentLocation.Item1, currentLocation.Item2 + nextPart.Length);
		}
		nextPart = MoveLeft(currentLocation, nextLocation);
		if (nextPart != null)
		{
			builder.Append(nextPart);
			currentLocation = (currentLocation.Item1, currentLocation.Item2 - nextPart.Length);
		}
		nextPart = MoveDown(currentLocation, nextLocation);
		if (nextPart != null)
		{
			builder.Append(nextPart);
			currentLocation = (currentLocation.Item1 + nextPart.Length, currentLocation.Item2);
		}
		nextPart = MoveUp(currentLocation, nextLocation);
		if (nextPart != null)
		{
			builder.Append(nextPart);
			currentLocation = (currentLocation.Item1 - nextPart.Length, currentLocation.Item2);
		}
		nextPart = MoveRight(currentLocation, nextLocation);
		if (nextPart != null)
		{
			builder.Append(nextPart);
		}
		builder.Append('A');
		segments[(start, end)] = builder.ToString();
	}

	private static string? MoveLeft((int, int) current, (int, int) next)
	{
		if (next.Item2 >= current.Item2)
		{
			return null;
		}
		if (next.Item2 <= voidLocation.Item2 && voidLocation.Item2 <= current.Item2 && current.Item1 == voidLocation.Item1)
		{
			return null;
		}
		return new string('<', current.Item2 - next.Item2);
	}

	private static string? MoveDown((int, int) current, (int, int) next)
	{
		if (next.Item1 <= current.Item1)
		{
			return null;
		}
		if (current.Item1 <= voidLocation.Item1 && voidLocation.Item1 <= next.Item1 && current.Item2 == voidLocation.Item2)
		{
			return null;
		}
		return new string('v', next.Item1 - current.Item1);
	}

	private static string? MoveUp((int, int) current, (int, int) next)
	{
		if (next.Item1 >= current.Item1)
		{
			return null;
		}
		if (next.Item1 <= voidLocation.Item1 && voidLocation.Item1 <= current.Item1 && current.Item2 == voidLocation.Item2)
		{
			return null;
		}
		return new string('^', current.Item1 - next.Item1);
	}

	private static string? MoveRight((int, int) current, (int, int) next)
	{
		if (next.Item2 <= current.Item2)
		{
			return null;
		}
		if (current.Item2 <= voidLocation.Item2 && voidLocation.Item2 <= next.Item2 && current.Item1 == voidLocation.Item1)
		{
			return null;
		}
		return new string('>', next.Item2 - current.Item2);
	}

}