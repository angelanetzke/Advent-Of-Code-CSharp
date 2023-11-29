using System.Text.RegularExpressions;

var inputLine = File.ReadAllText("input.txt");
Part1(inputLine);
Part2(inputLine);

static void Part1(string inputLine)
{
	var min = int.Parse(inputLine.Split('-')[0]);
	var max = int.Parse(inputLine.Split('-')[1]);
	var matchCount = 0;
	for (int number = min; number <= max; number++)
	{
		if (number.ToString().Length != 6)
		{
			continue;
		}
		if (!HasDoubleDigit(number))
		{
			continue;
		}
		if (!AreAllDigitsIncreasing(number))
		{
			continue;
		}
		matchCount++;
	}
	Console.WriteLine($"Part 1: {matchCount}");
}

static void Part2(string inputLine)
{
	var min = int.Parse(inputLine.Split('-')[0]);
	var max = int.Parse(inputLine.Split('-')[1]);
	var matchCount = 0;
	for (int number = min; number <= max; number++)
	{
		if (number.ToString().Length != 6)
		{
			continue;
		}
		if (!HasDoubleDigitStrict(number))
		{
			continue;
		}
		if (!AreAllDigitsIncreasing(number))
		{
			continue;
		}
		matchCount++;
	}
	Console.WriteLine($"Part 1: {matchCount}");
}

static bool HasDoubleDigit(int number)
{
	for (int i = 0; i <= 9; i++)
	{
		var expression = "^.*" + i.ToString() + i.ToString() + ".*$";
		if (Regex.Matches(number.ToString(), expression).Count > 0)
		{
			return true;
		}
	}
	return false;
}

static bool HasDoubleDigitStrict(int number)
{
	for (int i = 0; i <= 9; i++)
	{
		var expression = "^.*" + i.ToString() + i.ToString() + ".*$";
		if (Regex.Matches(number.ToString(), expression).Count > 0)
		{
			expression = "^.*" + i.ToString() + i.ToString() + i.ToString() + ".*$";
			if (Regex.Matches(number.ToString(), expression).Count == 0)
			{
				return true;
			}
		}
	}
	return false;
}

static bool AreAllDigitsIncreasing(int number)
{
	var numberAsString = number.ToString();
	for (int i = 1; i < numberAsString.Length; i++)
	{
		if (numberAsString[i] < numberAsString[i - 1])
		{
			return false;
		}
	}
	return true;
}
