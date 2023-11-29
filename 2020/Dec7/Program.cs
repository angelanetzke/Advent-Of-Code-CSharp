using Dec7;
using System.Text.RegularExpressions;

var allLines = System.IO.File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var parentColorList = new List<string>();
	foreach (var thisLine in allLines)
	{
		BagTree.AddRule(thisLine);
		var parentColorRegex = new Regex(@"^(?<parent>[\w\s]+) bags contain");
		string thisParentColor = parentColorRegex.Match(thisLine).Groups["parent"].Value;
		//Shiny gold bag must be contained in another bag.
		if (thisParentColor != "shiny gold")
		{
			parentColorList.Add(thisParentColor);
		}
	}
	int shinyGoldBagCount = 0;
	foreach (string thisParentColor in parentColorList)
	{
		if ((new BagTree(thisParentColor)).Contains("shiny gold"))
		{
			shinyGoldBagCount++;
		}
	}
	Console.WriteLine($"Part 1: {shinyGoldBagCount}");
}

static void Part2(string[] allLines)
{
	int insideCount = (new BagTree("shiny gold")).Count();
	Console.WriteLine($"Part2: {insideCount}");
}
