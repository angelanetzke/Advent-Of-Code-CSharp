namespace Day09
{
	internal class MarbleCirle
	{
		private Marble current;
		private int nextValue = 0;
		private static readonly int divisibleBy = 23;
		private static readonly int scoreMarbleOffset = -7;

		public MarbleCirle()
		{
			current = new Marble(nextValue);
			nextValue++;
		}

		public int PlayNext()
		{
			if (nextValue % divisibleBy == 0)
			{
				var score = nextValue;
				var marbleToRemove = Get(scoreMarbleOffset);
				current = marbleToRemove.GetNext();
				score += marbleToRemove.GetValue();
				Remove(marbleToRemove);
				nextValue++;
				return score;
			}
			else
			{				
				var newMarble = new Marble(nextValue);
				current = current.GetNext();
				var oldNext = current.GetNext();
				current.SetNext(newMarble);
				newMarble.SetNext(oldNext);
				oldNext.SetPrevious(newMarble);
				newMarble.SetPrevious(current);
				current = newMarble;
				nextValue++;
				return 0;
			}
		}

		private Marble Get(int offset)
		{
			var cursor = current;
			if (offset > 0)
			{
				for (int i = 1; i <= offset; i++)
				{
					cursor = cursor.GetNext();
				}
			}
			if (offset < 0)
			{
				for (int i = 1; i <= Math.Abs(offset); i++)
				{
					cursor = cursor.GetPrevious();
				}
			}
			return cursor;
		}

		private void Remove(Marble cursor)
		{
			var oldPrevious = cursor.GetPrevious();
			var oldNext = cursor.GetNext();
			oldPrevious.SetNext(oldNext);
			oldNext.SetPrevious(oldPrevious);
			cursor.SetPrevious(cursor);
			cursor.SetNext(cursor);
		}

		public override string ToString()
		{
			var valueList = new List<int>();
			var cursor = current;
			do
			{
				valueList.Add(cursor.GetValue());
				cursor = cursor.GetNext();
			} while (cursor != current);
			return string.Join(", ", valueList);
		}

		private class Marble
		{
			private int value;
			private Marble next;
			private Marble previous;

			public Marble(int value)
			{
				this.value = value;
				previous = this;
				next = this;
			}

			public int GetValue()
			{
				return value;
			}

			public void SetPrevious(Marble newPrevious)
			{
				previous = newPrevious;
			}

			public void SetNext(Marble newNext)
			{
				next = newNext;
			}

			public Marble GetPrevious()
			{
				return previous;
			}

			public Marble GetNext()
			{
				return next;
			}

		}
	}
}