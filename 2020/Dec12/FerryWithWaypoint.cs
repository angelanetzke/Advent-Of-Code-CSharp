using System.Text.RegularExpressions;

namespace Dec12
{
	internal class FerryWithWaypoint
	{
		private int shipX = 0;
		private int shipY = 0;
		private int waypointX = 10;
		private int waypointY = 1;

		public void Next(string nextCommand)
		{
			var commandRegex = new Regex(@"^(?<action>[A-Z]{1})(?<value>\d+)$");
			char action = commandRegex.Match(nextCommand).Groups["action"].Value.First();
			int value = int.Parse(commandRegex.Match(nextCommand).Groups["value"].Value);
			switch (action)
			{
				case 'N':
					waypointY += value;
					break;
				case 'S':
					waypointY -= value;
					break;
				case 'E':
					waypointX += value;
					break;
				case 'W':
					waypointX -= value;
					break;
				case 'L':
					for (int i = 0; i < value / 90; i++)
					{
						RotateLeft();
					}
					break;
				case 'R':
					for (int i = 0; i < value / 90; i++)
					{
						RotateRight();
					}
					break;
				case 'F':
					for (int i = 0; i < value; i++)
					{
						shipX += waypointX;
						shipY += waypointY;
					}
					break;
			}
		}

		private void RotateLeft()
		{
			double angle = Math.Atan2(waypointY, waypointX);
			angle += Math.PI / 2.0;
			double length = Math.Sqrt(Math.Pow(waypointX, 2.0) + Math.Pow(waypointY, 2.0));
			waypointX = (int)Math.Round(length * Math.Cos(angle));
			waypointY = (int)Math.Round(length * Math.Sin(angle));
		}

		private void RotateRight()
		{
			double angle = Math.Atan2(waypointY, waypointX);
			angle -= Math.PI / 2.0;
			double length = Math.Sqrt(Math.Pow(waypointX, 2.0) + Math.Pow(waypointY, 2.0));
			waypointX = (int)Math.Round(length * Math.Cos(angle));
			waypointY = (int)Math.Round(length * Math.Sin(angle));
		}

		public int[] GetCurrentLocation()
		{
			return new int[] { shipX, shipY };
		}

	}
}
