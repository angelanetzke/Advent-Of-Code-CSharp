using Dec08;

var inputLine = File.ReadAllText("input.txt");
Part1(inputLine);
Part2(inputLine);

static void Part1(string inputLine)
{
	var theImage = new Image(inputLine);
	var layerWithLeastZeros = -1;
	var leastZeros = int.MaxValue;
	for (int i = 0; i < theImage.GetLayerCount(); i++)
	{
		int thisLayerZeros = theImage.CountDigits(i, 0);
		if (thisLayerZeros < leastZeros)
		{
			layerWithLeastZeros = i;
			leastZeros = thisLayerZeros;
		}
	}
	var answer = theImage.CountDigits(layerWithLeastZeros, 1) * theImage.CountDigits(layerWithLeastZeros, 2);
	Console.WriteLine($"Part 1: {answer}");
}

static void Part2(string inputLine)
{
	var theImage = new Image(inputLine);
	Console.WriteLine(theImage);
}
