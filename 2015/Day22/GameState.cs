namespace Day22;

internal class GameState
{
	private int bossHP;
	private static int bossDamage;
	private int heroHP = 50;
	private int heroArmor = 0;
	private int manaSpent = 0;
	private int heroMana = 500;
	private static readonly int missileCost = 53;
	private static readonly int missileDamage = 4;
	private static readonly int drainCost = 73;
	private static readonly int drainDamage = 2;
	private static readonly int drainBonus = 2;
	private static readonly int shieldCost = 113;
	private int shieldCounter = 0;
	private static readonly int shieldDuration = 6;
	private static readonly int shieldBonus = 7;
	private static readonly int poisonCost = 173;
	private int poisonCounter = 0;
	private static readonly int poisonDuration = 6;
	private static readonly int poisonDamage = 3;
	private static readonly int rechargeCost = 229;
	private int rechargeCounter = 0;
	private static readonly int rechargeDuration = 5;
	private static readonly int rechargeBonus = 101;
	private static int bestScore;
	private bool isHeroTurn = true;

	public static int GetLeastMana(string[] allLines, bool isHardMode = false)
	{	
		bossDamage = int.Parse(allLines[1].Split(": ")[1]);
		bestScore = int.MaxValue;
		GameState start = new()
		{
			bossHP = int.Parse(allLines[0].Split(": ")[1]),
			heroHP = 50,
			manaSpent = 0,
			heroMana = 500,
			shieldCounter = 0,
			poisonCounter = 0,
			rechargeCounter = 0,
			isHeroTurn = true
		};
		start.GetLeastManaRecursive(isHardMode);
		return bestScore;
	}

	private void GetLeastManaRecursive(bool isHardMode = false)
	{
		if (heroHP <= 0)
		{
			return;
		}
		if (manaSpent >= bestScore)
		{
			return;
		}	
		if (bossHP <= 0)
		{
			bestScore = Math.Min(bestScore, manaSpent);
			return;
		}		
		if (shieldCounter > 0)
		{
			heroArmor = shieldBonus;
			shieldCounter--;
		}
		else
		{
			heroArmor = 0;
		}
		if (poisonCounter > 0)
		{
			bossHP -= poisonDamage;
			poisonCounter--;
		}
		if (rechargeCounter > 0)
		{
			heroMana += rechargeBonus;
			rechargeCounter--;
		}
		if (isHeroTurn)
		{
			if (isHardMode)
			{
				heroHP--;
				if (heroHP <= 0)
				{
					return;
				}
			}
			CastMissile(isHardMode);
			CastDrain(isHardMode);
			CastShield(isHardMode);
			CastPoison(isHardMode);
			CastRecharge(isHardMode);
		}
		else
		{
			if (bossHP <= 0)
			{
				bestScore = Math.Min(bestScore, manaSpent);
				return;
			}	
			TakeBossTurn(isHardMode);
		}		
	}

	private void TakeBossTurn(bool isHardMode = false)
	{
		GameState next = new()
		{
			bossHP = bossHP,
			heroHP = heroHP - int.Max(1, bossDamage - heroArmor),
			manaSpent = manaSpent,
			heroMana = heroMana,
			shieldCounter = shieldCounter,
			poisonCounter = poisonCounter,
			rechargeCounter = rechargeCounter,
			isHeroTurn = !isHeroTurn
		};
		next.GetLeastManaRecursive(isHardMode);
	}

	private void CastMissile(bool isHardMode = false)
	{
		if (heroMana < missileCost)
		{
			return;
		}
		GameState next = new()
		{
			bossHP = bossHP - missileDamage,
			heroHP = heroHP,
			manaSpent = manaSpent + missileCost,
			heroMana = heroMana - missileCost,
			shieldCounter = shieldCounter,
			poisonCounter = poisonCounter,
			rechargeCounter = rechargeCounter,
			isHeroTurn = !isHeroTurn
		};
		next.GetLeastManaRecursive(isHardMode);
	}

	private void CastDrain(bool isHardMode = false)
	{
		if (heroMana < drainCost)
		{
			return;
		}
		GameState next = new()
		{
			bossHP = bossHP - drainDamage,
			heroHP = heroHP + drainBonus,
			manaSpent = manaSpent + drainCost,
			heroMana = heroMana - drainCost,
			shieldCounter = shieldCounter,
			poisonCounter = poisonCounter,
			rechargeCounter = rechargeCounter,
			isHeroTurn = !isHeroTurn
		};
		next.GetLeastManaRecursive(isHardMode);
	}

	private void CastShield(bool isHardMode = false)
	{
		if (heroMana < shieldCost || shieldCounter > 0)
		{
			return;
		}
		GameState next = new()
		{
			bossHP = bossHP,
			heroHP = heroHP,
			manaSpent = manaSpent + shieldCost,
			heroMana = heroMana - shieldCost,
			shieldCounter = shieldDuration,
			poisonCounter = poisonCounter,
			rechargeCounter = rechargeCounter,
			isHeroTurn = !isHeroTurn
		};
		next.GetLeastManaRecursive(isHardMode);
	}

	private void CastPoison(bool isHardMode = false)
	{
		if (heroMana < poisonCost || poisonCounter > 0)
		{
			return;
		}
		GameState next = new()
		{
			bossHP = bossHP,
			heroHP = heroHP,
			manaSpent = manaSpent + poisonCost,
			heroMana = heroMana - poisonCost,
			shieldCounter = shieldCounter,
			poisonCounter = poisonDuration,
			rechargeCounter = rechargeCounter,
			isHeroTurn = !isHeroTurn
		};
		next.GetLeastManaRecursive(isHardMode);
	}

	private void CastRecharge(bool isHardMode = false)
	{
		if (heroMana < rechargeCost || rechargeCounter > 0)
		{
			return;
		}
		GameState next = new()
		{
			bossHP = bossHP,
			heroHP = heroHP,
			manaSpent = manaSpent + rechargeCost,
			heroMana = heroMana - rechargeCost,
			shieldCounter = shieldCounter,
			poisonCounter = poisonCounter,
			rechargeCounter = rechargeDuration,
			isHeroTurn = !isHeroTurn
		};
		next.GetLeastManaRecursive(isHardMode);
	}

}