using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatBonus
{
    public int BonusValue { get; set; } //For whenever there is a stat bonus 

    public StatBonus(int bonusValue)
    {
        this.BonusValue = BonusValue;
        Debug.Log("New stat bonus initiated");
    }
}
