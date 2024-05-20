using Dec23;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static int Part1(string[] allLines)
{
	char[] roomA = [allLines[2][3], allLines[3][3]];
	char[] roomB = [allLines[2][5], allLines[3][5]];
	char[] roomC = [allLines[2][7], allLines[3][7]];
	char[] roomD = [allLines[2][9], allLines[3][9]];
	Burrow startBurrow = new Burrow([roomA, roomB, roomC, roomD]);
	return startBurrow.CountSteps();
}

static int Part2(string[] allLines)
{
	char[] roomA = [allLines[2][3], 'D', 'D', allLines[3][3]];
	char[] roomB = [allLines[2][5], 'C', 'B', allLines[3][5]];
	char[] roomC = [allLines[2][7], 'B', 'A', allLines[3][7]];
	char[] roomD = [allLines[2][9], 'A', 'C', allLines[3][9]];
	Burrow startBurrow = new Burrow([roomA, roomB, roomC, roomD]);
	return startBurrow.CountSteps();
}
