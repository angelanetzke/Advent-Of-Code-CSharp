namespace Day07;

internal class CardWithJoker : IComparable<CardWithJoker>
{
	private readonly string hand;
	private readonly string pattern;
	private readonly int bid;
	private static readonly List<string> patternOrder 
		= ["11111", "2111", "221", "311", "32", "41", "5"];
	private static readonly List<char> cardOrder 
		= ['J', '2', '3', '4', '5', '6', '7', '8', '9', 'T','Q', 'K', 'A'];

	public CardWithJoker(string cardData)
	{
		string cardValues = cardData.Split(' ')[0];
		string cardBid = cardData.Split(' ')[1];
		hand = cardValues;
		bid = int.Parse(cardBid);
		pattern = GetPattern(hand);
	}

	private static string GetPattern(string hand)
	{
		Dictionary<char, int> counts = [];
		int jokerCount = 0;
		foreach (char thisCard in hand)
		{
			if (thisCard == 'J')
			{
				jokerCount++;
			}
			else if (counts.ContainsKey(thisCard))
			{
				counts[thisCard]++;
			}
			else
			{
				counts[thisCard] = 1;
			}
		}
		List<int> sortedValues = counts.Values.OrderByDescending(value => value).ToList();
		if (sortedValues.Count == 0)
		{
			sortedValues.Add(0);
		}
		sortedValues[0] += jokerCount;
		return string.Join("", sortedValues);
	}

	public int GetBid()
	{
		return bid;
	}

	public int CompareTo(CardWithJoker? other)
	{
		if (other != null)
		{
			int patternComparison = patternOrder.IndexOf(pattern)
				.CompareTo(patternOrder.IndexOf(other.pattern));
			if (patternComparison != 0)
			{
				return patternComparison;
			}
			for (int i = 0; i < hand.Length; i++)
			{
				int cardComparison = cardOrder.IndexOf(hand[i])
					.CompareTo(cardOrder.IndexOf(other.hand[i]));
				if (cardComparison != 0)
				{
					return cardComparison;
				}
			}
			return 0;
		}
		throw new ArgumentNullException();
	}

	public override string ToString()
	{
		return hand.ToString();
	}
}