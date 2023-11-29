using Day14;

var inputLine = File.ReadAllLines("input.txt")[0];
Part1(inputLine);
Part2(inputLine);

static void Part1(string inputLine)
{
	var iterationCount = int.Parse(inputLine);
	var theBoard = new RecipeBoard(3, 7, iterationCount, "", true);
	string? result;
	do
	{
		result = theBoard.NextIteration();
	} while(result == null);
	Console.WriteLine($"Part 1: {result}");
}

static void Part2(string inputLine)
{
	var theBoard = new RecipeBoard(3, 7, 0, inputLine, false);
	string? result;
	do
	{
		result = theBoard.NextIteration();
	} while(result == null);
	Console.WriteLine($"Part 1: {result}");
}
