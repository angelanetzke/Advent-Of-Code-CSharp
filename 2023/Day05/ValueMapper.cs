namespace Day05;

internal class ValueMapper
{
	private readonly List<MapRange> ranges = [];

	public long GetSize()
	{
		return ranges.Count;
	}
	
	public void AddRange(string rangeString)
	{
		ranges.Add(new MapRange(rangeString));
		ranges.Sort();
	}

	public List<long> MapToNextValues(List<long> oldValues)
	{
		List<long> newValues = [];
		foreach (long thisOldValue in oldValues)
		{
			bool hasNewValueBeenAdded = false;
			foreach (MapRange thisRange in ranges)
			{
				long thisNewValue = thisRange.MapValue(thisOldValue);
				if (thisNewValue != thisOldValue)
				{
					newValues.Add(thisNewValue);
					hasNewValueBeenAdded = true;
					break;
				}	
			}
			if (!hasNewValueBeenAdded)
			{
				newValues.Add(thisOldValue);
			}
		}
		return newValues;
	}

	public List<ValueRange> MapToNextRanges(List<ValueRange> oldValueRanges)
	{
		List<ValueRange> newValueRanges = [];
		foreach (ValueRange thisOldValueRange in oldValueRanges)
		{
			ValueRange? remaining = thisOldValueRange;
			foreach (MapRange thisMapRange in ranges)
			{
				if (remaining == null)
				{
					break;
				}
				ValueRange? thisPreviousRange = thisMapRange.GetPreviousValueRange(remaining);
				if (thisPreviousRange != null)
				{
					newValueRanges.Add(thisPreviousRange);
				}
				ValueRange? thisOverlappingRange = thisMapRange.GetOverlappingValueRange(remaining);
				if (thisOverlappingRange != null)
				{
					newValueRanges.Add(thisOverlappingRange);
				}
				remaining = thisMapRange.GetNextValueRange(remaining);
			}
			if (remaining != null)
			{
				newValueRanges.Add(remaining);
			}
		}
		return newValueRanges;
	}
	
	public void Clear()
	{
		ranges.Clear();
	}
}