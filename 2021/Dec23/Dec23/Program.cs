using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dec23
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] allLines = File.ReadAllLines("input.txt");
			BurrowState startState = new();
			startState.SetCharacter(7, allLines[2][3]);
			startState.SetCharacter(8, allLines[2][5]);
			startState.SetCharacter(9, allLines[2][7]);
			startState.SetCharacter(10, allLines[2][9]);
			startState.SetCharacter(11, allLines[3][3]);
			startState.SetCharacter(12, allLines[3][5]);
			startState.SetCharacter(13, allLines[3][7]);
			startState.SetCharacter(14, allLines[3][9]);
			HashSet<BurrowNode> visited = new();
			HashSet<BurrowNode> next = new();
			BurrowNode startNode = new(startState);
			startNode.SetTotalCost(0);
			next.Add(startNode);
			int minEnergy = -1;
			while (minEnergy == -1)
			{
				BurrowNode current = next.Min();
				if (current.IsEnd())
				{
					minEnergy = current.GetTotalCost();
				}
				else
				{
					next.Remove(current);
					visited.Add(current);
					List<BurrowNode> neighbors = current.GetNeighbors();
					foreach (BurrowNode thisNeighbor in neighbors)
					{
						if (!visited.Contains(thisNeighbor))
						{
							if (next.TryGetValue(thisNeighbor, out BurrowNode oldNode))
							{
								next.Remove(oldNode);
							}
							int newTotalCost = current.GetTotalCost() + thisNeighbor.GetMoveCost();
							if (oldNode == null || newTotalCost < oldNode.GetTotalCost())
							{
								thisNeighbor.SetTotalCost(newTotalCost);
							}
							else if (oldNode != null && oldNode.GetTotalCost() <= newTotalCost)
							{
								thisNeighbor.SetTotalCost(oldNode.GetTotalCost());
							}
							next.Add(thisNeighbor);
						}
					}
				}
			}
			Console.WriteLine($"part 1: {minEnergy}");
		}
	}
}
