Console.WriteLine($"Part 1: {Part1()}");
Console.WriteLine($"Part 2: {Part2()}");

static long Part1()
{
	long a = 1;
	long b = 1;
	long c = 0;
	long d = 26;
	while (d != 0)
	{
		c = a;
		while (b != 0)
		{			
			a++;
			b--;
		}
		b = c;
		d--;
	}
	c = 19;
	while (c != 0)
	{
		d = 14;
		while (d != 0)
		{
			a++;
			d--;
		}
		c--;
	}
	return a;
}

static long Part2()
{
	long a = 1;
	long b = 1;
	long c = 1;
	long d = 26;
	c = 7;
	while (c != 0)
	{
		d++;
		c--;
	}
	while (d != 0)
	{
		c = a;
		while (b != 0)
		{			
			a++;
			b--;
		}
		b = c;
		d--;
	}
	c = 19;
	while (c != 0)
	{
		d = 14;
		while (d != 0)
		{
			a++;
			d--;
		}
		c--;
	}
	return a;
}

