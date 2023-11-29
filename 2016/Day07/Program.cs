using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var supportedTLSCount = 0;
	foreach (string thisLine in allLines)
	{
		var isABBAOutside = false;
		var isABBAInside = false;
		var isOutside = true;
		var i = 0;
		while (i <= thisLine.Length - 4)
		{
			if (thisLine[i + 3] == '[' || thisLine[i + 3] == ']')
			{
				isOutside = !isOutside;
				i += 4;
			}
			else if (thisLine[i] == thisLine[i + 3] 
				&& thisLine[i + 1] == thisLine[i + 2] 
				&& thisLine[i] != thisLine[i + 1])
			{
				if (isOutside)
				{
					isABBAOutside = true;
				}
				else
				{
					isABBAInside = true;
				}
				i++;
			}
			else 
			{
				i++;
			}
		}
		supportedTLSCount += isABBAOutside && !isABBAInside ? 1 : 0;
	}
	Console.WriteLine($"Part 1: {supportedTLSCount}");
}

static void Part2(string[] allLines)
{
	var supportedSSLCount = 0;	
	foreach (string thisLine in allLines)
	{
		var isABAOutside = false;
		var isBABInside = false;
		var ABAList = new List<string>();
		var i = 0;
		while (i <= thisLine.Length - 3)
		{
			if (thisLine[i + 2] == '[')
			{
				i += thisLine[i..].IndexOf(']') + 1;
			}
			else if (thisLine[i] == thisLine[i + 2] && thisLine[i] != thisLine[i + 1])
			{
				isABAOutside = true;
				ABAList.Add(thisLine[i..(i + 3)]);
				i++;
			}
			else
			{
				i++;
			}			
		}
		if (!isABAOutside)
		{
			continue;
		}
		var matches = new Regex(@"\[{1}[a-z]+\]{1}").Matches(thisLine);
		foreach (Match thisMatch in matches)
		{
			foreach (string thisABA in ABAList)
			{
				var thisBAB = thisABA[1].ToString() + thisABA[0] + thisABA[1];
				if (thisMatch.Value.Contains(thisBAB))
				{
					isBABInside = true;
				}
			}			
		}
		supportedSSLCount += isABAOutside && isBABInside ? 1 : 0;
	}
	Console.WriteLine($"Part 2: {supportedSSLCount}");
}


