using Dec17;
using System.Text;
using System.Text.RegularExpressions;

var inputLine = File.ReadAllLines("input.txt")[0];
var outputMap = Part1(inputLine);
Part2(outputMap, inputLine);

static List<string> Part1(string inputLine)
{
	var theComputer = new IntcodeComputer();
	theComputer.SetMemory(inputLine);
	theComputer.Run();
	var outputMap = new List<string>();
	var builder = new StringBuilder();
	foreach (long thisOutput in theComputer.GetOutput())
	{
		if (thisOutput == 10L && builder.Length > 0)
		{
			outputMap.Add(builder.ToString());
			builder.Clear();
		}
		else
		{
			builder.Append((char)thisOutput);
		}
	}
	var alignmentParameterSum = 0;
	for (int row = 1; row < outputMap.Count - 1; row++)
	{
		for (int column = 1; column < outputMap[row].Length - 1; column++)
		{
			if (outputMap[row - 1][column] == '#'
				&& outputMap[row + 1][column] == '#'
				&& outputMap[row][column - 1] == '#'
				&& outputMap[row][column + 1] == '#'
				&& outputMap[row][column] == '#')
			{
				alignmentParameterSum += row * column;
			}
		}
	}
	Console.WriteLine($"Part 1: {alignmentParameterSum}");
	return outputMap;
}

static void Part2(List<string> outputMap, string inputLine)
{
	var row = 0;
	var column = 6;
	int corridorCount;
	var direction = 'N';
	var builder = new StringBuilder();
	do
	{
		if (direction == 'N' && column >= 1 && outputMap[row][column - 1] == '#')
		{
			direction = 'W';
			builder.Append('L');
		}
		else if (direction == 'N' && column <= outputMap[row].Length - 2 && outputMap[row][column + 1] == '#')
		{
			direction = 'E';
			builder.Append('R');
		}
		else if (direction == 'S' && column >= 1 && outputMap[row][column - 1] == '#')
		{
			direction = 'W';
			builder.Append('R');
		}
		else if (direction == 'S' && column <= outputMap[row].Length - 2 && outputMap[row][column + 1] == '#')
		{
			direction = 'E';
			builder.Append('L');
		}
		else if (direction == 'W' && row >= 1 && outputMap[row - 1][column] == '#')
		{
			direction = 'N';
			builder.Append('R');
		}
		else if (direction == 'W' && row <= outputMap.Count - 2 && outputMap[row + 1][column] == '#')
		{
			direction = 'S';
			builder.Append('L');
		}
		else if (direction == 'E' && row >= 1 && outputMap[row - 1][column] == '#')
		{
			direction = 'N';
			builder.Append('L');
		}
		else if (direction == 'E' && row <= outputMap.Count - 2 && outputMap[row + 1][column] == '#')
		{
			direction = 'S';
			builder.Append('R');
		}
		builder.Append(',');
		var moveCount = 0;
		var nextRow = row;
		var nextColumn = column;
		bool isEndFound;
		do
		{
			if (direction == 'N')
			{
				nextRow--;
			}
			else if (direction == 'S')
			{
				nextRow++;
			}
			else if (direction == 'W')
			{
				nextColumn--;
			}
			else // direction == 'E"
			{
				nextColumn++;
			}
			isEndFound = nextRow < 0 || nextRow >= outputMap.Count
				|| nextColumn < 0 || nextColumn >= outputMap[row].Length
				|| outputMap[nextRow][nextColumn] != '#';
			if (!isEndFound)
			{
				row = nextRow;
				column = nextColumn;
				moveCount++;
			}			
		} while (!isEndFound);
		builder.Append(moveCount.ToString() + ',');
		corridorCount = GetCorridorCount(row, column, outputMap);
	} while (corridorCount > 1);
	//Console.WriteLine(builder.ToString()); // print to manually derive mainRoutine and function values
	var mainRoutine = "A,C,A,C,B,C,B,A,C,B";
	var functionA = "R,4,R,10,R,8,R,4";
	var functionB = "R,4,L,12,R,6,L,12";
	var functionC = "R,10,R,6,R,4";
	var theComputer = new IntcodeComputer();
	theComputer.SetMemory(inputLine);
	theComputer.SetValue(0L, 2L);
	foreach (char thisChar in mainRoutine)
	{
		theComputer.AddInput((long)thisChar);
	}
	theComputer.AddInput(10L);
	foreach (char thisChar in functionA)
	{
		theComputer.AddInput((long)thisChar);
	}
	theComputer.AddInput(10L);
	foreach (char thisChar in functionB)
	{
		theComputer.AddInput((long)thisChar);
	}
	theComputer.AddInput(10L);
	foreach (char thisChar in functionC)
	{
		theComputer.AddInput((long)thisChar);
	}
	theComputer.AddInput(10L);
	theComputer.AddInput((long)'n');
	theComputer.AddInput(10L);
	theComputer.Run();
	foreach (long thisOutput in theComputer.GetOutput())
	{
		if (thisOutput == 10L)
		{
			Console.WriteLine();
		}
		else if ((long)' ' <= thisOutput && thisOutput <= (long)'~')
		{
			Console.Write((char)thisOutput);
		}
		else
		{
			Console.WriteLine(thisOutput);
		}
	}
}

static int GetCorridorCount(int row, int column, List<string> outputMap)
{
	var count = 0;
	if (row >= 1 && outputMap[row - 1][column] == '#')
	{
		count++;
	}
	if (row <= outputMap.Count - 2 && outputMap[row + 1][column] == '#')
	{
		count++;
	}
	if (column >= 1 && outputMap[row][column - 1] == '#')
	{
		count++;
	}
	if (column <= outputMap[row].Length -2 && outputMap[row][column + 1] == '#')
	{
		count++;
	}
	return count;
}
