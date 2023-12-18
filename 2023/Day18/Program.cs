string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static long Part1(string[] allLines)
{
	List<(long, long)> vertices = [];
	long row = 0;
	long column = 0;	
	foreach (string thisLine in allLines)
	{
		int count = int.Parse(thisLine.Split(' ')[1]);
		char direction = thisLine[0];
		switch (direction)
		{
			case 'U':
				row -= count;
				break;
			case 'D':
				row += count;
				break;
			case 'L':
				column -= count;
				break;
			case 'R':
				column += count;
				break;
		}
		vertices.Add((row, column));
	}
	return GetTrenchSize(vertices);
}

static long Part2(string[] allLines)
{
	List<(long, long)> vertices = [];
	long row = 0;
	long column = 0;	
	foreach (string thisLine in allLines)
	{
		long count = Convert.ToInt64(thisLine.Split('#')[1][..5], 16);
		char direction = thisLine.Split('#')[1][5];
		switch (direction)
		{
			case '3':
				row -= count;
				break;
			case '1':
				row += count;
				break;
			case '2':
				column -= count;
				break;
			case '0':
				column += count;
				break;
		}
		vertices.Add((row, column));
	}
	return GetTrenchSize(vertices);
}

static long GetTrenchSize(List<(long, long)> vertices)
{
	vertices.Reverse();
	long interiorSize = 0L;
	long perimeterSize = 0L;
	int vertexIndex = 0;
	do
	{
		int nextVertexIndex = (vertexIndex + 1) % vertices.Count;
		long a = vertices[vertexIndex].Item1;
		long b = vertices[nextVertexIndex].Item1;
		long c = vertices[vertexIndex].Item2;
		long d = vertices[nextVertexIndex].Item2;
		interiorSize += a * d - b * c;
		perimeterSize += Math.Abs(a - b) + Math.Abs(c - d);
		vertexIndex = nextVertexIndex;
	} while (vertexIndex != 0);
	return interiorSize / 2 + perimeterSize / 2 + 1;
}
