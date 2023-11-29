using Dec22;

var allLines = System.IO.File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var theGame = new Game(allLines);
	int finalScore = theGame.Play();
	Console.WriteLine($"Part 1: {finalScore}");
}

static void Part2(string[] allLines)
{
	var theGame = new Game(allLines);
	int finalScore = int.Parse(theGame.PlayRecursive()[0]);
	Console.WriteLine($"Part 2: {finalScore}");
}
