namespace Day16;

internal class Contraption
{
  private readonly string[] layout;
  private readonly Dictionary<(char, char), List<char>> nextDirection = new ()
  {
    {('.', 'N'), ['N']},
    {('.', 'S'), ['S']},
    {('.', 'W'), ['W']},
    {('.', 'E'), ['E']},
    {('\\', 'N'), ['W']},
    {('\\', 'S'), ['E']},
    {('\\', 'W'), ['N']},
    {('\\', 'E'), ['S']},
    {('/', 'N'), ['E']},
    {('/', 'S'), ['W']},
    {('/', 'W'), ['S']},
    {('/', 'E'), ['N']},
    {('-', 'N'), ['W', 'E']},
    {('-', 'S'), ['W', 'E']},
    {('-', 'W'), ['W', 'E']},
    {('-', 'E'), ['W', 'E']},
    {('|', 'N'), ['N', 'S']},
    {('|', 'S'), ['N', 'S']},
    {('|', 'W'), ['N', 'S']},
    {('|', 'E'), ['N', 'S']},
  }; 

  public Contraption(string[] layout)
  {
    this.layout = layout;
  }

  public int GetEnergizedCount(int row, int column, char direction)
  {
    (int, int, char) current = (row, column, direction);
    HashSet<(int, int, char)> visited = [];
    Queue<(int, int, char)> queue = [];
    queue.Enqueue(current);
    HashSet<(int, int)> energized = [];
    while (queue.Count > 0)
    {
      current = queue.Dequeue();
      visited.Add(current);
      energized.Add((current.Item1, current.Item2));
      List<(int, int, char)> next = GetNext(current);
      foreach ((int, int, char) thisNext in next)
      {
        if (!visited.Contains(thisNext) && !queue.Contains(thisNext))
        {
          queue.Enqueue(thisNext);
        }
      }
    }
    return energized.Count;
  }

  private List<(int, int, char)> GetNext((int, int, char) current)
  {
    List<(int, int, char)> result = [];
    char thisChar = layout[current.Item1][current.Item2];
    foreach (char thisNextDirection in nextDirection[(thisChar, current.Item3)])
    {
      if (thisNextDirection == 'N' && current.Item1 > 0)
      {
        result.Add(GoNorth(current.Item1, current.Item2));
      }
      if (thisNextDirection == 'S' && current.Item1 < layout.Length - 1)
      {
        result.Add(GoSouth(current.Item1, current.Item2));
      }
      if (thisNextDirection == 'W' && current.Item2 > 0)
      {
        result.Add(GoWest(current.Item1, current.Item2));
      }
      if (thisNextDirection == 'E' && current.Item2 < layout[0].Length - 1)
      {
        result.Add(GoEast(current.Item1, current.Item2));
      }
    }
    return result;
  }

  private static (int, int, char) GoNorth(int row, int column)
  {
    return (row - 1, column, 'N');
  }

  private static (int, int, char) GoSouth(int row, int column)
  {
    return (row + 1, column, 'S');
  }

  private static (int, int, char) GoWest(int row, int column)
  {
    return (row, column - 1, 'W');
  }

  private static (int, int, char) GoEast(int row, int column)
  {
    return (row, column + 1, 'E');
  }
}