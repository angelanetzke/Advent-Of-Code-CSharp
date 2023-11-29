namespace Day24;

internal class Battle
{
	private List<Group> groupList = new ();

	public Battle(string[] allLines)
	{
		var currentTeam = Group.Team.ImmuneSystem;
		foreach (string thisLine in allLines)
		{
			if (thisLine.Length == 0 || thisLine == "Immune System:")
			{
				continue;
			}
			else if (thisLine == "Infection:")
			{
				currentTeam = Group.Team.Infection;
			}
			else
			{
				groupList.Add(new Group(currentTeam, thisLine));
			}
		}
	}

	public int DoBattle()
	{
		var lastTotalUnitsLeft = groupList.Where(x => x.count > 0).Sum(x => x.count);
		while (groupList.Count(x => x.team == Group.Team.ImmuneSystem) > 0
			&& groupList.Count(x => x.team == Group.Team.Infection) > 0)
		{
			SelectTargets();
			Attack();
			var totalUnitsLeft = groupList.Where(x => x.count > 0).Sum(x => x.count);
			if (lastTotalUnitsLeft == totalUnitsLeft)
			{
				break;
			}
			lastTotalUnitsLeft = totalUnitsLeft;
		}
		if (groupList.Count(x => x.team == Group.Team.ImmuneSystem) > 0
			&& groupList.Count(x => x.team == Group.Team.Infection) > 0)
		{
			return -1;
		}
		if (groupList.Count(x => x.team == Group.Team.ImmuneSystem) > 0)
		{
			return groupList.Where(x => x.team == Group.Team.ImmuneSystem).Sum(x => x.count);
		}
		else
		{
			return -1 * groupList.Where(x => x.team == Group.Team.Infection).Sum(x => x.count);
		}
	}

	private void SelectTargets()
	{
		foreach (Group thisGroup in groupList)
		{
			thisGroup.currentTarget = null;
			thisGroup.isTargeted = false;
		}
		groupList = groupList
			.OrderByDescending(x => x.GetEffectivePower())
			.ThenByDescending(x => x.initiative)
			.ToList();
		foreach (Group thisGroup in groupList)
		{
			var remainingTargets = groupList
				.Where(x => x.team != thisGroup.team)
				.Where(x => !x.isTargeted)
				.Where(x => x.GetDamageDealtFrom(thisGroup) > 0)
				.ToList();
			if (remainingTargets.Count > 0)
			{
				remainingTargets = remainingTargets
					.OrderByDescending(x => x.GetDamageDealtFrom(thisGroup))				
					.ThenByDescending(x => x.GetEffectivePower())
					.ThenByDescending(x => x.initiative)
					.ToList();
				var selectedTarget = remainingTargets.First();
				selectedTarget.isTargeted = true;
				thisGroup.currentTarget = selectedTarget;
			}
		}
	}

	private void Attack()
	{
		groupList = groupList
			.OrderByDescending(x => x.initiative)
			.ToList();
		foreach (Group thisGroup in groupList)
		{
			if (thisGroup.count <= 0)
			{
				thisGroup.currentTarget = null;
			}
			thisGroup.Attack();
		}
		groupList = groupList.Where(x => x.count > 0).ToList();
	}

	public void Boost(int amount)
	{
		var immuneTeam = groupList.Where(x => x.team == Group.Team.ImmuneSystem).ToList();
		immuneTeam.ForEach(x => x.damage += amount);
	}

}