namespace Day17;

internal class Map
{
  private readonly Dictionary<(int, int), int> blockValues = [];
  private readonly (int, int) endCoordinates;
  private static readonly Dictionary<char, char[]> nextDirections = new ()
  {
    {'N', ['W', 'E']},
    {'S', ['W', 'E']},
    {'W', ['N', 'S']},
    {'E', ['N', 'S']},
    {'X', ['N', 'S', 'W', 'E']}
  };

  public Map(string[] mapData)
  {
    endCoordinates = (mapData.Length -1 , mapData[0].Length - 1);
    for (int row = 0; row < mapData.Length; row++)
    {
      for (int column = 0; column < mapData[0].Length; column++)
      {
        blockValues[(row, column)] = mapData[row][column] - '0';
      }
    }
  }

  public int GetMinHeatLoss(int minMoves, int maxMoves)
  {
    List<((int, int, char),int)> neighbors = [];
    (int, int, char) current = (0, 0, 'X');
    Dictionary<(int, int, char), int> distances = [];
    distances[current] = 0;
    HashSet<(int, int, char)> visited = [];
    List<(int, int, char)> queue = [];
    queue.Add(current);
    while (queue.Count > 0)
    {
      int minValue = queue.Select(x => distances[x]).Min();
      current = queue.Where(x => distances[x] == minValue).First();
      if ((current.Item1, current.Item2) == endCoordinates)
      {
        return distances[current];
      }
      visited.Add(current);
      queue.Remove(current);
      neighbors.Clear();
      for (int skip = minMoves; skip <= maxMoves; skip++)
      {
        neighbors.AddRange(GetNeighbors(current, skip));
      }
      foreach (((int, int, char), int) thisNeighbor in neighbors)
      {
        int distanceToNeighbor = distances[current] + thisNeighbor.Item2;
        if (distances.ContainsKey(thisNeighbor.Item1))
        {
          distances[thisNeighbor.Item1] 
            = Math.Min(distances[thisNeighbor.Item1], distanceToNeighbor);
        }
        else
        {
          distances[thisNeighbor.Item1] = distanceToNeighbor;
        }
        if (!visited.Contains(thisNeighbor.Item1) && !queue.Contains(thisNeighbor.Item1))
        {
          queue.Add(thisNeighbor.Item1);
        }
      }
    }
    return 0;
  }

  private List<((int, int, char), int)> GetNeighbors((int, int, char) state, int skip)
  {
    List<((int, int, char), int)> neighbors = [];
    char[] directions = nextDirections[state.Item3];
    foreach (char thisDirection in directions)
    {
      if (thisDirection == 'N' && blockValues.ContainsKey((state.Item1 - skip, state.Item2)))
      {
        int sum = 0;
        for (int i = 1; i <= skip; i++)
        {
          sum += blockValues[(state.Item1 - i, state.Item2)];
        }
        neighbors.Add(((state.Item1 - skip, state.Item2, 'N'), sum));
      }
      if (thisDirection == 'S' && blockValues.ContainsKey((state.Item1 + skip, state.Item2)))
      {
        int sum = 0;
        for (int i = 1; i <= skip; i++)
        {
          sum += blockValues[(state.Item1 + i, state.Item2)];
        }
        neighbors.Add(((state.Item1 + skip, state.Item2, 'S'), sum));
      }
      if (thisDirection == 'W' && blockValues.ContainsKey((state.Item1, state.Item2 - skip)))
      {
        int sum = 0;
        for (int i = 1; i <= skip; i++)
        {
          sum += blockValues[(state.Item1, state.Item2 - i)];
        }
        neighbors.Add(((state.Item1, state.Item2 - skip, 'W'), sum));
      }
      if (thisDirection == 'E' && blockValues.ContainsKey((state.Item1, state.Item2 + skip)))
      {
        int sum = 0;
        for (int i = 1; i <= skip; i++)
        {
          sum += blockValues[(state.Item1, state.Item2 + i)];
        }
        neighbors.Add(((state.Item1, state.Item2 + skip, 'E'), sum));
      }
    }
    return neighbors;
  }  

}