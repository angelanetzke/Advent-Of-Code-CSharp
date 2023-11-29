using System;
using System.Collections.Generic;
using System.Text;

namespace Dec20
{
	internal class Image
	{
		private readonly string algorithm;
		private HashSet<(int, int)> lights;
		private int minX;
		private int maxX;
		private int minY;
		private int maxY;
		private char outerChar;

		public Image(string algorithm, List<string> initialImage)
		{
			this.algorithm = algorithm;
			outerChar = '.';
			lights = new();
			minX = 0;
			maxX = initialImage[0].Length - 1;
			minY = 0;
			maxY = initialImage.Count - 1;
			for (int y = minY; y <= maxY; y++)
			{
				for (int x = minX; x <= maxX; x++)
				{
					if (initialImage[y][x] == '#')
					{
						lights.Add((x, y));
					}
				}
			}
		}

		public void Enhance()
		{
			HashSet<(int, int)> newLights = new();
			minX--;
			maxX++;
			minY--;
			maxY++;
			for (int x = minX; x <= maxX; x++)
			{
				for (int y = minY; y <= maxY; y++)
				{
					if (IsLight(x, y))
					{
						newLights.Add((x, y));
					}
				}
			}
			if (outerChar == '.')
			{
				outerChar = algorithm[0];
			}
			else
			{
				outerChar = algorithm[algorithm.Length - 1];
			}
			lights = newLights;
		}


		private bool IsLight(int x, int y)
		{
			StringBuilder builder = new();
			for (int squareY = y - 1; squareY <= y + 1; squareY++)
			{
				for (int squareX = x - 1; squareX <= x + 1; squareX++)
				{
					if (squareX <= minX || squareX >= maxX || squareY <= minY || squareY >= maxY)
					{
						if (outerChar == '#')
						{
							builder.Append('1');
						}
						else
						{
							builder.Append('0');
						}						
					}
					else
					{
						if (lights.Contains((squareX, squareY)))
						{
							builder.Append('1');
						}
						else
						{
							builder.Append('0');
						}
					}
				}
			}
			int index = Convert.ToInt32(builder.ToString(), 2);
			return algorithm[index] == '#';
		}

		public int CountLights()
		{
			return lights.Count;
		}


	}
}
