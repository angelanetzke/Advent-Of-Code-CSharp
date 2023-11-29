using Dec4;

var allLines = System.IO.File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var currentPassport = new Passport();
	int validCount = 0;
	foreach (string thisLine in allLines)
	{
		if (thisLine.Length == 0)
		{
			if (currentPassport.IsValid())
			{
				validCount++;
			}
			currentPassport = new();
		}
		else
		{
			currentPassport.AddFields(thisLine);
		}
	}
	//Make sure to process final Passport
	if (!currentPassport.IsEmpty() && currentPassport.IsValid())
	{
		validCount++;
	}
	Console.WriteLine($"Part 1: {validCount}");
}

static void Part2(string[] allLines)
{
	var currentPassport = new Passport();
	int validCount = 0;
	foreach (string thisLine in allLines)
	{
		if (thisLine.Length == 0)
		{
			if (currentPassport.IsDataValid())
			{
				validCount++;
			}
			currentPassport = new();
		}
		else
		{
			currentPassport.AddFields(thisLine);
		}
	}
	//Make sure to process final Passport
	if (!currentPassport.IsEmpty() && currentPassport.IsDataValid())
	{
		validCount++;
	}
	Console.WriteLine($"Part 1: {validCount}");
}
