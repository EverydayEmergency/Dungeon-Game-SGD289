using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour, IConsumable
{
    public void Comsume(CharacterStats stats)
    {
        stats = new CharacterStats(30, 0, 0, 0);
    }

    public void Consume()
    {
        Debug.Log("You drank a swig of the potion. Rad!");
    }
}
