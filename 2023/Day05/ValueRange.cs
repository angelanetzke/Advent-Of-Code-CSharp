namespace Day05;

internal class ValueRange : IComparable<ValueRange>
{
	public long Min { get; }
	public long Max { get; }

	public ValueRange(long min, long size)
	{
		Min = min;
		Max = min + size - 1;
	}

	public int CompareTo(ValueRange? other)
	{
		if (other != null)
		{
			return Min.CompareTo(other.Min);
		}
		throw new ArgumentNullException(nameof(other));
	}

}