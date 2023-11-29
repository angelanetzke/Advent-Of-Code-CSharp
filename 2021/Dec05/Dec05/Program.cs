using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dec05
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] allLines = File.ReadAllLines("input.txt");
			
			List<Segment> allSegments = new List<Segment>();
			foreach (string thisLine in allLines)
			{
				int x1 = int.Parse(thisLine.Split(" -> ")[0].Split(",")[0]);
				int y1 = int.Parse(thisLine.Split(" -> ")[0].Split(",")[1]);
				int x2 = int.Parse(thisLine.Split(" -> ")[1].Split(",")[0]);
				int y2 = int.Parse(thisLine.Split(" -> ")[1].Split(",")[1]);
				allSegments.Add(new Segment(x1, y1, x2, y2));
			}
			Dictionary<Point, int> pointCount1 = new() { };
			Dictionary<Point, int> pointCount2 = new() { };
			foreach (Segment thisSegment in allSegments)
			{
				List<Point> thisSegmentPoints = thisSegment.GetAllPoints();
				foreach (Point thisPoint in thisSegmentPoints)
				{
					if (thisSegment.IsHorizontal() || thisSegment.IsVertical())
					{
						if (!pointCount1.TryGetValue(thisPoint, out int _))
						{
							pointCount1[thisPoint] = 1;
						}
						else
						{
							pointCount1[thisPoint]++;
						}
					}
					if (!pointCount2.TryGetValue(thisPoint, out int _))
					{
						pointCount2[thisPoint] = 1;
					}
					else
					{
						pointCount2[thisPoint]++;
					}
				}
			}
			Console.WriteLine($"part 1: {pointCount1.Values.Where(element => element > 1).Count()}");
			Console.WriteLine($"part 2: {pointCount2.Values.Where(element => element > 1).Count()}");

		}
	}
}
