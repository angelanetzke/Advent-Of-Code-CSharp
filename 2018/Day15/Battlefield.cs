using System.Text;

namespace Day15
{
	internal class Battlefield
	{
		private readonly HashSet<(int, int)> spaces = new ();
		private List<Warrior> warriorList = new ();
		private int completeTurnCount = 0;
		private int startingElfCount = 0;
		private int currentElfCount = 0;

		public Battlefield(string[] allLines, int elfAttackPower)
		{
			Warrior.SetElfAttackPower(elfAttackPower);
			for (int row = 0; row < allLines.Length; row++)
			{
				for (int column = 0; column < allLines[row].Length; column++)
				{
					if (allLines[row][column] == '.')
					{
						spaces.Add((row, column));
					}
					if (allLines[row][column] == 'E')
					{
						startingElfCount++;
						warriorList.Add(new Warrior(row, column, allLines[row][column]));
						spaces.Add((row, column));
					}
					if (allLines[row][column] == 'G')
					{
						warriorList.Add(new Warrior(row, column, allLines[row][column]));
						spaces.Add((row, column));
					}
				}
			}
		}

		public bool TakeTurn()
		{
			warriorList = warriorList.OrderBy(w => w.GetLocation()).ToList();
			foreach (Warrior thisWarrior in warriorList)
			{
				if (thisWarrior.isRemoved)
				{
					continue;
				}
				if (!thisWarrior.TakeTurn(warriorList, spaces))
				{
					return false;
				}
			}
			warriorList = warriorList.Where(w => !w.isRemoved).ToList();
			currentElfCount = warriorList.Count(w => w.GetWarriorType() == 'E');
			completeTurnCount++;
			return true;			
		}

		public (int, int, int) GetStats()
		{
			var totalHP = warriorList.Where(w => !w.isRemoved).Select(w => w.GetHP()).Sum();
			var elfDeaths = startingElfCount - currentElfCount;
			return (completeTurnCount, totalHP, elfDeaths);
		}

	}
}