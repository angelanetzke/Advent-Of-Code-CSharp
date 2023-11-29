using System;
using System.IO;

namespace Dec09
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] allLines = File.ReadAllLines("input.txt");
			HeightMap theHeightMap = new();
			foreach (string thisLine in allLines)
			{
				theHeightMap.AddRow(thisLine);
			}
			Console.WriteLine($"part 1: {theHeightMap.GetTotalRiskLevel()}");

			Console.WriteLine($"part 2: {theHeightMap.GetTopBasinProduct()}");
		}


	}
}
