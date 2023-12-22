
namespace Day20;

internal class Conjunction : IModule
{
	private readonly string name;
	private readonly string[] nextModules;
	private readonly Dictionary<string, bool> memory = [];

	public Conjunction(string moduleData)
	{
		string[] tokens = moduleData.Split(" -> ");
		name = tokens[0][1..];
		nextModules = tokens[1].Split(", ");
	}

	public List<(string, string, bool)> ReceivePulse(string from, bool isHigh)
	{
		List<(string, string, bool)> nextModulePulses = [];
		memory[from] = isHigh;
		bool sendPulse = false;
		if (memory.Values.Any(x => !x))
		{
			sendPulse = true;
		}
		foreach (string thisNextModule in nextModules)
		{
			nextModulePulses.Add((name, thisNextModule, sendPulse));
		}
		return nextModulePulses;
	}

	public void AddInput(string inputName)
	{
		memory[inputName] = false;
	}

	public bool ContainsOutput(string outputName)
	{
		return nextModules.Contains(outputName);
	}

	public string GetName()
	{
		return name;
	}

}