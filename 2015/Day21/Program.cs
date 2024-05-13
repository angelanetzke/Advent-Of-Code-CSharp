using System.Text.RegularExpressions;

string[] shopLines = File.ReadAllLines("shop.txt");
Regex shopRegex = new (@"\s*(?<cost>\d+)\s+(?<damage>\d+)\s+(?<armor>\d+)");
List<(string, int, int, int)> weapons = []; // name, cost, damage, armor
List<(string, int, int, int)> armor = [ ("none", 0, 0, 0) ]; // name, cost, damage, armor
List<(string, int, int, int)> rings = [ ("none", 0, 0, 0) ]; // name, cost, damage, armor
List<(string, int, int, int)> currentList = weapons;
foreach (string thisShopLine in shopLines)
{
	if (thisShopLine.Length == 0)
	{
		continue;
	}
	if (thisShopLine.StartsWith("Weapons"))
	{
		currentList = weapons;
	}
	else if (thisShopLine.StartsWith("Armor"))
	{
		currentList = armor;
	}
	else if (thisShopLine.StartsWith("Rings"))
	{
		currentList = rings;
	}
	else
	{
		Match thisMatch = shopRegex.Match(thisShopLine[12..]);
		string thisName = thisShopLine[..12].Trim();
		int thisCost = int.Parse(thisMatch.Groups["cost"].Value);
		int thisDamage = int.Parse(thisMatch.Groups["damage"].Value);
		int thisArmor = int.Parse(thisMatch.Groups["armor"].Value);
		currentList.Add((thisName, thisCost, thisDamage, thisArmor));
	}
}
string[] allLines = File.ReadAllLines("input.txt");
(int, int) result = Part1AndPart2(allLines, weapons, armor, rings);
Console.WriteLine($"Part 1: {result.Item1}");
Console.WriteLine($"Part 2: {result.Item2}");

/*
	Prints out the combination of items needed for part 1 before returning the cost.
	This is not necessary to solve the problem. I was just curious.
*/
static (int, int) Part1AndPart2(string[] allLines, List<(string, int, int, int)> weapons, 
	List<(string, int, int, int)> armor, List<(string, int, int, int)> rings)
{
	int heroHP = 100;
	int bossHP = int.Parse(allLines[0].Split(": ")[1]);
	int bossDamage = int.Parse(allLines[1].Split(": ")[1]);
	int bossArmor = int.Parse(allLines[2].Split(": ")[1]);
	int[] combination = [0, 0, 0, 0];
	int[] lowestCombination = new int[4];
	int[] maxValues = [weapons.Count - 1, armor.Count - 1, rings.Count - 1, rings.Count - 1];
	bool isComplete = false;
	int lowestCost = int.MaxValue;
	int highestCost = int.MinValue;
	while (!isComplete)
	{
		isComplete = true;
		for (int i = 0; i < combination.Length; i++)
		{
			if (combination[i] != maxValues[i])
			{
				isComplete = false;
				break;
			}
		}
		if (combination[2] == combination[3] && rings[combination[2]].Item1 != "none")
		{
			combination = GetNextCombination(combination, maxValues);
			continue;
		}
		int thisCost = 0;
		int heroDamage = 0;
		int heroArmor = 0;
		thisCost += weapons[combination[0]].Item2;
		thisCost += armor[combination[1]].Item2;
		thisCost += rings[combination[2]].Item2;
		thisCost += rings[combination[3]].Item2;
		heroDamage += weapons[combination[0]].Item3;
		heroDamage += armor[combination[1]].Item3;
		heroDamage += rings[combination[2]].Item3;
		heroDamage += rings[combination[3]].Item3;
		heroArmor += weapons[combination[0]].Item4;
		heroArmor += armor[combination[1]].Item4;
		heroArmor += rings[combination[2]].Item4;
		heroArmor += rings[combination[3]].Item4;
		int damageHeroReceivesPerRound = int.Max(1, bossDamage - heroArmor);
		int damageBossReceivesPerRound = int.Max(1, heroDamage - bossArmor);		
		int heroRounds = heroHP / damageHeroReceivesPerRound 
			+ (heroHP % damageHeroReceivesPerRound > 0 ? 1 : 0);
		int bossRounds = bossHP / damageBossReceivesPerRound 
			+ (bossHP % damageBossReceivesPerRound > 0 ? 1 : 0);
		if (bossRounds <= heroRounds && thisCost < lowestCost)
		{
			lowestCost = thisCost;
			Array.Copy(combination, lowestCombination, combination.Length);
		}
		if (bossRounds > heroRounds && thisCost > highestCost)
		{
			highestCost = thisCost;
		}
		combination = GetNextCombination(combination, maxValues);
	}
	Console.WriteLine($"Weapon: {weapons[lowestCombination[0]].Item1}, "
		+ $"Armor: {armor[lowestCombination[1]].Item1}, "
		+ $"Ring 1: {rings[lowestCombination[2]].Item1}, "
		+ $"Ring 2: {rings[lowestCombination[3]].Item1}");
	return (lowestCost, highestCost);
}

static int[] GetNextCombination(int[] combination, int[] maxValues)
{
	int[] nextCombination = new int[combination.Length];
	Array.Copy(combination, nextCombination, combination.Length);
	for (int i = nextCombination.Length - 1; i >=0; i--)
	{
		nextCombination[i]++;
		if (nextCombination[i] <= maxValues[i])
		{
			break;
		}
		nextCombination[i] = 0;
	}
	return nextCombination;
}
