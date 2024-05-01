using System.Text;

string input = File.ReadAllLines("input.txt")[0];
Console.WriteLine($"Part 1: {Part1(input)}");
Console.WriteLine($"Part 2: {Part2(input)}");

static long Part1(string input)
{
	return CountSafe(input, 40);
}

static long Part2(string input)
{
	return CountSafe(input, 400000);
}

static long CountSafe(string input, int count)
{
	Dictionary<string, string> cache = [];
	string lastLine = input;
	long safeCount = lastLine.Count(x => x == '.');
	StringBuilder builder = new ();
	for (int i = 1; i <= count - 1; i++)
	{
		if (cache.TryGetValue(lastLine, out string? value))
		{
			lastLine = value;
		}
		else
		{
			builder.Clear();
			for (int j = 0; j < lastLine.Length; j++)
			{
				bool isTrap = false;
				if (j == 0)
				{
					isTrap = lastLine[j + 1] == '^';
				}
				else if (j == lastLine.Length - 1)
				{
					isTrap = lastLine[j - 1] == '^';
				}
				else if (lastLine[j - 1] == '^' && lastLine[j] == '^' && lastLine[j + 1] == '.' )
				{
					isTrap = true;
				}
				else if (lastLine[j - 1] == '.' && lastLine[j] == '^' && lastLine[j + 1] == '^' )
				{
					isTrap = true;
				}
				else if (lastLine[j - 1] == '^' && lastLine[j] == '.' && lastLine[j + 1] == '.' )
				{
					isTrap = true;
				}
				else if (lastLine[j - 1] == '.' && lastLine[j] == '.' && lastLine[j + 1] == '^' )
				{
					isTrap = true;
				}
				if (isTrap)
				{
					builder.Append('^');
				}
				else
				{
					builder.Append('.');			
				}
			}
			cache[lastLine] = builder.ToString();
			lastLine = builder.ToString();
		}
		safeCount += lastLine.Count(x => x == '.');
	}
	return safeCount;
}
