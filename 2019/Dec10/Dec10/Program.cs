using Dec10;

var allLines = File.ReadAllLines("input.txt");
var theField = new AsteroidField(allLines);
Part1(theField);
Part2(theField);

static void Part1(AsteroidField theField)
{
	int maxAsteroids = theField.GetMaxViewableAsteroids();
	Console.WriteLine($"Part 1: {maxAsteroids}");
}

static void Part2(AsteroidField theField)
{
	var lastDestroyed = theField.DestroyAsteroids(200);
	if (lastDestroyed != null)
	{
		var answer = 100 * lastDestroyed.Value.Item1 + lastDestroyed.Value.Item2;
		Console.WriteLine($"Part 2: {answer}");
	}
}
