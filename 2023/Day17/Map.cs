namespace Day17;

internal class Map
{
  private readonly Dictionary<(int, int), int> blocks = [];
  private readonly (int, int) endCoordinates;
  private static readonly Dictionary<char, (int, int, char)[]> twoDirections = new ()
  {
    {'N', [(0, -1, 'W'), (0, 1, 'E')]},
    {'S', [(0, -1, 'W'), (0, 1, 'E')]},
    {'W', [(-1, 0, 'N'), (1, 0, 'S')]},
    {'E', [(-1, 0, 'N'), (1, 0, 'S')]}
  };
  private static readonly Dictionary<char, (int, int, char)[]> threeDirections = new ()
  {
    {'N', [(0, -1, 'W'), (0, 1, 'E'), (-1, 0, 'N')]},
    {'S', [(0, -1, 'W'), (0, 1, 'E'), (1, 0, 'S')]},
    {'W', [(-1, 0, 'N'), (1, 0, 'S'), (0, -1, 'W')]},
    {'E', [(-1, 0, 'N'), (1, 0, 'S'), (0, 1, 'E')]}    
  };

  public Map(string[] mapData)
  {
    endCoordinates = (mapData.Length -1 , mapData[0].Length - 1);
    for (int row = 0; row < mapData.Length; row++)
    {
      for (int column = 0; column < mapData[0].Length; column++)
      {
        blocks[(row, column)] = mapData[row][column] - '0';
      }
    }
  }

  public int GetMinHeatLoss()
  {
    (int, int, int, char) current = (0, 0, 0, 'X');
    Dictionary<(int, int, int, char), int> distances = [];
    distances[current] = 0;
    List<(int, int, int, char)> queue = [];
    queue.Add(current);
    HashSet<(int, int, int, char)> visited = [];
    while (queue.Count > 0)
    {
      queue = queue.OrderBy(x => distances[x]).ToList();
      current = queue[0];
      if ((current.Item1, current.Item2) == endCoordinates)
      {
        return distances[current];
      }
      visited.Add(current);
      queue.Remove(current);
      List<(int, int, int, char)> neighbors = GetNeighbors(current);
      foreach ((int, int, int, char) thisNeighbor in neighbors)
      {
        if (!blocks.ContainsKey((thisNeighbor.Item1, thisNeighbor.Item2)))
        {
          continue;
        }
        int distanceToNeighbor = distances[current] + blocks[(thisNeighbor.Item1, thisNeighbor.Item2)];
        if (distances.ContainsKey(thisNeighbor))
        {
          distances[thisNeighbor] = Math.Min(distances[thisNeighbor], distanceToNeighbor);
        }
        else
        {
          distances[thisNeighbor] = distanceToNeighbor;
        }
        if (!visited.Contains(thisNeighbor) && !queue.Contains(thisNeighbor))
        {
          queue.Add(thisNeighbor);
        }
      }
    }
    return 0;
  }

  private static List<(int, int, int, char)> GetNeighbors((int, int, int, char) state)
  {
    List<(int, int, int, char)> neighbors = [];
    (int, int, char)[] neighborData;
    if (state.Item4 == 'X')
    {
      neighborData = [(-1, 0, 'N'), (1, 0, 'S'), (0, -1, 'W') , (0, 1, 'E')];
    }
    else if (state.Item3 < 3)
    {
      neighborData = threeDirections[state.Item4];
    }
    else
    {
      neighborData = twoDirections[state.Item4];
    }
    foreach ((int, int, char) thisNeighborData in neighborData)
    {
      if (state.Item4 == thisNeighborData.Item3)
      {
        neighbors.Add((state.Item1 + thisNeighborData.Item1, 
          state.Item2 + thisNeighborData.Item2, 
          state.Item3 + 1, thisNeighborData.Item3));
      }
      else
      {
        neighbors.Add((state.Item1 + thisNeighborData.Item1, 
          state.Item2 + thisNeighborData.Item2, 
          1, thisNeighborData.Item3));
      }
    }
    return neighbors;
  }

}