namespace Dec5
{
	internal class Seat
	{
		private readonly string code;

		public Seat(string code)
		{
			this.code = code;
		}

		public int GetSeatID()
		{
			int minRow = 0;
			int maxRow = 127;
			for (int i = 0; i < 7; i++)
			{
				int size = maxRow - minRow + 1;
				if (code[i] == 'F')
				{
					maxRow -= size / 2;
				}
				else if (code[i] == 'B')
				{
					minRow += size / 2;
				}
			}
			if (minRow != maxRow)
			{
				return -1;
			}
			int minColumn = 0;
			int maxColumn = 7;
			for (int i = 7; i < 10; i++)
			{
				int size = maxColumn - minColumn + 1;
				if (code[i] == 'L')
				{
					maxColumn -= size / 2;
				}
				else if (code[i] == 'R')
				{
					minColumn += size / 2;
				}
			}
			return minRow * 8 + minColumn;
		}

	}
}
