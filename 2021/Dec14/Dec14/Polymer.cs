using System.Collections.Generic;
using System.Linq;

namespace Dec14
{
	internal class Polymer
	{
		private readonly Dictionary<string, char> rules;
		private Dictionary<string, long> pairCount;
		private readonly Dictionary<char, long> elementCount;
		public Polymer(string template)
		{
			rules = new();
			pairCount = new();
			for (int i = 0; i < template.Length - 1; i++)
			{
				string thisNewKey = template.Substring(i, 2);
				if (pairCount.TryGetValue(thisNewKey, out long count))
				{
					pairCount[thisNewKey] = count + 1L;
				}
				else
				{
					pairCount[thisNewKey] = 1L;
				}
			}
			elementCount = new();
			foreach (char thisChar in template)
			{
				if (elementCount.TryGetValue(thisChar, out long count))
				{
					elementCount[thisChar] = count + 1L;
				}
				else
				{
					elementCount[thisChar] = 1L;
				}
			}
		}

		public void AddRule(string ruleString)
		{
			string pair = ruleString.Split(" -> ")[0];
			char middleChar = ruleString.Split(" -> ")[1][0];
			rules.Add(pair, middleChar);
		}

		public void Execute(int times)
		{
			for (int iteration = 1; iteration <= times; iteration++)
			{
				Dictionary<string, long> newPairCount = new();
				foreach (string thisKey in pairCount.Keys)
				{
					char thisMiddleChar = rules[thisKey];
					if (elementCount.TryGetValue(thisMiddleChar, out long count))
					{
						elementCount[thisMiddleChar] = count + pairCount[thisKey];
					}
					else
					{
						elementCount[thisMiddleChar] = pairCount[thisKey];
					}
					string firstNewKey = thisKey[0] + thisMiddleChar.ToString();					
					if (newPairCount.TryGetValue(firstNewKey, out long firstCount))
					{
						newPairCount[firstNewKey] = firstCount + pairCount[thisKey];
					}
					else
					{
						newPairCount[firstNewKey] = pairCount[thisKey];
					}
					string secondNewKey = thisMiddleChar.ToString() + thisKey[1];
					if (newPairCount.TryGetValue(secondNewKey, out long secondCount))
					{
						newPairCount[secondNewKey] = secondCount + pairCount[thisKey];
					}
					else
					{
						newPairCount[secondNewKey] = pairCount[thisKey];
					}
				}
				pairCount = newPairCount;
			}
		}

		public long GetAnswer()
		{
			return elementCount.Values.Max() - elementCount.Values.Min();
		}


	}
}
