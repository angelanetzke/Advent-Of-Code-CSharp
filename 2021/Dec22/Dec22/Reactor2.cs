using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dec22
{
	internal class Reactor2
	{
		private readonly List<Cuboid> onCuboids;
		private readonly List<Cuboid> offCuboids;

		public Reactor2()
		{
			onCuboids = new();
			offCuboids = new();
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
			Cuboid newCuboid = new(minX, maxX, minY, maxY, minZ, maxZ);
			List<Cuboid> newOffCuboids = new();
			foreach (Cuboid thisOnCuboid in onCuboids)
			{
				if (thisOnCuboid.Intersects(newCuboid))
				{
					newOffCuboids.Add(thisOnCuboid.GetIntersection(newCuboid));
				}
			}
			foreach (Cuboid thisOffCuboid in offCuboids)
			{
				if (thisOffCuboid.Intersects(newCuboid))
				{
					onCuboids.Add(thisOffCuboid.GetIntersection(newCuboid));
				}
			}
			if (action == "on")
			{
				onCuboids.Add(newCuboid);
			}
			offCuboids.AddRange(newOffCuboids);
		}

		public long CountCubes()
		{
			long count = 0L;
			foreach(Cuboid thisOnCuboid in onCuboids)
			{
				count += thisOnCuboid.CountCubes();
			}
			foreach (Cuboid thisOffCuboid in offCuboids)
			{
				count -= thisOffCuboid.CountCubes();
			}
			return count;
		}

	}
}
