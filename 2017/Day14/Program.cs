using Day14;
using System.Text;

var text = File.ReadAllLines("input.txt")[0];
var grid = new List<string>();
Circle circle;
var builder = new StringBuilder();
for (int row = 0; row <= 127; row++)
{
	builder.Clear();
	circle = new Circle(256);
	var thisInput = text + "-" + row.ToString();
	var thisHash = circle.Execute(thisInput);
	foreach (char thisHexChar in thisHash)
	{
		var thisCharAsBinary = Convert.ToString(Convert.ToInt32(thisHexChar.ToString(), 16), 2).PadLeft(4, '0');
		builder.Append(thisCharAsBinary);
	}
	grid.Add(builder.ToString());
}
Part1(grid);
Part2(grid);

void Part1(List<string> grid)
{
	var usedCount = 0;
	foreach (string thisString in grid)
	{
		usedCount += thisString.Count(x => x == '1');
	}
	Console.WriteLine($"Part 1: {usedCount}");
}

void Part2(List<string> grid)
{
	var regions = new Dictionary<(int row, int column), int?>();
	for (int row = 0; row < grid.Count; row++)
	{
		for (int column = 0; column < grid[row].Length; column++)
		{
			if (grid[row][column] == '1')
			{
				regions[(row, column)] = null;
			}
		}
	}
	int nextID = 0;
	foreach ((int, int) thisKey in regions.Keys)
	{
		if (regions[thisKey] == null)
		{
			AssignID(nextID, thisKey, regions);
			nextID++;
		}
	}
	Console.WriteLine($"Part 2: {nextID}");
}

void AssignID(int id, (int row, int column) key, Dictionary<(int row, int column), int?> regions)
{
	regions[key] = id;
	foreach ((int deltaRow, int deltaColumn) thisOffset 
		in new (int, int)[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
	{
		var next = (key.row + thisOffset.deltaRow, key.column + thisOffset.deltaColumn);
		if (regions.ContainsKey(next) && regions[next] == null)
		{
			AssignID(id, next, regions);
		}		
	}
}
