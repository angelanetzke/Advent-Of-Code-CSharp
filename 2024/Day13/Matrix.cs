namespace Day13;

internal class Matrix
{
	private readonly long[,] values;

	public Matrix(long[] originalValues)
	{
		values = new long[2, originalValues.Length / 2];
		for (int i = 0; i < originalValues.Length; i++)
		{
			if (i < originalValues.Length / 2)
			{
				values[0, i] = originalValues[i];
			}
			else
			{
				values[1, i - originalValues.Length / 2] = originalValues[i];
			}
		}
	}

	public static (long, long)? Solve(Matrix m1, Matrix m2)
	{
		long determinantDenominator = m1.values[0, 0] * m1.values[1, 1] - m1.values[0, 1] * m1.values[1, 0];
		long productFirstElement = m1.values[1, 1] * m2.values[0, 0] + -m1.values[0, 1] * m2.values[1, 0];
		long productSecondElement = -m1.values[1, 0] * m2.values[0, 0] + m1.values[0, 0] * m2.values[1, 0];
		if (productFirstElement % determinantDenominator != 0 || productSecondElement % determinantDenominator != 0)
		{
			return null;
		}
		return (productFirstElement / determinantDenominator, productSecondElement / determinantDenominator);
	}
}