namespace Day16
{
	internal class Sample
	{
		public int[]  initialRegisters { get; }
		public int[]  finalRegisters { get; }
		public int[] instruction { get;}

		public Sample (List<string> inputStrings)
		{
			var splitStart = inputStrings[0].IndexOf('[') + 1;
			var initialData = inputStrings[0].Substring(splitStart, inputStrings[0].Length - (splitStart + 1));
			initialRegisters = initialData.Split(", ").Select(x => int.Parse(x)).ToArray();
			instruction = inputStrings[1].Split(' ').Select(x => int.Parse(x)).ToArray();
			var finalData = inputStrings[2].Substring(splitStart, inputStrings[2].Length - (splitStart + 1));
			finalRegisters = finalData.Split(", ").Select(x => int.Parse(x)).ToArray();		
		}
	}
}