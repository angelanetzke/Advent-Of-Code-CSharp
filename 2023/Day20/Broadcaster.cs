
namespace Day20;

internal class Broadcaster : IModule
{
	private readonly string name = "broadcaster";
	private readonly string[] nextModules;

	public Broadcaster(string moduleData)
	{
		string[] tokens = moduleData.Split(" -> ");
		nextModules = tokens[1].Split(", ");
	}

	public List<(string, string, bool)> ReceivePulse(string from, bool isHigh)
	{
		List<(string, string, bool)> nextModulePulses = [];
		foreach (string thisNextModule in nextModules)
		{
			nextModulePulses.Add((name, thisNextModule, isHigh));
		}
		return nextModulePulses;
	}

	public bool ContainsOutput(string outputName)
	{
		return nextModules.Contains(outputName);
	}

	public string GetName()
	{
		return name;
	}

	public void Reset()
	{}
}