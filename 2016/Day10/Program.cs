using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
Dictionary<(int bot, string low, string high), bool> instructions = new ();
List<(int bot, int value)> startingValues = new ();
foreach (string thisLine in allLines)
{
	if (thisLine.StartsWith("value"))
	{
		var match = new Regex(@"value (?<value>\d+) goes to bot (?<bot>\d+)").Match(thisLine);
		startingValues.Add((int.Parse(match.Groups["bot"].Value), int.Parse(match.Groups["value"].Value)));
	}
	if (thisLine.StartsWith("bot"))
	{
		var match 
			= new Regex(@"bot (?<bot>\d+) gives low to (?<low>[a-z]+ \d+) and high to (?<high>[a-z]+ \d+)")
			.Match(thisLine);
		instructions[
			(int.Parse(match.Groups["bot"].Value), match.Groups["low"].Value, match.Groups["high"].Value)]
			= false;
	}
}
var bots = new Dictionary<int, HashSet<int>>();
foreach ((int bot, int value) thisStartingValue in startingValues)
{
	if (bots.ContainsKey(thisStartingValue.bot))
	{
		bots[thisStartingValue.bot].Add(thisStartingValue.value);
	}
	else
	{
		bots[thisStartingValue.bot] = new HashSet<int>() { thisStartingValue.value };
	}
}
var outputs = new Dictionary<int, HashSet<int>>();
var part1Solution = -1;
while (instructions.Count(x => !x.Value) > 0)
{
	foreach ((int bot, string low, string high) thisInstruction in instructions.Keys)
	{
		if (!bots.ContainsKey(thisInstruction.bot) || bots[thisInstruction.bot].Count < 2)
		{
			continue;
		}
		if (bots[thisInstruction.bot].Contains(61) && bots[thisInstruction.bot].Contains(17))
		{
			part1Solution = thisInstruction.bot;
		}
		instructions[thisInstruction] = true;
		var lowValue = bots[thisInstruction.bot].Min();
		bots[thisInstruction.bot].Remove(lowValue);
		Send(lowValue, thisInstruction.low, bots, outputs);
		var highValue = bots[thisInstruction.bot].Max();
		bots[thisInstruction.bot].Remove(highValue);
		Send(highValue, thisInstruction.high, bots, outputs);
	}
}
Console.WriteLine($"Part 1: {part1Solution}");
var part2Solution = outputs[0].First() * outputs[1].First() * outputs[2].First();
Console.WriteLine($"Part 2: {part2Solution}");


static void Send(int value, string destination, 
	Dictionary<int, HashSet<int>> bots, Dictionary<int, HashSet<int>> outputs)
{
	if (destination.Split(' ')[0] == "bot")
	{
		var botNumber = int.Parse(destination.Split(' ')[1]);
		if (bots.ContainsKey(botNumber))
		{
			bots[botNumber].Add(value);
		}
		else
		{
			bots[botNumber] = new HashSet<int>() { value };
		}
	}
	else
	{
		var outputNumber = int.Parse(destination.Split(' ')[1]);
		if (outputs.ContainsKey(outputNumber))
		{
			outputs[outputNumber].Add(value);
		}
		else
		{
			outputs[outputNumber] = new HashSet<int>() { value };
		}
	}
}

