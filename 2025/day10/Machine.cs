namespace Day10;

using Microsoft.Z3;

internal class Machine
{
	private readonly string targetLights;
	private readonly string startLights;
	private readonly List<int[]> buttons = [];
	private readonly int[] targetJoltage;

	public Machine(string data)
	{
		string[] tokens = data.Split(' ');
		targetLights = tokens[0][1..^1];
		startLights = new string('.', targetLights.Length);
		int i = 1;
		while (tokens[i].StartsWith('('))
		{
			buttons.Add([.. tokens[i][1..^1].Split(',').Select(x => int.Parse(x))]);
			i++;
		}
		targetJoltage = [..tokens[i][1..^1].Split(',').Select(x => int.Parse(x))];
	}

	public long CountPressesLights()
	{
		Queue<(string, long)> queue = [];
		queue.Enqueue((startLights, 0L));
		HashSet<string> visited = [];
		while (queue.Count > 0)
		{
			(string, long) current = queue.Dequeue();
			if (visited.Contains(current.Item1))
			{
				continue;
			}
			visited.Add(current.Item1);
			if (current.Item1 == targetLights)
			{
				return current.Item2;
			}
			foreach (int[] b in buttons)
			{
				string next = "";
				for (int i = 0; i < targetLights.Length; i++)
				{
					if (b.Contains(i))
					{
						next = $"{next}{(current.Item1[i] == '.' ? '#' : '.')}";
					}
					else
					{
						next = $"{next}{(current.Item1[i] == '.' ? '.' : '#')}";
					}
				}
				if (!visited.Contains(next))
				{
					queue.Enqueue((next, current.Item2 + 1));
				}
			}
		}
		return -1L;
	}

	public long CountPressesJoltage()
	{
		Context context = new ();
		Optimize optimizer = context.MkOptimize();
		IntExpr[] presses = new IntExpr[buttons.Count];
		for (int i = 0; i < buttons.Count; i++)
		{
			presses[i] = context.MkIntConst($"press_{i}");
		}
		for (int i = 0; i < targetJoltage.Length; i++)
		{
			List<ArithExpr> contributions = [];
			for (int j = 0; j < buttons.Count; j++)
			{
				if (buttons[j].Contains(i))
				{
					contributions.Add(presses[j]);	
				}				
			}
			optimizer.Add(context.MkEq(context.MkAdd([..contributions]), context.MkInt(targetJoltage[i])));
		}		
		foreach (IntExpr p in presses)
		{
			optimizer.Add(context.MkGe(p, context.MkInt(0)));
		}
		ArithExpr totalPresses = context.MkAdd(presses);
		optimizer.MkMinimize(totalPresses);
		if (optimizer.Check() == Status.SATISFIABLE)
		{
			Model m = optimizer.Model;
			return long.Parse(m.Evaluate(totalPresses).ToString());
		}
		return -1L;
	}
}