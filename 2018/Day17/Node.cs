namespace Day17
{
	internal class Node
	{
		public static Dictionary<(int, int), Node> allNodes = new ();
		public static int maxY = 0;
		public enum NodeType { SettledWater, UnsettledWater, Clay, Spring }
		public enum FlowDirection { Left, Right, Down }
		public NodeType nodeType { set; get; }
		private bool hitsWall { set; get; }

		public Node (NodeType nodeType)
		{
			this.nodeType = nodeType;
			if (nodeType == NodeType.Clay)
			{
				hitsWall = true;
			}
		}

		public static Node Execute((int, int) location, FlowDirection direction)
		{
			if (allNodes.TryGetValue(location, out Node? thisNode))
			{
				return thisNode;
			}
			else
			{
				allNodes[location] = new Node(NodeType.UnsettledWater);
			}
			if (location.Item2 == maxY)
			{
				return allNodes[location];
			}
			var downNode = Execute((location.Item1, location.Item2 + 1), FlowDirection.Down);
			if (direction == FlowDirection.Down
				&& (downNode.nodeType == NodeType.SettledWater || downNode.nodeType == NodeType.Clay))
			{
				var leftNode = Execute((location.Item1 - 1, location.Item2), FlowDirection.Left);
				var rightNode = Execute((location.Item1 + 1, location.Item2), FlowDirection.Right);
				if (leftNode.hitsWall && rightNode.hitsWall)
				{
					allNodes[location].nodeType = NodeType.SettledWater;
					Settle((location.Item1 - 1, location.Item2), FlowDirection.Left);
					Settle((location.Item1 + 1, location.Item2), FlowDirection.Right);
				}
			}
			if (direction == FlowDirection.Left
				&& (downNode.nodeType == NodeType.SettledWater || downNode.nodeType == NodeType.Clay))
			{
				var leftNode = Execute((location.Item1 - 1, location.Item2), FlowDirection.Left);
				allNodes[location].hitsWall = leftNode.hitsWall;
			}
			if (direction == FlowDirection.Right
				&& (downNode.nodeType == NodeType.SettledWater || downNode.nodeType == NodeType.Clay))
			{
				var rightNode = Execute((location.Item1 + 1, location.Item2), FlowDirection.Right);
				allNodes[location].hitsWall = rightNode.hitsWall;
			}
			return allNodes[location];
		}

		private static void Settle((int, int) location, FlowDirection direction)
		{
			if (allNodes[location].nodeType == NodeType.UnsettledWater)
			{
				allNodes[location].nodeType = NodeType.SettledWater;
				if (direction == FlowDirection.Left)
				{
					Settle((location.Item1 - 1, location.Item2), direction);
				}
				else
				{
					Settle((location.Item1 + 1, location.Item2), direction);
				}
			}
		}		

	}
}
