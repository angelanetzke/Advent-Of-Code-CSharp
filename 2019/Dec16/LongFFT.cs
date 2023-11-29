using System.Text;

namespace Dec16
{
	internal class LongFFT
	{
		private static readonly List<int> basePattern = new () { 0, 1, 0, -1 };
		private string inputString;
		private readonly int offset;
		private static readonly int length = 8;
		public LongFFT(string inputString)
		{
			offset = int.Parse(inputString.Substring(0, 7));
			this.inputString = string.Join("", Enumerable.Repeat(inputString, 10000));
			this.inputString = this.inputString.Substring(offset);
		}

		public string FinalResult()
		{
			var thisIteration = inputString.ToCharArray().Select(x => int.Parse(x.ToString())).ToArray();
			var nextIteration = new int[thisIteration.Length];
			for (int step = 1; step <= 100; step++)
			{
				nextIteration = new int[thisIteration.Length];
				var sum = 0;
				for (int digit = thisIteration.Length - 1; digit >= 0; digit--)
				{
					sum += thisIteration[digit];
					nextIteration[digit] = sum % 10;
				}
				thisIteration = nextIteration;
			}
			return string.Join("", thisIteration[0..length]);
		}


	}
}