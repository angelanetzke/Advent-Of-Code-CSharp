using System.Text.RegularExpressions;

namespace Day10
{
	internal class Light
	{
		private int x;
		private int y;
		private readonly int deltaX;
		private readonly int deltaY;
		private static readonly Regex dataRegex 
			= new Regex(@"position=< *(?<x>-?\d+), *(?<y>-?\d+)> velocity=< *(?<deltaX>-?\d+), *(?<deltaY>-?\d+)>");

		public Light(string data)
		{
			var match = dataRegex.Match(data);
			x = int.Parse(match.Groups["x"].Value);
			y = int.Parse(match.Groups["y"].Value);
			deltaX = int.Parse(match.Groups["deltaX"].Value);
			deltaY = int.Parse(match.Groups["deltaY"].Value);
		}

		public int GetX()
		{
			return x;
		}

		public int GetY()
		{
			return y;
		}

		public void Update()
		{
			x += deltaX;
			y += deltaY;
		}

		public void Reverse()
		{
			x -= deltaX;
			y -= deltaY;
		}

	}
}