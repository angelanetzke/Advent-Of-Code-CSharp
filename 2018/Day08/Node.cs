namespace Day08
{
	internal class Node
	{
		private static int[] values = Array.Empty<int>();
		private int metadataStart;
		private int metadataCount;
		private List<Node> children = new ();

		public static void SetValues(string inputLine)
		{
			values = inputLine.Split(' ').Select(x => int.Parse(x)).ToArray();
		}

		public Node() : this(0)
		{}
		public Node(int start)
		{
			var childrenCount = values[start];
			metadataCount = values[start + 1];
			if (childrenCount == 0)
			{
				metadataStart = start + 2;
			}
			else
			{
				var nextStart = start + 2;
				for (int i = 1; i <= childrenCount; i++)
				{
					children.Add(new Node(nextStart));
					nextStart = children.Last().metadataStart + children.Last().metadataCount;
				}
				metadataStart = nextStart;		
			}
		}

		public int GetMetadataSum()
		{
			int sum = 0;
			for (int i = metadataStart; i < metadataStart + metadataCount; i++)
			{
				sum += values[i];
			}
			foreach (Node thisChild in children)
			{
				sum += thisChild.GetMetadataSum();
			}
			return sum;
		}

		public int GetValue()
		{
			int sum = 0;
			if (children.Count == 0)
			{				
				for (int i = metadataStart; i < metadataStart + metadataCount; i++)
				{
					sum += values[i];
				}				
			}
			else
			{
				for (int i = metadataStart; i < metadataStart + metadataCount; i++)
				{
					var childIndex = values[i] - 1;
					if (childIndex < children.Count())
					{
						sum += children[childIndex].GetValue();
					}					
				}
			}
			return sum;
		}

	}
}