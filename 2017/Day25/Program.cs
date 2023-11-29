using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
var startState = allLines[0][^2];
var stepRegex = new Regex(@"after (?<steps>\d+) steps");
var steps = int.Parse(stepRegex.Match(allLines[1]).Groups["steps"].Value);
var nextMoves 
	= new Dictionary<(char state, char value), (char nextValue, int direction, char nextState)>();
var i = 3;
while (i < allLines.Length)
{
	var thisState = allLines[i][^2];
	var thisValue = allLines[i + 1][^2];
	var nextValue = allLines[i + 2][^2];
	int direction;
	if (allLines[i + 3].Substring(27) == "right.")
	{
		direction = 1;
	}
	else
	{
		direction = -1;
	}
	var nextState = allLines[i + 4][^2];
	nextMoves[(thisState, thisValue)] = (nextValue, direction, nextState);
	thisValue = allLines[i + 5][^2];
	nextValue = allLines[i + 6][^2];
	if (allLines[i + 7].Substring(27) == "right.")
	{
		direction = 1;
	}
	else
	{
		direction = -1;
	}
	nextState = allLines[i + 8][^2];
	nextMoves[(thisState, thisValue)] = (nextValue, direction, nextState);
	i += 10;
}
var currentState = startState;
var currentLocation = 0;
var tape = new Dictionary<int, char>();
for (i = 1; i <= steps; i++)
{
	char currentValue;
	if (tape.ContainsKey(currentLocation))
	{
		currentValue = tape[currentLocation];
	}
	else
	{
		currentValue = '0';
	}
	var thisNextMove = nextMoves[(currentState, currentValue)];
	tape[currentLocation] = thisNextMove.nextValue;
	currentLocation += thisNextMove.direction;
	currentState = thisNextMove.nextState;
}
var count = tape.Values.Count(x => x == '1');
Console.WriteLine($"Part 1: {count}");