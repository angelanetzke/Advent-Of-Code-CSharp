using Day19;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

static void Part1(string[] allLines)
{
	var theDevice = new Device(allLines);
	var register0 = theDevice.Execute(false);
	Console.WriteLine($"Part 1: {register0}");
}

/*
	By examining the input file, I determined that the program was calculating the sum of all the
	factors of a number. For part 2, it runs very slowly so I terminated the program when it
	had calculated the number (in register 4) and used a faster algorithm to calculate 
	the sum of the factors.
*/
static void Part2(string[] allLines)
{
	var theDevice = new Device(allLines);
	var number = theDevice.Execute(true);
	var sum = SumFactors(number);
	Console.WriteLine($"Part 2: {sum}");
}

static int SumFactors(int number)
{
	int sum = 0;
	for (int i = 1; i <= (int)Math.Round(Math.Sqrt(number)); i++)
	{
		if (number % i == 0)
		{
			sum += i;
			sum += number / i;
		}
	}
	return sum;
}

