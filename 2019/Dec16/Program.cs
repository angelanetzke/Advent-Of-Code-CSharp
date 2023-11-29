using Dec16;

var inputLine = File.ReadAllLines("input.txt")[0];
Part1(inputLine);
Part2(inputLine);

static void Part1(string inputLine)
{
	var theFFT = new FFT(inputLine);
	var finalString = "";
	for (int i = 1; i <= 100; i++)
	{
		finalString = theFFT.NextPhase();
	}
	Console.WriteLine($"Part 1: {finalString}");
}

static void Part2(string inputLine)
{
	var theFFT = new LongFFT(inputLine);
	Console.WriteLine($"Part 2: {theFFT.FinalResult()}");
}