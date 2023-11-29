namespace Dec14
{
	class Reaction
	{
		private readonly string productName;
		private readonly long productCount;
		private readonly List<(string, long)> components = new ();

		public Reaction(string reactionString)
		{
			var parts = reactionString.Split(" => ");
			productName = parts[1].Split(' ')[1];
			productCount = long.Parse(parts[1].Split(' ')[0]);
			foreach (string thisComponent in parts[0].Split(", "))
			{
				components.Add((thisComponent.Split(' ')[1], long.Parse(thisComponent.Split(' ')[0])));
			}
		}

		public long GetProductCount()
		{
			return productCount;
		}

		public List<(string, long)> GetComponentMultiples(long factor)
		{
			if (factor == 0)
			{
				return new List<(string, long)>();
			}
			else
			{
				var componentMultiples = new List<(string, long)>();
				components.ForEach(x => componentMultiples.Add((x.Item1, x.Item2 * factor)));
				return componentMultiples;
			}
		}

		public long GetFactor(long productNeededCount)
		{
			return (long)Math.Round(Math.Ceiling((double)productNeededCount / productCount));
		}

		public bool DoesProduce(string searchProductName)
		{
			return productName == searchProductName;
		}
	}
}