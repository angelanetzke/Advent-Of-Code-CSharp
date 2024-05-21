string[] allLines = File.ReadAllLines("input.txt");
(string, string) result = Part1AndPart2(allLines);
Console.WriteLine($"Part 1: {result.Item1}");
Console.WriteLine($"Part 2: {result.Item2}");

static (string, string) Part1AndPart2(string[] allLines)
{
	int[] largestModelNumber = new int[14];
	int[] smallestModelNumber = new int[14];
	(int, int)[] modulePairs = [ (0, 13), (1, 12), (2, 3), (4, 11), (5, 10), (6, 7), (8, 9) ];
	List<(int, int)> inputPairs = [];
	List<int> zValues = [];
	foreach ((int, int) thisModulePair in modulePairs)
	{
		inputPairs.Clear();
		for (int w1 = 1; w1 <= 9; w1++)
		{
			for (int w2 = 1; w2 <= 9; w2++)
			{
				int z1 = ALU(thisModulePair.Item1, w1, 0);
				int z2 = ALU(thisModulePair.Item2, w2, z1);
				if (z2 == 0)
				{
					inputPairs.Add((w1, w2));
				}
			}
		}
		inputPairs = [..inputPairs.OrderBy(x => x.Item1)];
		largestModelNumber[thisModulePair.Item1] = inputPairs.Last().Item1;
		largestModelNumber[thisModulePair.Item2] = inputPairs.Last().Item2;
		smallestModelNumber[thisModulePair.Item1] = inputPairs.First().Item1;
		smallestModelNumber[thisModulePair.Item2] = inputPairs.First().Item2;
	}
	return (string.Join("", largestModelNumber), string.Join("", smallestModelNumber));
}

static int ALU(int module, int w, int previousZ)
{
	int x = 0;
	int y = 0;
	int z = previousZ;

	switch(module)
	{
		case 0:
			x *= 0;
			x += z;
			x %= 26;
			z /= 1;
			x += 13;
			x = x == w ? 1 : 0;
			x = x == 0 ? 1 : 0;
			y *= 0;
			y += 25;
			y *= x;
			y += 1;
			z *= y;
			y *= 0;
			y += w;
			y += 8;
			y *= x;
			z += y;
			break;
		case 1:
			x *= 0;
			x += z;
			x %= 26;
			z /= 1;
			x += 12;
			x = x == w ? 1 : 0;
			x = x == 0 ? 1 : 0;
			y *= 0;
			y += 25;
			y *= x;
			y += 1;
			z *= y;
			y *= 0;
			y += w;
			y += 16;
			y *= x;
			z += y;
			break;
		case 2:
			x *= 0;
			x += z;
			x %= 26;
			z /= 1;
			x += 10;
			x = x == w ? 1 : 0;
			x = x == 0 ? 1 : 0;
			y *= 0;
			y += 25;
			y *= x;
			y += 1;
			z *= y;
			y *= 0;
			y += w;
			y += 4;
			y *= x;
			z += y;
			break;
		case 3:
			x *= 0;
			x += z;
			x %= 26;
			z /= 26;
			x += -11;
			x = x == w ? 1 : 0;
			x = x == 0 ? 1 : 0;
			y *= 0;
			y += 25;
			y *= x;
			y += 1;
			z *= y;
			y *= 0;
			y += w;
			y += 1;
			y *= x;
			z += y;
			break;
		case 4:
			x *= 0;
			x += z;
			x %= 26;
			z /= 1;
			x += 14;
			x = x == w ? 1 : 0;
			x = x == 0 ? 1 : 0;
			y *= 0;
			y += 25;
			y *= x;
			y += 1;
			z *= y;
			y *= 0;
			y += w;
			y += 13;
			y *= x;
			z += y;
			break;
		case 5:
			x *= 0;
			x += z;
			x %= 26;
			z /= 1;
			x += 13;
			x = x == w ? 1 : 0;
			x = x == 0 ? 1 : 0;
			y *= 0;
			y += 25;
			y *= x;
			y += 1;
			z *= y;
			y *= 0;
			y += w;
			y += 5;
			y *= x;
			z += y;
			break;
		case 6:
			x *= 0;
			x += z;
			x %= 26;
			z /= 1;
			x += 12;
			x = x == w ? 1 : 0;
			x = x == 0 ? 1 : 0;
			y *= 0;
			y += 25;
			y *= x;
			y += 1;
			z *= y;
			y *= 0;
			y += w;
			y += 0;
			y *= x;
			z += y;
			break;
		case 7:
			x *= 0;
			x += z;
			x %= 26;
			z /= 26;
			x += -5;
			x = x == w ? 1 : 0;
			x = x == 0 ? 1 : 0;
			y *= 0;
			y += 25;
			y *= x;
			y += 1;
			z *= y;
			y *= 0;
			y += w;
			y += 10;
			y *= x;
			z += y;
			break;
		case 8:
			x *= 0;
			x += z;
			x %= 26;
			z /= 1;
			x += 10;
			x = x == w ? 1 : 0;
			x = x == 0 ? 1 : 0;
			y *= 0;
			y += 25;
			y *= x;
			y += 1;
			z *= y;
			y *= 0;
			y += w;
			y += 7;
			y *= x;
			z += y;
			break;
		case 9:
			x *= 0;
			x += z;
			x %= 26;
			z /= 26;
			x += 0;
			x = x == w ? 1 : 0;
			x = x == 0 ? 1 : 0;
			y *= 0;
			y += 25;
			y *= x;
			y += 1;
			z *= y;
			y *= 0;
			y += w;
			y += 2;
			y *= x;
			z += y;
			break;
		case 10:
			x *= 0;
			x += z;
			x %= 26;
			z /= 26;
			x += -11;
			x = x == w ? 1 : 0;
			x = x == 0 ? 1 : 0;
			y *= 0;
			y += 25;
			y *= x;
			y += 1;
			z *= y;
			y *= 0;
			y += w;
			y += 13;
			y *= x;
			z += y;
			break;
		case 11:
			x *= 0;
			x += z;
			x %= 26;
			z /= 26;
			x += -13;
			x = x == w ? 1 : 0;
			x = x == 0 ? 1 : 0;
			y *= 0;
			y += 25;
			y *= x;
			y += 1;
			z *= y;
			y *= 0;
			y += w;
			y += 15;
			y *= x;
			z += y;
			break;
		case 12:
			x *= 0;
			x += z;
			x %= 26;
			z /= 26;
			x += -13;
			x = x == w ? 1 : 0;
			x = x == 0 ? 1 : 0;
			y *= 0;
			y += 25;
			y *= x;
			y += 1;
			z *= y;
			y *= 0;
			y += w;
			y += 14;
			y *= x;
			z += y;
			break;
		case 13:
			x *= 0;
			x += z;
			x %= 26;
			z /= 26;
			x += -11;
			x = x == w ? 1 : 0;
			x = x == 0 ? 1 : 0;
			y *= 0;
			y += 25;
			y *= x;
			y += 1;
			z *= y;
			y *= 0;
			y += w;
			y += 9;
			y *= x;
			z += y;
			break;
	}
	return z;
}
