namespace Day17;

internal class Computer(string[] data)
{
	private long registerA = long.Parse(data[0].Split(": ")[1]);
	private long registerB = long.Parse(data[1].Split(": ")[1]);
	private long registerC = long.Parse(data[2].Split(": ")[1]);
	private readonly int[] instructions = [..data[4][9..].Split(",").Select(x => int.Parse(x))];
	private readonly List<long> output = [];

	public void Execute()
	{
		output.Clear();
		int ip = 0;
		while (0 <= ip && ip < instructions.Length)
		{
			switch(instructions[ip])
			{
				case 0:
					registerA /= LongPower(2, GetComboValue(ip + 1));
					ip += 2;
					break;
				case 1:
					registerB ^= instructions[ip + 1];
					ip += 2;
					break;
				case 2:
					registerB = GetComboValue(ip + 1) % 8;
					ip += 2;
					break;
				case 3:
					if (registerA != 0)
					{
						ip = instructions[ip + 1];
					}
					else
					{
						ip += 2;
					}
					break;
				case 4:
					registerB ^= registerC;
					ip += 2;
					break;
				case 5:
					output.Add(GetComboValue(ip + 1) % 8);
					ip += 2;
					break;
				case 6:
					registerB = registerA / LongPower(2, GetComboValue(ip + 1));
					ip += 2;
					break;
				case 7:
					registerC = registerA / LongPower(2, GetComboValue(ip + 1));
					ip += 2;
					break;
			}
		}
	}

	private long GetComboValue(int index)
	{
		if (instructions[index] == 6)
		{
			return registerC;
		}
		if (instructions[index] == 5)
		{
			return registerB;
		}
		if (instructions[index] == 4)
		{
			return registerA;
		}
		return instructions[index];
	}

	public string GetOutput()
	{
		return string.Join(",", output);
	}

	public void SetRegisterA(long newValue)
	{
		registerA = newValue;
		registerB = 0L;
		registerC = 0L;
	}

	public long FindRegisterA()
	{
		long testRegisterA = 0L;
		bool isComplete = false;
		while (!isComplete)
		{
			SetRegisterA(testRegisterA);
			Execute();
			bool isPartialMatch = true;
			for (int i = 0; i < output.Count; i++)
			{
				if (output[i] != instructions[instructions.Length - output.Count + i])
				{
					isPartialMatch = false;
					break;
				}
			}
			if (isPartialMatch)
			{
				if (output.Count == instructions.Length)
				{
					isComplete = true;
					
				}
				else
				{
					testRegisterA *= 8;
				}				
			}
			else
			{
				testRegisterA++;
			}
		}		
		return testRegisterA;
	}

	private static long LongPower(long x, long y)
	{
		long result = 1L;
		for (int i = 1; i <= y; i++)
		{
			result *= x;
		}
		return result;
	}

}