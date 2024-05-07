using System.Text;

namespace Day11;

internal class Password
{
	private static readonly List<char> characters = [
		'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'j', 'k', 'm',
		'n', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z'
	];

	private readonly int[] indices;

	public Password(string data)
	{
		indices = new int[data.Length];
		for (int i = 0; i < data.Length; i++)
		{
			indices[i] = characters.IndexOf(data[i]);
		}
	}

	public void Increment()
	{
		int i = indices.Length - 1;
		while (i >= 0)
		{
			indices[i]++;
			if (indices[i] < characters.Count)
			{
				break;
			}
			if (i == 0)
			{
				for  (int j = 0; j < indices.Length; j++)
				{
					indices[j] = 0;
				}
			}
			else
			{
				indices[i] %= characters.Count;
				i--;
			}
			
		}
	}

	public bool IsValid()
	{
		bool hasStraight = false;
		for (int i = 2; i < indices.Length; i++)
		{
			if (indices[i -2] + 2 == indices[i] && indices[i - 1] + 1 == indices[i])
			{
				hasStraight = true;
				break;
			}
		}
		if (!hasStraight)
		{
			return false;
		}
		bool hasTwoPairs = false;
		for (int i = 1; i < indices.Length - 2; i++)
		{
			for (int j = i + 2; j < indices.Length; j++)
			{
				if (indices[i - 1] == indices[i] && indices[j - 1] == indices[j])
				{
					hasTwoPairs = true;
					break;
				}
			}
			if (hasTwoPairs)
			{
				break;
			}
		}
		if (!hasTwoPairs)
		{
			return false;
		}
		return true;
	}

	public override string ToString()
	{
		StringBuilder builder = new ();
		for (int i = 0; i < indices.Length; i++)
		{
			builder.Append(characters[indices[i]]);
		}
		return builder.ToString();
	}
}