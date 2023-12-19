using System.Text.RegularExpressions;

namespace Day19;

internal class Workflow
{
	private static readonly Dictionary<string, Workflow> workflowList = [];
	private readonly string[] rules;
	public Workflow(string data)
	{
		string workflowID = data[..data.IndexOf('{')];
		rules = Regex.Match(data, @"{(.+)}").Groups[1].Value.Split(',');
		workflowList[workflowID] = this;
	}

	public static int GetAcceptedValue(string partString)
	{
		bool isAccepted = workflowList["in"].IsAccepted(partString);
		return isAccepted ? GetPartValue(partString) : 0;
	}

	public static long GetAcceptableCount()
	{
		PartRange startRange = new ();
		List<PartRange> result = [];
		workflowList["in"].GetAppectableRules(startRange, result);
		long count = 0;
		foreach (PartRange thisRange in result)
		{
			long product = 1L;
			foreach (char thisKey in thisRange.ranges.Keys)
			{
				product *= thisRange.ranges[thisKey].Item2 - thisRange.ranges[thisKey].Item1 + 1;
			}
			count += product;
		}
		return count;
	}

	private static int GetPartValue(string partString)
	{
		int value = 0;
		foreach (char thisCategory in new char[] {'x', 'm', 'a', 's'})
		{
			string regex = thisCategory + @"=(\d+)";
			string match = Regex.Match(partString, regex).Groups[1].Value;
			if (match.Length > 0)
			{
				value += int.Parse(match);
			}
		}
		return value;
	}

	private bool IsAccepted(string partString)
	{
		for (int i = 0; i < rules.Length; i++)
		{
			if (rules[i] == "A")
			{
				return true;
			}
			if (rules[i] == "R")
			{
				return false;
			}
			if (!rules[i].Contains('<') && !rules[i].Contains('>'))
			{
				return workflowList[rules[i]].IsAccepted(partString);
			}
			string[] ruleTokens = rules[i].Split(':');
			char category = ruleTokens[0][0];
			string regex = category + @"=(\d+)";
			int partValue = int.Parse(Regex.Match(partString, regex).Groups[1].Value);
			char comparison = ruleTokens[0][1];
			int ruleValue = int.Parse(ruleTokens[0][2..]);
			bool isRuleMatch;
			if (comparison == '<')
			{
				isRuleMatch = partValue < ruleValue;
			}
			else
			{
				isRuleMatch = partValue > ruleValue;
			}
			if (isRuleMatch)
			{
				if (ruleTokens[1] == "A")
				{
					return true;
				}
				else if (ruleTokens[1] == "R")
				{
					return false;
				}
				else
				{
					return workflowList[ruleTokens[1]].IsAccepted(partString);
				}
			}
		}
		return false;
	}

	private void GetAppectableRules(PartRange currentRange, List<PartRange> acceptableList)
	{
		for (int i = 0; i < rules.Length; i++)
		{
			if (rules[i] == "A")
			{
				acceptableList.Add(currentRange);
				return;
			}
			if (rules[i] == "R")
			{
				return;
			}
			if (!rules[i].Contains('<') && !rules[i].Contains('>'))
			{
				workflowList[rules[i]]
						.GetAppectableRules(currentRange, acceptableList);
				return;
			}
			string[] ruleTokens = rules[i].Split(':');
			char category = ruleTokens[0][0];
			string regex = category + @"=(\d+)";
			char comparison = ruleTokens[0][1];
			int ruleValue = int.Parse(ruleTokens[0][2..]);
			(int, int) categoryRange = currentRange.ranges[category];
			if (comparison == '<')
			{
				if (categoryRange.Item2 < ruleValue)
				{
					if (ruleTokens[1] == "A")
					{
						acceptableList.Add(currentRange);
						return;
					}
					if (ruleTokens[1] == "R")
					{
						return;
					}
					workflowList[ruleTokens[1]]
						.GetAppectableRules(currentRange, acceptableList);
				}
				else if (categoryRange.Item1 < ruleValue && ruleValue <= categoryRange.Item2)
				{
					PartRange lowerRange = new (currentRange);
					lowerRange.ranges[category] = (categoryRange.Item1, ruleValue - 1);
					PartRange upperRange = new (currentRange);
					upperRange.ranges[category] = (ruleValue, categoryRange.Item2);
					if (ruleTokens[1] == "A")
					{
						acceptableList.Add(lowerRange);
					}
					else if (ruleTokens[1] != "R")
					{
						workflowList[ruleTokens[1]]
							.GetAppectableRules(lowerRange, acceptableList);
					}					
					currentRange = upperRange;
				}
			}
			else
			{
				if (categoryRange.Item1 > ruleValue)
				{
					if (ruleTokens[1] == "A")
					{
						acceptableList.Add(currentRange);
						return;
					}
					if (ruleTokens[1] == "R")
					{
						return;
					}
					workflowList[ruleTokens[1]]
						.GetAppectableRules(currentRange, acceptableList);
				}
				else if (categoryRange.Item1 <= ruleValue && ruleValue < categoryRange.Item2)
				{
					PartRange lowerRange = new (currentRange);
					lowerRange.ranges[category] = (categoryRange.Item1, ruleValue);
					PartRange upperRange = new (currentRange);
					upperRange.ranges[category] = (ruleValue + 1, categoryRange.Item2);
					if (ruleTokens[1] == "A")
					{
						acceptableList.Add(upperRange);
					}
					else if (ruleTokens[1] != "R")
					{
						workflowList[ruleTokens[1]]
							.GetAppectableRules(upperRange, acceptableList);
					}					
					currentRange = lowerRange;
				}
			}
		}
	}

	private class PartRange
	{
		public Dictionary<char, (int, int)> ranges = new ()
		{
			{'x', (1, 4000)},
			{'m', (1, 4000)},
			{'a', (1, 4000)},
			{'s', (1, 4000)}
		};

		public PartRange()
		{}

		public PartRange(PartRange other)
		{
			ranges = new Dictionary<char, (int, int)>(other.ranges);
		}
	}

}