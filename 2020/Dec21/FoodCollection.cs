using System.Text;
using System.Text.RegularExpressions;

namespace Dec21
{
	internal class FoodCollection
	{
		private readonly HashSet<string> allIngredients = new();
		private HashSet<string> safeIngredients = new();
		private readonly Dictionary<string, List<HashSet<string>>> allergens = new();
		private Dictionary<string, HashSet<string>> combinedSets = new();
		private readonly Dictionary<string, int> counts = new();
		public FoodCollection(string[] allLines)
		{
			foreach (string thisLine in allLines)
			{
				var ingredientArray = thisLine.Split(" (contains ")[0].Split(' ');
				foreach (string thisIngredient in ingredientArray)
				{
					allIngredients.Add(thisIngredient);
					if (counts.TryGetValue(thisIngredient, out int count))
					{
						counts[thisIngredient] = count + 1;
					}
					else
					{
						counts[thisIngredient] = 1;
					}
				}
				var ingredientSet = new HashSet<string>(ingredientArray);
				var allergenMatches = new Regex(@"(\w+)").Matches(thisLine.Split(" (contains ")[1]);
				foreach (Match thisMatch in allergenMatches)
				{
					if (allergens.TryGetValue(thisMatch.Groups[0].Value, out List<HashSet<string>>? list))
					{
						list.Add(ingredientSet);
					}
					else
					{
						allergens[thisMatch.Groups[0].Value] = new List<HashSet<string>>() { ingredientSet };
					}
				}
			}
		}

		public int GetSafeCount()
		{
			combinedSets = new();
			safeIngredients = new HashSet<string>(allIngredients);
			foreach (string thisKey in allergens.Keys)
			{
				var thisList = allergens[thisKey];
				HashSet<string> thisCombinedSet = thisList[0];
				for (int i = 0; i < thisList.Count; i++)
				{
					if (thisCombinedSet == null)
					{
						thisCombinedSet = thisList[i];
					}
					else
					{
						thisCombinedSet = thisCombinedSet.Intersect(thisList[i]).ToHashSet();
					}
				}
				combinedSets[thisKey] = thisCombinedSet;
				foreach (string thisIngredient in thisCombinedSet)
				{
					safeIngredients.Remove(thisIngredient);
				}
			}
			int totalCount = 0;
			foreach (string thisIngredient in safeIngredients)
			{
				totalCount += counts[thisIngredient];
			}
			return totalCount;
		}

		public string GetDangerousList()
		{
			var dangerousIngredients = allIngredients.Except(safeIngredients);
			//key is allergen name, value is possible ingredient names
			var possible = new Dictionary<string, HashSet<string>>(combinedSets);
			while (possible.Values.Any(thisSet => thisSet.Count > 1))
			{
				foreach (HashSet<string> thisSet in possible.Values)
				{
					if (thisSet.Count == 1)
					{
						foreach (HashSet<string> otherSet in possible.Values)
						{
							if (otherSet.Count > 1)
							{
								otherSet.Remove(thisSet.ToArray()[0]);
							}
						}
					}
				}
			}
			var allergenNames = (new List<string>(allergens.Keys));
			allergenNames.Sort();
			var builder = new StringBuilder();
			for (int i = 0; i < allergenNames.Count; i++)
			{
				builder.Append(possible[allergenNames[i]].ToArray()[0]);
				if (i < allergenNames.Count - 1)
				{
					builder.Append(',');
				}
			}
			return builder.ToString();
		}

	}
}
