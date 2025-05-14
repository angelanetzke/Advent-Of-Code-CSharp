namespace Dec23;

internal class IntcodeComputer
{
	private readonly Dictionary<long, long> memory = [];
	private long instructionPointer;
	private readonly List<long> input = [];
	private int inputPointer;
	private List<long> output = [];
	private long relativeBase = 0L;
	private bool isPaused = false;
	private static readonly List<(long, long, long)> globalOutput = [];
	private static long part1Solution = -1;
	private long id = -1;
	private bool hasID = false;
	public bool isIdle = false;
	private long lastInput = 0;
	private static long IDMin = long.MaxValue;
	private static long IDMax = long.MinValue;

	public void Run()
	{
		if (isPaused)
		{
			isPaused = false;
		}
		else
		{
			instructionPointer = 0;
			inputPointer = 0;
		}
		long opcode;
		do
		{
			opcode = memory[instructionPointer] % 100;
			switch (opcode)
			{
				case 1:
					Add();
					break;
				case 2:
					Multiply();
					break;
				case 3:
					GetInput();
					break;
				case 4:
					SetOutput();
					break;
				case 5:
					JumpIfTrue();
					break;
				case 6:
					JumpIfFalse();
					break;
				case 7:
					IsLessThan();
					break;
				case 8:
					AreEqual();
					break;
				case 9:
					SetRelativeBase();
					break;
			}
			isPaused = true;
		} while (opcode != 99L && !isPaused);
	}

	public void SetID(long id)
	{
		this.id = id;
		IDMin = Math.Min(IDMin, id);
		IDMax = Math.Max(IDMax, id);
	}

	public static long GetPart1Solution()
	{
		return part1Solution;
	}

	public static void SendToGlobalOutput((long, long, long) newOutput)
	{
		globalOutput.Add(newOutput);
	}

	public bool IsIdle()
	{
		return isIdle && globalOutput.Count == 0;
	}

	private void Add()
	{
		var parameter1 = GetValue(GetAddress(1));
		var parameter2 = GetValue(GetAddress(2));
		memory[GetAddress(3)] = parameter1 + parameter2;
		instructionPointer += 4;
	}

	private void Multiply()
	{
		var parameter1 = GetValue(GetAddress(1));
		var parameter2 = GetValue(GetAddress(2));
		memory[GetAddress(3)] = parameter1 * parameter2;
		instructionPointer += 4;
	}

	private void GetInput()
	{
		isIdle = false;
		if (inputPointer >= input.Count)
		{
			if (!hasID) 
			{
				input.Add(id);
				hasID = true;
			}
			else
			{
				List<(long, long, long)> inputForThisComputer = [..globalOutput.Where(x => x.Item1 == id)];
				if (inputForThisComputer.Count > 0)
				{
					foreach ((long, long, long) thisInput in inputForThisComputer)
					{
						input.Add(thisInput.Item2);
						input.Add(thisInput.Item3);
						globalOutput.Remove(thisInput);
					}
				}
				else
				{
					input.Add(-1L);
					if (lastInput == -1L)
					{
						isIdle = true;
					}
				}
			}
		}
		memory[GetAddress(1)] = input[inputPointer];
		lastInput = input[inputPointer];
		inputPointer++;
		instructionPointer += 2;
	}

	private void SetOutput()
	{
		output.Add(GetValue(GetAddress(1)));
		if (output.Count == 3)
		{
			if (output[0] == 255)
			{
				part1Solution = output[2];
				NAT.Receive(output[1], output[2]);
				
			}
			else if (IDMin <= output[0] && output[0] <= IDMax)
			{
				globalOutput.Add((output[0], output[1], output[2]));
			}
			ClearOutput();			
		}
		isIdle = false;
		instructionPointer += 2;
	}

	private void JumpIfTrue()
	{
		var parameter1 = GetValue(GetAddress(1));
		var parameter2 = GetValue(GetAddress(2));
		if (parameter1 == 0)
		{
			instructionPointer += 3;
		}
		else
		{
			instructionPointer = parameter2;
		}
	}

	private void JumpIfFalse()
	{
		var parameter1 = GetValue(GetAddress(1));
		var parameter2 = GetValue(GetAddress(2));
		if (parameter1 == 0)
		{
			instructionPointer = parameter2;
		}
		else
		{
			instructionPointer += 3;
		}
	}

	private void IsLessThan()
	{
		var parameter1 = GetValue(GetAddress(1));
		var parameter2 = GetValue(GetAddress(2));
		if (parameter1 < parameter2)
		{
			memory[GetAddress(3)] = 1;
		}
		else
		{
			memory[GetAddress(3)] = 0;
		}
		instructionPointer += 4;
	}

	private void AreEqual()
	{
		var parameter1 = GetValue(GetAddress(1));
		var parameter2 = GetValue(GetAddress(2));
		if (parameter1 == parameter2)
		{
			memory[GetAddress(3)] = 1;
		}
		else
		{
			memory[GetAddress(3)] = 0;
		}
		instructionPointer += 4;
	}

	private void SetRelativeBase()
	{
		var parameter1 = GetValue(GetAddress(1));
		relativeBase += parameter1;
		instructionPointer += 2;
	}

	private long GetAddress(long parameter)
	{
		var mode = memory[instructionPointer] / 100;
		for (int i = 0; i < parameter - 1; i++)
		{
			mode /= 10;
		}
		mode %= 10;
		if (mode == 0)
		{
			return GetValue(instructionPointer + parameter);
		}
		else if (mode == 1)
		{
			return instructionPointer + parameter;
		}
		else
		{
			return GetValue(instructionPointer + parameter) + relativeBase;
		}
	}

	public void SetMemory(string commands)
	{
		var commandArray = commands.Split(',');
		for (int i = 0; i < commandArray.Length; i++)
		{
			memory[i] = long.Parse(commandArray[i]);
		}
	}

	public void AddInput(long newInput)
	{
		input.Add(newInput);
	}

	public void ClearOutput()
	{
		output = new();
	}

	public List<long> GetOutput()
	{
		return output;
	}

	public void SetValue(long position, long value)
	{
		memory[position] = value;
	}
	
	public long GetValue(long position)
	{
		if (memory.ContainsKey(position))
		{
			return memory[position];
		}
		else
		{
			return 0L;
		}
	}

	public bool IsPaused()
	{
		return isPaused;
	}

}


