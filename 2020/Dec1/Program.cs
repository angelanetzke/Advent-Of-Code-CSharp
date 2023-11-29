var allLines = System.IO.File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	for (int i = 0; i < allLines.Length - 1; i++)
	{
		for (int j = i + 1; j < allLines.Length; j++)
		{
			long number1 = long.Parse(allLines[i]);
			long number2 = long.Parse(allLines[j]);
			if (number1 + number2 == 2020L)
			{
				long product = number1 * number2;
				Console.WriteLine($"Part 1: {product}");
				return;
			}
		}
	}
}

static void Part2(string[] allLines)
{
	for (int i = 0; i < allLines.Length - 2; i++)
	{
		for (int j = i + 1; j < allLines.Length - 1; j++)
		{
			for (int k = j + 1; k < allLines.Length; k++)
			{
				long number1 = long.Parse(allLines[i]);
				long number2 = long.Parse(allLines[j]);
				long number3 = long.Parse(allLines[k]);
				if (number1 + number2 + number3 == 2020L)
				{
					long product = number1 * number2 * number3;
					Console.WriteLine($"Part 2: {product}");
					return;
				}
			}
		}
	}
}
