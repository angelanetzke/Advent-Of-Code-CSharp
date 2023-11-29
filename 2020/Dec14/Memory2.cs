using System.Text.RegularExpressions;
using System.Text;

namespace Dec14
{
	internal class Memory2
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
				string addressString = memoryRegex.Match(nextLine).Groups["address"].Value;
				addressString = Convert.ToString(long.Parse(addressString), 2);
				addressString = new string('0', mask.Length - addressString.Length) + addressString;
				long value = long.Parse(memoryRegex.Match(nextLine).Groups["value"].Value);
				var builder = new StringBuilder();
				for (int i = 0; i < addressString.Length; i++)
				{
					if (mask[i] == '0')
					{
						builder.Append(addressString[i]);
					}
					else if (mask[i] == '1')
					{
						builder.Append('1');
					}
					else
					{
						builder.Append('X');
					}
				}
				var allAddressStrings = new List<string>();
				GetAddresses(builder.ToString(), allAddressStrings);
				foreach(string thisAddress in allAddressStrings)
				{
					memoryValues[Convert.ToInt64(thisAddress, 2)] = value;
				}
			}
		}

		private void GetAddresses(string startString, List<string> addressList)
		{
			int nextX = startString.IndexOf('X');
			if (nextX == -1)
			{
				addressList.Add(startString);
			}
			else
			{
				var builder = new StringBuilder(startString);
				builder[nextX] = '0';
				GetAddresses(builder.ToString(), addressList);
				builder[nextX] = '1';
				GetAddresses(builder.ToString(), addressList);
			}
		}

		public long GetSum()
		{
			return memoryValues.Keys.Sum(thisKey => memoryValues[thisKey]);
		}

	}
}
