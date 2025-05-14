using System.Diagnostics;

Console.WriteLine("Advent of Code 2019, Day 22");
string[] allLines = File.ReadAllLines("input.txt");
Part1(allLines, out long part1Result);
Console.WriteLine($"Part 1 answer: {part1Result}");
Part2(allLines, out long part2Result);
Console.WriteLine($"Part 2 answer: {part2Result}");

static void Part1(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	int position = 2019;
	int size = 10007;
	foreach (string thisLine in allLines)
	{
		if (thisLine == "deal into new stack")
		{
			position = size - 1 - position;
		}
		else if (thisLine.StartsWith("cut"))
		{
			int count = int.Parse(thisLine.Split(' ')[1]);
			position = (position + size - count) % size;
		}
		else if (thisLine.StartsWith("deal with increment"))
		{
			int increment = int.Parse(thisLine.Split(' ')[3]);
			position = (increment * position) % size;
		}
	}
	result = position;
	timer.Stop();
	Console.WriteLine($"Part 1 time: {timer.ElapsedMilliseconds} ms");
}

static void Part2(string[] allLines, out long result)
{
	Stopwatch timer = new ();
	timer.Start();
	Int128 addend = 0;
	Int128 factor = 1;
	Int128 position = 2020;
	long size = 119315717514047;
	Int128 iterations = 101741582076661;
	Int128 inverse;
	for (int i = allLines.Length - 1; i >= 0; i--)
	{
		if (allLines[i] == "deal into new stack")
		{
			addend += 1;
			addend = (addend * (size - 1)) % size;
			factor = (factor * (size - 1)) % size;
		}
		else if (allLines[i].StartsWith("cut"))
		{
			int count = int.Parse(allLines[i].Split(' ')[1]);
			addend = (addend + size + count)  % size;
		}
		else if (allLines[i].StartsWith("deal with increment"))
		{
			int increment = int.Parse(allLines[i].Split(' ')[3]);
			inverse = Euclidean(increment, size);
			addend = (addend * inverse) % size;
			factor = (factor * inverse) % size;
		}
	}
	Int128 adjustedFactor = Power(factor, iterations, size);
	Int128 adjustedAddend = (addend * adjustedFactor) % size;
	Int128 adjustedPosition = (position * adjustedFactor) % size;
	inverse = Euclidean(factor - 1, size);
	result = (long)((adjustedPosition + (adjustedAddend - addend) * inverse) % size);
	if (result < 0)
	{
		result += size;
	}
	timer.Stop();
	Console.WriteLine($"Part 2 time: {timer.ElapsedMilliseconds} ms");
}

static Int128 Euclidean(Int128 a, Int128 b)
{
	Int128 t = 0;
	Int128 newT = 1;
	Int128 r = b;
	Int128 newR = a;
	while (newR != 0) 
	{
		Int128 quotient = r / newR;
		t -= quotient * newT;
		r -= quotient * newR;
		(newT, t) = (t, newT);
		(newR, r) = (r, newR);
	}
	if (t < 0) 
	{
		t += b;
	}
	return t;
}

static Int128 Power(Int128 baseNumber, Int128 exponent, Int128 modulus)
{
	Int128 result = 1;
	baseNumber %= modulus;
	while (exponent > 0)
	{
		if (exponent % 2 == 1)
		{
			result = (result * baseNumber) % modulus;
		}
		exponent /= 2;
		baseNumber = (baseNumber * baseNumber) % modulus;
	}
	return result;
}

