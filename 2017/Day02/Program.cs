var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

void Part1(string[] allLines)
{
	var sum = 0;
	foreach (string thisLine in allLines)
	{
		var numberStrings = thisLine.Split('\t');
		var min = int.MaxValue;
		var max = int.MinValue;
		foreach (string thisNumberString in numberStrings)
		{
			var thisValue = int.Parse(thisNumberString);
			min = Math.Min(min, thisValue);
			max = Math.Max(max, thisValue);
		}
		sum += max - min;
	}
	Console.WriteLine($"Part 1: {sum}");
}

void Part2(string[] allLines)
{
	var sum = 0;
	foreach (string thisLine in allLines)
	{
		var isFound = false;
		var numberStrings = thisLine.Split('\t');
		for (int i = 0; i < numberStrings.Length - 1; i++)
		{
			for (int j = i + 1; j < numberStrings.Length; j++)
			{
				var num1 = int.Parse(numberStrings[i]);
				var num2 = int.Parse(numberStrings[j]);
				if (num1 % num2 == 0)
				{
					sum += num1 / num2;
					isFound = true;
					break;
				}
				if (num2 % num1 == 0)
				{
					sum += num2 / num1;
					isFound = true;
					break;
				}
			}
			if (isFound)
			{
				break;
			}
		}
	}
	Console.WriteLine($"Part 1: {sum}");
}

