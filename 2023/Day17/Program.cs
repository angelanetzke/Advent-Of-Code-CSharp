using Day17;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine("start");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static int Part1(string[] allLines)
{
  Map map = new (allLines);
  return map.GetMinHeatLoss(1, 3);
}

static int Part2(string[] allLines)
{
  Map map = new (allLines);
  return map.GetMinHeatLoss(4, 10);
}

