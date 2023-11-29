using System;

namespace Dec15
{
	internal class Node : IComparable<Node>
	{
		private readonly int x;
		private readonly int y;
		private readonly int cost;
		private int costToHere;
		private readonly int manhattanDistance;
		private readonly bool isEnd;

		public Node(int x, int y, int width, int height, int cost, bool isEnd)
		{
			this.x = x;
			this.y = y;
			this.cost = cost;
			costToHere = int.MaxValue;
			manhattanDistance = width - 1 - x + height - 1 - y;
			this.isEnd = isEnd;
		}

		public int GetX()
		{
			return x;
		}

		public int GetY()
		{
			return y;
		}

		public int GetCost()
		{
			return cost;
		}

		public int GetCostToHere()
		{
			return costToHere;
		}

		public bool IsEnd()
		{
			return isEnd;
		}

		public void SetCostToHere(int newCost)
		{
			costToHere = newCost;
		}

		public int GetEstimatedCost()
		{
			return costToHere + manhattanDistance;
		}

		public override bool Equals(object obj)
		{
			return obj is Node node &&
						 x == node.x &&
						 y == node.y;
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(x, y);
		}

		public int CompareTo(Node other)
		{
			return GetEstimatedCost().CompareTo(other.GetEstimatedCost());
		}
	}
}
