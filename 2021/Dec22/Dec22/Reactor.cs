using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dec22
{
	internal class Reactor
	{
		private readonly HashSet<(int, int, int)> onCubes;

		public Reactor()
		{
			onCubes = new();
		}

		public void Next(string cuboidString)
		{
			string action = cuboidString.Split(" ")[0];
			Match regexX = Regex.Match(cuboidString, @"x=(?<xvalues>-*\d+..-*\d+)");
			int minX = int.Parse(regexX.Groups["xvalues"].Value.Split("..")[0]);
			int maxX = int.Parse(regexX.Groups["xvalues"].Value.Split("..")[1]);
			Match regexY = Regex.Match(cuboidString, @"y=(?<yvalues>-*\d+..-*\d+)");
			int minY = int.Parse(regexY.Groups["yvalues"].Value.Split("..")[0]);
			int maxY = int.Parse(regexY.Groups["yvalues"].Value.Split("..")[1]);
			Match regexZ = Regex.Match(cuboidString, @"z=(?<zvalues>-*\d+..-*\d+)");
			int minZ = int.Parse(regexZ.Groups["zvalues"].Value.Split("..")[0]);
			int maxZ = int.Parse(regexZ.Groups["zvalues"].Value.Split("..")[1]);
			if (action == "on")
			{
				for (int x = minX; x <= maxX; x++)
				{
					for (int y = minY; y <= maxY; y++)
					{
						for (int z = minZ; z <= maxZ; z++)
						{
							onCubes.Add((x, y, z));
						}
					}
				}
			}
			else
			{
				for (int x = minX; x <= maxX; x++)
				{
					for (int y = minY; y <= maxY; y++)
					{
						for (int z = minZ; z <= maxZ; z++)
						{
							onCubes.Remove((x, y, z));
						}
					}
				}
			}
		}

		public long CountCubes()
		{
			return onCubes.Count;
		}


	}
}
