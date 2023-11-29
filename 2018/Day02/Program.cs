using System.Text;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var twoCount = allLines.Where(x => HasExactlyTwo(x)).Count();
	var threeCount = allLines.Where(x => HasExactlyThree(x)).Count();
	var result = twoCount * threeCount;
	Console.WriteLine($"Part 1: {result}");
}

static void Part2(string[] allLines)
{
	var result = "";
	for (int i = 0; i < allLines.Length - 1; i++)
	{
		for (int j = i + 1; j < allLines.Length; j++)
		{
			var theseCommonLetters = GetCommonLetters(allLines[i], allLines[j]);
			if (theseCommonLetters != null && allLines[i].Length - theseCommonLetters.Length == 1)
			{
				result = theseCommonLetters;
			}
		}
	}
	Console.WriteLine($"Part 2: {result}");
}

static bool HasExactlyTwo(string startString)
{
	var lastString = startString;
	string thisString;
	while (lastString.Length > 0)
	{
		thisString = lastString.Replace(lastString[0].ToString(), "");
		if (lastString.Length - thisString.Length == 2)
		{
			return true;
		}
		lastString = thisString;		
	}
	return false;
}

static bool HasExactlyThree(string startString)
{
	var lastString = startString;
	string thisString;
	while (lastString.Length > 0)
	{
		thisString = lastString.Replace(lastString[0].ToString(), "");
		if (lastString.Length - thisString.Length == 3)
		{
			return true;
		}
		lastString = thisString;		
	}
	return false;
}

static string? GetCommonLetters(string string1, string string2)
{
	if (string1.Length != string2.Length)
	{
		return null;
	}
	var builder = new StringBuilder();
	for (int i = 0; i < string1.Length; i++)
	{
		if (string1[i] == string2[i])
		{
			builder.Append(string1[i]);
		}
	}
	return builder.ToString();
}
