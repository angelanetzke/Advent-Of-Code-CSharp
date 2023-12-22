namespace Day20;

interface IModule
{
	List<(string, string, bool)> ReceivePulse(string from, bool isHigh);
	bool ContainsOutput(string outputName);
}