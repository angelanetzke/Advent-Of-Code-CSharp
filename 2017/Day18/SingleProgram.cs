namespace Day18;

internal class SingleProgram
{
	public static string[] commands = Array.Empty<string>();
	public SingleProgram? partner { set; get; } = null;
	private readonly Queue<long> queue = new ();
	private bool isPaused = false;
	private readonly Dictionary<char, long> registers = new ();
	private long lastSound = -1L;
	private long ip = 0L;
	private long sendCount = 0L;

	public SingleProgram(long id)
	{
		registers['p'] = id;
	}

	public long Execute(bool isSoundNeeded)
	{
		isPaused = false;
		while (0 <= ip && ip < commands.Length && !isPaused)
		{
			var tokens = commands[ip].Split(' ');
			var parameters = GetParameters(tokens);	
			switch (tokens[0])
			{
				case "snd":
					sendCount++;
					if (partner != null)
					{
						partner.queue.Enqueue(parameters.value1);
					}
					lastSound = parameters.value1;
					ip++;
					break;
				case "set":
					registers[parameters.register1] = parameters.value2;
					ip++;
					break;
				case "add":
					registers[parameters.register1] += parameters.value2;
					ip++;
					break;
				case "mul":
					registers[parameters.register1] *= parameters.value2;
					ip++;
					break;
				case "mod":
					registers[parameters.register1] %= parameters.value2;
					ip++;
					break;
				case "rcv":
					if (isSoundNeeded)
					{
						if (parameters.value1 != 0)
						{
							return lastSound;
						}
						else
						{
							ip++;
						}						
					}
					else
					{
						if (queue.Count == 0)
						{
							isPaused = true;
						}
						else
						{
							registers[parameters.register1] = queue.Dequeue();
							ip++;
						}
					}
					break;			
				case "jgz":
					if (parameters.value1 > 0)
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
		return sendCount;
	}

	public int GetQueueCount()
	{
		return queue.Count;
	}

	private (char register1, long value1, char register2, long value2) GetParameters(string[] tokens)
	{
		(char register1, long value1, char register2, long value2) parameters = ('z', 0, 'z', 0);
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
			parameters.value1 = long.Parse(tokens[1]);
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
			parameters.value2 = long.Parse(tokens[2]);
		}
		return parameters;
	}
	
}