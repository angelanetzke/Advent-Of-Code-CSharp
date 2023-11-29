using System.Text.RegularExpressions;

namespace Day16
{
	internal class Device
	{
		private List<Sample> sampleList = new ();
		private Dictionary<int, HashSet<string>> opcodes = new ();

		public void AddSample(List<string> sampleData)
		{
			sampleList.Add(new Sample(sampleData));
		}

		public int CountAtLeastThree()
		{
			int count = 0;
			foreach (Sample thisSample in sampleList)
			{
				count += CountMatches(thisSample) >= 3 ? 1 : 0;
			}
			return count;
		}

		private static int CountMatches(Sample theSample)
		{
			var matchCount = 0;
			matchCount += theSample.finalRegisters.SequenceEqual<int>(Addr(theSample.initialRegisters, theSample.instruction)) ? 1 : 0;
			matchCount += theSample.finalRegisters.SequenceEqual<int>(Addi(theSample.initialRegisters, theSample.instruction)) ? 1 : 0;
			matchCount += theSample.finalRegisters.SequenceEqual<int>(Mulr(theSample.initialRegisters, theSample.instruction)) ? 1 : 0;
			matchCount += theSample.finalRegisters.SequenceEqual<int>(Muli(theSample.initialRegisters, theSample.instruction)) ? 1 : 0;
			matchCount += theSample.finalRegisters.SequenceEqual<int>(Banr(theSample.initialRegisters, theSample.instruction)) ? 1 : 0;
			matchCount += theSample.finalRegisters.SequenceEqual<int>(Bani(theSample.initialRegisters, theSample.instruction)) ? 1 : 0;
			matchCount += theSample.finalRegisters.SequenceEqual<int>(Borr(theSample.initialRegisters, theSample.instruction)) ? 1 : 0;
			matchCount += theSample.finalRegisters.SequenceEqual<int>(Bori(theSample.initialRegisters, theSample.instruction)) ? 1 : 0;
			matchCount += theSample.finalRegisters.SequenceEqual<int>(Setr(theSample.initialRegisters, theSample.instruction)) ? 1 : 0;
			matchCount += theSample.finalRegisters.SequenceEqual<int>(Seti(theSample.initialRegisters, theSample.instruction)) ? 1 : 0;
			matchCount += theSample.finalRegisters.SequenceEqual<int>(Gtir(theSample.initialRegisters, theSample.instruction)) ? 1 : 0;
			matchCount += theSample.finalRegisters.SequenceEqual<int>(Gtri(theSample.initialRegisters, theSample.instruction)) ? 1 : 0;
			matchCount += theSample.finalRegisters.SequenceEqual<int>(Gtrr(theSample.initialRegisters, theSample.instruction)) ? 1 : 0;
			matchCount += theSample.finalRegisters.SequenceEqual<int>(Eqir(theSample.initialRegisters, theSample.instruction)) ? 1 : 0;
			matchCount += theSample.finalRegisters.SequenceEqual<int>(Eqri(theSample.initialRegisters, theSample.instruction)) ? 1 : 0;
			matchCount += theSample.finalRegisters.SequenceEqual<int>(Eqrr(theSample.initialRegisters, theSample.instruction)) ? 1 : 0;
			return matchCount;
		}

		public int Run(List<string> instructionList)
		{
			SetOpCodes();
			var registers = Enumerable.Repeat(0, 4).ToArray();
			foreach (string thisInstructionString in instructionList)
			{
				var thisInstruction = thisInstructionString.Split(' ').Select(x => int.Parse(x)).ToArray();
				switch (opcodes[thisInstruction[0]].First())
				{
					case "addr":
						registers = Addr(registers, thisInstruction);
						break;
					case "addi":
						registers = Addi(registers, thisInstruction);
						break;
					case "mulr":
						registers = Mulr(registers, thisInstruction);
						break;
					case "muli":
						registers = Muli(registers, thisInstruction);
						break;
					case "banr":
						registers = Banr(registers, thisInstruction);
						break;
					case "bani":
						registers = Bani(registers, thisInstruction);
						break;
					case "borr":
						registers = Borr(registers, thisInstruction);
						break;
					case "bori":
						registers = Bori(registers, thisInstruction);
						break;
					case "setr":
						registers = Setr(registers, thisInstruction);
						break;
					case "seti":
						registers = Seti(registers, thisInstruction);
						break;
					case "gtir":
						registers = Gtir(registers, thisInstruction);
						break;
					case "gtri":
						registers = Gtri(registers, thisInstruction);
						break;
					case "gtrr":
						registers = Gtrr(registers, thisInstruction);
						break;
					case "eqir":
						registers = Eqir(registers, thisInstruction);
						break;
					case "eqri":
						registers = Eqri(registers, thisInstruction);
						break;
					case "eqrr":
						registers = Eqrr(registers, thisInstruction);
						break;
				}
			}
			return registers[0];
		}

		private void SetOpCodes()
		{
			foreach (Sample thisSample in sampleList)
			{
				var thisOpcode = thisSample.instruction[0];
				if (!opcodes.ContainsKey(thisOpcode))
				{
					opcodes[thisOpcode] = new HashSet<string>();
				}
				if (thisSample.finalRegisters.SequenceEqual<int>(Addr(thisSample.initialRegisters, thisSample.instruction)))
				{
					opcodes[thisOpcode].Add("addr");
				}
				if (thisSample.finalRegisters.SequenceEqual<int>(Addi(thisSample.initialRegisters, thisSample.instruction)))
				{
					opcodes[thisOpcode].Add("addi");
				}
				if (thisSample.finalRegisters.SequenceEqual<int>(Mulr(thisSample.initialRegisters, thisSample.instruction)))
				{
					opcodes[thisOpcode].Add("mulr");
				}
				if (thisSample.finalRegisters.SequenceEqual<int>(Muli(thisSample.initialRegisters, thisSample.instruction)))
				{
					opcodes[thisOpcode].Add("muli");
				}
				if (thisSample.finalRegisters.SequenceEqual<int>(Banr(thisSample.initialRegisters, thisSample.instruction)))
				{
					opcodes[thisOpcode].Add("banr");
				}
				if (thisSample.finalRegisters.SequenceEqual<int>(Bani(thisSample.initialRegisters, thisSample.instruction)))
				{
					opcodes[thisOpcode].Add("bani");
				}
				if (thisSample.finalRegisters.SequenceEqual<int>(Borr(thisSample.initialRegisters, thisSample.instruction)))
				{
					opcodes[thisOpcode].Add("borr");
				}
				if (thisSample.finalRegisters.SequenceEqual<int>(Bori(thisSample.initialRegisters, thisSample.instruction)))
				{
					opcodes[thisOpcode].Add("bori");
				}
				if (thisSample.finalRegisters.SequenceEqual<int>(Setr(thisSample.initialRegisters, thisSample.instruction)))
				{
					opcodes[thisOpcode].Add("setr");
				}
				if (thisSample.finalRegisters.SequenceEqual<int>(Seti(thisSample.initialRegisters, thisSample.instruction)))
				{
					opcodes[thisOpcode].Add("seti");
				}
				if (thisSample.finalRegisters.SequenceEqual<int>(Gtir(thisSample.initialRegisters, thisSample.instruction)))
				{
					opcodes[thisOpcode].Add("gtir");
				}
				if (thisSample.finalRegisters.SequenceEqual<int>(Gtri(thisSample.initialRegisters, thisSample.instruction)))
				{
					opcodes[thisOpcode].Add("gtri");
				}
				if (thisSample.finalRegisters.SequenceEqual<int>(Gtrr(thisSample.initialRegisters, thisSample.instruction)))
				{
					opcodes[thisOpcode].Add("gtrr");
				}
				if (thisSample.finalRegisters.SequenceEqual<int>(Eqir(thisSample.initialRegisters, thisSample.instruction)))
				{
					opcodes[thisOpcode].Add("eqir");
				}
				if (thisSample.finalRegisters.SequenceEqual<int>(Eqri(thisSample.initialRegisters, thisSample.instruction)))
				{
					opcodes[thisOpcode].Add("eqri");
				}
				if (thisSample.finalRegisters.SequenceEqual<int>(Eqrr(thisSample.initialRegisters, thisSample.instruction)))
				{
					opcodes[thisOpcode].Add("eqrr");
				}
			}
			while (opcodes.Values.Count(x => x.Count > 1) > 0)
			{
				foreach (int thisOpcode in opcodes.Keys)
				{
					if (opcodes[thisOpcode].Count == 1)
					{
						foreach (int thisOtherOpcode in opcodes.Keys)
						{
							if (thisOpcode == thisOtherOpcode)
							{
								continue;
							}
							opcodes[thisOtherOpcode].RemoveWhere(x => opcodes[thisOpcode].Contains(x));
						}
					}
				}
			}
		}

		private static int[] Addr(int[] register, int[] instruction)
		{
			int[] result = new int[register.Length];
			Array.Copy(register, result, register.Length);
			var a = register[instruction[1]];
			var b = register[instruction[2]];
			result[instruction[3]] = a + b;
			return result;
		}

		private static int[] Addi(int[] register, int[] instruction)
		{
			int[] result = new int[register.Length];
			Array.Copy(register, result, register.Length);
			var a = register[instruction[1]];
			var b = instruction[2];
			result[instruction[3]] = a + b;
			return result;
		}

		private static int[] Mulr(int[] register, int[] instruction)
		{
			int[] result = new int[register.Length];
			Array.Copy(register, result, register.Length);
			var a = register[instruction[1]];
			var b = register[instruction[2]];
			result[instruction[3]] = a * b;
			return result;
		}

		private static int[] Muli(int[] register, int[] instruction)
		{
			int[] result = new int[register.Length];
			Array.Copy(register, result, register.Length);
			var a = register[instruction[1]];
			var b = instruction[2];
			result[instruction[3]] = a * b;
			return result;
		}

		private static int[] Banr(int[] register, int[] instruction)
		{
			int[] result = new int[register.Length];
			Array.Copy(register, result, register.Length);
			var a = register[instruction[1]];
			var b = register[instruction[2]];
			result[instruction[3]] = a & b;
			return result;
		}

		private static int[] Bani(int[] register, int[] instruction)
		{
			int[] result = new int[register.Length];
			Array.Copy(register, result, register.Length);
			var a = register[instruction[1]];
			var b = instruction[2];
			result[instruction[3]] = a & b;
			return result;
		}
		
		private static int[] Borr(int[] register, int[] instruction)
		{
			int[] result = new int[register.Length];
			Array.Copy(register, result, register.Length);
			var a = register[instruction[1]];
			var b = register[instruction[2]];
			result[instruction[3]] = a | b;
			return result;
		}

		private static int[] Bori(int[] register, int[] instruction)
		{
			int[] result = new int[register.Length];
			Array.Copy(register, result, register.Length);
			var a = register[instruction[1]];
			var b = instruction[2];
			result[instruction[3]] = a | b;
			return result;
		}

		private static int[] Setr(int[] register, int[] instruction)
		{
			int[] result = new int[register.Length];
			Array.Copy(register, result, register.Length);
			result[instruction[3]] = register[instruction[1]];
			return result;
		}

		private static int[] Seti(int[] register, int[] instruction)
		{
			int[] result = new int[register.Length];
			Array.Copy(register, result, register.Length);
			result[instruction[3]] = instruction[1];
			return result;
		}

		private static int[] Gtir(int[] register, int[] instruction)
		{
			int[] result = new int[register.Length];
			Array.Copy(register, result, register.Length);
			var a = instruction[1];
			var b = register[instruction[2]];
			result[instruction[3]] = a > b ? 1 : 0;
			return result;
		}

		private static int[] Gtri(int[] register, int[] instruction)
		{
			int[] result = new int[register.Length];
			Array.Copy(register, result, register.Length);
			var a = register[instruction[1]];
			var b = instruction[2];
			result[instruction[3]] = a > b ? 1 : 0;
			return result;
		}

		private static int[] Gtrr(int[] register, int[] instruction)
		{
			int[] result = new int[register.Length];
			Array.Copy(register, result, register.Length);
			var a = register[instruction[1]];
			var b = register[instruction[2]];
			result[instruction[3]] = a > b ? 1 : 0;
			return result;
		}

		private static int[] Eqir(int[] register, int[] instruction)
		{
			int[] result = new int[register.Length];
			Array.Copy(register, result, register.Length);
			var a = instruction[1];
			var b = register[instruction[2]];
			result[instruction[3]] = a == b ? 1 : 0;
			return result;
		}

		private static int[] Eqri(int[] register, int[] instruction)
		{
			int[] result = new int[register.Length];
			Array.Copy(register, result, register.Length);
			var a = register[instruction[1]];
			var b = instruction[2];
			result[instruction[3]] = a == b ? 1 : 0;
			return result;
		}

		private static int[] Eqrr(int[] register, int[] instruction)
		{
			int[] result = new int[register.Length];
			Array.Copy(register, result, register.Length);
			var a = register[instruction[1]];
			var b = register[instruction[2]];
			result[instruction[3]] = a == b ? 1 : 0;
			return result;
		}

	}
}