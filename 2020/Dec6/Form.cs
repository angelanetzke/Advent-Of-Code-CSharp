namespace Dec6
{
	internal class Form
	{
		private readonly bool[] answers = new bool[26];

		public void ProcessLine(String nextLine)
		{
			foreach (char thisLetter in nextLine)
			{
				answers[thisLetter - 'a'] = true;
			}
		}

		public int GetYesCount()
		{
			int count = 0;
			foreach (bool thisAnswer in answers)
			{
				if (thisAnswer)
				{
					count++;
				}
			}
			return count;
		}
	}
}
