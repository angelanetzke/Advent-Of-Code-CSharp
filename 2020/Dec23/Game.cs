using System.Text;

namespace Dec23
{
	internal class Game
	{
		private Cup current;
		private readonly int size;
		private readonly bool isLargeGame;
		private Dictionary<int, Cup> cupsByIndex = new();

		public Game(string start, bool isLargeGame)
		{
			this.isLargeGame = isLargeGame;
			if (isLargeGame)
			{
				size = 1000000;
			}
			else
			{
				size = start.Length;
			}
			current = new Cup(int.Parse(start[0].ToString()));
			cupsByIndex[current.GetLabel()] = current;
			Cup cursor = current;
			for (int i = 1; i < size; i++)
			{
				Cup newCup;
				if (i < start.Length)
				{
					newCup = new Cup(int.Parse(start[i].ToString()));
				}
				else
				{
					newCup = new Cup(i + 1);
				}
				cupsByIndex[newCup.GetLabel()] = newCup;
				cursor.SetNext(newCup);
				cursor = newCup;
			}
			cursor.SetNext(current);
		}

		public void Play(int rounds)
		{
			List<int> removedCupLabels = new();
			for (int turn = 0; turn < rounds; turn++)
			{
				removedCupLabels.Clear();
				var cursor = current;
				var removedStart = current.GetNext();
				for (int i = 0; i < 3; i++)
				{
					cursor = cursor.GetNext();
					removedCupLabels.Add(cursor.GetLabel());
				}
				var removedEnd = cursor;
				cursor = cursor.GetNext();
				int destinationLabel = current.GetLabel() - 1;
				if (destinationLabel < 1)
				{
					destinationLabel = size;
				}
				while (removedCupLabels.Contains(destinationLabel))
				{
					destinationLabel--;
					if (destinationLabel < 1)
					{
						destinationLabel = size;
					}
				}
				var destinationCup = cupsByIndex[destinationLabel];
				var afterDestination = destinationCup.GetNext();
				destinationCup.SetNext(removedStart);
				removedEnd.SetNext(afterDestination);
				current.SetNext(cursor);
				current = current.GetNext();
			}
		}

		public string GetLabels()
		{
			if (isLargeGame)
			{
				Cup? cursor = current;
				while (cursor != null && cursor.GetLabel() != 1)
				{
					cursor = cursor.GetNext();
				}
				cursor = cursor.GetNext();
				long label1 = cursor.GetLabel();
				cursor = cursor.GetNext();
				long label2 = cursor.GetLabel();
				return (label1 * label2).ToString();
			}
			else
			{
				Cup? cursor = current;
				while (cursor != null && cursor.GetLabel() != 1)
				{
					cursor = cursor.GetNext();
				}
				var builder = new StringBuilder();
				for (int i = 1; i <= size - 1; i++)
				{
					cursor = cursor.GetNext();
					builder.Append(cursor.GetLabel());
				}
				return builder.ToString();
			}
		}

	}
}
