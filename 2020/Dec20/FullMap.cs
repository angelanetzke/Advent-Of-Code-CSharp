using System.Text;
using System.Text.RegularExpressions;

namespace Dec20
{
	internal class FullMap
	{
		private static readonly int MAP_SIZE = 12;
		private static readonly int TILE_SIZE = 10;
		private List<string> data = new();
		public FullMap(List<Tile> tileMap)
		{
			for (int mapRow = 0; mapRow < MAP_SIZE; mapRow++)
			{
				for (int tileRow = 1; tileRow < TILE_SIZE - 1; tileRow++)
				{
					var builder = new StringBuilder();
					for (int tileIndex = mapRow * MAP_SIZE; tileIndex < (mapRow + 1) * MAP_SIZE; tileIndex++)
					{
						var thisTile = tileMap[tileIndex];
						var test2 = thisTile.GetData()[tileRow];
						var test3 = test2[1..^1];
						builder.Append(tileMap[tileIndex].GetData()[tileRow][1..^1]);
					}
					data.Add(builder.ToString());
				}
			}
		}

		public int GetRoughness()
		{
			const int SEA_MONSTER_WIDTH = 20;
			const int SEA_MONSTER_SIZE = 15;
			var seaMonster1 = new Regex(@"^.{18}#{1}");
			var seaMonster2 = new Regex(@"^#{1}.{4}#{2}.{4}#{2}.{4}#{3}");
			var seaMonster3 = new Regex(@"^.{1}#{1}.{2}#{1}.{2}#{1}.{2}#{1}.{2}#{1}.{2}#{1}");
			int total = data.Sum(thisLine => thisLine.Count(thisChar => thisChar == '#'));
			int seaMonsterCount = 0;
			for (int i = 1; i <= 8; i++)
			{
				seaMonsterCount = 0;
				for (int row = 0; row < data.Count - 3; row++)
				{
					for (int column = 0; column < data[0].Length - SEA_MONSTER_WIDTH; column++)
					{
						if (seaMonster1.IsMatch(data[row][column..])
							&& seaMonster2.IsMatch(data[row + 1][column..])
							&& seaMonster3.IsMatch(data[row + 2][column..]))
						{
							seaMonsterCount++;
						}
					}
				}
				if (seaMonsterCount > 0)
				{
					return total - seaMonsterCount * SEA_MONSTER_SIZE;
				}
				Rotate();
				if (i == 4)
				{
					Flip();
				}
			}
			return -1;
		}

		private void Rotate()
		{
			var newData = new List<string>();
			for (int i = 0; i < data.Count; i++)
			{
				var builder = new StringBuilder();
				for (int j = data.Count - 1; j >= 0; j--)
				{
					builder.Append(data[j][i]);
				}
				newData.Add(builder.ToString());
			}
			data = newData;
		}

		private void Flip()
		{
			for (int i = 0; i < data.Count; i++)
			{
				data[i] = new string(data[i].Reverse().ToArray());
			}
		}

	}
}
