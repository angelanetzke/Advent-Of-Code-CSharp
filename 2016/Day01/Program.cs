var text = File.ReadAllLines("input.txt")[0];
var part2Distance = -1;
var visited = new HashSet<(int, int)>() { (0, 0) };
var part2Found = false;
var instructions = text.Split(", ");
var moves = new Dictionary<char, (int, int)>()
{
	{ 'N', (0, -1) },
	{ 'S', (0, 1) },
	{ 'W', (-1, 0) },
	{ 'E', (1, 0) }
};
var turnLeft = new Dictionary<char, char>()
{
	{ 'N', 'W' },
	{ 'S', 'E' },
	{ 'W', 'S' },
	{ 'E', 'N' }
};
var turnRight = new Dictionary<char, char>()
{
	{ 'N', 'E' },
	{ 'S', 'W' },
	{ 'W', 'N' },
	{ 'E', 'S' }
};
var currentLocation = (0, 0);
var currentDirection = 'N';
foreach (string thisInstruction in instructions)
{
	if (thisInstruction[0] == 'L')
	{
		currentDirection = turnLeft[currentDirection];
	}
	else
	{
		currentDirection = turnRight[currentDirection];
	}
	var steps = int.Parse(thisInstruction[1..]);
	for (int i = 1; i <= steps; i++)
	{
		currentLocation = (currentLocation.Item1 + moves[currentDirection].Item1,
			currentLocation.Item2 + moves[currentDirection].Item2);
		if (part2Found)
		{
			continue;
		}
		if (visited.Contains(currentLocation))
		{
			part2Distance = Math.Abs(currentLocation.Item1) + Math.Abs(currentLocation.Item2);
			part2Found = true;
		}
		visited.Add(currentLocation);		
	}
}
var distance = Math.Abs(currentLocation.Item1) + Math.Abs(currentLocation.Item2);
Console.WriteLine($"Part 1: {distance}");
Console.WriteLine($"Part 2: {part2Distance}");


