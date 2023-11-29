namespace Dec11
{
	internal class Robot
	{
		private enum DIRECTION { N, E, S, W};
		private DIRECTION currentDirection = DIRECTION.N;
		private int x = 0;
		private int y = 0;

		public (int, int) GetPosition()
		{
			return (x, y);
		}

		public void Rotate(int instruction)
		{
			if (instruction == 0)
			{
				currentDirection = (DIRECTION)((int)(currentDirection + 3) % 4);
			}
			else
			{
				currentDirection = (DIRECTION)((int)(currentDirection + 1) % 4);
			}
		}

		public void Move()
		{
			switch (currentDirection)
			{
				case DIRECTION.N:
					y++;
					break;
				case DIRECTION.E:
					x++;
					break;
				case DIRECTION.S:
					y--;
					break;
				case DIRECTION.W:
					x--;
					break;
			}
		}

	}


}
