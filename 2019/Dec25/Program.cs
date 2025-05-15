using Dec25;
using System.Diagnostics;

Console.WriteLine("Advent of Code 2019, Day 25");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result);
Console.WriteLine($"Part 1 answer: {part1Result}");

static void Part1(string[] allLines, out long result)
{
	Stopwatch timer = new();
	timer.Start();
	IntcodeComputer ic = new();
	ic.SetMemory(allLines[0]);
	List<string> itemList = [];
	string? input = "";
	while (input != "exit")
	{
		ic.Run();
		List<long> output = ic.GetOutput();
		foreach (long thisChar in output)
		{
			Console.Write((char)thisChar);
		}
		Console.WriteLine();
		ic.ClearOutput();
		input = Console.ReadLine();
		if (input != null && input != "exit")
		{
			if (input.StartsWith("take"))
			{
				itemList.Add(input[5..]);
			}
			if (input == "inv")
			{
				Console.WriteLine(string.Join(", ", itemList));
			}
			foreach (char c in input)
			{
				ic.AddInput(c);
			}
			ic.AddInput(10);
		}		
	}
	List<long> dropItems = DropItems(itemList);
	List<long> takeItems;
	foreach (long c in dropItems)
	{
		ic.AddInput(c);
	}
	List<List<string>> allCombinations = GetCombinations(itemList);
	input = "1";
	foreach (List<string> thisCombination in allCombinations)
	{
		Console.WriteLine($"this combination: {string.Join(", ", thisCombination)}");
		takeItems = TakeItems(thisCombination);
		foreach (long c in takeItems)
		{
			ic.AddInput(c);
		}
		foreach (char c in "inv")
		{
			ic.AddInput(c);
		}
		ic.AddInput(10);
		foreach (char c in "east")
		{
			ic.AddInput(c);
		}
		ic.AddInput(10);
		ic.Run();
		List<long> output = ic.GetOutput();
		foreach (long thisChar in output)
		{
			Console.Write((char)thisChar);
		}
		Console.WriteLine();
		ic.ClearOutput();
		input = Console.ReadLine();
		if (input == "1")
		{
			dropItems = DropItems(thisCombination);
			foreach (long c in dropItems)
			{
				ic.AddInput(c);
			}
		}		
		if (input != "1")
		{
			break;
		}
	}
	result = -1L;
	timer.Stop();
	Console.WriteLine($"Part 1 time: {timer.ElapsedMilliseconds} ms");
}

static List<List<string>> GetCombinations(List<string> itemList)
{
	List<List<string>> combinations = [];
	int n = itemList.Count;
	for (int i = 0; i < (1 << n); i++)
	{
		List<string> combination = [];
		for (int j = 0; j < n; j++)
		{
			if ((i & (1 << j)) != 0)
			{
				combination.Add(itemList[j]);
			}
		}
		if (combination.Count > 0)
		{
			combinations.Add(combination);
		}		
	}
	return combinations;
}

static List<long> DropItems(List<string> itemList)
{
	List<long> result = [];
	foreach (string thisItem in itemList)
	{
		foreach (char c in "drop ")
		{
			result.Add(c);
		}
		foreach (char c in thisItem)
		{
			result.Add(c);
		}
		result.Add(10);
	}
	return result;
}

static List<long> TakeItems(List<string> itemList)
{
	List<long> result = [];
	foreach (string thisItem in itemList)
	{
		foreach (char c in "take ")
		{
			result.Add(c);
		}
		foreach (char c in thisItem)
		{
			result.Add(c);
		}
		result.Add(10);
	}
	return result;
}


