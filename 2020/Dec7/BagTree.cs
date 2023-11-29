using System.Text.RegularExpressions;

namespace Dec7
{
	internal class BagTree
	{
		private static readonly Dictionary<string, List<string>> rules = new();
		private static HashSet<string>? deadEnds = null;
		private readonly string rootColor;

		public BagTree(string rootColor)
		{
			this.rootColor = rootColor;
		}

		public bool Contains(string color)
		{
			deadEnds = new();
			return Contains(rootColor, color);
		}

		private bool Contains(string startColor, string searchColor)
		{
			if (deadEnds == null)
			{
				deadEnds = new();
			}
			if (deadEnds.Contains(startColor))
			{
				return false;
			}
			else if (rules.TryGetValue(startColor, out List<string>? children))
			{
				foreach (string thisChild in children)
				{
					string[] data = thisChild.Split('|');
					if (data[1] == searchColor)
					{
						return true;
					}
					else if (Contains(data[1], searchColor))
					{
						return true;
					}
				}
				deadEnds.Add(startColor);
				return false;
			}
			else
			{
				deadEnds.Add(startColor);
				return false;
			}
		}

		public int Count()
		{
			//Subtract 1 so we don't count the outermost bag
			return Count(rootColor) - 1;
		}
		
		private int Count(string color)
		{
			if (rules.TryGetValue(color, out List<string>? children))
			{
				int bagCount = 1;
				foreach (string thisChild in children)
				{
					var data = thisChild.Split("|");
					int childCount = int.Parse(data[0]);
					bagCount += childCount * Count(data[1]);
				}
				return bagCount;
			}
			else
			{
				return 1;
			}
		}

		public static void AddRule(string nextLine)
		{
			if (!nextLine.Contains("contain no other bags"))
			{
				var childColors = new List<string>();
				var parentColorRegex = new Regex(@"^(?<parent>[\w\s]+) bags contain");
				string parentColor = parentColorRegex.Match(nextLine).Groups["parent"].Value;
				var childColorsRegex = new Regex(@"(?<number>\d+) (?<color>[\w\s]+) bag");
				var childMatch = childColorsRegex.Match(nextLine);
				while (childMatch.Success)
				{
					string numberString = childMatch.Groups["number"].Value;
					string colorString = childMatch.Groups["color"].Value;
					childColors.Add(numberString + "|" + colorString);
					childMatch = childMatch.NextMatch();
				}
				rules[parentColor] = childColors;
			}
		}

	}
}
