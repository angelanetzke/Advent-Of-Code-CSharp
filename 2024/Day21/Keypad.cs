using System.Text;

namespace Day21;

internal class Keypad
{
	private static readonly Dictionary<char, (int, int)> locations = new ()
	{
		{'7', (0, 0)},
		{'8', (0, 1)},
		{'9', (0, 2)},
		{'4', (1, 0)},
		{'5', (1, 1)},
		{'6', (1, 2)},
		{'1', (2, 0)},
		{'2', (2, 1)},
		{'3', (2, 2)},
		{'0', (3, 1)},
		{'A', (3, 2)}
	};
	private static readonly (int, int) voidLocation = (3, 0);
	private static char currentButton = 'A';
	private static readonly StringBuilder builder = new ();

	public static Dictionary<string, long> GetDirections(string code)
	{		
		Dictionary<string, long> result = [];
		foreach (char c in code)
		{
			builder.Clear();
			(int, int) currentLocation = locations[currentButton];
			(int, int) nextLocation = locations[c];
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
			if (result.TryGetValue(builder.ToString(), out long oldCount))
			{
				result[builder.ToString()] += 1;
			}
			else
			{
				result[builder.ToString()] = 1;
			}			
			currentButton = c;
		}
		return result;
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