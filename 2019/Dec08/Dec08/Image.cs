using System.Text;
using System.Text.RegularExpressions;

namespace Dec08
{
	internal class Image
	{
		private readonly int WIDTH = 25;
		private readonly int HEIGHT = 6;
		private readonly string[] data;

		public Image(string input)
		{
			int layerCount = input.Length / (WIDTH * HEIGHT);
			data = new string[layerCount];
			for (int i = 0; i < layerCount; i++)
			{
				data[i] = input.Substring(i * WIDTH * HEIGHT, WIDTH * HEIGHT);
			}
		}

		public int GetLayerCount()
		{
			return data.Length;
		}

		public int CountDigits(int layer, int digit)
		{
			return Regex.Matches(data[layer], digit.ToString()).Count;
		}

		public override string ToString()
		{
			var builder = new StringBuilder();
			for (int pixel = 0; pixel < data[0].Length; pixel++)
			{
				for (int layer = 0; layer < data.Length; layer++)
				{
					var thisPixel = data[layer][pixel];
					if (thisPixel == '0')
					{
						builder.Append(' ');
						break;
					}
					if (thisPixel == '1')
					{
						builder.Append('#');
						break;
					}
				}
			}
			var singleLine = builder.ToString();
			builder.Clear();
			for (int row = 0; row < HEIGHT; row++)
			{
				builder.Append(singleLine.Substring(row * WIDTH, WIDTH));
				builder.Append('\n');
			}
			return builder.ToString();
		}

	}
}
