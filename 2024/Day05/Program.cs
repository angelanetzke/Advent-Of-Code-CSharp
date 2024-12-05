Console.WriteLine("Advent of Code 2024, Day 5");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result, out List<int[]> unsorted, out List<int[]> rulesList);
Console.WriteLine($"Part 1: {part1Result}");
Part2(unsorted, rulesList, out long part2Result);
Console.WriteLine($"Part 2: {part2Result}");

static void Part1(string[] allLines, out long result, out List<int[]> unsorted, out List<int[]> rulesList)
{
	rulesList = [];
	List<int[]> pagesList = [];
	unsorted = [];
	bool isProcressingRules = true;
	foreach (string thisLine in allLines)
	{
		if (thisLine.Length == 0)
		{
			isProcressingRules = false;
		}
		else if (isProcressingRules)
		{
			rulesList.Add(thisLine.Split("|").Select(x => int.Parse(x)).ToArray());
		}
		else
		{
			pagesList.Add(thisLine.Split(",").Select(x => int.Parse(x)).ToArray());
		}
	}
	long sum = 0L;
	foreach (int[] thesePages in pagesList)
	{
		if (AreInOrder(rulesList, thesePages))
		{
			sum += thesePages[thesePages.Length / 2];
		}
		else
		{
			unsorted.Add(thesePages);
		}
	}
	result = sum;
}

static void Part2(List<int[]> unsorted, List<int[]> rulesList, out long result)
{
	long sum = 0L;
	foreach (int[] thesePages in unsorted)
	{
		int[] newPages = Sort(rulesList, thesePages);
		sum += newPages[newPages.Length / 2];
	}
	result = sum;
}

static bool AreInOrder(List<int[]> rulesList, int[] pages)
{
	foreach (int[] thisRule in rulesList)
	{
		int firstIndex = Array.IndexOf(pages, thisRule[0]);
		int secondIndex = Array.IndexOf(pages, thisRule[1]);
		if (firstIndex < 0 || secondIndex < 0)
		{
			continue;
		}
		if (firstIndex > secondIndex)
		{			
			return false;
		}
	}
	return true;
}

static int[] Sort(List<int[]> rulesList, int[] pages)
{
	int[] newPages = new int[pages.Length];
	Array.Copy(pages, newPages, pages.Length);
	for (int i = 0; i < newPages.Length - 1;  i++)
	{
		for (int j = i + 1; j < newPages.Length; j++)
		{
			if (AreOutOfOrder(rulesList, newPages[i], newPages[j]))
			{
				(newPages[j], newPages[i]) = (newPages[i], newPages[j]);
			}
		}
	}
	return newPages;
}

static bool AreOutOfOrder(List<int[]> rulesList, int first, int second)
{
	var applicableRule = rulesList.Where(x => x[0] == second && x[1] == first);
	return applicableRule.Any();
}