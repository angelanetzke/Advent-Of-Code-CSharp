using System.Text;

namespace Day18
{
	internal class Area
	{
		private enum State { Ground, Trees, Lumberyard }
		private Dictionary<(int, int), State> currentStates = new ();
		private readonly int height;
		private readonly int width;
		private Dictionary<string, (long iteration, int resourceValue)> cache = new ();

		public Area(string[] allLines)
		{
			height = allLines.Length;
			width = allLines[0].Length;
			for (int row = 0; row < allLines.Length; row++)
			{
				for (int column = 0; column < allLines[row].Length; column++)
				{
					switch (allLines[row][column])
					{
						case '.':
							currentStates[(row, column)] = State.Ground;
							break;
						case '|':
							currentStates[(row, column)] = State.Trees;
							break;
						case '#':
							currentStates[(row, column)] = State.Lumberyard;
							break;
					}
				}
			}
			UpdateCache(0);
		}

		public void Iterate()
		{
			var temp = new Dictionary<(int, int), State>();
			for (int row = 0; row < height; row++)
			{
				for (int column = 0; column < width; column++)
				{
					var neighborList = GetNeighborStates((row, column));
					switch (currentStates[(row, column)])
					{
						case State.Ground:
							temp[(row, column)] 
								= neighborList.Count(x => x == State.Trees) >= 3 
									? State.Trees : State.Ground;
							break;
						case State.Trees:
							temp[(row, column)] 
								= neighborList.Count(x => x == State.Lumberyard) >= 3 
									? State.Lumberyard : State.Trees;
							break;
						case State.Lumberyard:
							temp[(row, column)] 
								= neighborList.Count(x => x == State.Lumberyard) >= 1 
									&& neighborList.Count(x => x == State.Trees) >= 1
									? State.Lumberyard : State.Ground;
							break;
					}
				}
			}
			currentStates = temp;
		}

		public int Iterate(long times)
		{
			for (long i = 1; i <= times; i++)
			{
				Iterate();
				var lastEntry = UpdateCache(i);
				if (lastEntry != i)
				{
					var period = i - lastEntry;
					var equivalentIteration = (times - lastEntry) % period + lastEntry;
					return cache.Where(x => x.Value.iteration == equivalentIteration)
						.Select(x => x.Value.resourceValue).First();
				}
			}
			return GetResourceValue();
		}

		private List<State> GetNeighborStates((int, int) location)
		{
			var neighborList = new List<State>();
			for (int deltaRow = -1; deltaRow <= 1; deltaRow++)
			{
				for (int deltaColumn = -1; deltaColumn <= 1; deltaColumn++)
				{
					if (deltaRow == 0 && deltaColumn == 0)
					{
						continue;
					}
					if (currentStates.ContainsKey((location.Item1 + deltaRow, location.Item2 + deltaColumn)))
					{
						neighborList.Add(currentStates[(location.Item1 + deltaRow, location.Item2 + deltaColumn)]);
					}
				}
			}
			return neighborList;
		}

		public int GetResourceValue()
		{
			return currentStates.Values.Count(x => x == State.Trees) * currentStates.Values.Count(x => x == State.Lumberyard);
		}

		private long UpdateCache(long iteration)
		{
			var builder = new StringBuilder();
			for (int row = 0; row < height; row++)
			{
				for (int column = 0; column < width; column++)
				{
					builder.Append(currentStates[(row, column)].ToString());
				}
			}
			if (cache.Keys.Contains(builder.ToString()))
			{
				return cache[builder.ToString()].Item1;
			}
			else
			{
				cache[builder.ToString()] = (iteration, GetResourceValue());
				return iteration;
			}			
		}

	}
}