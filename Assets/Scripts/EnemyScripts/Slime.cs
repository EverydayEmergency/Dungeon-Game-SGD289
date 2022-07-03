using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour, IEnemy
{
    public float currenthHealth, power, toughness;
    public float maxHealth;

    void Start()
    {
        currenthHealth = maxHealth;
    }
    public void PerformAttack()
    {
        throw new System.NotImplementedException();
    }

    public void TakeDamage(int amount)
    {
        currenthHealth -= amount;
        if(currenthHealth <= 0)
        {
            Die();
        }
    }  

    void Die()
    {
        Destroy(gameObject);
    }
}
