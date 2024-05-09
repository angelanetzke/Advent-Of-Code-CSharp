string[] allLines = File.ReadAllLines("input.txt");
List<(int, int, int)> reindeerData = [];
foreach (string thisLine in allLines)
{
	string[] parts = thisLine.Split(' ');
	int speed = int.Parse(parts[3]);
	int flyTime = int.Parse(parts[6]);
	int restTime = int.Parse(parts[13]);	
	reindeerData.Add((speed, flyTime, restTime));
}
Console.WriteLine($"Part 1: {Part1(reindeerData)}");
Console.WriteLine($"Part 2: {Part2(reindeerData)}");

static int Part1(List<(int, int, int)> reindeerData)
{	
	int longestDistance = int.MinValue;
	foreach ((int, int, int) thisReindeerData in reindeerData)
	{
		int thisDistance = GetDistance(thisReindeerData, 2503);
		longestDistance = Math.Max(longestDistance, thisDistance);
	}	
	return longestDistance;
}

static int Part2(List<(int, int, int)> reindeerData)
{
	Dictionary<int, int> distances = []; // index, distance after this second
	Dictionary<int, int> scores = []; // index, cumulative score
	for (int i = 0; i < reindeerData.Count; i++)
	{
		distances[i] = 0;
		scores[i] = 0;
	}
	for (int time = 1; time <= 2503; time++)
	{
		for (int i = 0; i < reindeerData.Count; i++)
		{
			distances[i] = GetDistance(reindeerData[i], time);
		}
		int farthestDistance = distances.Values.Max();
		List<int> leadingReindeer = distances
			.Where(x => x.Value == farthestDistance).Select(x => x.Key).ToList();
		foreach (int thisReinderIndex in leadingReindeer)
		{
			scores[thisReinderIndex]++;
		}
	}
	return scores.Values.Max();
}

static int GetDistance((int speed, int flyTime, int restTime) data, int time)
{
	int completeCycles = time / (data.flyTime + data.restTime);
	int timeIntoLastCycle = time % (data.flyTime + data.restTime);
	return data.speed * data.flyTime * completeCycles 
		+ Math.Min(data.flyTime, timeIntoLastCycle) * data.speed;
}