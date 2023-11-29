namespace Day21;

internal class Subsquare
{
	private char[,] pixels = new char[0, 0];
	private static readonly List<Pattern> patternList = new ();

	public Subsquare(char[,] artPixels, int startRow, int startColumn, int size)
	{
		pixels = new char[size, size];
		for (int row = startRow; row < startRow + size; row++)
		{
			for  (int column = startColumn; column < startColumn + size; column++)
			{
				pixels[row - startRow, column - startColumn] = artPixels[row, column];
			}
		}
	}

	public static void SetPatterns(string[] allLines)
	{
		foreach (string thisLine in allLines)
		{
			patternList.Add(new Pattern(thisLine));
		}
	}

	public char GetPixel(int row, int column)
	{
		return pixels[row, column];
	}

	public int GetSize()
	{
		return pixels.GetLength(0);
	}

	public void Rotate()
	{
		var temp = new char[pixels.GetLength(0), pixels.GetLength(1)];
		for (int row = 0; row < pixels.GetLength(0); row++)
		{
			for (int column = 0; column < pixels.GetLength(1); column++)
			{
				temp[column, pixels.GetLength(0) - 1 - row] = pixels[row, column];
			}			
		}
		pixels = temp;
	}
	
	public void Flip()
	{
		var temp = new char[pixels.GetLength(0), pixels.GetLength(1)];
		for (int row = 0; row < pixels.GetLength(0); row++)
		{
			for (int column = 0; column < pixels.GetLength(1); column++)
			{
				temp[row, pixels.GetLength(1) - 1 - column] = pixels[row, column];
			}			
		}
		pixels = temp;
	}

}