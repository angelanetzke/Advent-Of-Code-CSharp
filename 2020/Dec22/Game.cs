using System.Text;
using System.Text.RegularExpressions;

namespace Dec22
{
	internal class Game
	{
		private readonly List<int> player1Start = new();
		private readonly List<int> player2Start = new();
		private readonly HashSet<string> previousStates = new();
		public Game(string[] allLines)
		{
			bool isPlayer1Filling = true;
			foreach(string thisLine in allLines)
			{
				if (thisLine.Length == 0)
				{
					isPlayer1Filling = false;
				}
				else if (new Regex(@"^\d+$").IsMatch(thisLine))
				{
					if (isPlayer1Filling)
					{
						player1Start.Add(int.Parse(thisLine));
					}
					else
					{
						player2Start.Add(int.Parse(thisLine));
					}
				}
			}
		}

		public Game(List<int> player1, List<int> player2)
		{
			player1Start = new List<int>(player1);
			player2Start = new List<int>(player2);
		}

		public int Play()
		{
			var player1 = new List<int>(player1Start);
			var player2 = new List<int>(player2Start);
			while (player1.Count > 0 && player2.Count > 0)
			{
				int player1Card = player1[0];
				player1.RemoveAt(0);
				int player2Card = player2[0];
				player2.RemoveAt(0);
				if (player1Card > player2Card)
				{
					player1.Add(player1Card);
					player1.Add(player2Card);
				}
				else
				{
					player2.Add(player2Card);
					player2.Add(player1Card);
				}
			}
			if (player1.Count > player2.Count)
			{
				return CalculateScore(player1);
			}
			else
			{
				return CalculateScore(player2);
			}
		}

		//returns score as string and winner as string ("player1" or "player2")
		public string[] PlayRecursive()
		{
			var player1 = new List<int>(player1Start);
			var player2 = new List<int>(player2Start);
			while (player1.Count > 0 && player2.Count > 0)
			{
				if (!SaveState(player1, player2))
				{
					return new string[] { CalculateScore(player1).ToString(), "player1" };
				}
				int player1Card = player1[0];
				int player2Card = player2[0];
				player1.RemoveAt(0);
				player2.RemoveAt(0);
				if (player1.Count >= player1Card && player2.Count >= player2Card)
				{
					var subdeck1 = player1.GetRange(0, player1Card);
					var subdeck2 = player2.GetRange(0, player2Card);
					if (new Game(subdeck1, subdeck2).PlayRecursive()[1] == "player1")
					{
						player1.Add(player1Card);
						player1.Add(player2Card);
					}
					else
					{
						player2.Add(player2Card);
						player2.Add(player1Card);
					}
				}
				else
				{
					if (player1Card > player2Card)
					{
						player1.Add(player1Card);
						player1.Add(player2Card);
					}
					else
					{
						player2.Add(player2Card);
						player2.Add(player1Card);
					}
				}
			}
			if (player1.Count > player2.Count)
			{
				return new string[] { CalculateScore(player1).ToString(), "player1" };
			}
			else
			{
				return new string[] { CalculateScore(player2).ToString(), "player2" };
			}
		}

		private static int CalculateScore(List<int> hand)
		{
			int score = 0;
			for (int i = 0; i < hand.Count; i++)
			{
				score += (hand.Count - i) * hand[i];
			}
			return score;
		}

		private bool SaveState(List<int> list1, List<int> list2)
		{
			var builder = new StringBuilder();
			foreach (int i in list1)
			{
				builder.Append(i);
				builder.Append(':');
			}
			builder.Append('-');
			foreach (int i in list2)
			{
				builder.Append(i);
				builder.Append(':');
			}
			return previousStates.Add(builder.ToString());
		}


	}
}
