namespace Day20;

using System.Text.RegularExpressions;

internal class Particle
{
	private static Regex positionRegex = new Regex(@"p=<(?<position>[-,\d]+)>");
	private static Regex velocityRegex = new Regex(@"v=<(?<velocity>[-,\d]+)>");
	private static Regex accelerationRegex = new Regex(@"a=<(?<acceleration>[-,\d]+)>");
	private long[] position;
	private long[] velocity;
	private long[] acceleration;
	public long potential { get; }

	public Particle(string data)
	{
		var match = positionRegex.Match(data);
		var positionData = match.Groups["position"].Value;
		position = positionData.Split(',').Select(x => long.Parse(x)).ToArray();
		match = velocityRegex.Match(data);
		var velocityData = match.Groups["velocity"].Value;
		velocity = velocityData.Split(',').Select(x => long.Parse(x)).ToArray();
		match = accelerationRegex.Match(data);
		var accelerationData = match.Groups["acceleration"].Value;
		acceleration = accelerationData.Split(',').Select(x => long.Parse(x)).ToArray();
		potential = GetAcceleration() + GetVelocity();
	}

	public void Iterate()
	{
		velocity[0] += acceleration[0];
		velocity[1] += acceleration[1];
		velocity[2] += acceleration[2];
		position[0] += velocity[0];
		position[1] += velocity[1];
		position[2] += velocity[2];
	}

	public bool CollidesWith(Particle other)
	{
		return position[0] == other.position[0]
			&& position[1] == other.position[1]
			&& position[2] == other.position[2];
	}

	public long GetPosition()
	{
		return position.Select(x => Math.Abs(x)).Sum();
	}

	public long GetVelocity()
	{
		return velocity.Select(x => Math.Abs(x)).Sum();
	}

	public long GetAcceleration()
	{
		return acceleration.Select(x => Math.Abs(x)).Sum();
	}

}