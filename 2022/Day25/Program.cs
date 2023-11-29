using Day25;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);

static void Part1(string[] allLines)
{
	var longest = allLines.Select(x => x.Length).Max();
	var paddedLength = longest + allLines.Length;
	var total = new Snafu("0", paddedLength);
	foreach (string thisLine in allLines)
	{
		var thisSnafu = new Snafu(thisLine, paddedLength);
		total = total.Add(thisSnafu);
	}
	Console.WriteLine($"Part 1: {total}");
}
