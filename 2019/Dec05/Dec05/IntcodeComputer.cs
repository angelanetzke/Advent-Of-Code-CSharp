namespace Dec05
{
	internal class IntcodeComputer
	{
		private long[] memory = Array.Empty<long>();
		private long instructionPointer;
		private long[] input = Array.Empty<long>();
		private long inputPointer;
		private List<long> output = new ();
		public void Run()
		{
			instructionPointer = 0;
			inputPointer = 0;
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
				}
			} while (opcode != 99L);
		}

		private void Add()
		{
			long parameter1;
			long parameter2;
			if (memory[instructionPointer] / 100 % 10 == 0)
			{
				parameter1 = memory[memory[instructionPointer + 1]];
			}
			else
			{
				parameter1 = memory[instructionPointer + 1];
			}
			if (memory[instructionPointer] / 1000 % 10 == 0)
			{
				parameter2 = memory[memory[instructionPointer + 2]];
			}
			else
			{
				parameter2 = memory[instructionPointer + 2];
			}
			memory[memory[instructionPointer + 3]] = parameter1 + parameter2;
			instructionPointer += 4;
		}

		private void Multiply()
		{
			long parameter1;
			long parameter2;
			if (memory[instructionPointer] / 100 % 10 == 0)
			{
				parameter1 = memory[memory[instructionPointer + 1]];
			}
			else
			{
				parameter1 = memory[instructionPointer + 1];
			}
			if (memory[instructionPointer] / 1000 % 10 == 0)
			{
				parameter2 = memory[memory[instructionPointer + 2]];
			}
			else
			{
				parameter2 = memory[instructionPointer + 2];
			}
			memory[memory[instructionPointer + 3]] = parameter1 * parameter2;
			instructionPointer += 4;
		}

		private void GetInput()
		{
			memory[memory[instructionPointer + 1]] = input[inputPointer];
			inputPointer++;
			instructionPointer += 2;
		}

		private void SetOutput()
		{
			if (memory[instructionPointer] / 100 % 10 == 0)
			{
				output.Add(memory[memory[instructionPointer + 1]]);
			}
			else
			{
				output.Add(memory[instructionPointer + 1]);
			}			
			instructionPointer += 2;
		}

		private void JumpIfTrue()
		{
			long parameter1;
			long parameter2;
			if (memory[instructionPointer] / 100 % 10 == 0)
			{
				parameter1 = memory[memory[instructionPointer + 1]];
			}
			else
			{
				parameter1 = memory[instructionPointer + 1];
			}
			if (memory[instructionPointer] / 1000 % 10 == 0)
			{
				parameter2 = memory[memory[instructionPointer + 2]];
			}
			else
			{
				parameter2 = memory[instructionPointer + 2];
			}
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
			long parameter1;
			long parameter2;
			if (memory[instructionPointer] / 100 % 10 == 0)
			{
				parameter1 = memory[memory[instructionPointer + 1]];
			}
			else
			{
				parameter1 = memory[instructionPointer + 1];
			}
			if (memory[instructionPointer] / 1000 % 10 == 0)
			{
				parameter2 = memory[memory[instructionPointer + 2]];
			}
			else
			{
				parameter2 = memory[instructionPointer + 2];
			}
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
			long parameter1;
			long parameter2;
			if (memory[instructionPointer] / 100 % 10 == 0)
			{
				parameter1 = memory[memory[instructionPointer + 1]];
			}
			else
			{
				parameter1 = memory[instructionPointer + 1];
			}
			if (memory[instructionPointer] / 1000 % 10 == 0)
			{
				parameter2 = memory[memory[instructionPointer + 2]];
			}
			else
			{
				parameter2 = memory[instructionPointer + 2];
			}
			if (parameter1 < parameter2)
			{
				memory[memory[instructionPointer + 3]] = 1;
			}
			else
			{
				memory[memory[instructionPointer + 3]] = 0;
			}			
			instructionPointer += 4;
		}

		private void AreEqual()
		{
			long parameter1;
			long parameter2;
			if (memory[instructionPointer] / 100 % 10 == 0)
			{
				parameter1 = memory[memory[instructionPointer + 1]];
			}
			else
			{
				parameter1 = memory[instructionPointer + 1];
			}
			if (memory[instructionPointer] / 1000 % 10 == 0)
			{
				parameter2 = memory[memory[instructionPointer + 2]];
			}
			else
			{
				parameter2 = memory[instructionPointer + 2];
			}
			if (parameter1 == parameter2)
			{
				memory[memory[instructionPointer + 3]] = 1;
			}
			else
			{
				memory[memory[instructionPointer + 3]] = 0;
			}
			instructionPointer += 4;
		}

		public void SetMemory(string commands)
		{
			var commandArray = commands.Split(',');
			memory = new long[commandArray.Length];
			for (int i = 0; i < commandArray.Length; i++)
			{
				memory[i] = long.Parse(commandArray[i]);
			}
		}

		public void SetInput(long[] input)
		{
			this.input = input;
		}

		public void ClearOutput()
		{
			output = new ();
		}

		public List<long> GetOutput()
		{
			return output;
		}

		public void SetValue(int position, long value)
		{
			memory[position] = value;
		}
		public long GetValue(int position)
		{
			return memory[position];
		}
	}

}
