var allLines = File.ReadAllLines("input.txt");
var nextSteps = new Dictionary<char, List<char>>();
var requirements = new Dictionary<char, List<char>>();
foreach (string thisLine in allLines)
{
	var firstLetter = thisLine[5];
	var secondLetter = thisLine[36];
	if (nextSteps.ContainsKey(firstLetter))
	{
		nextSteps[firstLetter].Add(secondLetter);
	}
	else
	{
		nextSteps[firstLetter] = new List<char>() { secondLetter };
	}
	if (requirements.ContainsKey(secondLetter))
	{
		requirements[secondLetter].Add(firstLetter);
	}
	else
	{
		requirements[secondLetter] = new List<char>() { firstLetter };
	}
}
var stepOrder = Part1(nextSteps, requirements);
Part2(stepOrder, requirements);

static string Part1(Dictionary<char, List<char>> nextSteps, Dictionary<char, List<char>> requirements)
{
	var solution = new List<char>();
	var queue = new HashSet<char>(nextSteps.Keys);
	queue.RemoveWhere(x => requirements.Keys.Contains(x));
	var visited = new HashSet<char>();
	while (queue.Count > 0)
	{
		var current = queue.Min();
		visited.Add(current);
		queue.Remove(current);
		solution.Add(current);
		if (nextSteps.ContainsKey(current))
		{			
			foreach (char thisNextStep in nextSteps[current])
			{
				var requirementsMet = true;
				if (requirements.ContainsKey(thisNextStep))
				{
					foreach (char thisRequirement in requirements[thisNextStep])
					{
						if (!visited.Contains(thisRequirement))
						{
							requirementsMet = false;
						}
					}
				}
				if (!visited.Contains(thisNextStep) && requirementsMet)
				{
					queue.Add(thisNextStep);
				}				
			}
		}
	}
	var stepOrder = 	string.Join("", solution);
	Console.WriteLine($"Part 1: {stepOrder}");
	return stepOrder;
}

static void Part2(string stepOrder, Dictionary<char, List<char>> requirements)
{
	var time = 0;
	// is available, current task, time available
	var workers = Enumerable.Repeat((true, (char)0, 0), 5).ToList();
	var workersOccupied = workers.Where(x => !x.Item1);
	var completed = new HashSet<char>();
	do
	{
		for (int i = 0; i < workers.Count; i++)
		{
			if (!workers[i].Item1 && workers[i].Item3 <= time)
			{
				completed.Add(workers[i].Item2);
				workers[i] = (true, (char)0, time);
			}
		}
		for (int i = 0; i < workers.Count; i++)
		{
			char? nextTask = null;
			for (int stepIndex = 0; stepIndex < stepOrder.Length; stepIndex++)
			{
				nextTask = GetNextTask(stepOrder, stepIndex, requirements, completed);
				if (nextTask != null)
				{
					break;
				}
			}
			if (workers[i].Item1 && nextTask != null)
			{
				var endTime = time + (int)nextTask - 'A' + 1 + 60;
				workers[i] = (false, (char)nextTask, endTime);
				stepOrder = stepOrder.Replace(((char)nextTask).ToString(), "");
			}
		}
		workersOccupied = workers.Where(x => !x.Item1);
		if (workersOccupied.Count() > 0)
		{
			time = workersOccupied.Select(x => x.Item3).ToList().Min();
		}
	} while (workersOccupied.Count() > 0);
	Console.WriteLine($"Part 2: {time}");
}

static char? GetNextTask(string stepOrder, int stepIndex,
	Dictionary<char, List<char>> requirements, HashSet<char> completed)
{
	if (stepIndex >= stepOrder.Length)
	{
		return null;
	}
	var requirementsMet = true;
	if (requirements.ContainsKey(stepOrder[stepIndex]))
	{
		foreach (char thisRequirement in requirements[stepOrder[stepIndex]])
		{
			if (!completed.Contains(thisRequirement))
			{
				requirementsMet = false;
			}
		}
	}
	if (requirementsMet)
	{
		return stepOrder[stepIndex];
	}
	else
	{
		return null;
	}
}

