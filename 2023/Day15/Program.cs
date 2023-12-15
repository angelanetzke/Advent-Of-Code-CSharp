using System.Text.RegularExpressions;

string inputLine = File.ReadAllLines("input.txt")[0];
Console.WriteLine($"Part 1: {Part1(inputLine)}");
Console.WriteLine($"Part 2: {Part2(inputLine)}");

static int Part1(string inputLine)
{
  string[] steps = inputLine.Split(',');
  int hashSum = 0;
  foreach (string thisStep in steps)
  {
    hashSum += GetHash(thisStep);
  }
  return hashSum;
}

static int Part2(string inputLine)
{
  string symbolRegex = @"[=-]{1}";
  Dictionary<int, List<(string, string)>> boxes = [];
  string[] steps = inputLine.Split(',');
  foreach (string thisStep in steps)
  {
    int operatorIndex = Regex.Match(thisStep, symbolRegex).Index;
    string label = thisStep[..operatorIndex];
    int boxNumber = GetHash(label);
    char operatorSymbol = thisStep[operatorIndex];
    if (!boxes.ContainsKey(boxNumber))
    {
      boxes[boxNumber] = [];
    }
    List<(string, string)> currentBox = boxes[boxNumber];
    int lensIndex = currentBox.FindIndex(x => x.Item1 == label);
    if (operatorSymbol == '-' && lensIndex >= 0)
    {
      currentBox.RemoveAt(lensIndex);
    }
    else if (operatorSymbol == '=')
    {
      string focalLength = thisStep[(operatorIndex + 1)..];
      if (lensIndex >= 0)
      {        
        currentBox[lensIndex] = (label, focalLength);
      }
      else
      {
        currentBox.Add((label, focalLength));
      }
    }
  }
  int focusingPowerSum = 0;
  foreach (int thisBoxNumber in boxes.Keys)
  {
    List<(string, string)> thisBox = boxes[thisBoxNumber];
    if (thisBox.Count == 0)
    {
      continue;
    }
    for (int lensIndex = 0; lensIndex < thisBox.Count; lensIndex++)
    {
      focusingPowerSum += (thisBoxNumber + 1) * (lensIndex + 1) * int.Parse(thisBox[lensIndex].Item2);
    }
  }
  return focusingPowerSum;
}

static int GetHash(string s)
{
  int currentValue = 0;
  foreach (char thisChar in s)
  {
    currentValue += thisChar;
    currentValue *= 17;
    currentValue %= 256;
  }
  return currentValue;
}
