using System;
using System.Collections.Generic;

namespace Dec23
{
	internal class BurrowNode : IComparable<BurrowNode>
	{
		private readonly BurrowState state;
		private int totalCost;
		private int moveCost;

		public BurrowNode(BurrowState state)
		{
			this.state = state;
			totalCost = int.MaxValue;
			moveCost = int.MaxValue;
		}

		public int GetMoveCost()
		{
			return moveCost;
		}

		public void SetMoveCost(int newCost)
		{
			moveCost = newCost;
		}

		public int GetTotalCost()
		{
			return totalCost;
		}

		public void SetTotalCost(int newCost)
		{
			totalCost = newCost;
		}

		public bool IsEnd()
		{
			return state.IsComplete();
		}

		public List<BurrowNode> GetNeighbors()
		{
			Dictionary<BurrowState, int> nextStates = state.GetNext();
			List<BurrowNode> neighbors = new();
			foreach(BurrowState thisState in nextStates.Keys)
			{
				BurrowNode thisNeighbor = new(thisState);
				thisNeighbor.SetMoveCost(nextStates[thisState]);
				neighbors.Add(thisNeighbor);
			}
			return neighbors;
		}

		public override bool Equals(object obj)
		{
			return obj is BurrowNode node &&
						 state.Equals(node.state);
		}

		public override int GetHashCode()
		{
			return state.GetHashCode();
		}

		public int CompareTo(BurrowNode other)
		{
			return totalCost.CompareTo(other.totalCost);
		}

		public override string ToString()
		{
			return state.ToString();
		}

	}
}
