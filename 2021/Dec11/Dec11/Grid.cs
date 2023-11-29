using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dec11
{
	internal class Grid
	{
		private readonly Dictionary<Location, Octopus> octopuses;
		private int nextRow = 0;
		private int maxColumn = 0;
		private int maxRow = 0;
		private int size = 0;
		public Grid()
		{
			octopuses = new();
		}
		public void AddRow(string rowString)
		{
			for (int column = 0; column < rowString.Length; column++)
			{
				octopuses[new Location(nextRow, column)] =
					new Octopus(int.Parse(rowString[column].ToString()), nextRow, column);
			}
			size += rowString.Length;
			maxRow = nextRow;
			maxColumn = rowString.Length - 1;
			nextRow++;
		}

		public int GetSize()
		{
			return size;
		}
		public int Update()
		{
			int flashCount = 0;
			for (int row = 0; row <= maxRow; row++)
			{
				for (int column = 0; column <= maxColumn; column++)
				{
					if (octopuses.TryGetValue(new Location(row, column), out Octopus thisOctopus))
					{
						thisOctopus.IncreaseEnergy();
						if (thisOctopus.GetEnergy() > 9)
						{
							Flash(thisOctopus);
						}
					}
				}
			}
			List<Octopus> flashedOctopuses = octopuses.Values.Where<Octopus>(element => element.HasFlashed()).ToList();
			flashedOctopuses.ForEach(element => element.SetEnergyToZero());
			flashCount += flashedOctopuses.Count;
			return flashCount;
		}

		private void Flash(Octopus theOctopus)
		{
			if (!theOctopus.HasFlashed())
			{
				theOctopus.IncreaseEnergy();
				if (theOctopus.GetEnergy() > 9)
				{
					theOctopus.Flash();
					(GetNeighbors(theOctopus.GetRow(), theOctopus.GetColumn())).ForEach(element => Flash(element));
				}
			}
		}

		private List<Octopus> GetNeighbors(int row, int column)
		{
			List<Octopus> neighbors = new();
			for (int rowDelta = -1; rowDelta <= 1; rowDelta++)
			{
				for (int columnDelta = -1; columnDelta <= 1; columnDelta++)
				{
					if (rowDelta == 0 && columnDelta == 0)
					{
						continue;
					}
					if (octopuses.TryGetValue(new Location(row + rowDelta, column + columnDelta), out Octopus thisNeighbor))
					{
						neighbors.Add(thisNeighbor);
					}
				}
			}
			return neighbors;
		}

		public override string ToString()
		{
			StringBuilder builder = new();
			for (int row = 0; row <= maxRow; row++)
			{
				for (int column = 0; column <= maxColumn; column++)
				{
					builder.Append(octopuses[new Location(row, column)].GetEnergy());
				}
				builder.Append('\n');
			}
			return builder.ToString();
		}

	}
}
