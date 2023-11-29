namespace Day24;
using System.Text.RegularExpressions;

internal class Group
{
	public enum Team { ImmuneSystem, Infection }
	public Team team { get; }
	public int count { set; get; }
	public int HP { get; }
	public readonly List<string> weaknessList = new ();
	public readonly List<string> immunityList = new ();
	public string attackType { get; }
	public int damage { set; get; }
	public int initiative { get; }
	public bool isTargeted { set; get; } = false;
	public Group? currentTarget { set; get; } = null;

	public Group(Team team, string definition)
	{
		this.team = team;
		var match = (new Regex(@"(?<count>\d+) units")).Match(definition);
		count = int.Parse(match.Groups["count"].Value);
		match = (new Regex(@"(?<HP>\d+) hit points")).Match(definition);
		HP = int.Parse(match.Groups["HP"].Value);
		match = (new Regex(@"weak to (?<weaknessData>[a-z, ]+)[;)]")).Match(definition);
		var weaknessData = match.Groups["weaknessData"].Value;
		if (weaknessData.Length > 0)
		{
			weaknessList = weaknessData.Split(", ").ToList();
		}
		match = (new Regex(@"immune to (?<immunityData>[a-z, ]+)[;)]")).Match(definition);
		var immunityData = match.Groups["immunityData"].Value;
		if (immunityData.Length > 0)
		{
			immunityList = immunityData.Split(", ").ToList();
		}
		match = (new Regex(@"that does (?<damage>\d+) (?<attackType>[a-z]+) damage")).Match(definition);
		damage = int.Parse(match.Groups["damage"].Value);
		attackType = match.Groups["attackType"].Value;
		match = (new Regex(@"initiative (?<initiative>\d+)")).Match(definition);
		initiative = int.Parse(match.Groups["initiative"].Value);
	}

	public int GetEffectivePower()
	{
		return count * damage;
	}

	public int GetDamageDealtFrom(Group attacker)
	{
		if (immunityList.Contains(attacker.attackType))
		{
			return 0;
		}
		if (weaknessList.Contains(attacker.attackType))
		{
			return 2 * attacker.GetEffectivePower();
		}
		return attacker.GetEffectivePower();
	}

	public void Attack()
	{
		if (currentTarget == null)
		{
			return;
		}
		currentTarget.count -= currentTarget.GetDamageDealtFrom(this) / currentTarget.HP;
	}

}