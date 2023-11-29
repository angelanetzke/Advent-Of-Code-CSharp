using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Dec17
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string inputString = File.ReadAllText("input.txt");
			Match regexX = Regex.Match(inputString, @"x=(?<xvalues>-*\d+..-*\d+)");
			int minTargetX = int.Parse(regexX.Groups["xvalues"].Value.Split("..")[0]);
			int maxTargetX = int.Parse(regexX.Groups["xvalues"].Value.Split("..")[1]);
			Match regexY = Regex.Match(inputString, @"y=(?<yvalues>-*\d+..-*\d+)");
			int minTargetY = int.Parse(regexY.Groups["yvalues"].Value.Split("..")[0]);
			int maxTargetY = int.Parse(regexY.Groups["yvalues"].Value.Split("..")[1]);
			int depth = 0 - minTargetY;
			int maxHeight = (depth - 1) * depth / 2;
			Console.WriteLine($"part 1: {maxHeight}");

			int minInitialX = 0;
			int maxInitialX = maxTargetX;
			int minInitialY = minTargetY;
			int maxInitialY = 0 - minTargetY + 1;
			HashSet<(int, int)> startSpeeds = new();
			for (int thisInitialX = minInitialX; thisInitialX <= maxInitialX; thisInitialX++)
			{
				for (int thisInitialY = minInitialY; thisInitialY <= maxInitialY; thisInitialY++)
				{
					int currentX = 0;
					int currentY = 0;
					int speedX = thisInitialX;
					int speedY = thisInitialY;
					while (currentY >= minTargetY)
					{
						if (minTargetX <= currentX && currentX <= maxTargetX
								&& minTargetY <= currentY && currentY <= maxTargetY)
						{
							startSpeeds.Add((thisInitialX, thisInitialY));
							break;
						}
						currentX += speedX;
						currentY += speedY;
						if (speedX < 0)
						{
							speedX++;
						}
						else if (speedX > 0)
						{
							speedX--;
						}
						speedY--;
					}
				}
			}
			Console.WriteLine($"part 2: {startSpeeds.Count}");

		}


	}
}
