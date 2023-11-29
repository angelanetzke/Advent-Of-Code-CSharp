using System.Text;

var allLines = File.ReadAllLines("input.txt");
var counts = new Dictionary<char, int>[allLines[0].Length];
for (int i = 0; i < counts.Length; i++)
{
	counts[i] = new Dictionary<char, int>();
}
foreach (string thisLine in allLines)
{
	for (int i = 0; i < thisLine.Length; i++)
	{
		if (counts[i].ContainsKey(thisLine[i]))
		{
			counts[i][thisLine[i]]++;
		}
		else
		{
			counts[i][thisLine[i]] = 1;
		}
	}
}
var part1Builder = new StringBuilder();
var part2Builder = new StringBuilder();
foreach (Dictionary<char, int> thisCount in counts)
{
	part1Builder.Append(thisCount.OrderByDescending(x => x.Value).Select(x => x.Key).First());
	part2Builder.Append(thisCount.OrderBy(x => x.Value).Select(x => x.Key).First());
}
Console.WriteLine($"Part 1: {part1Builder.ToString()}");
Console.WriteLine($"Part 2: {part2Builder.ToString()}");


