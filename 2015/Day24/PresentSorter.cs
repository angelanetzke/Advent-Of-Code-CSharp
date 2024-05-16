namespace Day24;

internal class PresentSorter
{
	private readonly int[] weights;
	private readonly int targetWeight;

	public PresentSorter(string[] allLines, bool isPart2 = false)
	{
		weights = allLines.Select(int.Parse).ToArray();
		if (isPart2)
		{
			targetWeight = weights.Sum() / 4;
		}
		else
		{
			targetWeight = weights.Sum() / 3;
		}		
	}

	public long GetQuantumEntanglement()
	{
		long highestIndexCombination = FindSmallestGroup();
		return highestIndexCombination;
	}

	private long FindSmallestGroup()
	{
		for (int size = 1; size <= weights.Length; size++)
		{
			List<int[]> groups = FindGroupofSize(size);
			long minQuantumEntanglement = long.MaxValue;
			if (groups.Count > 0)
			{
				foreach (int[] thisGroup in groups)
				{
					long quantumEntanglement = 1;
					foreach (int index in thisGroup)
					{
						quantumEntanglement *= weights[index];
					}
					minQuantumEntanglement = Math.Min(minQuantumEntanglement, quantumEntanglement);
				}
				return minQuantumEntanglement;
			}
		}
		return -1;
	}

	private List<int[]> FindGroupofSize(int size)
	{
		List<int[]> groups = [];
		int[] indices = new int[size];
		for (int i = weights.Length - 1, j = 0; i >= weights.Length - size; i--, j++)
		{
			indices[j] = i;
		}
		bool isComplete = false;
		while (!isComplete)
		{
			int sum = 0;
			foreach (int index in indices)
			{
				sum += weights[index];
			}
			if (sum == targetWeight)
			{
				int[] indicesCopy = new int[size];
				Array.Copy(indices, indicesCopy, size);
				groups.Add(indicesCopy);
			}
			for (int i = indices.Length - 1; i >= 0; i--)
			{
				indices[i]--;
				if (indices[i] < size - (i + 1))
				{
					if (i == 0)
					{
						isComplete = true;
						break;
					}
					indices[i] = Math.Max(indices[i - 1] - 2, size - (i + 1));
				}
				else
				{
					break;
				}
			}			
		}
		return groups;
	}

}


	