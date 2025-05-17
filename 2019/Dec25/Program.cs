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
	string[] commands = File.ReadAllLines("commands.txt");
	foreach (string thisCommand in commands)
	{
		ic.AddInput(thisCommand);
		if (thisCommand.StartsWith("take"))
		{
			itemList.Add(thisCommand[5..]);
		}		
	}
	ic.Run();
	foreach (string thisItem in itemList)
	{
		ic.AddInput($"drop {thisItem}");
	}
	ic.Run();
	List<List<string>> allCombinations = GetCombinations(itemList);
	bool isComplete = false;
	foreach (List<string> thisCombination in allCombinations)
	{
		foreach (string thisItem in thisCombination)
		{
			ic.AddInput($"take {thisItem}");
		}
		ic.Run();
		ic.ClearOutput();
		ic.AddInput("inv");
		ic.AddInput("east");
		ic.Run();
		List<long> output = ic.GetOutput();
		foreach (long thisOutputChar in output)
		{
			if ('0' <= thisOutputChar && thisOutputChar <= '9')
			{
				isComplete = true;
				break;
			}
		}
		if (isComplete)
		{
			foreach (long thisOutputChar in output)
			{
				Console.Write((char)thisOutputChar);
			}
			Console.WriteLine();
			break;
		}
		foreach (string thisItem in thisCombination)
		{
			ic.AddInput($"drop {thisItem}");
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
	for (int i = 1; i < (1 << n); i++)
	{
		List<string> thisCombination = [];
		for (int j = 0; j < n; j++)
		{
			if ((i & (1 << j)) != 0)
			{
				thisCombination.Add(itemList[j]);
			}
		}
		combinations.Add(thisCombination);
	}
	return combinations;
}




