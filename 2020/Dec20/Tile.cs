using System.Text;
using System.Text.RegularExpressions;

namespace Dec20
{
	internal class Tile
	{
		private static readonly int SIZE = 10;
		private string[] data = new string[SIZE];
		private readonly string[] borders = new string[4];
		private readonly int id;
		private readonly HashSet<Tile> neighbors = new();
		private string northBorder;
		private string eastBorder;
		private string southBorder;
		private string westBorder;

		public Tile(List<string> inputLines)
		{
			id = int.Parse(new Regex(@"Tile (?<id>\d+):").Match(inputLines[0]).Groups["id"].Value);
			for (int i = 1; i < inputLines.Count; i++)
			{
				data[i - 1] = inputLines[i];
				if (i == 1)
				{
					borders[0] = inputLines[i];
				}
				else if (i == inputLines.Count - 1)
				{
					borders[1] = inputLines[i];
				}
				borders[2] += inputLines[i][0];
				borders[3] += inputLines[i][inputLines[i].Length - 1];
			}
			northBorder = borders[0];
			eastBorder = borders[3];
			southBorder = borders[1];
			westBorder = borders[2];
		}

		public int GetID()
		{
			return id;
		}

		public int GetNeighborCount()
		{
			return neighbors.Count;
		}

		public void Connect(Tile other)
		{
			for (int i = 0; i < borders.Length; i++)
			{
				for (int j = 0; j < borders.Length; j++)
				{
					if (borders[i] == other.borders[j])
					{
						neighbors.Add(other);
						other.neighbors.Add(this);
						return;
					}
					if (borders[i] == new string(other.borders[j].Reverse().ToArray()))
					{
						neighbors.Add(other);
						other.neighbors.Add(this);
						return;
					}
				}
			}
		}

		public Tile? SetEast(List<Tile> map)
		{
			foreach (Tile thisNeighbor in neighbors)
			{
				if (!map.Contains(thisNeighbor) && SetEast(thisNeighbor))
				{
					return thisNeighbor;
				}
			}
			return null;
		}

		private bool SetEast(Tile other)
		{
			for (int i = 1; i <= 8; i++)
			{
				if (eastBorder == other.westBorder)
				{
					return true;
				}
				other.Rotate();
				if (i == 4)
				{
					other.Flip();
				}
			}
			return false;
		}

		public Tile? SetSouth(List<Tile> map)
		{
			foreach (Tile thisNeighbor in neighbors)
			{
				if (!map.Contains(thisNeighbor) && SetSouth(thisNeighbor))
				{
					return thisNeighbor;
				}
			}
			return null;
		}
		private bool SetSouth(Tile other)
		{
			for (int i = 1; i <= 8; i++)
			{
				if (southBorder == other.northBorder)
				{
					return true;
				}
				other.Rotate();
				if (i == 4)
				{
					other.Flip();
				}
			}
			return false;
		}

		public bool IsUpperLeft(List<Tile> map)
		{
			return SetEast(map) != null && SetSouth(map) != null;
		}

		public void Rotate()
		{
			var temp = northBorder;
			northBorder = new string(westBorder.Reverse().ToArray());
			westBorder = southBorder;
			southBorder = new string(eastBorder.Reverse().ToArray());
			eastBorder = temp;
			var newData = new string[SIZE];
			for (int i = 0; i < SIZE; i++)
			{
				var builder = new StringBuilder();
				for (int j = SIZE - 1; j >= 0; j--)
				{
					builder.Append(data[j][i]);
				}
				newData[i] = builder.ToString();
			}
			data = newData;
		}

		public void Flip()
		{
			northBorder = new string(northBorder.Reverse().ToArray());
			southBorder = new string(southBorder.Reverse().ToArray());
			var temp = westBorder;
			westBorder = eastBorder;
			eastBorder = temp;
			for (int i = 0; i < data.Length; i++)
			{
				data[i] = new string(data[i].Reverse().ToArray());
			}
		}

		public string[] GetData()
		{
			return data;
		}

		public override bool Equals(object? obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (obj is Tile other)
			{
				return id == other.id;
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return id.GetHashCode();
		}

		public override string ToString()
		{
			var builder = new StringBuilder();
			builder.Append(id);
			builder.Append('\n');
			foreach (string thisString in data)
			{
				builder.Append(thisString);
				builder.Append('\n');
			}
			return builder.ToString();
		}

	}
}
