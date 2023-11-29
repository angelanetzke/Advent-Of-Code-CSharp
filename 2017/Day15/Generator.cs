namespace Day15;

internal class Generator
{
	private static readonly long divisor = 2147483647L;
	private readonly long factor;
	private long value;
	private long multiple;

	public Generator(long factor, long value, long multiple)
	{
		this.factor = factor;
		this.value = value;
		this.multiple = multiple;
	}

	public long GetValue()
	{
		return value;
	}

	public void Execute()
	{
		value = (value * factor) % divisor;
		while (value % multiple != 0)
		{
			value = (value * factor) % divisor;
		}
	}
}