using Day20;
using System.Text.RegularExpressions;

var allLines = File.ReadAllLines("input.txt");
Part1(allLines);
Part2(allLines);

void Part1(string[] allLines)
{
	var velocityRegex = new Regex(@"v=<(?<velocity>[-,\d]+)>");
	var accelerationRegex = new Regex(@"a=<(?<acceleration>[-,\d]+)>");
	var particles = new List<(int id, int velocity, int acceleration)>();
	for (int i = 0; i < allLines.Length; i++)
	{
		var match = velocityRegex.Match(allLines[i]);
		var velocityData = match.Groups["velocity"].Value;
		var velocity = velocityData.Split(',').Select(x => Math.Abs(int.Parse(x))).Sum();
		match = accelerationRegex.Match(allLines[i]);
		var accelerationData = match.Groups["acceleration"].Value;
		var acceleration = accelerationData.Split(',').Select(x => Math.Abs(int.Parse(x))).Sum();		
		particles.Add((i, velocity, acceleration));
	}
	var slowestParticle = particles.OrderBy(x => x.acceleration).ThenBy(x => x.velocity).First();
	Console.WriteLine($"Part 1: {slowestParticle.id}");
}

void Part2(string[] allLines)
{
	var maxRepeats = 50;
	var repeatCount = 0;
	var lastCount = -1;
	var particles = new List<Particle>();
	foreach (string thisLine in allLines)
	{
		particles.Add(new Particle(thisLine));
	}
	var toRemove = new HashSet<Particle>();
	while (repeatCount < maxRepeats)
	{
		foreach (Particle thisParticle in particles)
		{
			thisParticle.Iterate();
		}
		toRemove.Clear();
		for (int i = 0; i < particles.Count - 1; i++)
		{
			for (int j = i + 1; j < particles.Count; j++)
			{
				if (particles[i].CollidesWith(particles[j]))
				{
					toRemove.Add(particles[i]);
					toRemove.Add(particles[j]);
				}
			}
		}
		particles.RemoveAll(x => toRemove.Contains(x));
		if (lastCount == particles.Count)
		{
			repeatCount++;
		}
		else
		{
			repeatCount = 0;
		}
		lastCount = particles.Count;		
	}
	Console.WriteLine($"Part 2: {particles.Count}");
}
