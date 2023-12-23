using System.Collections.Frozen;
using System.Security.Cryptography.X509Certificates;
using Day20;

string[] allLines = File.ReadAllLines("input.txt");
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
Console.WriteLine($"Part 1: {Part1(modules)}");
Console.WriteLine($"Part 2: {Part2(modules)}");

static long Part1(Dictionary<string, IModule> modules)
{	
	long lowPulseCount = 0;
	long highPulseCount = 0;
	for (int i = 1; i <= 1000; i++)
	{
		(long, long) result = PushButton(modules, null);
		lowPulseCount += result.Item1;
		highPulseCount += result.Item2;
	}
	return lowPulseCount * highPulseCount;
}

static long Part2(Dictionary<string, IModule> modules)
{	
	string penultimate = modules.Where(x => x.Value.ContainsOutput("rx"))
		.Select(x => x.Key).First();
	List<string> feeders = modules.Where(x => x.Value.ContainsOutput(penultimate))
		.Select(x => x.Key).ToList();
	List<int> cycleLengths = [];
	foreach (string thisFeeder in feeders)
	{
		foreach (string thisModuleName in modules.Keys)
		{
			modules[thisModuleName].Reset();
		}		
		int buttonPressCount = 1;
		(long, long) result = PushButton(modules, (thisFeeder, penultimate, true));
		while (result != (-1L, -1L))
		{	
			buttonPressCount++;		
			result = PushButton(modules, (thisFeeder, penultimate, true));
			if (result == (-1L, -1L))
			{				
				cycleLengths.Add(buttonPressCount);
			}			
		}
	}
	long lcm = cycleLengths[0];
	for (int i = 1; i < cycleLengths.Count; i++)
	{
		lcm = GetLCM(lcm, cycleLengths[i]);
	}
	return lcm;
}

static (long, long) PushButton(Dictionary<string, IModule> modules, (string, string, bool)? end)
{
	long lowPulseCount = 0L;
	long highPulseCount = 0L;
	Queue<(string, string, bool)> pulseQueue = [];
	(string, string, bool) current = ("button", "broadcaster", false);
	pulseQueue.Enqueue(current);
	while (pulseQueue.Count > 0)
	{
		current = pulseQueue.Dequeue();
		if (end != null && current == end)
		{
			return (-1L, -1L);
		}
		if (current.Item3)
		{
			highPulseCount++;
		}
		else
		{
			lowPulseCount++;
		}
		if (current.Item2 == "rx" && !current.Item3)
		{
			return (-1L, -1L);
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

static long GetLCM(long number1, long number2)
{
	return Math.Abs(number1 * number2) / GetGCD(number1, number2);
}

static long GetGCD(long number1, long number2)
{
	while (number2 != 0)
	{
		long temp = number2;
		number2 = number1 % number2;
		number1 = temp;
	}
	return number1;
}
