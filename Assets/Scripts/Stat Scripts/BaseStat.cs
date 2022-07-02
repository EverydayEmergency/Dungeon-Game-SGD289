using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat
{
    public List<StatBonus> BaseAdditives { get; set; }
    public int BaseValue { get; set; } //Stat without buffs or debuffs
    public string StatName { get; set; }
    public string StatDescription { get; set; }
    public int FinalValue { get; set; } //Stat with buffs and debuffs

    public BaseStat(int baseValue, string statName, string statDescription)
    {
        BaseAdditives = new List<StatBonus>();
        BaseValue = baseValue;
        StatName = statName;
        StatDescription = statDescription;
    }

    public void AddStatBonus(StatBonus statBonus)
    {
        BaseAdditives.Add(statBonus);
    }

    public void RemoveStatBonus(StatBonus statBonus)
    {
        //Looks through base additives list for value that matches statBonus to remove
        BaseAdditives.Remove(BaseAdditives.Find(x => x.BonusValue == statBonus.BonusValue)); 
    }

    public int GetCalculatedStatValue()
    {
        FinalValue = 0;
        BaseAdditives.ForEach(x => FinalValue += x.BonusValue); // For each additive in base additives it will be added to final value
        FinalValue += BaseValue; // Adds the base value to the final value
        return FinalValue;
    }

}
