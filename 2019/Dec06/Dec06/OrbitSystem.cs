namespace Dec06
{
	internal class OrbitSystem
	{
		private readonly Dictionary<string, List<string>> orbits = new ();
		private readonly HashSet<string> allMassNames = new();

		public OrbitSystem(string[] allLines)
		{
			for (int i = 0; i < allLines.Length; i++)
			{
				string parentName = allLines[i].Split(')')[0];
				string childName = allLines[i].Split(')')[1];
				if (orbits.ContainsKey(parentName))
				{
					orbits[parentName].Add(childName);
				}
				else
				{
					orbits[parentName] = new() { childName  };
				}
				allMassNames.Add(parentName);
				allMassNames.Add(childName);
			}
		}

		public int CountOrbits()
		{
			int totalOrbits = 0;
			foreach (string thisMassName in allMassNames)
			{
				totalOrbits += CountOrbits("COM", thisMassName, 0);
			}
			return totalOrbits;
		}

		private int CountOrbits(string currentMass, string targetMass, int parentCount)
		{
			if (currentMass == targetMass)
			{
				return parentCount;
			}
			else
			{
				if (orbits.ContainsKey(currentMass))
				{
					foreach (string thisChild in orbits[currentMass])
					{
						var thisChildOrbitCount = CountOrbits(thisChild, targetMass, parentCount + 1);
						if (thisChildOrbitCount != 0)
						{
							return thisChildOrbitCount;
						}
					}
				}
				return 0;
			}
		}

		public int GetShortestPath()
		{
			int shortest = int.MaxValue;
			foreach (string thisMassName in allMassNames)
			{
				int pathToSanta = CountOrbits(thisMassName, "SAN", 0);
				int pathToYou = CountOrbits(thisMassName, "YOU", 0);
				int totalPathLength = pathToSanta + pathToYou;
				if (pathToSanta > 0 && pathToYou > 0)
				{
					shortest = Math.Min(shortest, totalPathLength);
				}
			}
			return shortest - 2;
		}

	}
}
