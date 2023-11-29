using System;
using System.Collections.Generic;

namespace Dec12
{
	internal class Cave
	{
		private readonly string id;
		private readonly HashSet<string> adjacent;
		private readonly Dictionary<string, Cave> caveDictionary;
		public enum CaveType
		{
			LARGE,
			SMALL,
			SPECIAL
		}
		private readonly CaveType caveType;

		public Cave(string id, Dictionary<string, Cave> caveDictionary)
		{
			this.id = id;
			adjacent = new();
			this.caveDictionary = caveDictionary;
			if (id == "start" || id == "end")
			{
				caveType = CaveType.SPECIAL;
			}
			else if (id == id.ToUpper())
			{
				caveType = CaveType.LARGE;
			}
			else
			{
				caveType = CaveType.SMALL;
			}
		}

		public string GetID()
		{
			return id;
		}

		public CaveType GetCaveType()
		{
			return caveType;
		}

		public void Connect(string other)
		{
			adjacent.Add(other);
		}

		public List<Route> Traverse(Route toHere, Cave end, Cave smallDuplicate)
		{
			toHere.Add(this);
			if (this.Equals(end))
			{
				return new List<Route> { toHere };
			}
			else
			{
				List<Route> routes = new();
				foreach (string nextCaveId in adjacent)
				{
					Cave nextCave = caveDictionary[nextCaveId];
					int maxVisitedTimes = GetMaxVisitedTimes(nextCave, smallDuplicate);
					if (toHere.Count(nextCave) < maxVisitedTimes)
					{ 
						List<Route> nextCaveRoutes = nextCave.Traverse(toHere.Clone(), end, smallDuplicate);
						if (nextCaveRoutes != null)
						{
							routes.AddRange(nextCaveRoutes);
						}
					}
				}
				return routes.Count > 0 ? routes : null;
			}
		}

		private static int GetMaxVisitedTimes(Cave aCave, Cave smallDuplicate)
		{
			if (aCave.caveType == CaveType.LARGE)
			{
				return int.MaxValue;
			}
			else if (aCave == smallDuplicate)
			{
				return 2;
			}
			else
			{
				return 1;
			}
		}

		public override bool Equals(object obj)
		{
			return obj is Cave cave &&
						 id == cave.id;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(id);
		}
	}
}
