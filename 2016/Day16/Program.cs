string input = File.ReadAllLines("input.txt")[0];
Console.WriteLine($"Part 1: {Part1(input)}");
Console.WriteLine($"Part 2: {Part2(input)}");

static string Part1(string input)
{
	return Solve(input, 272);
}

static string Part2(string input)
{
	return Solve(input, 35651584);
}

static string Solve(string input, int length)
{
	int[] data = input.Split(' ')[0].ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
	while (data.Length < length)
	{
		int[] newData = new int[data.Length * 2 + 1];
		newData[data.Length] = 0;
		for (int i = 0, j = newData.Length - 1; i < data.Length; i++, j--)
		{
			newData[i] = data[i];
			newData[j] = data[i] == 0 ? 1 : 0;
		}
		data = newData;
	}
	int[] checkSum = GetCheckSum(data.Take(length).ToArray());
	while (checkSum.Length % 2 == 0)
	{
		checkSum = GetCheckSum(checkSum);
	}
	return string.Join("", checkSum);
}

static int[] GetCheckSum(int[] data)
{
	int[] checkSum = new int[data.Length / 2];
	for (int i = 1; i < data.Length; i += 2)
	{
		checkSum[i / 2] = data[i - 1] == data[i] ? 1 : 0;
	}
	return checkSum;
}
