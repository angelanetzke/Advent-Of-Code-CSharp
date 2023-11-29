using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Dec19
{
	internal class Program
	{
		static void Main(string[] args)
		{
			List<Scanner> allScanners = new();
			Scanner current = null;
			string[] allLines = File.ReadAllLines("input.txt");
			foreach (string thisLine in allLines)
			{
				if (thisLine.StartsWith("--- scanner"))
				{
					current = new();
					allScanners.Add(current);
				}
				else if (current != null && thisLine.Length > 0)
				{
					current.AddBeacon(thisLine);
				}
			}
			foreach (Scanner thisScanner in allScanners)
			{
				thisScanner.CalculateDistances();
			}
			int beaconCount = allScanners.Sum(element => element.GetBeaconCount());
			for (int i = 0; i < allScanners.Count - 1; i++)
			{
				for (int j = i + 1; j < allScanners.Count; j++)
				{
					int scannerMatchCount = allScanners[i].CountMatchingBeacons(allScanners[j]);
					if (scannerMatchCount >= 12)
					{
						beaconCount -= scannerMatchCount;
					}
				}
			}
			Console.WriteLine($"part 1: {beaconCount}");

			List<int> translatedIndices = new() { 0 };
			while (translatedIndices.Count < allScanners.Count)
			{
				List<int> nextLevel = new();
				foreach (int i in translatedIndices)
				{
					for (int j = 0; j < allScanners.Count; j++)
					{
						if (!translatedIndices.Contains(j) && !nextLevel.Contains(j))
						{
							if (allScanners[i].TranslateOther(allScanners[j]))
							{
								nextLevel.Add(j);
							}
						}
					}
				}
				translatedIndices.AddRange(nextLevel);
			}
			int maxDistance = int.MinValue;
			for (int i = 0; i < allScanners.Count; i++)
			{
				for (int j = 0; j < allScanners.Count; j++)
				{
					if (i != j)
					{
						int thisDistance = Math.Abs(allScanners[i].GetX() - allScanners[j].GetX())
							+ Math.Abs(allScanners[i].GetY() - allScanners[j].GetY())
							+ Math.Abs(allScanners[i].GetZ() - allScanners[j].GetZ());
						maxDistance = Math.Max(maxDistance, thisDistance);
					}
				}
			}
			Console.WriteLine($"part 2: {maxDistance}");
		}




	}
}

