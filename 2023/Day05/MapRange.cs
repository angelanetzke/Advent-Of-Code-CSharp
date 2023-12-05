namespace Day05;

internal class MapRange : IComparable<MapRange>
{
	private readonly long oldMin;
	private readonly long newMin;
	private readonly long size;

	public MapRange(string rangeString)
	{
		oldMin = long.Parse(rangeString.Split()[1]);
		newMin = long.Parse(rangeString.Split()[0]);
		size = long.Parse(rangeString.Split()[2]);
	}

	public long MapValue(long value)
	{
		if (oldMin <= value && value <= oldMin + size - 1)
		{
			return value - oldMin + newMin;
		}
		return value;
	}

	public ValueRange? GetPreviousValueRange(ValueRange range)
	{
		if (range.Min < oldMin )
		{
			return new ValueRange(range.Min, Math.Min(range.Max, oldMin - 1) - range.Min + 1);
		}
		return null;
	}

	public ValueRange? GetOverlappingValueRange(ValueRange range)
	{
		if (Math.Max(oldMin, range.Min) <= Math.Min(oldMin + size - 1, range.Max))
		{
			long newMin = MapValue(Math.Max(oldMin, range.Min));
			long newSize = MapValue(Math.Min(oldMin + size - 1, range.Max)) - newMin + 1;
			return new ValueRange(newMin, newSize);
		}
		return null;
	}

	public ValueRange? GetNextValueRange(ValueRange range)
	{
		if (range.Max > oldMin + size - 1)
		{
			return new ValueRange(
				Math.Max(range.Min, oldMin + size), range.Max - Math.Max(range.Min, oldMin + size) + 1);
		}
		return null;
	}

	public int CompareTo(MapRange? other)
	{
		if (other != null)
		{
			return oldMin.CompareTo(other.oldMin);
		}
		throw new ArgumentNullException(nameof(other));
	}
}