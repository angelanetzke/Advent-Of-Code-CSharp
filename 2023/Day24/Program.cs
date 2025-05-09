using Day24;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Part2(allLines);

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

static void Part2(string[] allLines)
{
	List<(long px, long py, long pz, long vx, long vy, long vz)> hailstones = [];
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
	double[,] xyValues = new double[4, 4];
	double[,] xzValues = new double[4, 4];
	double[,] xySums = new double[4, 1];
	double[,] xzSums = new double[4, 1];
	for (int i = 0; i < 4; i++)
	{
		xyValues[i, 0] = hailstones[i + 1].vy - hailstones[i].vy;
		xyValues[i, 1] = hailstones[i].vx - hailstones[i + 1].vx;
		xyValues[i, 2] = hailstones[i].py - hailstones[i + 1].py;
		xyValues[i, 3] = hailstones[i + 1].px - hailstones[i].px;
		xySums[i, 0] = hailstones[i + 1].px * hailstones[i + 1].vy
			- hailstones[i + 1].py * hailstones[i + 1].vx
			- hailstones[i].px * hailstones[i].vy
			+ hailstones[i].py * hailstones[i].vx;
		xzValues[i, 0] = hailstones[i + 1].vz - hailstones[i].vz;
		xzValues[i, 1] = hailstones[i].vx - hailstones[i + 1].vx;
		xzValues[i, 2] = hailstones[i].pz - hailstones[i + 1].pz;
		xzValues[i, 3] = hailstones[i + 1].px - hailstones[i].px;
		xzSums[i, 0] = hailstones[i + 1].px * hailstones[i + 1].vz
			- hailstones[i + 1].pz * hailstones[i + 1].vx
			- hailstones[i].px * hailstones[i].vz
			+ hailstones[i].pz * hailstones[i].vx;
		
	}	
	
	Matrix xyInverse = new Matrix(xyValues).GetInverse();
	Matrix yzInverse = new Matrix(xzValues).GetInverse();
	Matrix xyResult = xyInverse.Multiply(new Matrix(xySums));
	Matrix yzResult = yzInverse.Multiply(new Matrix(xzSums));
	Console.WriteLine("Part 2:");
	Console.WriteLine($"X: {xyResult.GetValue(0, 0):f2}");
	Console.WriteLine($"Y: {xyResult.GetValue(1, 0):f2}");
	Console.WriteLine($"Z: {yzResult.GetValue(1, 0):f2}");
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