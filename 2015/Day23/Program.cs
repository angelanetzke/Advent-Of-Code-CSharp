string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static int Part1(string[] allLines)
{
	return Execute(allLines);
}

static int Part2(string[] allLines)
{
	return Execute(allLines, 1);
}

static int Execute(string[] allLines, int value = 0)
{
	Dictionary<char, int> registers = [];
	registers['a'] = value;
	registers['b'] = 0;
	int i = 0;
	while (0 <= i && i < allLines.Length)
	{
		string[] parts = allLines[i].Split(' ');
		switch (parts[0])
		{
			case "hlf":
				registers[parts[1][0]] /= 2;
				i++;
				break;
			case "tpl":
				registers[parts[1][0]] *= 3;
				i++;
				break;
			case "inc":
				registers[parts[1][0]]++;
				i++;
				break;
			case "jmp":
				i += int.Parse(parts[1]);
				break;
			case "jie":
				if (registers[parts[1][0]] % 2 == 0)
				{
					i += int.Parse(parts[2]);
				}
				else
				{
					i++;
				}
				break;
			case "jio":
				if (registers[parts[1][0]] == 1)
				{
					i += int.Parse(parts[2]);
				}
				else
				{
					i++;
				}
				break;
			default:
				throw new Exception("Invalid instruction");
		}
	}
	return registers['b'];
}