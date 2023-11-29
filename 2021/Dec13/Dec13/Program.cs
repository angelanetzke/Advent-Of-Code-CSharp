using System;
using System.Collections.Generic;
using System.IO;

namespace Dec13
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] allLines = File.ReadAllLines("input.txt");
			List<string> dotLines = new();
			List<string> foldLines = new();
			bool areDotsComplete = false;
			foreach (string thisLine in allLines)
			{
				if (thisLine.Length == 0)
				{
					areDotsComplete = true;
				}
				else if (areDotsComplete)
				{
					foldLines.Add(thisLine);
				}
				else
				{
					dotLines.Add(thisLine);
				}
			}
			Transparency theTransparency = new();
			foreach (string thisDotLine in dotLines)
			{
				theTransparency.AddDot(thisDotLine);
			}
			bool hasPart1BeenShown = false;
			foreach (string thisFoldLine in foldLines)
			{
				int thisFoldValue = int.Parse(thisFoldLine.Split('=')[1]);
				if (thisFoldLine.StartsWith("fold along x"))
				{
					theTransparency.FoldLeft(thisFoldValue);
				}
				else if (thisFoldLine.StartsWith("fold along y"))
				{
					theTransparency.FoldUp(thisFoldValue);
				}
				if (!hasPart1BeenShown)
				{
					Console.WriteLine($"part 1: {theTransparency.CountDots()}");
					hasPart1BeenShown = true;
				}
			}
			Console.WriteLine("part 2:");
			Console.WriteLine(theTransparency);
			File.WriteAllText("output.txt", theTransparency.ToString());

		}




	}
}
