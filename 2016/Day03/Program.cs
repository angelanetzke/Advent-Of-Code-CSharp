using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var validCount = 0;
	foreach (string thisLine in allLines)
	{
		var matches = (new Regex(@"\d+")).Matches(thisLine);
		var number0 = int.Parse(matches[0].Value);
		var number1 = int.Parse(matches[1].Value);
		var number2 = int.Parse(matches[2].Value);
		validCount += number0 < number1 + number2
			&& number1 < number0 + number2
			&& number2 < number1 + number0
			? 1 : 0;
	}
	Console.WriteLine($"Part 1: {validCount}");
}

static void Part2(string[] allLines)
{
	var validCount = 0;
	for (int i = 0; i < allLines.Length; i += 3)
	{
		var matches = new MatchCollection[3];
		for (int j = 0; j < matches.Length; j++)
		{
			matches[j] = (new Regex(@"\d+")).Matches(allLines[i + j]);
		}
		var numbers = new int[3, 3];
		for (int matchesI = 0; matchesI < matches.Length; matchesI++)
		{
			numbers[matchesI, 0] = int.Parse(matches[matchesI][0].Value);
			numbers[matchesI, 1] = int.Parse(matches[matchesI][1].Value);
			numbers[matchesI, 2] = int.Parse(matches[matchesI][2].Value);
		}
		for (int column = 0; column < numbers.GetLength(1); column++)
		{
			validCount += numbers[0, column] < numbers[1, column] + numbers[2, column]
				&& numbers[1, column] < numbers[0, column] + numbers[2, column]
				&& numbers[2, column] < numbers[1, column] + numbers[0, column]
				? 1 : 0;
		}
	}
	Console.WriteLine($"Part 2: {validCount}");
}
