using System;
using System.IO;

namespace Dec18
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] allLines = File.ReadAllLines("input.txt");
			PairTree combinedTree = null;
			foreach (string thisLine in allLines)
			{
				if (combinedTree == null)
				{
					combinedTree = new PairTree(thisLine);
				}
				else
				{
					combinedTree = combinedTree.Add(new PairTree(thisLine));
				}
				Reduce(combinedTree);
			}
			Console.WriteLine($"part 1: {combinedTree.GetMagnitude()}");

			long largestMagnitude = long.MinValue;
			for (int i = 0; i < allLines.Length; i++)
			{
				for (int j = 0; j < allLines.Length; j++)
				{
					combinedTree = new PairTree(allLines[i]).Add(new PairTree(allLines[j]));
					Reduce(combinedTree);
					largestMagnitude = Math.Max(largestMagnitude, combinedTree.GetMagnitude());
				}
			}
			Console.WriteLine($"part 2: {largestMagnitude}");

		}
		public static void Reduce(PairTree tree)
		{
			bool didSplit;
			bool didExplode;
			do
			{
				didExplode = true;
				didSplit = true;
				int explodeCount = 0;
				while (tree.Explode())
				{
					explodeCount++;
				}
				if (explodeCount == 0)
				{
					didExplode = false;
				}
				didSplit = tree.Split();
			} while (didSplit || didExplode);
		}
	}
}
