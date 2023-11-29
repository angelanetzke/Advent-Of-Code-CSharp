namespace Dec23
{
	internal class Cup
	{
		private readonly int label;
		private Cup? next;

		public Cup(int label)
		{
			this.label = label;
			next = null;
		}

		public int GetLabel()
		{
			return label;
		}

		public void SetNext(Cup next)
		{
			this.next = next;
		}

		public Cup? GetNext()
		{
			return next;
		}
	}
}
