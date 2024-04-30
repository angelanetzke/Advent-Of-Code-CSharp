string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static long Part1(string[] allLines)
{
	long[] remainders = new long[allLines.Length];
	long[] moduli = new long[allLines.Length];
	for (int i = 0; i < allLines.Length; i++)
	{
		string[] parts = allLines[i].Split(' ');
		moduli[i] = long.Parse(parts[3]);
		remainders[i] = moduli[i] - (long.Parse(parts[11][..^1]) + long.Parse(parts[1][1..])) % moduli[i];	
	}
	return SolveCRT(remainders, moduli);
}

static long Part2(string[] allLines)
{
	long[] remainders = new long[allLines.Length + 1];
	long[] moduli = new long[allLines.Length + 1];
	for (int i = 0; i < allLines.Length; i++)
	{
		string[] parts = allLines[i].Split(' ');
		moduli[i] = long.Parse(parts[3]);
		remainders[i] = moduli[i] - (long.Parse(parts[11][..^1]) + long.Parse(parts[1][1..])) % moduli[i];	
	}
	moduli[^1] = 11;
	remainders[^1] = moduli[^1] - (allLines.Length + 1) % moduli[^1];
	return SolveCRT(remainders, moduli);
}

static long SolveCRT(long[] remainders, long[] moduli)
{
	long product = 1;
	foreach (long modulus in moduli)
	{
		product *= modulus;
	}
	long result = 0;
	for (int i = 0; i < remainders.Length; i++)
	{
		long partialProduct = product / moduli[i];
		result += remainders[i] * ModularInverse(partialProduct, moduli[i]) * partialProduct;
	}
	return result % product;
}

static long ModularInverse(long a, long m)
{
	long m0 = m;
	long y = 0, x = 1;
	if (m == 1)
	{
		return 0;
	}
	while (a > 1)
	{
		long q = a / m;
		long t = m;
		m = a % m;
		a = t;
		t = y;
		y = x - q * y;
		x = t;
	}
	if (x < 0)
	{
		x += m0;
	}
	return x;
}
