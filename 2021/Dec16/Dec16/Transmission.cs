using System;
using System.Text;

namespace Dec16
{
	internal class Transmission
	{
		private readonly string binaryString;
		private int index;
		public Transmission(string hexString)
		{
			index = 0;
			StringBuilder builder = new();
			foreach (char thisChar in hexString)
			{
				switch (thisChar)
				{
					case '0':
						builder.Append("0000");
						break;
					case '1':
						builder.Append("0001");
						break;
					case '2':
						builder.Append("0010");
						break;
					case '3':
						builder.Append("0011");
						break;
					case '4':
						builder.Append("0100");
						break;
					case '5':
						builder.Append("0101");
						break;
					case '6':
						builder.Append("0110");
						break;
					case '7':
						builder.Append("0111");
						break;
					case '8':
						builder.Append("1000");
						break;
					case '9':
						builder.Append("1001");
						break;
					case 'A':
						builder.Append("1010");
						break;
					case 'B':
						builder.Append("1011");
						break;
					case 'C':
						builder.Append("1100");
						break;
					case 'D':
						builder.Append("1101");
						break;
					case 'E':
						builder.Append("1110");
						break;
					case 'F':
						builder.Append("1111");
						break;
				}
			}
			binaryString = builder.ToString();
		}

		public int GetVersionNumberSum()
		{
			index = 0;
			int sum = 0;
			while (binaryString.Substring(index).Contains('1') && index < binaryString.Length)
			{
				sum += GetsubpacketVersionNumber();

			}
			return sum;
		}

		private int GetsubpacketVersionNumber()
		{
			int sum = 0;
			int versionNumber = Convert.ToInt32(GetNextSubstring(3), 2);
			sum += versionNumber;
			int typeID = Convert.ToInt32(GetNextSubstring(3), 2);
			if (typeID == 4)
			{
				string groupString;
				do
				{
					groupString = GetNextSubstring(5);
				} while (groupString.StartsWith('1'));
			}
			else
			{
				int lengthType = Convert.ToInt32(GetNextSubstring(1), 2);
				if (lengthType == 0)
				{
					int subpacketLength = Convert.ToInt32(GetNextSubstring(15), 2);
					int endIndex = index + subpacketLength;
					while (index < endIndex)
					{
						sum += GetsubpacketVersionNumber();
					}
				}
				else
				{
					int subpacketCount = Convert.ToInt32(GetNextSubstring(11), 2);
					for (int i = 1; i <= subpacketCount; i++)
					{
						sum += GetsubpacketVersionNumber();
					}
				}
			}
			return sum;
		}

		private string GetNextSubstring(int length)
		{
			string nextString = binaryString.Substring(index, length);
			index += length;
			return nextString;
		}

		private void AdvanceIndex(int length)
		{
			index += length;
		}

		public long Evaluate()
		{
			index = 0;
			return EvaluateSubpacket();
		}

		public long EvaluateSubpacket()
		{
			AdvanceIndex(3);
			int typeID = Convert.ToInt32(GetNextSubstring(3), 2);
			if (typeID == 4)
			{
				return Value();
			}
			else
			{
				int lengthType = Convert.ToInt32(GetNextSubstring(1), 2);
				int subpacketLength = 0;
				int subpacketCount = 0;
				if (lengthType == 0)
				{
					subpacketLength = Convert.ToInt32(GetNextSubstring(15), 2);
				}
				else
				{
					subpacketCount = Convert.ToInt32(GetNextSubstring(11), 2);
				}
				if (typeID == 0 && lengthType == 0)
				{
					return SumByLength(subpacketLength);
				}
				else if (typeID == 0 && lengthType == 1)
				{
					return SumByCount(subpacketCount);
				}
				else if (typeID == 1 && lengthType == 0)
				{
					return ProductByLength(subpacketLength);
				}
				else if (typeID == 1 && lengthType == 1)
				{
					return ProductByCount(subpacketCount);
				}
				else if (typeID == 2 && lengthType == 0)
				{
					return MinimumByLength(subpacketLength);
				}
				else if (typeID == 2 && lengthType == 1)
				{
					return MinimumByCount(subpacketCount);
				}
				else if (typeID == 3 && lengthType == 0)
				{
					return MaximumByLength(subpacketLength);
				}
				else if (typeID == 3 && lengthType == 1)
				{
					return MaximumByCount(subpacketCount);
				}
				else  if (typeID == 5)
				{
					return GreaterThan();
				}
				else if (typeID == 6)
				{
					return LessThan();
				}
				else  if (typeID == 7)
				{
					return EqualTo();
				}
				else
				{
					return 0L;
				}
			}
		}

		private long SumByLength(int length)
		{
			int endIndex = index + length;
			long sum = 0L;
			while (index < endIndex)
			{
				sum += EvaluateSubpacket();
			}
			return sum;
		}

		private long SumByCount(int count)
		{
			long sum = 0L;
			for (int i = 1; i <= count; i++)
			{
				sum += EvaluateSubpacket();
			}
			return sum;
		}

		private long ProductByLength(int length)
		{
			int endIndex = index + length;
			long product = 1L;
			while (index < endIndex)
			{
				product *= EvaluateSubpacket();
			}
			return product;
		}

		private long ProductByCount(int count)
		{
			long product = 1L;
			for (int i = 1; i <= count; i++)
			{
				product *= EvaluateSubpacket();
			}
			return product;
		}

		private long MinimumByLength(int length)
		{
			int endIndex = index + length;
			long minimum = long.MaxValue;
			while (index < endIndex)
			{
				 minimum = Math.Min(EvaluateSubpacket(), minimum);
			}
			return minimum;
		}

		private long MinimumByCount(int count)
		{
			long minimum = long.MaxValue;
			for (int i = 1; i <= count; i++)
			{
				minimum = Math.Min(EvaluateSubpacket(), minimum);
			}
			return minimum;
		}

		private long MaximumByLength(int length)
		{
			int endIndex = index + length;
			long maximum = long.MinValue;
			while (index < endIndex)
			{
				maximum = Math.Max(EvaluateSubpacket(), maximum);
			}
			return maximum;
		}

		private long MaximumByCount(int count)
		{
			long maximum = long.MinValue;
			for (int i = 1; i <= count; i++)
			{
				maximum = Math.Max(EvaluateSubpacket(), maximum);
			}
			return maximum;
		}

		private long Value()
		{
			string valueString = "";
			string groupString;
			do
			{
				groupString = GetNextSubstring(5);
				valueString += groupString.Substring(1);
			} while (groupString.StartsWith('1'));
			return Convert.ToInt64(valueString, 2);
		}

		private long GreaterThan()
		{
			long value1 = EvaluateSubpacket();
			long value2 = EvaluateSubpacket();
			if (value1 > value2)
			{
				return 1L;
			}
			else
			{
				return 0L;
			}
		}

		private long LessThan()
		{
			long value1 = EvaluateSubpacket();
			long value2 = EvaluateSubpacket();
			if (value1 < value2)
			{
				return 1L;
			}
			else
			{
				return 0L;
			}
		}

		private long EqualTo()
		{
			long value1 = EvaluateSubpacket();
			long value2 = EvaluateSubpacket();
			if (value1 == value2)
			{
				return 1L;
			}
			else
			{
				return 0L;
			}
		}


	}
}
