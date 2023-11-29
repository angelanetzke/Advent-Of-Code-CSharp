namespace Day6;

using System.Text;

internal class Memory
{
	private Bank head;
	
	public Memory(string initialValues)
	{
		var valueStrings = initialValues.Split('\t');
		head = new Bank(int.Parse(valueStrings[0]));
		var cursor = head;
		for (int i = 1; i < valueStrings.Length; i++)
		{
			var nextBank = new Bank(int.Parse(valueStrings[i]));
			cursor.next = nextBank;
			cursor = nextBank;
		}
		cursor.next = head;
	}

	public (int, int) Execute()
	{
		var stepCount = 0;
		var cache = new Dictionary<string, int>();
		var thisBanksString = BanksToString();
		do
		{				
			cache[thisBanksString] = stepCount;
			var cursor = FindMax();
			var blockCount = cursor.value;
			cursor.value = 0;
			cursor = cursor.next;
			while (blockCount > 0 && cursor != null)
			{
				cursor.Increment();
				cursor = cursor.next;
				blockCount--;
			}
			stepCount++;
			thisBanksString = BanksToString();
		} while (!cache.ContainsKey(thisBanksString));
		return (stepCount, stepCount - cache[thisBanksString]);
	}

	private Bank FindMax()
	{
		var cursor = head;
		var max = head;
		do
		{
			if (cursor.value > max.value)
			{
				max = cursor;
			}
			cursor = cursor.next;
		} while (cursor != null && cursor != head);
		return max;
	}

	private string BanksToString()
	{
		var builder = new StringBuilder();
		var cursor = head;
		do
		{
			builder.Append(cursor.value);
			builder.Append('-');
			cursor = cursor.next;
		} while (cursor != null && cursor != head);
		return builder.ToString();
	}

	private class Bank
	{
		public int value { set; get; }
		public Bank? next { set; get; } = null;

		public Bank(int value)
		{
			this.value = value;
		}

		public void Increment()
		{
			value++;
		}
	}
}