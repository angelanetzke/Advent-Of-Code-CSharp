namespace Day19
{
	internal class Device
	{
		private int[] registers = Enumerable.Repeat(0, 6).ToArray();
		private readonly string[] commandList;
		private readonly int ipRegister;

		public Device(string[] commandList)
		{
			ipRegister = int.Parse(commandList[0].Split(' ')[1]);
			this.commandList = commandList.Skip(1).Take(commandList.Length - 1).ToArray();
		}

		public int Execute(bool isPart2)
		{
			if (isPart2)
			{
				registers = new int[] {1, 0, 0, 0, 0, 0};
			}
			int ip = 0;
			while (0 <= ip && ip < commandList.Length)
			{
				registers[ipRegister] = ip;
				var nextCommand = commandList[ip].Split(' ');				
				var opcode = nextCommand[0];
				var parameters = nextCommand.Skip(1)
					.Take(nextCommand.Length - 1)
					.Select(x => int.Parse(x))
					.ToArray();
				switch (opcode)
				{
					case "addr":
						Addr(parameters);
						break;
					case "addi":
						Addi(parameters);
						break;
					case "mulr":
						Mulr(parameters);
						break;
					case "muli":
						Muli(parameters);
						break;
					case "banr":
						Banr(parameters);
						break;
					case "bani":
						Bani(parameters);
						break;
					case "borr":
						Borr(parameters);
						break;
					case "bori":
						Bori(parameters);
						break;
					case "setr":
						Setr(parameters);
						break;
					case "seti":
						Seti(parameters);
						break;
					case "gtir":
						Gtir(parameters);
						break;
					case "gtri":
						Gtri(parameters);
						break;
					case "gtrr":
						Gtrr(parameters);
						break;
					case "eqir":
						Eqir(parameters);
						break;
					case "eqri":
						Eqri(parameters);
						break;
					case "eqrr":
						Eqrr(parameters);
						break;
				}
				if (isPart2 && ip == 33)
				{
					return registers[4];
				}
				ip = registers[ipRegister] + 1;
			}
			return registers[0];
		}

		private void Addr(int[] parameters)
		{
			var a = registers[parameters[0]];
			var b = registers[parameters[1]];
			registers[parameters[2]] = a + b;
		}

		private void Addi(int[] parameters)
		{
			var a = registers[parameters[0]];
			var b = parameters[1];
			registers[parameters[2]] = a + b;
		}

		private void Mulr(int[] parameters)
		{
			var a = registers[parameters[0]];
			var b = registers[parameters[1]];
			registers[parameters[2]] = a * b;
		}

		private void Muli(int[] parameters)
		{
			var a = registers[parameters[0]];
			var b = parameters[1];
			registers[parameters[2]] = a * b;
		}

		private void Banr(int[] parameters)
		{
			var a = registers[parameters[0]];
			var b = registers[parameters[1]];
			registers[parameters[2]] = a & b;
		}

		private void Bani(int[] parameters)
		{
			var a = registers[parameters[0]];
			var b = parameters[1];
			registers[parameters[2]] = a & b;
		}
		
		private void Borr(int[] parameters)
		{
			var a = registers[parameters[0]];
			var b = registers[parameters[1]];
			registers[parameters[2]] = a | b;
		}

		private void Bori(int[] parameters)
		{
			var a = registers[parameters[0]];
			var b = parameters[1];
			registers[parameters[2]] = a | b;
		}

		private void Setr(int[] parameters)
		{
			registers[parameters[2]] = registers[parameters[0]];
		}

		private void Seti(int[] parameters)
		{
			registers[parameters[2]] = parameters[0];
		}

		private void Gtir(int[] parameters)
		{
			var a = parameters[0];
			var b = registers[parameters[1]];
			registers[parameters[2]] = a > b ? 1 : 0;
		}

		private void Gtri(int[] parameters)
		{
			var a = registers[parameters[0]];
			var b = parameters[1];
			registers[parameters[2]] = a > b ? 1 : 0;
		}

		private void Gtrr(int[] parameters)
		{
			var a = registers[parameters[0]];
			var b = registers[parameters[1]];
			registers[parameters[2]] = a > b ? 1 : 0;
		}

		private void Eqir(int[] parameters)
		{
			var a = parameters[0];
			var b = registers[parameters[1]];
			registers[parameters[2]] = a == b ? 1 : 0;
		}

		private void Eqri(int[] parameters)
		{
			var a = registers[parameters[0]];
			var b = parameters[1];
			registers[parameters[2]] = a == b ? 1 : 0;
		}

		private void Eqrr(int[] parameters)
		{
			var a = registers[parameters[0]];
			var b = registers[parameters[1]];
			registers[parameters[2]] = a == b ? 1 : 0;
		}	
	}
}