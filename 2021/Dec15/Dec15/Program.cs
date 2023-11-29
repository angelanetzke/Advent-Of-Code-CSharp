using System;
using System.Collections.Generic;
using System.IO;

namespace Dec15
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] allLines = File.ReadAllLines("input.txt");
			List<List<int>> costs = new();
			foreach (string thisLine in allLines)
			{
				List<int> row = new();
				foreach (char thisChar in thisLine)
				{
					row.Add(int.Parse(thisChar.ToString()));
				}
				costs.Add(row);
			}			
			Grid theGrid = new();
			int width = costs[0].Count;
			int height = costs.Count;
			for (int y = 0; y < costs.Count; y++)
			{
				for (int x = 0; x < costs[y].Count; x++)
				{
					theGrid.AddNode(x, y, width, height, costs[y][x]);
				}
			}
			Console.WriteLine($"part 1: {theGrid.GetShortestPath()}");
			int size = 5;
			int fullWidth = width * size;
			int fullHeight = height * size;
			Grid theGrid2 = new();
			for (int y = 0; y < fullHeight; y++)
			{
				for (int x = 0; x < fullWidth; x++)
				{
					int gridX = x / width;
					int gridY = y / height;
					int costX = x % width;
					int costY = y % height;
					int thisCost = costs[costY][costX] + gridX + gridY;
					if (thisCost > 9)
					{
						thisCost -= 9;
					}
					theGrid2.AddNode(x, y, fullWidth, fullHeight, thisCost);
				}
			}
			
			Console.WriteLine($"part 2: {theGrid2.GetShortestPath()}");
		}



	}
}
