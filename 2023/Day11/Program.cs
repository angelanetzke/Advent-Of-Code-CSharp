string[] allLines = File.ReadAllLines("input.txt");
List<string> image = allLines.ToList();
List<int> emptyRows = [];
for (int row = 0; row < image.Count; row++)
{
	if (!image[row].Any(x => x == '#'))
	{
		emptyRows.Add(row);
	}
}
List<int> emptyColumns = [];
for (int column = 0; column < image[0].Length; column++)
{
	int columnGalaxyCount = 0;
	image.ForEach(row => columnGalaxyCount += row[column] == '#' ? 1 : 0);
	if (columnGalaxyCount == 0)
	{
		emptyColumns.Add(column);
	}
}
List<(int, int)> galaxyPositions = [];
for (int row = 0; row < image.Count; row++)
{
	for (int column = 0; column < image[0].Length; column++)
	{
		if (image[row][column] == '#')
		{
			galaxyPositions.Add((row, column));
		}
	}
}
Console.WriteLine($"Part 1: {Part1(galaxyPositions, emptyRows, emptyColumns)}");
Console.WriteLine($"Part 2: {Part2(galaxyPositions, emptyRows, emptyColumns)}");

static long Part1(List<(int, int)> galaxyPositions, List<int> emptyRows, List<int> emptyColumns)
{ 
	return GetDistanceSum(galaxyPositions, emptyRows, emptyColumns, 1);
}

static long Part2(List<(int, int)> galaxyPositions, List<int> emptyRows, List<int> emptyColumns)
{ 
	return GetDistanceSum(galaxyPositions, emptyRows, emptyColumns, 1000000 - 1);
}

static long GetDistanceSum(
	List<(int, int)> galaxyPositions, List<int> emptyRows, List<int> emptyColumns, int increase)
{
	long distanceSum = 0;
	for (int i = 0; i < galaxyPositions.Count - 1; i++)
	{
		for (int j = i + 1; j < galaxyPositions.Count; j++)
		{
			long minRow = Math.Min(galaxyPositions[i].Item1, galaxyPositions[j].Item1);
			long maxRow = Math.Max(galaxyPositions[i].Item1, galaxyPositions[j].Item1);
			long minColumn = Math.Min(galaxyPositions[i].Item2, galaxyPositions[j].Item2);
			long maxColumn = Math.Max(galaxyPositions[i].Item2, galaxyPositions[j].Item2);
			long rowDistance = maxRow - minRow
				+ increase * emptyRows.Count(x => minRow < x && x < maxRow);
			long columnDistance = maxColumn - minColumn
				+ increase * emptyColumns.Count(x => minColumn < x && x < maxColumn);
			distanceSum += rowDistance + columnDistance;
		}
	}
	return distanceSum;
}
