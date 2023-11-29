using System;
using System.Collections.Generic;
using System.Linq;

namespace Dec12
{
	internal class Route
	{
		private List<Cave> caves;
		private Dictionary<Cave, int> visitedCount;

		public Route()
		{
			caves = new();
			visitedCount = new();
		}

		public Route Clone()
		{
			Route newRoute = new();
			newRoute.caves = new(caves);
			newRoute.visitedCount = new(visitedCount);
			return newRoute;
		}
		public void Add(Cave newCave)
		{
			caves.Add(newCave);
			if (visitedCount.TryGetValue(newCave, out int count))
			{
				visitedCount[newCave] = count + 1;
			}
			else
			{
				visitedCount[newCave] = 1;
			}
		}

		public bool Contains(Cave aCave)
		{
			return caves.Contains(aCave);
		}

		public int Count(Cave aCave)
		{
			if (visitedCount.TryGetValue(aCave, out int count))
			{
				return count;
			}
			else
			{
				return 0;
			}
		}

		public override bool Equals(object obj)
		{
			if (obj is not Route)
			{
				return false;
			}
			else
			{
				Route other = (Route)obj;
				if (caves.Count != other.caves.Count)
				{
					return false;
				}
				for (int i = 0; i < caves.Count; i++)
				{
					if (caves[i] != other.caves[i])
					{
						return false;
					}
				}
				return true;
			}
		}

		public override int GetHashCode()
		{
			int hash = 17;
			foreach (Cave thisCave in caves)
			{
				hash = HashCode.Combine(hash, thisCave.GetHashCode());
			}
			return hash;
		}
	}
}
