//Note: Day 25 only has one part.

using Day25;

var allLines = File.ReadAllLines("input.txt");
var pointList = new List<Point>();
foreach (string thisLine in allLines)
{
	pointList.Add(new Point(thisLine));
}
for (int i = 0; i < pointList.Count - 1; i++)
{
	for (int j = i + 1; j < pointList.Count; j++)
	{
		pointList[i].TryConnect(pointList[j]);
	}
}
foreach (Point thisPoint in pointList)
{
	thisPoint.AssignID();
}
Console.WriteLine($"Part 1: {Point.GetMaxID()}");

