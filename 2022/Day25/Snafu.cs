using System.Text;

namespace Day25
{
	internal class Snafu
	{
		private readonly string value;
		private static readonly Dictionary<char, int> digitToValue = new ()
		{
			{ '=', -2 },
			{ '-', -1 },
			{ '0', 0 },
			{ '1', 1 },
			{ '2', 2 }
		};

		private static readonly Dictionary<int, char> valueToDigit = new ()
		{
			{ -2, '=' },
			{ -1, '-' },
			{ 0, '0' },
			{ 1, '1' },
			{ 2, '2' }
		};
		private static readonly int MIN_VALUE = -2;
		private static readonly int MAX_VALUE = 2;
		private static readonly int BASE = 5;
		private readonly int length;
		

		public Snafu(string value, int length)
		{
			this.value = new String('0', length - value.Length) + value;
			this.length = length;
		}

		public Snafu Add(Snafu other)
		{
			var builder = new StringBuilder();
			builder.Append(value);
			for (int i = length - 1; i >= 0; i--)
			{
				AddDigit(builder, other.value[i], i);
			}
			return new Snafu(builder.ToString(), length);
		}

		private void AddDigit(StringBuilder number, char digit, int index)
		{
			int thisDigit = digitToValue[number[index]];
			thisDigit += digitToValue[digit];
			if (thisDigit > MAX_VALUE)
			{
				thisDigit -= BASE;
				AddDigit(number, '1', index - 1);
			}
			else if (thisDigit < MIN_VALUE)
			{
				thisDigit += BASE;
				AddDigit(number, '-', index - 1);
			}
			number[index] = valueToDigit[thisDigit];
		}

		public override string ToString()
		{
			return value.TrimStart('0');
		}
	}
}