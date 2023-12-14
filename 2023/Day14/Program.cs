using System.Text;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part 1: {Part1(allLines)}");
Console.WriteLine($"Part 2: {Part2(allLines)}");

static int Part1(string[] allLines)
{
  List<char[]> platform = [];
  foreach (string thisLine in allLines)
  {
    platform.Add(thisLine.ToCharArray());
  }
  TiltNorth(platform);
  return GetLoad(platform);
}

static long Part2(string[] allLines)
{
  long cycles = 1000000000L;
  Dictionary<string, long> stateCache = [];
  Dictionary<long, long> loadCache = [];
  List<char[]> platform = [];
  foreach (string thisLine in allLines)
  {
    platform.Add(thisLine.ToCharArray());
  }
  long result = 0;
  for (long i = 1; i < cycles; i++)
  {
    TiltNorth(platform);
    TiltWest(platform);
    TiltSouth(platform);
    TiltEast(platform);
    string state = GetState(platform);
    int load = GetLoad(platform);
    if (stateCache.ContainsKey(state))
    {
      long cycleStart = stateCache[state];
      long cycleLength = i - stateCache[state];
      long loadCacheKey = (cycles - cycleStart) % cycleLength + cycleStart;
      result = loadCache[loadCacheKey];
      break;
    }
    stateCache[state] = i;
    loadCache[i] = load;
  }
  return result;
}

static string GetState(List<char[]> platform)
{
  StringBuilder builder = new ();
  platform.ForEach(x => builder.Append(new string(x)));
  return builder.ToString();
}

static int GetLoad(List<char[]> platform)
{
  int load = 0;
  for (int row = 0; row < platform.Count; row++)
  {
    load += (platform.Count - row) * platform[row].Count(x => x == 'O');
  }
  return load;
}

static void TiltNorth(List<char[]> platform)
{
  for (int column = 0; column < platform[0].Length; column++)
  {
    for (int row = 0; row < platform.Count - 1; row++)
    {
      if (platform[row][column] == '.')
      {
        int nextRow = row + 1;
        while (nextRow < platform.Count && platform[nextRow][column] == '.')
        {
          nextRow++;
        }
        if (nextRow < platform.Count && platform[nextRow][column] == 'O')
        {
          platform[row][column] = 'O';
          platform[nextRow][column] = '.';
        }
      }
    }
  }
}

static void TiltWest(List<char[]> platform)
{
  for (int row = 0; row < platform.Count; row++)
  {
    for (int column = 0; column < platform[0].Length - 1; column++)
    {
      if (platform[row][column] == '.')
      {
        int nextColumn = column + 1;
        while (nextColumn < platform[0].Length && platform[row][nextColumn] == '.')
        {
          nextColumn++;
        }
        if (nextColumn < platform[0].Length && platform[row][nextColumn] == 'O')
        {
          platform[row][column] = 'O';
          platform[row][nextColumn] = '.';
        }
      }
    }
  }
}

static void TiltSouth(List<char[]> platform)
{
  for (int column = 0; column < platform[0].Length; column++)
  {
    for (int row = platform.Count - 1; row >= 0; row--)
    {
      if (platform[row][column] == '.')
      {
        int nextRow = row - 1;
        while (nextRow >= 0 && platform[nextRow][column] == '.')
        {
          nextRow--;
        }
        if (nextRow >= 0 && platform[nextRow][column] == 'O')
        {
          platform[row][column] = 'O';
          platform[nextRow][column] = '.';
        }
      }
    }
  }
}

static void TiltEast(List<char[]> platform)
{
  for (int row = 0; row < platform.Count; row++)
  {
    for (int column = platform[0].Length - 1; column >= 0; column--)
    {
      if (platform[row][column] == '.')
      {
        int nextColumn = column - 1;
        while (nextColumn >= 0 && platform[row][nextColumn] == '.')
        {
          nextColumn--;
        }
        if (nextColumn >= 0 && platform[row][nextColumn] == 'O')
        {
          platform[row][column] = 'O';
          platform[row][nextColumn] = '.';
        }
      }
    }
  }
}