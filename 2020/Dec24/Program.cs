using Dec24;

var allLines = System.IO.File.ReadAllLines("input.txt");
var tiles = new Dictionary<Tile, bool>();
Part1(allLines, tiles);
Part2(tiles);

static void Part1(string[] allLines, Dictionary<Tile, bool> tiles)
{
	foreach (string thisLine in allLines)
	{
		int x = 0;
		int y = 0;
		int cursor = 0;
		while (cursor < thisLine.Length)
		{
			if (thisLine[cursor] == 'e')
			{
				x += 10;
				cursor++;
			}
			else if (thisLine[cursor] == 's')
			{
				cursor++;
				if (thisLine[cursor] == 'e')
				{
					x += 5;
					y += 5;
					cursor++;
				}
				else if (thisLine[cursor] == 'w')
				{
					x -= 5;
					y += 5;
					cursor++;
				}
			}
			else if (thisLine[cursor] == 'w')
			{
				x -= 10;
				cursor++;
			}
			else if (thisLine[cursor] == 'n')
			{
				cursor++;
				if (thisLine[cursor] == 'e')
				{
					x += 5;
					y -= 5;
					cursor++;
				}
				else if (thisLine[cursor] == 'w')
				{
					x -= 5;
					y -= 5;
					cursor++;
				}
			}
		}
		var thisTile = new Tile(x, y);
		if (tiles.TryGetValue(thisTile, out bool isBlack))
		{
			tiles[thisTile] = !isBlack;
		}
		else
		{
			tiles[thisTile] = true;
		}
	}
	int blackCount = tiles.Values.Count(thisBool => thisBool);
	Console.WriteLine($"Part 1: {blackCount}");
}

static void Part2(Dictionary<Tile, bool> tiles)
{
	for (int day = 1; day <= 100; day++)
	{
		var tempTiles = new Dictionary<Tile, bool>();
		var tileKeys = tiles.Keys.ToList();
		foreach (Tile thisTile in tileKeys)
		{
			var neighbors = thisTile.GetNeighbors();
			foreach (Tile thisNeighbor in neighbors)
			{
				if (!tiles.ContainsKey(thisNeighbor))
				{
					tiles[thisNeighbor] = false;
				}
			}
		}
		tileKeys = tiles.Keys.ToList();
		foreach (Tile thisTile in tileKeys)
		{
			var neighbors = thisTile.GetNeighbors();
			int blackNeighborCount = 0;
			foreach (Tile thisNeighbor in neighbors)
			{
				if (tiles.TryGetValue(thisNeighbor, out bool isBlack))
				{
					if (isBlack)
					{
						blackNeighborCount++;
					}
				}
			}
			if (tiles[thisTile] && (blackNeighborCount == 0 || blackNeighborCount > 2))
			{
				tempTiles[thisTile] = false;
			}
			else if (!tiles[thisTile] && blackNeighborCount == 2)
			{
				tempTiles[thisTile] = true;
			}
			else
			{
				tempTiles[thisTile] = tiles[thisTile];
			}	
		}
		tiles = tempTiles;
	}
	int blackCount = tiles.Values.Count(thisBool => thisBool);
	Console.WriteLine($"Part 2: {blackCount}");
}
