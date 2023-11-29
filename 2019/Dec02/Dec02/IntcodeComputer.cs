namespace Dec02
{
	internal class IntcodeComputer
	{
		private long[] memory = Array.Empty<long>();
		private int ip;

		public void Run()
		{
			ip = 0;
			long opcode;
			do
			{
				opcode = memory[ip];
				if (opcode == 1)
				{
					long parameter1 = memory[memory[ip + 1]];
					long paramter2 = memory[memory[ip + 2]];
					memory[memory[ip + 3]] = parameter1 + paramter2;
					ip += 4;
				}
				else if (opcode == 2)
				{
					long parameter1 = memory[memory[ip + 1]];
					long paramter2 = memory[memory[ip + 2]];
					memory[memory[ip + 3]] = parameter1 * paramter2;
					ip += 4;
				}
			} while (opcode != 99L);
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
