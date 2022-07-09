using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    public List<BaseStat> stats = new List<BaseStat>();

    public CharacterStats(int health, int attack, int defense, int attackSpeed)
    {
        stats = new List<BaseStat>() {
            new BaseStat(BaseStat.BaseStatType.Health, health, "Health"),
            new BaseStat(BaseStat.BaseStatType.Attack, attack, "Attack"),
            new BaseStat(BaseStat.BaseStatType.Defense, defense, "Defense"),
            new BaseStat(BaseStat.BaseStatType.AttackSpeed, attackSpeed, "Attack Speed"),
        };
    }

    public BaseStat GetStat(BaseStat.BaseStatType stat)
    {
        return this.stats.Find(x => x.StatType == stat);
    }

    public void AddStatBonus(List<BaseStat> statBonuses) //Passes multiple stats
    {
        foreach (BaseStat statBonus in statBonuses)
        {
            GetStat(statBonus.StatType).AddStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }

    public void RemoveStatBonus(List<BaseStat> statBonuses) //Passes multiple stats
    {
        foreach (BaseStat statBonus in statBonuses)
        {
            GetStat(statBonus.StatType).RemoveStatBonus(new StatBonus(statBonus.BaseValue));
        }
    }
}
