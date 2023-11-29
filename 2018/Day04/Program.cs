using Day04;

var allLines = File.ReadAllLines("input.txt");
var guards = GetGuards(allLines);
Part1(guards);
Part2(guards);

static void Part1(Dictionary<string, Guard> guards)
{
	var guardData = new List<(string, int, int)>();
	foreach (string thisGuardID in guards.Keys)
	{
		var thisGuardStats = guards[thisGuardID].GetSleepStats();
		guardData.Add((thisGuardID, thisGuardStats.Item1, thisGuardStats.Item2));
	}
	guardData = guardData.OrderBy(x => x.Item2).ToList();
	var result = int.Parse(guardData.Last().Item1) * guardData.Last().Item3;
	Console.WriteLine($"Part 1: {result}");
}

static void Part2(Dictionary<string, Guard> guards)
{
	var guardData = new List<(string, int, int)>();
	foreach (string thisGuardID in guards.Keys)
	{
		var thisGuardStats = guards[thisGuardID].GetSleepStats2();
		guardData.Add((thisGuardID, thisGuardStats.Item1, thisGuardStats.Item2));
	}
	guardData = guardData.OrderBy(x => x.Item2).ToList();
	var result = int.Parse(guardData.Last().Item1) * guardData.Last().Item3;
	Console.WriteLine($"Part 2: {result}");
}

static Dictionary<string, Guard> GetGuards(string[] allLines)
{
	var eventList = new List<(string, int, string)>();
	foreach (string thisLine in allLines)
	{
		var dateString = thisLine.Substring(1, 16);
		var minuteValue = int.Parse(thisLine.Substring(15, 2));
		var eventString = thisLine.Substring(19);
		eventList.Add((dateString, minuteValue, eventString));
	}
	eventList = eventList.OrderBy(x => x.Item1).ToList();
	var isAwake = true;
	var currentGuard = new Guard();
	var guards = new Dictionary<string, Guard>();
	foreach ((string, int, string) thisEvent in eventList)
	{
		if (thisEvent.Item3.Contains("Guard"))
		{
			var guardID = thisEvent.Item3.Substring(7).Split(' ')[0];
			if (!guards.ContainsKey(guardID))
			{
				guards[guardID] = new Guard();
			}
			currentGuard = guards[guardID];
			currentGuard.NewShift();
		}
		else
		{
			isAwake = thisEvent.Item3.Contains("wakes up");
			currentGuard.AddEvent(thisEvent.Item2, isAwake);
		}
	}
	return guards;
}
