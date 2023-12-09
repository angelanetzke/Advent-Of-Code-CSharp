namespace Day09;

internal class History
{
	private readonly List<int> historyValues;

	public History(string valueString)
	{
		string[] splitValues = valueString.Split(' ');
		historyValues = [];
		foreach (string thisSplitValue in splitValues)
		{
			historyValues.Add(int.Parse(thisSplitValue));
		}		
	}

	public int GetNextValue()
	{
		return GetNextValue(historyValues);
	}

	public int GetPreviousValue()
	{
		List<int> startValues = new(historyValues);
		startValues.Reverse();
		return GetNextValue(startValues.ToList());
	}
	
	private static int GetNextValue(List<int> startValues)
	{
		List<int> nextLine = new(startValues);
		List<int> temp = [];
		List<int> finalValues = [];
		finalValues.Add(startValues.Last());
		while (nextLine.Count(x => x == 0) < nextLine.Count)
		{
			temp.Clear();
			for (int i = 0; i < nextLine.Count - 1; i++)
			{
				temp.Add(nextLine[i + 1] - nextLine[i]);
			}
			finalValues.Add(temp.Last());
			nextLine = new(temp);
		}
		return finalValues.Sum();
	}

}

