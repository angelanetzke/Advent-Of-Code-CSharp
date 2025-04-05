using System.Text;

namespace Day24;

internal class Device
{
	private readonly Dictionary<string, (string, string, string)> wires = [];
	private readonly Dictionary<string, int> values = [];
	private readonly List<string> zWires = [];
	private readonly List<string> printLines = [];

	public Device(string[] allLines)
	{
		bool isInValueSection = true;
		foreach (string thisLine in allLines)
		{
			if (thisLine.Length == 0)
			{
				isInValueSection = false;
			}
			else if (isInValueSection)
			{
				string[] parts = thisLine.Split(": ");
				values[parts[0]] = int.Parse(parts[1]);
			}
			else
			{
				string[] parts = thisLine.Split(" -> ");
				string[] inputParts = parts[0].Split(' ');
				wires[parts[1]] = (inputParts[0], inputParts[1], inputParts[2]);
				if (parts[1].StartsWith('z'))
				{
					zWires.Add(parts[1]);
				}
			}
		}
		zWires.Sort();
		zWires.Reverse();
	}

	public long Part1()
	{
		StringBuilder builder = new ();
		foreach (string thisZWire in zWires)
		{
			builder.Append(GetValue(thisZWire));
		}
		return Convert.ToInt64(builder.ToString(), 2);
	}

	public string Part2()
	{		
		foreach (string thisWire in zWires)
		{
			Print(thisWire, 0, 3);
		}
		File.WriteAllLines("output.txt", printLines);
		return "solved by visual examination of the data";
	}

	private int GetValue(string gateName)
	{
		if (values.TryGetValue(gateName, out int thisValue))
		{
			return thisValue;
		}
		int left = GetValue(wires[gateName].Item1);
		int right = GetValue(wires[gateName].Item3);
		int result;
		if (wires[gateName].Item2 == "AND")
		{
			result = left == 1 && right == 1 ? 1: 0;
		}
		else if (wires[gateName].Item2 == "OR")
		{
			result = left == 1 || right == 1 ? 1 : 0;
		}
		else
		{
			result = left != right ? 1 : 0;
		}
		values[gateName] = result;
		return result;
	}

	private void Print(string current, int currentLevel, int endLevel)
	{
		if (currentLevel > endLevel)
		{
			return;
		}
		StringBuilder builder = new ();
		if (currentLevel > 0)
		{
			builder.Append(new string(' ', currentLevel * 2));
		}
		builder.Append(current);
		if (wires.TryGetValue(current, out (string, string, string) wireData))
		{
			builder.Append(": ");
			builder.Append(wireData.Item1);
			builder.Append(' ');
			builder.Append(wireData.Item2);
			builder.Append(' ');
			builder.Append(wireData.Item3);
			printLines.Add(builder.ToString());
			Print(wireData.Item1, currentLevel + 1, endLevel);
			Print(wireData.Item3, currentLevel + 1, endLevel);
		}		
		if (currentLevel == 0)
		{
			printLines.Add("\n");
		}
	}

}