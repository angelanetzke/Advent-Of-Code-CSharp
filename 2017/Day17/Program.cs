using Day17;

var steps = int.Parse(File.ReadAllLines("input.txt")[0]);
Part1(steps);
Part2(steps);

void Part1(int steps)
{
	var result = Spinlock.Execute(steps);
	Console.WriteLine($"Part 1: {result}");
}

void Part2(int steps)
{
	var result = AngrySpinlock.Execute(steps);
	Console.WriteLine($"Part 2: {result}");
}