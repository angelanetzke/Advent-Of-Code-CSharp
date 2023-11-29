namespace Dec8
{
	internal class BootCode
	{
		private readonly string[] instructions;
		private int ip = 0;
		private int acc = 0;
		private bool[] hasBeenExecuted;
		private int lastChangedInstruction = -1;

		public BootCode(string[] instructions)
		{
			this.instructions = instructions;
			hasBeenExecuted = new bool[instructions.Length];
		}

		public int GetLastAcc()
		{
			ip = 0;
			acc = 0;
			hasBeenExecuted = new bool[instructions.Length];
			Execute();
			return acc;
		}

		public int Debug()
		{
			ip = 0;
			lastChangedInstruction = -1;
			while (ip < instructions.Length)
			{
				ip = 0;
				acc = 0;
				hasBeenExecuted = new bool[instructions.Length];
				ChangeNextInstruction();
				Execute();
			}
			return acc;
		}

		private void Execute()
		{
			if (ip >= instructions.Length)
			{
				return;
			}
			else if (hasBeenExecuted[ip])
			{
				return;
			}
			else
			{
				hasBeenExecuted[ip] = true;
				string opcode = instructions[ip].Split(' ')[0];
				int argument = int.Parse(instructions[ip].Split(' ')[1]);
				switch (opcode)
				{
					case "acc":
						acc += argument;
						ip++;
						break;
					case "jmp":
						ip += argument;
						break;
					case "nop":
						ip++;
						break;
				}
				Execute();
			}
		}


		private void ChangeNextInstruction()
		{
			//Put last changed instruction back the way it was.
			if (lastChangedInstruction >= 0)
			{
				FlipInstruction(lastChangedInstruction);
			}
			string thisOpcode;
			do
			{
				lastChangedInstruction++;
				thisOpcode = instructions[lastChangedInstruction].Split(' ')[0];
			} while (thisOpcode != "jmp" && thisOpcode != "nop");
			FlipInstruction(lastChangedInstruction);
		}

		private void FlipInstruction(int address)
		{
			string opcode = instructions[address].Split(' ')[0];
			string argumentString = instructions[address].Split(' ')[1];
			switch (opcode)
			{
				case "jmp":
					instructions[address] = "nop" + " " + argumentString;
					break;
				case "nop":
					instructions[address] = "jmp" + " " + argumentString;
					break;
			}

		}


	}
}
