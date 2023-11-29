namespace Day25;

internal class Point
{
	private static int nextID = 0;
	private int id = -1;
	private readonly List<Point> connections = new ();
	private readonly int w;
	private readonly int x;
	private readonly int y;
	private readonly int z;

	public Point(string data)
	{
		this.w = int.Parse(data.Split(',')[0]);
		this.x = int.Parse(data.Split(',')[1]);
		this.y = int.Parse(data.Split(',')[2]);
		this.z = int.Parse(data.Split(',')[3]);
	}

	public void TryConnect(Point other)
	{
		if (Math.Abs(w - other.w) + Math.Abs(x - other.x) 
			+ Math.Abs(y - other.y) + Math.Abs(z - other.z) <= 3)
		{
			connections.Add(other);
			other.connections.Add(this);
		}
	}

	public void AssignID()
	{
		if (id == -1)
		{
			nextID++;
			AssignID(nextID);
		}		
	}

	private void AssignID(int newID)
	{
		id = newID;
		foreach (Point thisConnection in connections)
		{
			if (thisConnection.id == -1)
			{
				thisConnection.AssignID(newID);
			}			
		}
	}

	public static int GetMaxID()
	{
		return nextID;
	}
}