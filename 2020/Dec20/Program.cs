using Dec20;

var allLines = System.IO.File.ReadAllLines("input.txt");
var tiles = new List<Tile>();
Part1(allLines, tiles);
Part2(tiles);

static void Part1(string[] allLines, List<Tile> tiles)
{
	var thisTileData = new List<string>();
	foreach (string thisLine in allLines)
	{
		if (thisLine.Length == 0)
		{
			tiles.Add(new Tile(thisTileData));
			thisTileData = new();
		}
		else
		{
			thisTileData.Add(thisLine);
		}
	}
	if (thisTileData.Count > 0)
	{
		tiles.Add(new Tile(thisTileData));
	}
	for (int i = 0; i < tiles.Count - 1; i++)
	{
		for (int j = i + 1; j < tiles.Count; j++)
		{
			if (i != j)
			{
				tiles[i].Connect(tiles[j]);
			}
		}
	}
	long product = 1L;
	foreach (Tile thisTile in tiles)
	{
		if (thisTile.GetNeighborCount() == 2)
		{
			product *= thisTile.GetID();
		}
	}
	Console.WriteLine($"Part 1: {product}");
}

static void Part2(List<Tile> tiles)
{
	var combinedMap = new List<Tile>();
	Tile upperLeft = tiles[0];
	foreach (Tile thisTile in tiles)
	{
		if (thisTile.GetNeighborCount() == 2)
		{
			upperLeft = thisTile;
			break;
		}
	}
	for (int i = 1; i <= 8; i++)
	{
		if (upperLeft.IsUpperLeft(combinedMap))
		{
			break;
		}
		upperLeft.Rotate();
		if (i == 4)
		{
			upperLeft.Flip();
		}
	}
	Tile? rowStart = upperLeft;
	Tile? cursor = upperLeft;
	while (rowStart != null)
	{
		while (cursor != null)
		{
			combinedMap.Add(cursor);
			cursor = cursor.SetEast(combinedMap);
		}
		rowStart = rowStart.SetSouth(combinedMap);
		cursor = rowStart;
	}
	var finalMap = new FullMap(combinedMap);
	Console.WriteLine(finalMap.GetRoughness());
}

