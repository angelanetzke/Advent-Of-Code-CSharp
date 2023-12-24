string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");

static int Part1(string[] allLines)
{
	List<(long, long, long, long, long, long)> hailstones = [];
	foreach (string thisLine in allLines)
	{
		string[] position = thisLine.Split(" @ ")[0].Split(", ");
		string[] velocity = thisLine.Split(" @ ")[1].Split(", ");
		hailstones.Add((
			long.Parse(position[0]), 
			long.Parse(position[1]), 
			long.Parse(position[2]), 
			long.Parse(velocity[0]), 
			long.Parse(velocity[1]), 
			long.Parse(velocity[2])));
	}
	int count = 0;
	for (int i = 0; i < hailstones.Count - 1; i++)
	{
		for (int j = i + 1; j < hailstones.Count; j++)
		{
			count += DoesIntersectInRange(hailstones[i], hailstones[j], 200000000000000, 400000000000000) ? 1 : 0;
		}		
	}
	return count;
}

static bool DoesIntersectInRange((long px, long py, long pz, long vx, long vy, long vz) hailstone1, 
	(long px, long py, long pz, long vx, long vy , long vz) hailstone2, float min, float max)
{
	if (-hailstone1.vy * hailstone2.vx + hailstone1.vx * hailstone2.vy == 0L)
	{
		return false;
	}
	float time2 = (float)(-hailstone1.vy * hailstone1.px + hailstone1.vx * hailstone1.py
		+ hailstone1.vy * hailstone2.px - hailstone1.vx * hailstone2.py)
		/ (-hailstone1.vy * hailstone2.vx + hailstone1.vx * hailstone2.vy);
	if (time2 < 0F)
	{
		return false;
	}
	float x = hailstone2.px + hailstone2.vx * time2;
	float y = hailstone2.py + hailstone2.vy * time2;
	float time1 = (x - hailstone1.px) / hailstone1.vx;
	if (time1 < 0F)
	{
		return false;
	}
	return min <= x && x <= max && min <= y && y <= max;
}