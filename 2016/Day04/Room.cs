using System.Text;

namespace Day04;

internal class Room
{
	private readonly string encryptedName;
	private readonly int sectorID;
	private readonly string checksum;

	public Room(string data)
	{
		var lastDash = data.LastIndexOf('-');
		encryptedName = data[..lastDash];
		var firstBracket = data.IndexOf('[');
		sectorID = int.Parse(data[(lastDash + 1)..firstBracket]);
		checksum = data[(firstBracket + 1)..(firstBracket + 6)];
	}

	public bool IsReal()
	{
		var counts = new Dictionary<char, int>();
		foreach (char thisChar in encryptedName)
		{
			if (thisChar == '-')
			{
				continue;
			}
			if (counts.ContainsKey(thisChar))
			{
				counts[thisChar]++;
			}
			else
			{
				counts[thisChar] = 1;
			}
		}
		var topFive = counts.OrderByDescending(x => x.Value)
			.ThenBy(x => x.Key)
			.Take(5)
			.Select(x => x.Key);
		return checksum == string.Join("", topFive);
	}

	public int GetSectorID()
	{
		return sectorID;
	}

	public string Decrpyt()
	{
		var builder = new StringBuilder();
		foreach (char thisChar in encryptedName)
		{
			if (thisChar == '-')
			{
				builder.Append(' ');
			}
			else
			{
				builder.Append((char)((thisChar - 'a' + sectorID) % 26 + 'a'));
			}
		}
		return builder.ToString();
	}
}