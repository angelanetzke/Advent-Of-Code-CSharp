string input = File.ReadAllLines("input.txt")[0];
Console.WriteLine($"Part 1: {Part1(input)}");
Console.WriteLine($"Part 2: {Part2(input)}");

static int Part1(string input)
{
	int x = 0;
	int y = 0;
	HashSet<(int, int)> visited = [];
	visited.Add((0,0));
	foreach (char thisDirection in input)
	{
		switch (thisDirection)
		{
			case '^':
				y--;
				break;
			case 'v':
				y++;
				break;
			case '>':
				x++;
				break;
			case '<':
				x--;
				break;
		}
		visited.Add((x, y));
	}
	return visited.Count;
}

static int Part2(string input)
{
	int santaX = 0;
	int santaY = 0;
	int robotX = 0;
	int robotY = 0;
	HashSet<(int, int)> visited = [];
	visited.Add((0,0));
	for (int i = 0; i < input.Length; i += 2)
	{
		switch (input[i])
		{
			case '^':
				santaY--;
				break;
			case 'v':
				santaY++;
				break;
			case '>':
				santaX++;
				break;
			case '<':
				santaX--;
				break;
		}
		switch (input[i + 1])
		{
			case '^':
				robotY--;
				break;
			case 'v':
				robotY++;
				break;
			case '>':
				robotX++;
				break;
			case '<':
				robotX--;
				break;
		}
		visited.Add((santaX, santaY));
		visited.Add((robotX, robotY));
	}
	return visited.Count;
}