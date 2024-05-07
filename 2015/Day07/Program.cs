using Day07;

string[] allLines = File.ReadAllLines("input.txt");
(ushort, ushort) result = Part1AndPart2(allLines);
Console.WriteLine($"Part 1: {result.Item1}");
Console.WriteLine($"Part 2: {result.Item2}");

static (ushort, ushort) Part1AndPart2(string[] allLines)
{
	(ushort, ushort) result = (0, 0);
	foreach (string thisLine in allLines)
	{
		Wire.AddWire(thisLine);
	}
	ushort aValue = Wire.GetValue("a");
	result.Item1 = aValue;
	Wire.ClearCache();
	Wire.SetValue("b", aValue);
	aValue = Wire.GetValue("a");
	result.Item2 = aValue;
	return result;
}