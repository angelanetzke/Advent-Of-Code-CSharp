using Dec14;

var allLines = File.ReadAllLines("input.txt");
var reactionList = new List<Reaction>();
foreach (string thisLine in allLines)
{
	reactionList.Add(new Reaction(thisLine));
}
Part1(reactionList);
Part2(reactionList);

static void Part1(List<Reaction> reactionList)
{
	var neededOreCount = GetOreNeeded(reactionList, 1L);
	Console.WriteLine($"Part 1: {neededOreCount}");
}

static void Part2(List<Reaction> reactionList)
{
	var oreAvailableCount = 1000000000000L;
	var increment = 1000000L;
	var lowerValue = 0L;
	var upperValue = 	increment;
	var fuelProduced = 0L;
	var oreNeededCount = GetOreNeeded(reactionList, upperValue);	
	while (oreNeededCount < oreAvailableCount)
	{
		lowerValue += increment;
		upperValue += increment;
		oreNeededCount = GetOreNeeded(reactionList, upperValue);
	}
	while (upperValue - lowerValue > 10L)
	{
		fuelProduced = (lowerValue + upperValue) / 2L;
		oreNeededCount = GetOreNeeded(reactionList, fuelProduced);
		if (oreNeededCount < oreAvailableCount)
		{
			lowerValue = fuelProduced;
		}
		else if (oreNeededCount > oreAvailableCount)
		{
			upperValue = fuelProduced;
		}
	}
	fuelProduced = lowerValue;
	while (fuelProduced <= upperValue)
	{
		var nextOreNeededCount = GetOreNeeded(reactionList, fuelProduced + 1);
		if (nextOreNeededCount > oreAvailableCount)
		{
			break;
		}
		else
		{
			fuelProduced++;
		}
	}	
	Console.WriteLine($"Part 2: {fuelProduced}");	
}

static long GetOreNeeded(List<Reaction> reactionList, long fuelNeeded)
{
	var stockpile = new Dictionary<string, long>();
	var neededOreCount = 0L;
	var neededComponentQueue = new Queue<(string, long)>();
	neededComponentQueue.Enqueue(("FUEL", fuelNeeded));
	while (neededComponentQueue.Count > 0)
	{
		var thisProduct = neededComponentQueue.Dequeue();
		var productNeeded = thisProduct.Item2;
		if (stockpile.ContainsKey(thisProduct.Item1) && stockpile[thisProduct.Item1] > 0)
		{
			var removeFromStockpile = Math.Min(stockpile[thisProduct.Item1], thisProduct.Item2);
			productNeeded -= removeFromStockpile;
			stockpile[thisProduct.Item1] -= removeFromStockpile;
		}
		var thisReaction = reactionList.Where(x => x.DoesProduce(thisProduct.Item1)).First();
		var factor = thisReaction.GetFactor(productNeeded);
		var excess = thisReaction.GetProductCount() * factor - productNeeded;
		if (stockpile.ContainsKey(thisProduct.Item1))
		{
			stockpile[thisProduct.Item1] += excess;
		}
		else
		{
			stockpile[thisProduct.Item1] = excess;
		}
		var neededComponents = thisReaction.GetComponentMultiples(factor);
		foreach ((string, long) thisComponent in neededComponents)
		{
			if (thisComponent.Item1 == "ORE")
			{
				neededOreCount += thisComponent.Item2;
			}
			else
			{
				neededComponentQueue.Enqueue((thisComponent.Item1, thisComponent.Item2));
			}
		}
	}
	return neededOreCount;
}
