string input = File.ReadAllLines("input.txt")[0];
int endRow = int.Parse(input.Split(' ')[16].Trim(','));
int endColumn = int.Parse(input.Split(' ')[18].Trim('.'));
int row = 1;
int column = 1;
long value = 20151125;
while (row != endRow || column != endColumn)
{
	value = (value * 252533) % 33554393;
	if (row == 1)
	{
		row = column + 1;
		column = 1;
	}
	else
	{
		row--;
		column++;
	}
}
Console.WriteLine($"Part 1: {value}");
