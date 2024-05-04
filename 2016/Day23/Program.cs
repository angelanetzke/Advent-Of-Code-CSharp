string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static long Part1(string[] allLines)
{
	return Execute(7, allLines);
}

static long Part2(string[] allLines)
{
	return Execute(12, allLines);
}

static long Execute(int eggCount, string[] allLines)
{
	string[] assembunny = new string[allLines.Length];
	Array.Copy(allLines, assembunny, allLines.Length);
	Dictionary<string, long> registers = new ()
	{
		{ "a", eggCount },
		{ "b", 0 },
		{ "c", 0 },
		{ "d", 0 }
	};
	long i = 0;
	while (i < assembunny.Length)
	{
		string[] parts = assembunny[i].Split(' ');
		if (i == 5)
		{
			registers["a"] += registers["b"] * registers["d"];
			registers["c"] = 0;
			registers["d"] = 0;
			i = 10;
		}
		else if (i == 21)
		{
			registers["a"] += registers["c"] * registers["d"];
			i = assembunny.Length;
		}
		else if (parts[0] == "cpy")
		{
			if (registers.ContainsKey(parts[2]))
			{
				if (long.TryParse(parts[1], out long value))
				{
					registers[parts[2]] = value;
				}
				else
				{
					registers[parts[2]] = registers[parts[1]];
				}
			}			
			i++;
		}
		else if (parts[0] == "inc")
		{
			registers[parts[1]]++;
			i++;
		}
		else if (parts[0] == "dec")
		{
			registers[parts[1]]--;
			i++;
		}
		else if (parts[0] == "jnz")
		{
			if (long.TryParse(parts[1], out long value))
			{
				if (value != 0)
				{
					if (long.TryParse(parts[2], out long jump))
					{
						i += jump;
					}
					else
					{
						i += registers[parts[2]];
					}
				}
				else
				{
					i++;
				}
			}
			else
			{
				if (registers[parts[1]] != 0)
				{
					if (long.TryParse(parts[2], out long jump))
					{
						i += jump;
					}
					else
					{
						i += registers[parts[2]];
					}
				}
				else
				{
					i++;
				}
			}
		}
		else if (parts[0] == "tgl")
		{
			long target = 0;
			if (long.TryParse(parts[1], out long value))
			{
				target = i + value;
			}
			else
			{
				target = i + registers[parts[1]];
			}
			if (target >= 0 && target < assembunny.Length)
			{
				string[] targetParts = assembunny[target].Split(' ');
				if (targetParts.Length == 2)
				{
					if (targetParts[0] == "inc")
					{
						assembunny[target] = $"dec {targetParts[1]}";
					}
					else
					{
						assembunny[target] = $"inc {targetParts[1]}";
					}
				}
				else if (targetParts.Length == 3)
				{
					if (targetParts[0] == "jnz")
					{
						assembunny[target] = $"cpy {targetParts[1]} {targetParts[2]}";
					}
					else
					{
						assembunny[target] = $"jnz {targetParts[1]} {targetParts[2]}";
					}
				}
			}
			i++;
		}
	}
	return registers["a"];
}
