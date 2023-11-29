namespace Day21;

internal class Art
{
	private char[,] pixels = new char[,] {  { '.', '#', '.'}, { '.', '.', '#'}, { '#', '#', '#'} };
	private static readonly List<Pattern> patternList = new ();

	public void Enhance()
	{
		int subsquareCount;
		int subsquareSize;
		if (pixels.GetLength(0) % 2 == 0)
		{
			subsquareCount = pixels.GetLength(0) / 2;
			subsquareSize = 2;
		}
		else
		{
			subsquareCount = pixels.GetLength(0) / 3;
			subsquareSize = 3;
		}
		var subsquareList = new List<Subsquare>();
		for (int row = 0; row < subsquareCount; row++)
		{
			for  (int column = 0; column < subsquareCount; column++)
			{
				var newSubsquare = new Subsquare(pixels, row * subsquareSize, column * subsquareSize, subsquareSize);
				Subsquare? enhancedSubsquare =  null;
				for (int step = 1; step <= 8; step++)
				{
					if (step == 5)
					{
						newSubsquare.Flip();
					}
					enhancedSubsquare = GetEnhanced(newSubsquare);
					if (enhancedSubsquare != null)
					{
						break;
					}					
					newSubsquare.Rotate();
				}
				if (enhancedSubsquare != null)
				{
					subsquareList.Add(enhancedSubsquare);
				}				
			}
		}
		pixels = new char[subsquareList[0].GetSize() * subsquareCount
			, subsquareList[0].GetSize() * subsquareCount];
		for (int row = 0; row < subsquareCount; row++)
		{
			for  (int column = 0; column < subsquareCount; column++)
			{
				var thisSubsquare = subsquareList[row * subsquareCount + column];
				for (int subsquareRow = 0; subsquareRow < thisSubsquare.GetSize(); subsquareRow++)
				{
					for (int subsquareColumn = 0; subsquareColumn < thisSubsquare.GetSize(); subsquareColumn++)
					{
						pixels[row * thisSubsquare.GetSize() + subsquareRow
							, column * thisSubsquare.GetSize() + subsquareColumn] 
							= thisSubsquare.GetPixel(subsquareRow, subsquareColumn);
					}
				}
			}
		}
	}

	private Subsquare? GetEnhanced(Subsquare subsquare)
	{
		foreach (Pattern thisPattern in patternList)
		{
			if (thisPattern.IsMatch(subsquare))
			{
				return thisPattern.GenerateSubsquare();
			}
		}
		return null;
	}

	public static void SetPatterns(string[] allLines)
	{
		foreach (string thisLine in allLines)
		{
			patternList.Add(new Pattern(thisLine));
		}
	}

	public int CountOnPixels()
	{
		int count = 0;
		foreach (char thisPixel in pixels)
		{
			count += thisPixel == '#' ? 1 : 0;
		}
		return count;
	}


}