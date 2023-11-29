using System;
using System.IO;

namespace Dec11
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string[] allLines = File.ReadAllLines("input.txt");
			Grid theGrid = new();
			foreach (string thisLine in allLines)
			{
				theGrid.AddRow(thisLine);
			}
			int totalFlashes = 0;
			int STEPS = 100;
			int thisStep = 1;
			bool areSynchronized = false;
			int synchronizedStep = -1;
			while (!areSynchronized || thisStep <= STEPS)
			{
				int flashesThisStep = theGrid.Update();
				if (thisStep <= STEPS)
				{
					totalFlashes += flashesThisStep;
				}
				if (!areSynchronized && flashesThisStep == theGrid.GetSize())
				{
					synchronizedStep = thisStep;
					areSynchronized = true;
				}
				thisStep++;
			}
			Console.WriteLine($"part 1: {totalFlashes}");
			Console.WriteLine($"part 2: {synchronizedStep}");

		}
	}
}
