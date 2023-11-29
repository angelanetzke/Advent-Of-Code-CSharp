namespace Day7;

internal class Node
{
	public Node? parent { set; get; } = null;
	public List<Node> children { set; get; } = new ();
	public int weight { get; }

	public Node(int weight)
	{
		this.weight = weight;
	}

	public int? BalanceTree()
	{
		var unbalancedTree = GetUnbalancedTree();
		if (unbalancedTree.Item1 == null)
		{
			return null;
		}
		return unbalancedTree.Item1.BalanceTree(unbalancedTree.Item2);
	}

	private int BalanceTree(int expectedWeight)
	{
		if (children.Count == 0)
		{
			return expectedWeight;
		}
		var unbalancedTree = GetUnbalancedTree();
		if (unbalancedTree.Item1 == null)
		{
			var totalSubtreeWeight = children.Count * children[0].GetTotalWeight();
			return expectedWeight - totalSubtreeWeight;
		}
		return unbalancedTree.Item1.BalanceTree(unbalancedTree.Item2);
	}

	private int GetTotalWeight()
	{
		if (children.Count == 0)
		{
			return weight;
		}
		else
		{
			var sum = weight;
			foreach (Node thisChild in children)
			{
				sum += thisChild.GetTotalWeight();
			}
			return sum;
		}
	}

	private (Node?, int) GetUnbalancedTree()
	{
		var subtreeWeights = new Dictionary<Node, int>();
		foreach (Node thisChild in children)
		{
			subtreeWeights[thisChild] = thisChild.GetTotalWeight();
		}
		Node? unbalancedTree = null;
		var expectedWeight = -1;
		foreach (int thisWeight in subtreeWeights.Values)
		{
			if (subtreeWeights.Count(x => x.Value == thisWeight) == 1)
			{
				unbalancedTree = subtreeWeights
					.Where(x => x.Value == thisWeight)
					.Select(x => x.Key)
					.First();
			}
			else
			{
				expectedWeight = thisWeight;
			}
		}
		return (unbalancedTree, expectedWeight);
	}
}