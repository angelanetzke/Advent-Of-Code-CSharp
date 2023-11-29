namespace Day24;

internal class Component
{
	private readonly int connection1;
	private readonly int connection2;

	public Component(string data)
	{
		connection1 = int.Parse(data.Split('/')[0]);
		connection2 = int.Parse(data.Split('/')[1]);
	}

	public bool HasConnection(int otherConnection)
	{
		return connection1 == otherConnection || connection2 == otherConnection;
	}

	public int GetStrength()
	{
		return connection1 + connection2;
	}

	public override bool Equals(object? obj)
	{
		if (obj != null && obj is Component other)
		{
			return connection1 == other.connection1 && connection2 == other.connection2;
		}
		return false;
	}

	public override int GetHashCode()
	{
		return (connection1 + connection2).GetHashCode();
	}
}