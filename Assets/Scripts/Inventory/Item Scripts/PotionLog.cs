using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionLog : MonoBehaviour, IConsumable
{
    public void Comsume(CharacterStats stats)
    {
        Debug.Log("You drank a swig of the potion. Cool!");
    }

    public void Consume()
    {
        Debug.Log("You drank a swig of the potion. Rad!");
    }
}
