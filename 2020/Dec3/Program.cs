using Dec3;

var allLines = System.IO.File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var grid = new TreeGrid(allLines);
	long treeCount = grid.CountTrees(3, 1);
	Console.WriteLine($"Part 1: {treeCount}");
}

static void Part2(string[] allLines)
{
	var grid = new TreeGrid(allLines);
	var xSlopes = new int[] { 1, 3, 5, 7, 1 };
	var ySlopes = new int[] { 1, 1, 1, 1, 2 };
	long product = 1L;
	for (int i = 0; i < xSlopes.Length; i++)
	{
		product *= grid.CountTrees(xSlopes[i], ySlopes[i]);
	}
	Console.WriteLine($"Part 2: {product}");
}
