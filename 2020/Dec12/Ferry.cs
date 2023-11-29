using System.Text.RegularExpressions;

namespace Dec12
{
	internal class Ferry
	{
		private int x = 0;
		private int y = 0;
		enum Direction { N, E, S, W};
		private Direction currentDirection = Direction.E;
		private static readonly int DIRECTION_COUNT = 4;

		public void Next(string nextCommand)
		{
			var commandRegex = new Regex(@"^(?<action>[A-Z]{1})(?<value>\d+)$");
			char action = commandRegex.Match(nextCommand).Groups["action"].Value.First();
			int value = int.Parse(commandRegex.Match(nextCommand).Groups["value"].Value);
			switch(action)
			{
				case 'N':
					MoveNorth(value);
					break;
				case 'S':
					MoveSouth(value);
					break;
				case 'E':
					MoveEast(value);
					break;
				case 'W':
					MoveWest(value);
					break;
				case 'L':
					for (int i =0; i < value / 90; i++)
					{
						RotateLeft();
					}
					break;
				case 'R':
					for (int i =0; i < value / 90; i++)
					{
						RotateRight();
					}
					break;
				case 'F':
					switch(currentDirection)
					{
						case Direction.N:
							MoveNorth(value);
							break;
						case Direction.S:
							MoveSouth(value);
							break;
						case Direction.E:
							MoveEast(value);
							break;
						case Direction.W:
							MoveWest(value);
							break;
					}
					break;
			}
		}

		public int[] GetCurrentLocation()
		{
			return new int[] { x, y };
		}

		private void MoveNorth(int value)
		{
			y += value;
		}

		private void MoveSouth(int value)
		{
			y -= value;
		}

		private void MoveEast(int value)
		{
			x += value;
		}

		private void MoveWest(int value)
		{
			x -= value;
		}

		private void RotateLeft()
		{
			currentDirection = (Direction)(((int)currentDirection - 1 + DIRECTION_COUNT) % DIRECTION_COUNT);
		}

		private void RotateRight()
		{
			currentDirection = (Direction)(((int)currentDirection + 1) % DIRECTION_COUNT);
		}

	}
}
