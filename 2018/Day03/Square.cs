using System.Text.RegularExpressions;

namespace Day03
{
	internal class Square
	{
		private readonly int minX;
		private readonly int maxX;
		private readonly int minY;
		private readonly int maxY;
		private readonly string id;
		private static readonly Regex dataRegex 
			= new Regex(@"#(?<id>\d+) @ (?<x>\d+),(?<y>\d+): (?<width>\d+)x(?<height>\d+)");

		public Square(string data)
		{
			var match = dataRegex.Match(data);
			minX = int.Parse(match.Groups["x"].Value);
			maxX = minX + int.Parse(match.Groups["width"].Value) - 1;
			minY = int.Parse(match.Groups["y"].Value);
			maxY = minY + int.Parse(match.Groups["height"].Value) - 1;
			id = match.Groups["id"].Value;
		}

		public string GetID()
		{
			return id;
		}

		public HashSet<(int, int)> GetOverlap(Square other)
		{
			var result = new HashSet<(int, int)>();
			if (Math.Max(minX, other.minX) <= Math.Min(maxX, other.maxX)
				&& Math.Max(minY, other.minY) <= Math.Min(maxY, other.maxY))
			{
				for (int x = Math.Max(minX, other.minX); x <= Math.Min(maxX, other.maxX); x++)
				{
					for ( int y = Math.Max(minY, other.minY); y <= Math.Min(maxY, other.maxY); y++)
					{
						result.Add((x, y));
					}
				}
			}
			return result;
		}



	}
}