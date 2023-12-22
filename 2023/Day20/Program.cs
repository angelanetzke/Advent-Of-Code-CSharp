using Day20;

string[] allLines = File.ReadAllLines("input.txt");
int x = 9;
Console.WriteLine($"Part 1: {Part1(allLines)}");

static long Part1(string[] allLines)
{
	Dictionary<string, IModule> modules = [];
	foreach (string thisLine in allLines)
	{
		if (thisLine.StartsWith("broadcaster"))
		{
			Broadcaster newModule = new (thisLine);
			modules[newModule.GetName()] = newModule;
		}
		if (thisLine.StartsWith('%'))
		{
			FlipFlop newModule = new (thisLine);
			modules[newModule.GetName()] = newModule;
		}
		if (thisLine.StartsWith('&'))
		{
			Conjunction newModule = new (thisLine);
			modules[newModule.GetName()] = newModule;
		}
	}
	foreach (string thisModuleName in modules.Keys)
	{
		if (modules[thisModuleName] is Conjunction thisConjuntion)
		{
			List<string> inputModules = modules
				.Where(x => x.Value.ContainsOutput(thisConjuntion.GetName()))
				.Select(x => x.Key)
				.ToList();
			foreach (string thisInputModule in inputModules)
			{
				thisConjuntion.AddInput(thisInputModule);
			}
		}
	}
	long lowPulseCount = 0;
	long highPulseCount = 0;
	for (int i = 1; i <= 1000; i++)
	{
		(long, long) result = PushButton(modules);
		lowPulseCount += result.Item1;
		highPulseCount += result.Item2;
	}
	return lowPulseCount * highPulseCount;
}

static (long, long) PushButton(Dictionary<string, IModule> modules)
{
	long lowPulseCount = 0L;
	long highPulseCount = 0L;
	Queue<(string, string, bool)> pulseQueue = [];
	(string, string, bool) current = ("button", "broadcaster", false);
	pulseQueue.Enqueue(current);
	while (pulseQueue.Count > 0)
	{
		current = pulseQueue.Dequeue();
		if (current.Item3)
		{
			highPulseCount++;
		}
		else
		{
			lowPulseCount++;
		}
		if (!modules.Keys.Contains(current.Item2))
		{
			continue;
		}
		List<(string, string, bool)> result = modules[current.Item2]
			.ReceivePulse(current.Item1, current.Item3);
		result.ForEach(x => pulseQueue.Enqueue(x));
	}
	return (lowPulseCount, highPulseCount);
}
