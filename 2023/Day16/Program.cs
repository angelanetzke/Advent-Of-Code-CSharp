using Day16;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static int Part1(string[] allLines)
{
  Contraption c = new (allLines);
  return c.GetEnergizedCount(0, 0, 'E');
}

static int Part2(string[] allLines)
{
  Contraption c = new (allLines);
  int maxEnergized = 0;
  for (int row = 0; row < allLines.Length; row++)
  {
    maxEnergized = Math.Max(maxEnergized, c.GetEnergizedCount(row, 0, 'E'));
    maxEnergized = Math.Max(maxEnergized, c.GetEnergizedCount(row, allLines[0].Length - 1, 'W'));
  }
  for (int column = 0; column < allLines.Length; column++)
  {
    maxEnergized = Math.Max(maxEnergized, c.GetEnergizedCount(0, column, 'S'));
    maxEnergized = Math.Max(maxEnergized, c.GetEnergizedCount(allLines.Length - 1, column, 'N'));
  }
  return maxEnergized;
}