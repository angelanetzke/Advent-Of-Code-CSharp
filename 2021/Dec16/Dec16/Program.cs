using System;
using System.IO;

namespace Dec16
{
	internal class Program
	{
		static void Main(string[] args)
		{
			string hexString = File.ReadAllLines("input.txt")[0];
			Transmission theTransmission = new(hexString);
			Console.WriteLine($"part 1: {theTransmission.GetVersionNumberSum()}");
			Console.WriteLine($"part 2: {theTransmission.Evaluate()}");


		}
	}
}
