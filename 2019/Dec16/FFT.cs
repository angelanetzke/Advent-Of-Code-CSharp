using System.Text;

namespace Dec16
{
	internal class FFT
	{
		private static readonly List<int> basePattern = new () { 0, 1, 0, -1 };
		private string inputString;
		private Dictionary<int, List<int>> appliedPatterns = new ();

		public FFT(string inputString)
		{
			this.inputString = inputString;
			for (int i = 1; i <= inputString.Length; i++)
			{
				var thisAppliedPattern = new List<int>();
				thisAppliedPattern.AddRange(Enumerable.Repeat(basePattern[0], i - 1));
				var basePatternIndex = 1;
				while (thisAppliedPattern.Count < inputString.Length)
				{
					if (thisAppliedPattern.Count + i <= inputString.Length)
					{
						thisAppliedPattern.AddRange(Enumerable.Repeat(basePattern[basePatternIndex], i));
					}
					else
					{
						thisAppliedPattern.AddRange(
							Enumerable.Repeat(basePattern[basePatternIndex], 
							inputString.Length - thisAppliedPattern.Count));
					}					
					basePatternIndex = (basePatternIndex + 1) % basePattern.Count;
				}
				appliedPatterns[i] = thisAppliedPattern;
			}
		}

		public string NextPhase()
		{
			var builder = new StringBuilder();
			for (int i = 1; i <= inputString.Length; i++)
			{
				var thisSum = 0;
				for (int j = 0; j < inputString.Length; j++)
				{
					thisSum += int.Parse(inputString[j].ToString()) * appliedPatterns[i][j];
				}
				builder.Append((Math.Abs(thisSum) % 10).ToString());
			}
			inputString = builder.ToString();
			return inputString.Substring(0, 8);
		}


	}
}