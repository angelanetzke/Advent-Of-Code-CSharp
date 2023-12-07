using Day07;

string[] allLines = File.ReadAllLines("input.txt");
Console.WriteLine($"Part1 : {Part1(allLines)}");
Console.WriteLine($"Part2 : {Part2(allLines)}");


static int Part1(string[] allLines)
{
	List<Card> cardList = [];
	foreach (string thisLine in allLines)
	{
		cardList.Add(new Card(thisLine));
	}
	cardList.Sort();
	int score = 0;
	for (int rank = 1; rank <= cardList.Count; rank++)
	{
		score += rank * cardList[rank - 1].GetBid();
	}
	return score;
}

static int Part2(string[] allLines)
{
	List<CardWithJoker> cardList = [];
	foreach (string thisLine in allLines)
	{
		cardList.Add(new CardWithJoker(thisLine));
	}
	cardList.Sort();
	int score = 0;
	for (int rank = 1; rank <= cardList.Count; rank++)
	{
		score += rank * cardList[rank - 1].GetBid();
	}
	return score;
}
