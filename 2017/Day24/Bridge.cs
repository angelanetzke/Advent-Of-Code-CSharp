namespace Day24;

internal class Bridge
{
	private readonly List<Component> availableComponents;
	private readonly int nextConnection;
	private readonly int strength;
	private readonly int length;

	public Bridge (List<Component> availableComponents, int nextConnection, int strength, int length)
	{
		this.availableComponents = availableComponents;
		this.nextConnection = nextConnection;
		this.strength = strength;
		this.length = length;
	}

	public List<Bridge> Extend()
	{
		var bridgeList = new List<Bridge>();
		var nextComponents = availableComponents.Where(x => x.HasConnection(nextConnection)).ToList();
		if (nextComponents.Count == 0)
		{
			bridgeList.Add(this);
		}
		else
		{
			foreach (Component thisComponent in nextComponents)
			{
				var nextStrength = thisComponent.GetStrength() + strength;
				var nextNextConnection = thisComponent.GetStrength() - nextConnection;
				var nextAvailableComponents = new List<Component>(availableComponents);
				var nextLength = length + 1;
				nextAvailableComponents.Remove(thisComponent);
				var nextBridge = new Bridge(nextAvailableComponents, nextNextConnection, nextStrength, nextLength);
				bridgeList.AddRange(nextBridge.Extend());
			}
		}
		return bridgeList;
	}

	public int GetStrength()
	{
		return strength;
	}

	public int GetLength()
	{
		return length;
	}

}