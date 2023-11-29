using System.Text.RegularExpressions;
using System.Text;

namespace Dec14
{
	internal class Memory
	{
		private string mask = "";
		//key is memory address, value is value at that location
		private readonly Dictionary<long, long> memoryValues = new();

		public void Next(string nextLine)
		{
			if (nextLine.StartsWith("mask = "))
			{
				SetMask(nextLine);
			}
			else
			{
				SetMemory(nextLine);
			}
		}

		private void SetMask(string nextLine)
		{
			mask = nextLine.Substring(7);
		}

		private void SetMemory(string nextLine)
		{
			if (mask.Length > 0)
			{
				var memoryRegex = new Regex(@"mem\[(?<address>\d+)\] = (?<value>\d+)");
				long address = long.Parse(memoryRegex.Match(nextLine).Groups["address"].Value);
				string valueString = memoryRegex.Match(nextLine).Groups["value"].Value;
				valueString = Convert.ToString(long.Parse(valueString), 2);
				valueString = new string('0', mask.Length - valueString.Length) + valueString;
				var builder = new StringBuilder();
				for (int i = 0; i < valueString.Length; i++)
				{
					if (mask[i] == 'X')
					{
						builder.Append(valueString[i]);
					}
					else
					{
						builder.Append(mask[i]);
					}
				}
				long value = Convert.ToInt64(builder.ToString(), 2);
				memoryValues[address] = value;
			}
		}

		public long GetSum()
		{
			return memoryValues.Keys.Sum(thisKey => memoryValues[thisKey]);
		}

	}
}
