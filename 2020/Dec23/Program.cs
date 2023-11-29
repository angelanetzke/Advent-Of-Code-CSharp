using Dec23;

var allLines = System.IO.File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var theGame = new Game(allLines[0], false);
	theGame.Play(100);
	string labels = theGame.GetLabels();
	Console.WriteLine($"Part 1: {labels}");
}

static void Part2(string[] allLines)
{
	var theGame = new Game(allLines[0], true);
	theGame.Play(10000000);
	string labels = theGame.GetLabels();
	Console.WriteLine($"Part 2: {labels}");
}
