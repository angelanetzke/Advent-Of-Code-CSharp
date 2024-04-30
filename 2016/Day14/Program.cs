using System.Security.Cryptography;
using System.Text;

string input = File.ReadAllLines("input.txt")[0];
(int, int) result = Part1AndPart2(input);
Console.WriteLine($"Part 1: {result.Item1}");
Console.WriteLine($"Part 2: {result.Item2}");

static (int, int) Part1AndPart2(string input)
{
	(int, int) result = (-1, -1);
	int keysToCollect = 64;
	List<(char, int)> candidateKeys = []; // repeated char, index
	List<int> confirmedKeys = []; // index
	List<(char, int)> staleCandidates = []; // repeated char, index
	for (int i = 0; i <= 1; i++)
	{
		candidateKeys.Clear();
		confirmedKeys.Clear();
		staleCandidates.Clear();	
		byte[] hash;
		char[] hashArray;	
		int currentIndex = 0;
		while (confirmedKeys.Count < keysToCollect 
			|| candidateKeys.Where(x => x.Item2 < confirmedKeys[keysToCollect - 1]).Any())
		{
			hash = MD5.HashData(Encoding.ASCII.GetBytes(input + currentIndex.ToString()));
			if (i == 1)
			{
				for (int j = 0; j < 2016; j++)
				{
					hash = MD5.HashData(Encoding.ASCII.GetBytes(string.Join("", hash.Select(x => x.ToString("x2")))));
				}
			}
			hashArray = string.Join("", hash.Select(x => x.ToString("x2"))).ToCharArray();
			char? threeInARowChar = GetThreeInARow(hashArray);
			if (threeInARowChar != null)
			{
				candidateKeys.Add(((char, int))(threeInARowChar, currentIndex));
			}		
			foreach ((char repeatedChar, int candidateIndex) in candidateKeys)
			{
				if (currentIndex == candidateIndex)
				{
					continue;
				}
				if (currentIndex - candidateIndex > 1000)
				{
					staleCandidates.Add((repeatedChar, candidateIndex));
					continue;
				}
				if (HasFiveInARow(hashArray, repeatedChar))
				{
					if (!confirmedKeys.Contains(candidateIndex))
					{
						confirmedKeys.Add(candidateIndex);
					}
				}
			}
			foreach ((char repeatedChar, int candidateIndex) in staleCandidates)
			{
				candidateKeys.Remove((repeatedChar, candidateIndex));
			}
			staleCandidates.Clear();
			confirmedKeys.Sort();
			currentIndex++;
		}
		if (i == 0)
		{
			result.Item1 = confirmedKeys[keysToCollect - 1];
		}
		else
		{
			result.Item2 = confirmedKeys[keysToCollect - 1];
		}
	}	
	return result;
}

static char? GetThreeInARow(char[] hashArray)
{
	for (int i = 2; i < hashArray.Length; i++)
	{
		if (hashArray[i] == hashArray[i - 1] && hashArray[i] == hashArray[i - 2])
		{
			return hashArray[i];
		}
	}
	return null;
}

static bool HasFiveInARow(char[] hashArray, char c)
{
	for (int i = 4; i < hashArray.Length; i++)
	{
		if (hashArray[i] == c 
			&& hashArray[i] == hashArray[i - 1] 
			&& hashArray[i] == hashArray[i - 2] 
			&& hashArray[i] == hashArray[i - 3] 
			&& hashArray[i] == hashArray[i - 4])
		{
			return true;
		}
	}
	return false;
}