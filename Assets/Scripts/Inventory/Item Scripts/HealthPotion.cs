using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour, IConsumable
{
    public void Comsume(CharacterStats stats)
    {       
        Debug.Log("You drank a swig of the potion. Cool!");
        Destroy(gameObject);
    }

    public void Consume()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().currentHealth += 30;
        Debug.Log("You drank a swig of the potion. Rad!");
        Destroy(gameObject);
    }
}
