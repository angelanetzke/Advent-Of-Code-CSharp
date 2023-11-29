namespace Day21;

internal class Pattern
{
	private readonly string[] inputs;
	private readonly string[] outputs;

	public Pattern(string thisLine)
	{
		var split = thisLine.Split(" => ");
		inputs = split[0].Split('/');
		outputs = split[1].Split('/');
	}

	public bool IsMatch(Subsquare square)
	{
		if (square.GetSize() != inputs.Length)
		{
			return false;
		}
		var isMatch = true;
		for (int row = 0; row < inputs.Length; row++)
		{
			for (int column = 0; column < inputs[0].Length; column++)
			{
				if (square.GetPixel(row, column) != inputs[row][column])
				{
					isMatch = false;
					break;
				}
			}
			if (!isMatch)
			{
				break;
			}
		}
		return isMatch;
	}

	public Subsquare GenerateSubsquare()
	{
		var newCharArray = new char[outputs.Length, outputs.Length];
		for (int row = 0; row < outputs.Length; row++)
		{
			for (int column = 0; column < outputs.Length; column++)
			{
				newCharArray[row, column] = outputs[row][column];
			}
		}
		var newSubsquare = new Subsquare(newCharArray, 0, 0, outputs.GetLength(0));
		return newSubsquare;
	}
}