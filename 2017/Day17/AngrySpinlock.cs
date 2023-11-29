namespace Day17;

internal class AngrySpinlock
{
	private static readonly int maxValue = 50_000_000;

	public static int Execute(int steps)
	{
		var lastInsertedValue = -1;
		var currentIndex = 0;
		for (int nextValue = 1; nextValue <= maxValue; nextValue++)
		{
			currentIndex = (currentIndex + steps) % nextValue;		
			if (currentIndex == 0)
			{
				lastInsertedValue = nextValue;
			}
			currentIndex++;
		}
		return lastInsertedValue;
	}
}