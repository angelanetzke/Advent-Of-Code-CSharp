using System.Collections.Generic;

namespace Dec21
{
	internal class Game
	{
		private int nextRoll;
		private int player1Start;
		private int player2Start;
		private int player1Position;
		private int player2Position;
		int player1Score;
		int player2Score;
		private bool isPlayer1Turn;
		private int rollCount;
		private readonly Dictionary<(int, int, int, int, bool), long[]> cache;

		public Game(int player1Position, int player2Position)
		{
			nextRoll = 1;
			player1Start = player1Position;
			player2Start = player2Position;
			this.player1Position = player1Position;
			this.player2Position = player2Position;
			player1Score = 0;
			player2Score = 0;
			isPlayer1Turn = true;
			rollCount = 0;
			cache = new();
		}

		public void Next()
		{
			int thisRoll = nextRoll * 3 + 3;
			nextRoll += 3;
			if (isPlayer1Turn)
			{
				player1Position = (player1Position + thisRoll - 1) % 10 + 1;
				player1Score += player1Position;
			}
			else
			{
				player2Position = (player2Position + thisRoll - 1) % 10 + 1;
				player2Score += player2Position;
			}
			rollCount += 3;
			isPlayer1Turn = !isPlayer1Turn;
		}

		public int GetPlayer1Score()
		{
			return player1Score;
		}

		public int GetPlayer2Score()
		{
			return player2Score;
		}

		public int GetRollCount()
		{
			return rollCount;
		}

		public long[] CountWins()
		{
			return CountWins(player1Start, 0, player2Start, 0, true);
		}

		private long[] CountWins(
			int p1Position, int p1Score, int p2Position, int p2Score, bool isP1Turn)
		{
			long player1WinCount = 0L;
			long player2WinCount = 0L;
			for (int roll1 = 1; roll1 <= 3; roll1++)
			{
				for (int roll2 = 1; roll2 <= 3; roll2++)
				{
					for (int roll3 = 1; roll3 <= 3; roll3++)
					{
						int totalRoll = roll1 + roll2 + roll3;
						if (isP1Turn)
						{
							int[] newPositionAndScore = GetPositionAndScore(p1Position, p1Score, totalRoll);
							if (newPositionAndScore[1] >= 21)
							{
								player1WinCount++;
							}
							else
							{
								if (!cache.TryGetValue((newPositionAndScore[0], newPositionAndScore[1],
									p2Position, p2Score, !isP1Turn), out long[] subtotals))
								{
									subtotals = CountWins(newPositionAndScore[0], newPositionAndScore[1],
									p2Position, p2Score, !isP1Turn);
									cache[(newPositionAndScore[0], newPositionAndScore[1],
									p2Position, p2Score, !isP1Turn)] = subtotals;
								}
								player1WinCount += subtotals[0];
								player2WinCount += subtotals[1];
							}
						}
						else
						{
							int[] newPositionAndScore = GetPositionAndScore(p2Position, p2Score, totalRoll);
							if (newPositionAndScore[1] >= 21)
							{
								player2WinCount++;
							}
							else
							{
								if (!cache.TryGetValue((p1Position, p1Score,
									newPositionAndScore[0], newPositionAndScore[1], !isP1Turn), out long[] subtotals))
								{
									subtotals = CountWins(p1Position, p1Score,
									newPositionAndScore[0], newPositionAndScore[1], !isP1Turn);
									cache[(p1Position, p1Score,
									newPositionAndScore[0], newPositionAndScore[1], !isP1Turn)] = subtotals;
								}
								player1WinCount += subtotals[0];
								player2WinCount += subtotals[1];
							}
						}

					}
				}
			}
			return new long[] { player1WinCount, player2WinCount };
		}

		public static int[] GetPositionAndScore(int position, int score, int totalRoll)
		{
			position = (position + totalRoll - 1) % 10 + 1;
			score += position;
			return new int[] { position, score };
		}


	}
}
