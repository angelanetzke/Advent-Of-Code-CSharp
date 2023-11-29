using System.Collections.Generic;
using System.Linq;

namespace Dec08
{
	internal class Display
	{
		private readonly Dictionary<string, int> solution;
		private readonly string[] outputs;
		private readonly string one;
		private readonly string four;
		private readonly string seven;

		public Display(string line)
		{
			solution = new();
			string[] inputs	= line.Split(" | ")[0].Split(" "); ;
			outputs = line.Split(" | ")[1].Split(" ");
			List<string> allEntries = new(inputs);
			allEntries.AddRange(outputs);
			foreach (string thisEntry in allEntries)
			{
				switch(thisEntry.Length)
				{
					case 2:						
						solution.TryAdd(thisEntry, 1);
						one = thisEntry;
						break;
					case 3:
						solution.TryAdd(thisEntry, 7);
						seven = thisEntry;
						break;
					case 4:
						solution.TryAdd(thisEntry, 4);
						four = thisEntry;
						break;
					case 7:
						solution.TryAdd(thisEntry, 8);
						break;
				}
			}
			foreach (string thisEntry in allEntries)
			{
				if (MatchesZero(thisEntry))
				{
					solution.TryAdd(thisEntry, 0);
				}
				if (MatchesTwo(thisEntry))
				{
					solution.TryAdd(thisEntry, 2);
				}
				if (MatchesThree(thisEntry))
				{
					solution.TryAdd(thisEntry, 3);
				}
				if (MatchesFive(thisEntry))
				{
					solution.TryAdd(thisEntry, 5);
				}				
				if (MatchesSix(thisEntry))
				{
					solution.TryAdd(thisEntry, 6);
				}
				if (MatchesNine(thisEntry))
				{
					solution.TryAdd(thisEntry, 9);
				}
			}
		}

		public long GetOutput()
		{
			long result = 0L;
			foreach (string thisOutput in outputs)
			{
					result = result * 10 + solution[thisOutput];
			}
			return result;
		}

		private bool MatchesZero(string entry)
		{
			if (entry.Length != 6)
			{
				return false;
			}
			if (entry.ToCharArray().Count(element => one.Contains(element)) != 2)
			{
				return false;
			}
			if (entry.ToCharArray().Count(element => four.Contains(element)) != 3)
			{
				return false;
			}
			if (entry.ToCharArray().Count(element => seven.Contains(element)) != 3)
			{
				return false;
			}
			return true;
		}

		private bool MatchesTwo(string entry)
		{
			if (entry.Length != 5)
			{
				return false;
			}
			if (entry.ToCharArray().Count(element => one.Contains(element)) != 1) {
				return false;
			}
			if (entry.ToCharArray().Count(element => four.Contains(element)) != 2)
			{
				return false;
			}
			if (entry.ToCharArray().Count(element => seven.Contains(element)) != 2)
			{
				return false;
			}
			return true;
		}

		private bool MatchesThree(string entry)
		{
			if (entry.Length != 5)
			{
				return false;
			}
			if (entry.ToCharArray().Count(element => one.Contains(element)) != 2)
			{
				return false;
			}
			if (entry.ToCharArray().Count(element => four.Contains(element)) != 3)
			{
				return false;
			}
			if (entry.ToCharArray().Count(element => seven.Contains(element)) != 3)
			{
				return false;
			}
			return true;
		}

		private bool MatchesFive(string entry)
		{
			if (entry.Length != 5)
			{
				return false;
			}
			if (entry.ToCharArray().Count(element => one.Contains(element)) != 1)
			{
				return false;
			}
			if (entry.ToCharArray().Count(element => four.Contains(element)) != 3)
			{
				return false;
			}
			if (entry.ToCharArray().Count(element => seven.Contains(element)) != 2)
			{
				return false;
			}
			return true;
		}

		private bool MatchesSix(string entry)
		{
			if (entry.Length != 6)
			{
				return false;
			}
			if (entry.ToCharArray().Count(element => one.Contains(element)) != 1)
			{
				return false;
			}
			if (entry.ToCharArray().Count(element => four.Contains(element)) != 3)
			{
				return false;
			}
			if (entry.ToCharArray().Count(element => seven.Contains(element)) != 2)
			{
				return false;
			}
			return true;
		}

		private bool MatchesNine(string entry)
		{
			if (entry.Length != 6)
			{
				return false;
			}
			if (entry.ToCharArray().Count(element => one.Contains(element)) != 2)
			{
				return false;
			}
			if (entry.ToCharArray().Count(element => four.Contains(element)) != 4)
			{
				return false;
			}
			if (entry.ToCharArray().Count(element => seven.Contains(element)) != 3)
			{
				return false;
			}
			return true;
		}

	}
}
