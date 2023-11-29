namespace Day15
{
	internal class Warrior
	{
		private int row;
		private int column;
		private readonly char warriorType;
		private static readonly Dictionary<char, char> enemyType = new ()
		{
			{ 'E', 'G' },
			{ 'G', 'E' }
		};
		private static readonly Dictionary<char, int> attack = new ()
		{
			{ 'E', 3 },
			{ 'G', 3 }
		};
		private int HP = 200;
		public bool isRemoved { set; get; } = false;
		private static (int, int)[] offsets = new (int, int)[]{ (-1, 0), (0, -1), (0, 1), (1, 0) };

		public Warrior(int row, int column, char warriorType)
		{
			this.row = row;
			this.column = column;
			this.warriorType = warriorType;
		}

		public static void SetElfAttackPower(int newAttack)
		{
			attack['E'] = newAttack;
		}

		public (int, int) GetLocation()
		{
			return (row, column);
		}

		public bool TakeTurn(List<Warrior> warriorList, HashSet<(int, int)> spaces)
		{
			var enemyCount = warriorList.Count(
				w => !w.isRemoved
				&& w.warriorType == enemyType[warriorType]);
			if (enemyCount == 0)
			{
				return false;
			}
			if (Attack(warriorList, spaces))
			{
				return true;
			}
			if (!Move(warriorList, spaces))
			{
				return true;
			}
			Attack(warriorList, spaces);
			return true;
		}

		private bool Attack(List<Warrior> warriorList, HashSet<(int, int)> spaces)
		{
			var adjacentSpaces = GetAdjacentSpaces(spaces);
			var targets = warriorList.Where(
				w => !w.isRemoved
				&& w.warriorType == enemyType[warriorType]
				&& adjacentSpaces.Contains((w.row, w.column)));
			if (targets.Count() > 0)
			{
				Warrior other = targets
					.OrderBy(t => t.GetHP())
					.ThenBy(t => t.GetLocation())
					.First();
				other.HP -= attack[warriorType];
				if (other.HP <= 0)
				{
					other.isRemoved = true;
				}
				return true;
			}
			return false;
		}

		private bool Move(List<Warrior> warriorList, HashSet<(int, int)> spaces)
		{
			var warriorLocations = warriorList
				.Where(w => !w.isRemoved)
				.Select(w => w.GetLocation())
				.ToHashSet();
			var enemyList = warriorList
				.Where(w => !w.isRemoved && w.warriorType == enemyType[warriorType]);
			var targets = new HashSet<(int, int)>();
			foreach (Warrior thisEnemy in enemyList)
			{
				targets.UnionWith(thisEnemy.GetAdjacentSpaces(spaces));
			}
			targets.RemoveWhere(t => warriorLocations.Contains(t));
			// distance, location, step direction
			var reachableTargets = new HashSet<(int, (int, int), (int, int))>();
			// location, step direction
			var queue = new Queue<((int, int), (int, int))>();
			// location, step direction : distance
			var visited = new Dictionary<((int, int), (int, int)), int>();
			foreach ((int, int) thisOffset in offsets)
			{
				var nextRow = row + thisOffset.Item1;
				var nextColumn = column + thisOffset.Item2;
				if (spaces.Contains((nextRow, nextColumn))
					&& !warriorLocations.Contains((nextRow, nextColumn)))
				{
					visited[((nextRow, nextColumn), thisOffset)] = 1;
					queue.Enqueue(((nextRow, nextColumn), thisOffset));
				}
			}
			while (queue.Count > 0)
			{
				var current = queue.Dequeue();
				if (targets.Contains(current.Item1))
				{
					reachableTargets.Add((visited[current], current.Item1, current.Item2));
				}
				var currentLocation = current.Item1;
				var currentStepDirection = current.Item2;
				var currentDistance = visited[current];
				foreach ((int, int) thisOffset in offsets)
				{
					var nextRow = currentLocation.Item1 + thisOffset.Item1;
					var nextColumn = currentLocation.Item2 + thisOffset.Item2;
					if (spaces.Contains((nextRow, nextColumn))
						&& !warriorLocations.Contains((nextRow, nextColumn))
						&& !visited.ContainsKey(((nextRow, nextColumn), currentStepDirection))
						&& !queue.Contains(((nextRow, nextColumn), currentStepDirection)))
					{
						visited[((nextRow, nextColumn), currentStepDirection)] = currentDistance + 1;
						queue.Enqueue(((nextRow, nextColumn), currentStepDirection));
					}
				}
			}
			if (reachableTargets.Count > 0)
			{
				var selected = reachableTargets.ToList()
					.OrderBy(x => x.Item1)
					.ThenBy(x => x.Item2)
					.ThenBy(x => x.Item3)
					.First();
				row += selected.Item3.Item1;
				column += selected.Item3.Item2;
				return true;
			}
			return false;
		}

		public int GetHP()
		{
			return HP;
		}

		public char GetWarriorType()
		{
			return warriorType;
		}

		private HashSet<(int, int)> GetAdjacentSpaces(HashSet<(int, int)> spaces)
		{
			var adjacentSet = new HashSet<(int, int)>();
			foreach ((int, int) thisOffset in offsets)
			{
				if (spaces.Contains((row + thisOffset.Item1, column + thisOffset.Item2)))
				{
					adjacentSet.Add((row + thisOffset.Item1, column + thisOffset.Item2));
				}
			}
			return adjacentSet;
		}

		

	}
}