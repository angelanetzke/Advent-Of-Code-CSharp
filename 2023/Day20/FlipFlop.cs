
namespace Day20;

internal class FlipFlop : IModule 
{	
	private readonly string name;
	private readonly string[] nextModules;	
	private bool isOn = false;

	public FlipFlop(string moduleData)
	{
		string[] tokens = moduleData.Split(" -> ");
		name = tokens[0][1..];
		nextModules = tokens[1].Split(", ");
	}

	public List<(string, string, bool)> ReceivePulse(string from, bool isHigh)
	{
		List<(string,  string,bool)> nextModulePulses = [];
		if (!isHigh)
		{
			isOn = !isOn;
			foreach (string thisNextModule in nextModules)
			{
				nextModulePulses.Add((name, thisNextModule, isOn));
			}
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
	{
		isOn = false;
	}
}