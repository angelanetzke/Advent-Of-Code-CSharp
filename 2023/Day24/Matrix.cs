namespace Day24;

internal class Matrix (double[,] values)
{
	private readonly double[,] values = values;

	public Matrix GetInverse()
	{	
		int size = values.GetLength(0);;
		double[,] augmentedMatrix = new double[size, 2 * size];
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				augmentedMatrix[i, j] = values[i, j];
				augmentedMatrix[i, j + size] = (i == j) ? 1 : 0;
			}
		}
		for (int i = 0; i < size; i++)
		{
			if (augmentedMatrix[i, i] == 0)
			{
				for (int j = i + 1; j < size; j++)
				{
					if (augmentedMatrix[j, i] != 0)
					{
						SwapRows(augmentedMatrix, i, j);
						break;
					}
				}
			}
			double pivot = augmentedMatrix[i, i];
			for (int j = 0; j < 2 * size; j++)
			{
				augmentedMatrix[i, j] /= pivot;
			}
			for (int j = 0; j < size; j++)
			{
				if (j != i)
				{
					double factor = augmentedMatrix[j, i];
					for (int k = 0; k < 2 * size; k++)
					{
						augmentedMatrix[j, k] -= factor * augmentedMatrix[i, k];
					}
				}
			}
		}		
		double[,] inverseMatrix = new double[size, size];
		for (int i = 0; i < size; i++)
		{
			for (int j = 0; j < size; j++)
			{
				inverseMatrix[i, j] = augmentedMatrix[i, j + size];
			}
		}		
		return new Matrix(inverseMatrix);			
	}

	private static void SwapRows(double[,] matrix, int row1, int row2)
	{
		int n = matrix.GetLength(1);
		for (int i = 0; i < n; i++)
		{
			double temp = matrix[row1, i];
			matrix[row1, i] = matrix[row2, i];
			matrix[row2, i] = temp;
		}
	}

	public Matrix Multiply(Matrix other)
	{
		int rows1 = values.GetLength(0);
		int cols1 = values.GetLength(1);
		int cols2 = other.values.GetLength(1);
		double[,] result = new double[rows1, cols2];
		for (int i = 0; i < rows1; i++)
		{
			for (int j = 0; j < cols2; j++)
			{
				for (int k = 0; k < cols1; k++)
				{
					result[i, j] += values[i, k] * other.values[k, j];
				}
			}
		}
		return new Matrix(result);		
	}

	public double GetValue(int row, int column)
	{
		return values[row, column];
	}

}