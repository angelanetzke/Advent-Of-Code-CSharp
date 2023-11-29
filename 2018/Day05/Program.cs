var inputLine = File.ReadAllLines("input.txt")[0];
Part1(inputLine);
Part2(inputLine);

static void Part1(string inputLine)
{	
	Console.WriteLine($"Part 1: {GetFinalLength(inputLine)}");
}

static void Part2(string inputLine)
{
	var shortestLength = int.MaxValue;
	for (char thisUnit = 'a'; thisUnit <= 'z'; thisUnit++)
	{
		var thisString = inputLine.Replace(thisUnit.ToString(), "");
		thisString = thisString.Replace(char.ToUpper(thisUnit).ToString(), "");
		shortestLength = Math.Min(shortestLength, GetFinalLength(thisString));
	}
	Console.WriteLine($"Part 2: {shortestLength}");
}

static int GetFinalLength(string inputLine)
{
	int reactionCount;
	var lastString = inputLine;
	string thisString;
	do 
	{
		reactionCount = 0;
		for (char thisUnit = 'a'; thisUnit <= 'z'; thisUnit++)
		{
			var pair1 = thisUnit.ToString() + char.ToUpper(thisUnit);
			var pair2 = char.ToUpper(thisUnit).ToString() + thisUnit;
			thisString = lastString.Replace(pair1, "");
			thisString = thisString.Replace(pair2, "");
			if (lastString.Length > thisString.Length)
			{
				reactionCount++;
			}
			lastString = thisString;
		}
	} while (reactionCount > 0);
	return lastString.Length;
}