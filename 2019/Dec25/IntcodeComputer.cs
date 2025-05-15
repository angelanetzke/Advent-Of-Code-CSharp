namespace Dec25;

internal class IntcodeComputer
{
	private readonly Dictionary<long, long> memory = [];
	private long instructionPointer;
	private readonly List<long> input = [];
	private int inputPointer;
	private List<long> output = [];
	private long relativeBase = 0L;
	private bool isPaused = false;
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
		} while (opcode != 99L && !isPaused);
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
		if (inputPointer >= input.Count)
		{
			isPaused = true;
		}
		else
		{
			memory[GetAddress(1)] = input[inputPointer];
			inputPointer++;
			instructionPointer += 2;
		}
	}

	private void SetOutput()
	{
		output.Add(GetValue(GetAddress(1)));
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


