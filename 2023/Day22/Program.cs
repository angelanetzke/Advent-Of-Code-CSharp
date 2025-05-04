using Day22;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines, out Dictionary<Brick, List<Brick>> supporting)}");
Console.WriteLine($"Part 2: {Part2(allLines, supporting)}");

static int Part1(string[] allLines, out Dictionary<Brick, List<Brick>> supporting)
{
	List<Brick> bricks = [];
	foreach (string thisLine in allLines)
	{
		bricks.Add(new Brick(thisLine));
	}
	bricks.Sort();
	Brick ground = Brick.GetGround();
	bool hasBrickFallen = true;
	bool canThisBrickFall;
	while (hasBrickFallen)
	{
		hasBrickFallen = false;		
		for (int i = 0; i < bricks.Count; i++)
		{
			canThisBrickFall = true;
			for (int j = i + 1; j < bricks.Count; j++)
			{
				if (i == j)
				{
					continue;
				}
				canThisBrickFall = canThisBrickFall && bricks[i].CanFall(bricks[j]);
			}
			canThisBrickFall = canThisBrickFall && bricks[i].CanFall(ground);
			if (canThisBrickFall)
			{
				bricks[i].Fall();
				hasBrickFallen = true;
			}
		}
	}
	supporting = [];
	for (int i = 0; i < bricks.Count; i++)
	{
		supporting[bricks[i]] = []; 
		for (int j = 0; j < i; j++)
		{
			if (bricks[i].IsSupporting(bricks[j]))
			{
				supporting[bricks[i]].Add(bricks[j]);
			}
		}
	}
	int destroyableBrickCount = 0;
	foreach (Brick thisSupportingBrick in bricks)
	{
		bool canDestroy = true;
		foreach (Brick thisSupportedBrick in supporting[thisSupportingBrick])
		{
			int otherSupportingBricksCount = supporting
				.Where(x => x.Value.Contains(thisSupportedBrick))
				.Select(x => x.Key)
				.Where(x => !x.Equals(thisSupportingBrick))
				.Count();
			if (otherSupportingBricksCount == 0)
			{
				canDestroy = false;
			}
		}
		destroyableBrickCount += canDestroy ? 1 : 0;
	}
	return destroyableBrickCount;
}

static int Part2(string[] allLines, Dictionary<Brick, List<Brick>> supporting)
{
	int count = 0;
	HashSet<Brick> fallingBricks = [];
	foreach (Brick thisBrick in supporting.Keys)
	{
		fallingBricks.Clear();
		GetFallingBricks(thisBrick, supporting, fallingBricks);
		count += fallingBricks.Count;
	}
	return count;
}

static void GetFallingBricks(Brick baseBrick, Dictionary<Brick, List<Brick>> supporting, HashSet<Brick> included)
{
	foreach (Brick thisSupportedBrick in supporting[baseBrick])
	{
		int otherSupportingBricksCount = supporting
				.Where(x => x.Value.Contains(thisSupportedBrick))
				.Select(x => x.Key)
				.Where(x => !x.Equals(baseBrick))
				.Where(x => !included.Contains(x))
				.Count();
		if (otherSupportingBricksCount == 0)
		{
			included.Add(thisSupportedBrick);
			GetFallingBricks(thisSupportedBrick, supporting, included);
		}		
	}
}



