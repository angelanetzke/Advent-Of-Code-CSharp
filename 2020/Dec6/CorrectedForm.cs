namespace Dec6
{
	internal class CorrectedForm
	{
		private readonly bool[] answers = new bool[26];
		private bool hasFirstEntry = false;
		public void ProcessLine(String nextLine)
		{
			if (hasFirstEntry)
			{
				bool[] newAnswers = new bool[26];
				foreach (char thisLetter in nextLine)
				{
					newAnswers[thisLetter - 'a'] = true;
				}
				for (int i = 0; i < answers.Length; i++)
				{
					answers[i] = answers[i] && newAnswers[i];
				}
			}
			else
			{
				foreach (char thisLetter in nextLine)
				{
					answers[thisLetter - 'a'] = true;
				}
				hasFirstEntry = true;
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
