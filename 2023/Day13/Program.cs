using Day13;

string[] allLines = File.ReadAllLines("input.txt");
List<Pattern> patternList = [];
Pattern current = new ();
foreach (string thisLine in allLines)
{
  if (thisLine.Length == 0)
  {
    patternList.Add(current);
    current = new ();
  }
  else
  {
    current.AddRow(thisLine);
  }
}
if (!current.IsEmpty())
{
  patternList.Add(current);
}
Console.WriteLine($"Part 1: {Part1(patternList)}");
Console.WriteLine($"Part 2: {Part2(patternList)}");

static int Part1(List<Pattern> patternList)
{  
  int reflectionValueSum = 0;
  patternList.ForEach(x => reflectionValueSum += x.GetReflectionValue());
  return reflectionValueSum;
}

static int Part2(List<Pattern> patternList)
{  
  int reflectionValueSum = 0;
  patternList.ForEach(x => reflectionValueSum += x.GetSmudgedReflectionValue());  
  return reflectionValueSum;
}
