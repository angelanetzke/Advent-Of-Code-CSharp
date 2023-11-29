namespace Dec07
{
	internal class IntcodeComputer
	{
		private long[] memory = Array.Empty<long>();
		private long instructionPointer = 0;
		private readonly List<long> input = new ();
		private int inputPointer = 0;
		private readonly List<long> output = new ();
		public enum EXIT_REASON { HALT, PAUSE };
		bool isPaused = false;
		public EXIT_REASON Run()
		{
			isPaused = false;
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
						isPaused = true;
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
			} while (opcode != 99L && !isPaused);
			if (isPaused)
			{
				return EXIT_REASON.PAUSE;
			}
			else
			{
				return EXIT_REASON.HALT;
			}
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
			instructionPointer = 0;
			inputPointer = 0;
		}

		public void AddInput(long nextInput)
		{
			input.Add(nextInput);
		}

		public void ClearOutput()
		{
			output.Clear();
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
