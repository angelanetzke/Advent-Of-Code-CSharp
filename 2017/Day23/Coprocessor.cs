namespace Day23;

internal class Coprocessor
{
	private readonly Dictionary<char, int> registers = new ();
	private int ip = 0;

	public int Execute(string[] commands)
	{
		registers.Clear();
		var mulCount = 0;
		while (0 <= ip && ip < commands.Length)
		{
			var tokens = commands[ip].Split(' ');
			var parameters = GetParameters(tokens);	
			switch (tokens[0])
			{
				case "set":
					registers[parameters.register1] = parameters.value2;
					ip++;
					break;
				case "sub":
					registers[parameters.register1] -= parameters.value2;
					ip++;
					break;
				case "mul":
					registers[parameters.register1] *= parameters.value2;
					ip++;
					mulCount++;
					break;						
				case "jnz":					
					if (parameters.value1 != 0)
					{	
						ip += parameters.value2;
					}
					else
					{
						ip++;
					}
					break;
			}			
		}
		return mulCount;
	}
	
	public int Execute2()
	{
		var b = 107900;
		var c = 124900;
		var f = 0;
		var g = 0;
		var h = 0;
		do
		{
			f = 1;
			for (int d = 2; d < b; d++)
			{
				if (b % d == 0)
				{
					f = 0;
					break;
				}
			}
			h += f == 0 ? 1 : 0;
			g = b - c;
			b += 17;
		} while (g != 0);
		return h;
	}

	private (char register1, int value1, char register2, int value2) GetParameters(string[] tokens)
	{
		(char register1, int value1, char register2, int value2) parameters = ('z', 0, 'z', 0);
		if (char.IsLetter(tokens[1][0]))
		{
			parameters.register1 = tokens[1][0];
			if (!registers.ContainsKey(parameters.register1))
			{
				registers[parameters.register1] = 0;
			}
			parameters.value1 = registers[parameters.register1];
		}
		else
		{
			parameters.value1 = int.Parse(tokens[1]);
		}
		if (tokens.Length < 3)
		{
			return parameters;
		}
		if (char.IsLetter(tokens[2][0]))
		{
			parameters.register2 = tokens[2][0];
			if (!registers.ContainsKey(parameters.register2))
			{
				registers[parameters.register2] = 0;
			}
			parameters.value2 = registers[parameters.register2];
		}
		else
		{
			parameters.value2 = int.Parse(tokens[2]);
		}
		return parameters;
	}
}