using System.Text;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var currentDigit = 5;
	var code = 0;
	foreach (string thisLine in allLines)
	{
		foreach (char thisDirection in thisLine)
		{
			switch (thisDirection)
			{
				case 'U':
					currentDigit = currentDigit <= 3 ? currentDigit : currentDigit - 3;
					break;
				case 'D':
					currentDigit = currentDigit >= 7 ? currentDigit : currentDigit + 3;
					break;
				case 'L':
					currentDigit = currentDigit % 3 == 1 ? currentDigit : currentDigit - 1;
					break;
				case 'R':
					currentDigit = currentDigit % 3 == 0 ? currentDigit : currentDigit + 1;
					break;
			}
		}
		code = code * 10 + currentDigit;
	}
	Console.WriteLine($"Part 1: {code}");
}

static void Part2(string[] allLines)
{
	var currentDigit = 5;
	var code = new StringBuilder();
	var top = new HashSet<int>() { 5, 2, 1, 4, 9 };
	var bottom = new HashSet<int>() { 5, 10, 13, 12, 9 };
	var left = new HashSet<int>() { 1, 2, 5, 10, 13 };
	var right = new HashSet<int>() { 1, 4, 9, 12, 13 };
	var upTwo = new HashSet<int> { 3, 13 };
	var downTwo = new HashSet<int> { 1, 11 };
	foreach (string thisLine in allLines)
	{		
		foreach (char thisDirection in thisLine)
		{
			switch (thisDirection)
			{
				case 'U':
					currentDigit = top.Contains(currentDigit) ? currentDigit 
						: upTwo.Contains(currentDigit) ? currentDigit - 2 
						: currentDigit - 4;
					break;
				case 'D':
					currentDigit = bottom.Contains(currentDigit) ? currentDigit 
						: downTwo.Contains(currentDigit) ? currentDigit + 2 
						: currentDigit + 4;
					break;
				case 'L':
					currentDigit = left.Contains(currentDigit) ? currentDigit : currentDigit - 1;
					break;
				case 'R':
					currentDigit = right.Contains(currentDigit) ? currentDigit : currentDigit + 1;
					break;
			}
		}
		if (currentDigit <= 9)
		{
			code.Append(currentDigit);
		}
		else
		{
			code.Append((char)(currentDigit - 10 + 'A'));
		}
	}
	Console.WriteLine($"Part 2: {code.ToString()}");
}
