using System.Collections.Generic;
using System.Linq;

namespace Dec15
{
	internal class Grid
	{
		private readonly Dictionary<(int, int), Node> nodes;

		public Grid()
		{
			nodes = new();
		}

		public void AddNode(int x, int y, int width, int height, int cost)
		{
			if (x == width - 1 && y == height - 1)
			{
				nodes[(x, y)] = new Node(x, y, width, height, cost, true);
			}
			else
			{
				nodes[(x, y)] = new Node(x, y, width, height, cost, false);
			}
		}

		public int GetShortestPath()
		{
			Node end = null;
			HashSet<Node> next = new();
			Node start = nodes[(0, 0)];
			start.SetCostToHere(0);
			next.Add(start);
			HashSet<Node> visited = new();
			while (end == null)
			{
				Node current = next.Min();
				next.Remove(current);
				visited.Add(current);
				List<Node> neighbors = GetNeighbors(current);
				foreach (Node thisNeighbor in neighbors)
				{
					if (thisNeighbor.IsEnd())
					{
						end = thisNeighbor;
						end.SetCostToHere(current.GetCostToHere() + end.GetCost());
						break;
					}
					else if (!visited.Contains(thisNeighbor))
					{
						int newCostToNeighbor = current.GetCostToHere() + thisNeighbor.GetCost();
						if (thisNeighbor.GetCostToHere() > newCostToNeighbor)
						{
							thisNeighbor.SetCostToHere(newCostToNeighbor);
						}
						next.Add(thisNeighbor);
					}
				}
			}
			return end.GetCostToHere();
		}

		private List<Node> GetNeighbors(Node node)
		{
			int x = node.GetX();
			int y = node.GetY();
			List<Node> neighbors = new();
			if (nodes.TryGetValue((x - 1, y), out Node thisNeighbor))
			{
				neighbors.Add(thisNeighbor);
			}
			if (nodes.TryGetValue((x + 1, y), out thisNeighbor))
			{
				neighbors.Add(thisNeighbor);
			}
			if (nodes.TryGetValue((x, y - 1), out thisNeighbor))
			{
				neighbors.Add(thisNeighbor);
			}
			if (nodes.TryGetValue((x, y + 1), out thisNeighbor))
			{
				neighbors.Add(thisNeighbor);
			}
			return neighbors;
		}



	}
}
