namespace Dec11
{
	internal class Octopus
	{
		private int energy;
		private readonly int row;
		private readonly int column;
		private bool hasFlashed;

		public Octopus(int energy, int row, int column)
		{
			this.energy = energy;
			this.row = row;
			this.column = column;
			hasFlashed = false;
		}
		public int GetEnergy()
		{
			return energy;
		}

		public int GetRow()
		{
			return row;
		}

		public bool HasFlashed()
		{
			return hasFlashed;
		}

		public void Flash()
		{
			hasFlashed = true;
		}
		public int GetColumn()
		{
			return column;
		}
		public void IncreaseEnergy()
		{
			energy += 1;
		}

		public void SetEnergyToZero()
		{
			energy = 0;
			hasFlashed = false;
		}
	}
}
