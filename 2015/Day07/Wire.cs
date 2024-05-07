namespace Day07;

internal class Wire
{
	private static readonly Dictionary<string, Wire> allWires = [];
	private readonly string id;
	private readonly string? operation;
	private readonly string? inputWire1;
	private readonly string? inputWire2;
	private readonly int shiftAmount;
	private static readonly Dictionary<string, int> wireCache = [];
	
	public Wire(string data)
	{
		string[] parts = data.Split(' ');
		id = parts[^1];
		if (parts.Length == 3)
		{
			operation = "GET";
			inputWire1 = parts[0];
		}
		else if (parts[0] == "NOT")
		{
			operation = "NOT";
			inputWire1 = parts[1];
		}
		else if (parts[1] == "AND" || parts[1] == "OR")
		{
			operation = parts[1];
			inputWire1 = parts[0];
			inputWire2 = parts[2];
		}
		else if (parts[1] == "LSHIFT" || parts[1]== "RSHIFT")
		{
			operation = parts[1];
			inputWire1 = parts[0];
			shiftAmount = int.Parse(parts[2]);
		}
	}

	public static void AddWire(string data)
	{
		string[] parts = data.Split(" -> ");
		allWires[parts[1]] = new Wire(data);
	}

	public static void ClearCache()
	{
		wireCache.Clear();
	}

	public static void SetValue(string wireName, ushort value)
	{
		wireCache[wireName] = value;
	}

	public static ushort GetValue(string wireName)
	{
		return allWires[wireName].GetValue();
	}

	private ushort GetValue()
	{
		if (wireCache.TryGetValue(id, out int cachedValue))
		{
			return (ushort)cachedValue;
		}
		if (operation == "GET")
		{
			ushort inputValue1 = GetOneInputValue();
			wireCache[id] = inputValue1;			
			return inputValue1;
		}
		else if (operation == "NOT")
		{
			ushort inputValue1 = GetOneInputValue();
			wireCache[id] = ~inputValue1;
			return (ushort)~inputValue1;
		}
		else if (operation == "AND")
		{
			(ushort, ushort) bothInputValues = GetTwoInputValues();
			ushort inputValue1 = bothInputValues.Item1;
			ushort inputValue2 = bothInputValues.Item2;
			wireCache[id] = inputValue1 & inputValue2;
			return (ushort)(inputValue1 & inputValue2);
		}
		else if (operation == "OR")
		{
			(ushort, ushort) bothInputValues = GetTwoInputValues();
			ushort inputValue1 = bothInputValues.Item1;
			ushort inputValue2 = bothInputValues.Item2;
			wireCache[id] = inputValue1 | inputValue2;
			return (ushort)(inputValue1 | inputValue2);
		}
		else if (operation == "LSHIFT")
		{
			ushort inputValue1 = allWires[inputWire1!].GetValue();
			wireCache[id] = inputValue1 << shiftAmount;
			return (ushort)(inputValue1 << shiftAmount);
		}
		else if (operation == "RSHIFT")
		{
			ushort inputValue1 = allWires[inputWire1!].GetValue();
			wireCache[id] = inputValue1 >> shiftAmount;
			return (ushort)(inputValue1 >> shiftAmount);
		}
		return 0;
	}

	private ushort GetOneInputValue()
	{
		ushort inputValue1;
		if (allWires.TryGetValue(inputWire1!, out Wire? wireValue))
		{
			if (wireCache.TryGetValue(inputWire1!, out int cachedValue))
			{
				inputValue1 = (ushort)cachedValue;
			}
			else
			{
				inputValue1 = wireValue.GetValue();
				wireCache[inputWire1!] = inputValue1;
			}			
		}
		else
		{
			inputValue1 = ushort.Parse(inputWire1!);
		}
		return inputValue1;
	}

	private (ushort, ushort) GetTwoInputValues()
	{
		ushort inputValue1;
		ushort inputValue2;
		if (allWires.TryGetValue(inputWire1!, out Wire? wireValue1))
		{
			if (wireCache.TryGetValue(inputWire1!, out int cachedValue))
			{
				inputValue1 = (ushort)cachedValue;
			}
			else
			{
				inputValue1 = wireValue1.GetValue();
				wireCache[inputWire1!] = inputValue1;
			}			
		}
		else
		{
			inputValue1 = ushort.Parse(inputWire1!);
		}
		if (allWires.TryGetValue(inputWire2!, out Wire? wireValue2))
		{
			if (wireCache.TryGetValue(inputWire2!, out int cachedValue))
			{
				inputValue2 = (ushort)cachedValue;
			}
			else
			{
				inputValue2 = wireValue2.GetValue();
				wireCache[inputWire2!] = inputValue2;
			}
		}
		else
		{
			inputValue2 = ushort.Parse(inputWire2!);
		}
		return (inputValue1, inputValue2);
	}
}