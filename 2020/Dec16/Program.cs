using Dec16;

var allLines = System.IO.File.ReadAllLines("input.txt");
var collection = new TicketCollection(allLines);
Part1(collection);
Part2(collection);

static void Part1(TicketCollection collection)
{
	int errorRate = collection.GetErrorRate();
	Console.WriteLine($"Part 1: {errorRate}");
}

static void Part2(TicketCollection collection)
{
	long part2Answer = collection.GetDepartureProduct();
	Console.WriteLine($"Part 2: {part2Answer}");
}